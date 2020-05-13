using Caliburn.Micro;
using System;
using Okuma.Scout;
using Okuma_Monitor_Tools.Okuma.OkumaLathe;
using Okuma_Monitor_Tools.Okuma.OkumaMill;
using Okuma.Scout.Enums;

namespace Okuma_Monitor_Tools.Okuma
{
    public abstract class ScannableBase : PropertyChangedBase, IScannable
    {
        private MachineType _machineType = Platform.BaseMachineType;
        internal IOkumaLathe _okumaLathe;
        internal IEventAggregator _eventAggr;
        internal IOkumaMill _okumaMill;

        public ScannableBase(IOkumaLathe okuma, IEventAggregator eventAggr)
        {
            if (_machineType == MachineType.L)
            {
                _okumaLathe = okuma;
                _eventAggr = eventAggr;
            }
            
        }

        public ScannableBase(IOkumaMill okuma, IEventAggregator eventAggr)
        {
            if (_machineType == MachineType.M)
            {
                _okumaMill = okuma;
                _eventAggr = eventAggr;
            }
            
        }

        public virtual DateTime LastScan { get; set; }
        public virtual ScanPriority Priority { get; set; }

        public abstract void Update();

    }
}
