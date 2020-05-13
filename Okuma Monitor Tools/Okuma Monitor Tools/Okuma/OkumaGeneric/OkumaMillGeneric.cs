using Okuma_Monitor_Tools.Enums;
using Okuma_Monitor_Tools.Utilities;
using System;
using CmdAPI = Okuma.CMCMDAPI.CommandAPI;
using CmdEnum = Okuma.CMCMDAPI.Enumerations;
using DataAPI = Okuma.CMDATAPI.DataAPI;
using DataEnum = Okuma.CMDATAPI.Enumerations;

namespace Okuma_Monitor_Tools.Okuma.IOkumaAPI
{
    public class OkumaMillGeneric : IOkuma
    {
        DataAPI.CMachine _machine;
        DataAPI.CVariables _variables;
        DataAPI.CTools _tools;
        DataAPI.CATC _atc;
        CmdAPI.CMachine _cmdMachine;
        DataAPI.CIO _io;
        DataAPI.CProgram cProgram;

        public OkumaMillGeneric()
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
            _io = null;
            cProgram = null;
        }

        /// <summary>
        /// Gets the Status of an IO Point.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="address"></param>
        /// <param name="bit"></param>
        /// <returns>IOStatus Emum (ON / OFF</returns>
        public bool GetIOBitStatus(IOTypeEnum type, int address, BitsEnum bit)
        {
            if (_io == null) _io = new DataAPI.CIO();

            //https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum
            DataEnum.IOTypeEnum _ioType = (DataEnum.IOTypeEnum)Enum.Parse(typeof(DataEnum.IOTypeEnum), type.ToString());
            DataEnum.BitsEnum _bit = (DataEnum.BitsEnum)Enum.Parse(typeof(DataEnum.BitsEnum), bit.ToString());

            return _io.GetBitIO(_ioType, address, _bit) == DataEnum.OnOffStateEnum.ON ? true : false;
        }

        public double GetVariableValue(int index)
        {
            throw new NotImplementedException();
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
            if (_cmdMachine == null)
                _cmdMachine = new CmdAPI.CMachine();
            //https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum
            CmdEnum.UserAlarmEnum _level = (CmdEnum.UserAlarmEnum)Enum.Parse(typeof(CmdEnum.UserAlarmEnum), AlarmLevel.ToString());
            AlarmHelpEdit.WriteAlarmHelp(AlarmLevel, ShortMessage, HelpMessage);
        }

        /// <summary>
        /// Sets the Common Variable number to the value.
        /// </summary>
        /// <param name="index">Variable number to change.</param>
        /// <param name="value">Value to change to.</param>
        public void SetVariable(int index, double value)
        {
            if (_variables == null)
            {
                _variables = new DataAPI.CVariables();
            }

            _variables.SetCommonVariableValue(index, value);
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

            AlarmHelpEdit.WriteAlarmHelp(AlarmLevel, ShortMessage, HelpMessage);
        }





        /// <summary>
        /// Do Not Use this method, this is for a Lathe.
        /// Use GetAddressByLabel_Mill.
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public global::Okuma.CLDATAPI.DataAPI.CIOAddress GetAddressByLabel_Lathe(string label)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the IO Address, Bit, and IO Type
        /// by passing in a IO Label.
        /// </summary>
        /// <param name="label">The IO Label</param>
        /// <returns>CIOAddress</returns>
        public DataAPI.CIOAddress GetAddressByLabel_Mill(string label)
        {
            if (_io == null) _io = new DataAPI.CIO();
            return _io.GetIO(label);
        }

    }
}
