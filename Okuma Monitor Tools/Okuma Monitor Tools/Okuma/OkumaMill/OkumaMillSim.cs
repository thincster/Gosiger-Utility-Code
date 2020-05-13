using Okuma.CMDATAPI.DataAPI;
using Okuma_Monitor_Tools.Enums;
//using Okuma.CMDATAPI.Enumerations;
using System;

namespace Okuma_Monitor_Tools.Okuma.OkumaMill
{
    public class OkumaMillSim : IOkumaMill
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CIOAddress GetIO(string label)
        {
            throw new NotImplementedException();
        }

        public bool GetIOBitStatus(IOTypeEnum type, int address, BitsEnum bit)
        {
            throw new NotImplementedException();
        }

        public void SetUserAlarm(AlarmLevelEnum AlarmLevel, string Message)
        {
            throw new NotImplementedException();
        }

        public void SetUserAlarmWithHelp(AlarmLevelEnum AlarmLevel, string ShortMessage, string HelpMessage)
        {
            throw new NotImplementedException();
        }

        public void WriteExternalThincAlarmHelp(AlarmLevelEnum AlarmLevel, string ShortMessage, string HelpMessage)
        {
            throw new NotImplementedException();
        }

        public void RemoveAlarmHelp()
        {
            throw new NotImplementedException();
        }

        public double GetVariableValue(int index)
        {
            throw new NotImplementedException();
        }

        public void SetVariable(int index, double value)
        {
            throw new NotImplementedException();
        }
    }
}
