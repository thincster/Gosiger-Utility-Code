using Okuma.CMDATAPI.DataAPI;
using Okuma_Monitor_Tools.Enums;
using System;

namespace Okuma_Monitor_Tools.Okuma.OkumaMill
{
  public interface IOkumaMill: IDisposable
  {

        void SetUserAlarm(AlarmLevelEnum AlarmLevel, string Message);
           

         void SetUserAlarmWithHelp(AlarmLevelEnum AlarmLevel, string ShortMessage, string HelpMessage);
           

         void WriteExternalThincAlarmHelp(AlarmLevelEnum AlarmLevel, string ShortMessage, string HelpMessage);
            

        void RemoveAlarmHelp();
           
        
        Double GetVariableValue(int index);
        void SetVariable(int index, double value);

        CIOAddress GetIO(string label);
        bool GetIOBitStatus(IOTypeEnum type, int address, BitsEnum bit);

    }
}
