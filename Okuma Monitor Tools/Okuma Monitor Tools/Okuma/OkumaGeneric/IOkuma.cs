using Okuma_Monitor_Tools.Enums;
using System;
using LatheAddress = Okuma.CLDATAPI.DataAPI;
using MillAddress = Okuma.CMDATAPI.DataAPI;

namespace Okuma_Monitor_Tools.Okuma.IOkumaAPI
{
    public interface IOkuma : IDisposable
    { 

    void SetUserAlarm(AlarmLevelEnum AlarmLevel, string Message);


    void SetUserAlarmWithHelp(AlarmLevelEnum AlarmLevel, string ShortMessage, string HelpMessage);


    void WriteExternalThincAlarmHelp(AlarmLevelEnum AlarmLevel, string ShortMessage, string HelpMessage);


    void RemoveAlarmHelp();


    Double GetVariableValue(int index);
    void SetVariable(int index, double value);

    bool GetIOBitStatus(IOTypeEnum type, int address, BitsEnum bit);



    LatheAddress.CIOAddress GetAddressByLabel_Lathe(string label);

    MillAddress.CIOAddress GetAddressByLabel_Mill(string label);
     
    }
}
