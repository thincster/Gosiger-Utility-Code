using Caliburn.Micro;
using Okuma_Monitor_Tools.Okuma.OkumaEvents;
using System;
//using Okuma.CLDATAPI.Enumerations;
using Okuma.Scout;
//using Okuma.CMDATAPI.Enumerations;
using Okuma_Monitor_Tools.Okuma.OkumaLathe;
using Okuma_Monitor_Tools.Okuma.OkumaMill;
using Okuma.Scout.Enums;
using Okuma_Monitor_Tools.Enums;



namespace Okuma_Monitor_Tools.Okuma
{
    public class OkumaIOPoint : ScannableBase
    {
        private MachineType _machineType = Platform.BaseMachineType;
        public OkumaIOPoint(IOkumaLathe okuma, IEventAggregator events, string name, int address, BitsEnum bit) : base(okuma, events)
        {
            if (_machineType == MachineType.L)
            {
                _okumaLathe = okuma;
                Address = address;
                Bit = bit;
                Name = name;
            }
          
        }

        public OkumaIOPoint(IOkumaMill okuma, IEventAggregator events, string name, int address, BitsEnum bit) : base(okuma, events)
        {
            if (_machineType == MachineType.M)
            {
                _okumaMill = okuma;
                Address = address;
                Bit = bit;
                Name = name;
            }
            
        }

        

        public int Address { get; set; }
        public IOTypeEnum IOType { get; set; }
        public BitsEnum Bit { get; set; }
        public string Name { get; set; }
        bool status;
        public bool Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status == value)
                    return;
                status = value;
                StatusChanged = true;
                NotifyOfPropertyChange();
            }
        }
        public bool StatusChanged { get; set; }

        public override void Update()
        {
            try
            {
                if (_machineType == MachineType.L)
                {
                    Status = _okumaLathe.GetIOBitStatus(IOType, Address, Bit);
                }
                else if (_machineType == MachineType.M)
                {
                   
                    Status = _okumaMill.GetIOBitStatus(IOType, Address, Bit);
                }
                
                if (StatusChanged)
                {
                    StatusChanged = false;
                    _eventAggr.PublishOnUIThread(new OkumaIOChangedEvent(this, Status));
                }
            }
            catch (Exception ex)
            {
                _eventAggr.PublishOnUIThread(new OkumaScanningExceptionEvent(ex, Name));
            }

            //LastScan = DateTime.Now;
        }
    }
}
