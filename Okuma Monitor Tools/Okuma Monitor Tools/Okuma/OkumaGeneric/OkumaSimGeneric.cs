using Okuma_Monitor_Tools.Enums;
using System;

namespace Okuma_Monitor_Tools.Okuma.IOkumaAPI
{
    public class OkumaSimGeneric : IOkuma
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

       

        public bool GetIOBitStatus(IOTypeEnum type, int address, BitsEnum bit)
        {
            throw new NotImplementedException();
        }

       

        public double GetVariableValue(int index)
        {
            if (index == 100)
            {
                return  325;
            }

            return 0;
        }

        public void RemoveAlarmHelp()
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

        public void SetVariable(int index, double value)
        {
            throw new NotImplementedException();
        }

        public void WriteExternalThincAlarmHelp(AlarmLevelEnum AlarmLevel, string ShortMessage, string HelpMessage)
        {
            throw new NotImplementedException();
        }

        public global::Okuma.CLDATAPI.DataAPI.CIOAddress GetAddressByLabel_Lathe(string label)
        {
            throw new NotImplementedException();
        }

        public global::Okuma.CMDATAPI.DataAPI.CIOAddress GetAddressByLabel_Mill(string label)
        {
            throw new NotImplementedException();
        }

        
    }
}
