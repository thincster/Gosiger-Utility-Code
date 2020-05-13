using Caliburn.Micro;
using Okuma.Scout;
using Okuma.Scout.Enums;
using Okuma_Monitor_Tools.Okuma.OkumaEvents;
using Okuma_Monitor_Tools.Okuma.OkumaLathe;
using Okuma_Monitor_Tools.Okuma.OkumaMill;
using System;

namespace Okuma_Monitor_Tools.Okuma
{
    public class OkumaVariable : ScannableBase
    {
        private MachineType _machineType = Platform.BaseMachineType;
        private double _newValue = 0;
        private double _oldValue = 0;

        //public OkumaVariable(IOkuma okuma, IEventAggregator eventAggr, int variableNumber) : base(okuma, eventAggr)
        //{
            
        //    VariableNumber = variableNumber;
        //}

        public OkumaVariable(IOkumaLathe okumaLathe, IEventAggregator eventAggr, int variableNumber) : base(okumaLathe, eventAggr)
        {
            if (_machineType == MachineType.L)
            {
                VariableNumber = variableNumber;
            }
            
        }

        public OkumaVariable(IOkumaMill okumaMill, IEventAggregator eventAggr, int variableNumber) : base(okumaMill, eventAggr)
        {
            if (_machineType == MachineType.M)
            {
                VariableNumber = variableNumber;
            }
            
        }

        public override void Update()
        {

            try
            {
                if (_machineType == MachineType.L)
                {
                     _newValue = _okumaLathe.GetVariableValue(this.VariableNumber);

                    _oldValue = Value;

                    if (double.IsInfinity(_newValue))
                    {
                        _okumaLathe.SetVariable(this.VariableNumber, 0);
                        
                    }
                }

                if (_machineType == MachineType.M)
                {
                    _newValue = _okumaMill.GetVariableValue(this.VariableNumber);

                    _oldValue = Value;

                    if (double.IsInfinity(_newValue))
                    {
                        _okumaMill.SetVariable(this.VariableNumber, 0);

                    }
                }
                
                _newValue = 0;

                Value = _newValue;

                if (ValueChanged)
                {
                    ValueChanged = false;

                    _eventAggr.BeginPublishOnUIThread(new VariableChangedEvent(VariableNumber, _newValue, _oldValue));
                }
            }
            catch (Exception ex)
            {
                _eventAggr.PublishOnUIThread(new OkumaScanningExceptionEvent(ex, VariableNumber.ToString()));
            }

            //LastScan = DateTime.Now;
        }

        public int VariableNumber { get; set; }

        double val;
        public double Value
        {
            get
            {
                return val;
            }
            set
            {
                if (val == value)
                    return;
                val = value;
                valueChanged = true;
                NotifyOfPropertyChange();
            }
        }

        bool valueChanged;
        public bool ValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                if (valueChanged == value)
                    return;
                valueChanged = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
