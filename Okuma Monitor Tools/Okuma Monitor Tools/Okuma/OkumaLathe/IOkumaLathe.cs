using Okuma.CLDATAPI.DataAPI;
using Okuma_Monitor_Tools.Enums;
using System;

namespace Okuma_Monitor_Tools.Okuma.OkumaLathe
{
    public interface IOkumaLathe: IDisposable
    {
        void SetUserAlarm(AlarmLevelEnum AlarmLevel, string Message);


        void SetUserAlarmWithHelp(AlarmLevelEnum AlarmLevel, string ShortMessage, string HelpMessage);


        void WriteExternalThincAlarmHelp(AlarmLevelEnum AlarmLevel, string ShortMessage, string HelpMessage);


        void RemoveAlarmHelp();


        Double GetVariableValue(int index);
        void SetVariable(int index, double value);

        bool GetIOBitStatus(IOTypeEnum type, int address, BitsEnum bit);



        CIOAddress GetIO(string label);

       
    }
}

