using Caliburn.Micro;
using Okuma_Monitor_Tools.Events;
using Okuma_Monitor_Tools.Okuma.OkumaLathe;
using Okuma_Monitor_Tools.Okuma.OkumaMill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Okuma.Scout;
using Okuma.Scout.Enums;
using Okuma_Monitor_Tools.Enums;
using UtilityCode;

namespace Okuma_Monitor_Tools.Okuma
{
    public interface IOkumaScanner
    {
        OkumaIOPoint AddIOSignal(string signalName, ScanPriority priority = ScanPriority.Normal);
        OkumaVariable AddVariable(int variableNumber, ScanPriority priority = ScanPriority.Normal);
        void StartScanning();
        void StopScanning();
        ICollection<IScannable> ScanList { get; }
    }
    public class OkumaScanner : STANeverEndingTask, IOkumaScanner
    {
        private MachineType _machineType = Platform.BaseMachineType;
        private IOkumaLathe _okumaLathe;
        private IOkumaMill _okumaMill;
        private IEventAggregator _events;
        private bool _scanning;

       
        public OkumaScanner(IOkumaLathe okumaAPI, IEventAggregator events) : base(events, "Okuma")
        {
            if (_machineType == MachineType.L)
            {
                ScanList = new List<IScannable>();
                _okumaLathe = okumaAPI;
                _events = events;
                _events.Subscribe(this);
            }
            
        }

        public OkumaScanner(IOkumaMill okumaAPI, IEventAggregator events) : base(events, "Okuma")
        {
            if (_machineType == MachineType.M)
            {
                ScanList = new List<IScannable>();
                _okumaMill = okumaAPI;
                _events = events;
                _events.Subscribe(this);
            }
            
        }

        public void StartScanning()
        {
            _scanning = true;
            Start();
        }

        public void StopScanning()
        {
            _scanning = false;
            Stop();
        }

        public OkumaIOPoint AddIOSignal(string signalName, ScanPriority priority = ScanPriority.Normal)
        {
            OkumaIOPoint rtn = null;

            if (_machineType == MachineType.L)
            {
               var latheSignalToAdd = _okumaLathe.GetIO(signalName);
                if (latheSignalToAdd.Address == -1) { throw new ArgumentException($"'{signalName}' is not a valid I/O signal on this machine."); }
                //https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum
                BitsEnum _bit = (BitsEnum) Enum.Parse(typeof(BitsEnum), latheSignalToAdd.Bit.ToString());
                IOTypeEnum _ioType = (IOTypeEnum)Enum.Parse(typeof(IOTypeEnum), latheSignalToAdd.IOType.ToString());
                 rtn = new OkumaIOPoint(_okumaLathe, _events, signalName, latheSignalToAdd.Address, _bit) { Priority = priority, IOType = _ioType };

                ScanList.Add(rtn);
                
            }
            else if (_machineType == MachineType.M )
            {
                var millSignalToAdd = _okumaMill.GetIO(signalName);
                if (millSignalToAdd.Address == -1) { throw new ArgumentException($"'{signalName}' is not a valid I/O signal on this machine."); }
                //https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum
                BitsEnum _bit = (BitsEnum) Enum.Parse(typeof(BitsEnum), millSignalToAdd.Bit.ToString());
                IOTypeEnum _ioType = (IOTypeEnum) Enum.Parse(typeof(IOTypeEnum), millSignalToAdd.IOType.ToString());
                 rtn = new OkumaIOPoint(_okumaMill, _events, signalName, millSignalToAdd.Address, _bit) { Priority = priority, IOType = _ioType};

                ScanList.Add(rtn);
                
            }
            
            return rtn;
        }

        public OkumaVariable AddVariable(int variableNumber, ScanPriority priority = ScanPriority.Normal)
        {
            OkumaVariable rtn = null;

            if (_machineType == MachineType.L)
            {
                 rtn = new OkumaVariable(_okumaLathe, _events, variableNumber) { Priority = priority };
                ScanList.Add(rtn);
                
            }

            if (_machineType == MachineType.M)
            {
                rtn = new OkumaVariable(_okumaMill, _events, variableNumber) { Priority = priority};
                ScanList.Add(rtn);
            }

            return rtn;
        }

        protected override void ExecutionCore(CancellationToken cancellationToken)
        {
            if (_scanning)
            {
                ScanList.Where(s => s.Priority == ScanPriority.High).Apply(s => s.Update());

                ScanList.Where(s => s.Priority == ScanPriority.Normal
                                    && Math.Abs(DateTime.Now.Subtract(s.LastScan).TotalMilliseconds) >= 200)
                                    .Apply(s => s.Update());

                ScanList.Where(s => s.Priority == ScanPriority.Normal
                                    && Math.Abs(DateTime.Now.Subtract(s.LastScan).TotalMilliseconds) >= 1000)
                                    .Apply(s => s.Update());
            }
        }

        public ICollection<IScannable> ScanList { get; internal set; }

    }
}
