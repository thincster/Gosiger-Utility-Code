
using Okuma_Monitor_Tools.Enums;
using Okuma_Monitor_Tools.Utilities;
using System;
using Okuma_Monitor_Tools.Okuma.IOkumaAPI;
using CmdAPI = Okuma.CLCMDAPI.CommandAPI;
using CmdEnum = Okuma.CLCMDAPI.Enumerations;
using DataAPI = Okuma.CLDATAPI.DataAPI;
using DataEnum = Okuma.CLDATAPI.Enumerations;

namespace Okuma_Monitor_Tools.Okuma.OkumaLathe
{
    public class OkumaLathe : IOkumaLathe
    {
        DataAPI.CMachine _machine;
        DataAPI.CVariables _variables;
        DataAPI.CTools _tools;
        DataAPI.CATC _atc;
        CmdAPI.CMachine _cmdMachine;
        DataAPI.CIO _io;
        DataAPI.CProgram cProgram;

       
        public OkumaLathe()
        {
            
            _machine = new DataAPI.CMachine();
            _machine.Init();

        }

        public void Dispose()
        {
            if (_machine != null) _machine.Close();
            _variables = null;
            _tools = null;
            _atc = null;
        }

        /// <summary>
        /// Gets the IO Address, Bit, and IO Type
        /// by passing in a IO Label.
        /// </summary>
        /// <param name="label">The IO Label</param>
        /// <returns></returns>
        public DataAPI.CIOAddress GetIO(string label)
        {
            if (_io == null) _io = new DataAPI.CIO();
            return _io.GetIO(label);
        }

        /// <summary>
        /// Gets the Status of an IO Point.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="address"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public bool GetIOBitStatus(IOTypeEnum type, int address, BitsEnum bit)
        {
            if (_io == null) _io = new DataAPI.CIO();

            //https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum
            DataEnum.IOTypeEnum _ioType = (DataEnum.IOTypeEnum)Enum.Parse(typeof(DataEnum.IOTypeEnum), type.ToString());
            DataEnum.BitsEnum _bit = (DataEnum.BitsEnum)Enum.Parse(typeof(DataEnum.BitsEnum), bit.ToString());

            return _io.GetBitIO(_ioType, address, _bit) == DataEnum.OnOffStateEnum.ON ? true : false;
        }

        public DataAPI.CIOAddress GetAddressByLabel_Lathe(string label)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Creates an Okuma Alarm to display on the control.
        /// </summary>
        /// <param name="AlarmLevel">B, C, or D Alarms are valid here.</param>
        /// <param name="Message">15 character message to display.</param>
        public void SetUserAlarm(AlarmLevelEnum AlarmLevel, string Message)
        {
            if (_cmdMachine == null)
                _cmdMachine = new CmdAPI.CMachine();
            //https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum
            CmdEnum.UserAlarmEnum _level = (CmdEnum.UserAlarmEnum)Enum.Parse(typeof(CmdEnum.UserAlarmEnum), AlarmLevel.ToString());

            var trimmedMsg = Message.Length <= 15 ? Message : Message.Substring(0, 15);
            _cmdMachine.SetUserAlarm(_level, trimmedMsg);
        }


        /// <summary>
        /// Used for THINC User Alarm with Okuma Help File Entry
        /// The Help Entry is placed in the Okuma Help File that is accesible
        /// by pressing the Information Key on the Control
        /// </summary>
        /// <param name="AlarmLevel">B, C, or D Alarms are valid here.</param>
        /// <param name="ShortMessage">15 character message to display.</param>
        /// <param name="HelpMessage">Information you want to show in the Help File.</param>
        public void SetUserAlarmWithHelp(AlarmLevelEnum AlarmLevel, string ShortMessage, string HelpMessage)
        {
            SetUserAlarm(AlarmLevel, ShortMessage);
            //https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum
            CmdEnum.UserAlarmEnum _level = (CmdEnum.UserAlarmEnum)Enum.Parse(typeof(CmdEnum.UserAlarmEnum), AlarmLevel.ToString());
            AlarmHelpEdit.WriteAlarmHelp(AlarmLevel, ShortMessage, HelpMessage);
        }

        /// <summary>
        /// Used for THINC External (hard wired) Alarm with Okuma Help File Entry
        /// The alarm on the control will be generated from the hard wire input. 
        /// </summary>
        /// <param name="AlarmLevel">A, B, or C Alarms are valid here.</param>
        /// <param name="ShortMessage">15 character message to display.</param>
        /// <param name="HelpMessage">Information you want to show in the Help File.</param>
        public void WriteExternalThincAlarmHelp(AlarmLevelEnum AlarmLevel, string ShortMessage, string HelpMessage)
        {
            AlarmHelpEdit.WriteExternalThincAlarmHelp(AlarmLevel, ShortMessage, HelpMessage);
        }

        /// <summary>
        /// This Removes Okuma Help File Entries.
        /// Keeps the Alarm Help Files Clean.
        /// Should be fired after the RestButton is pressed.
        /// </summary>
       public void RemoveAlarmHelp()
        {
            AlarmHelpEdit.RemoveAlarmHelp(AlarmLevelEnum.A);
            AlarmHelpEdit.RemoveAlarmHelp(AlarmLevelEnum.B);
            AlarmHelpEdit.RemoveAlarmHelp(AlarmLevelEnum.C);
            AlarmHelpEdit.RemoveAlarmHelp(AlarmLevelEnum.D);
        }

        /// <summary>
        /// Gets the value of a Common Variable.
        /// </summary>
        /// <param name="index">Common Variable to get value for.</param>
        /// <returns>Value of the Common variable</returns>
        public double GetVariableValue(int index)
        {
            if (_variables == null) _variables = new DataAPI.CVariables();
            return _variables.GetCommonVariableValue(index);
        }

        /// <summary>
        /// Sets the Common Variable number to the value.
        /// </summary>
        /// <param name="index">Variable number to change.</param>
        /// <param name="value">Value to change to.</param>
        public void SetVariable(int index, double value)
        {
            if (_variables == null) _variables = new DataAPI.CVariables();
            _variables.SetCommonVariableValue(index, value);
        }

    }
}
