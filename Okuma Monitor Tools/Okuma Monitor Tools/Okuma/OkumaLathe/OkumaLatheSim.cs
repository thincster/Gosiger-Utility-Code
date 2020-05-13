using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Okuma.CLDATAPI.DataAPI;
//using Okuma.CLDATAPI.Enumerations;
using Okuma_Monitor_Tools.Okuma.OkumaLathe;
using Okuma_Monitor_Tools.Enums;

namespace Okuma_Monitor_Tools.Okuma
{
    public class OkumaLatheSim : IOkumaLathe
    {
        public OkumaLatheSim()
        {
            
        }
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

        public CIOAddress GetAddressByLabel_Lathe(string label)
        {
            throw new NotImplementedException();
        }

        public double GetVariableValue(int index)
        {
            throw new NotImplementedException();
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
    }
}
