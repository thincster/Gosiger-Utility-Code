using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Okuma_Monitor_Tools.Enums
{

    public enum WorkpieceCounterEnum
    {
        // Fields
        CounterA = 0,
        CounterB = 1,
        CounterC = 2,
        CounterD = 3
    }

    public enum enumMachineType : int
    {
        Lathe = 0,
        Mill = 1,
        Sim = 2,
        S = 3,
        Grinder = 4
    }

    public enum ToolModeEnum
    {
        TD = 0,
        TL = 1
    }

    public enum SpindleEnum : int
    {
        Main = 1,
        Sub = 2
    }

    public enum enumWaitForOspReturnCode
    {
        OspStarted = 0,
        SimulationMode = 1,
        Timeout = 2
    }

    public enum enumAppType : int
    {
        Process = 0,
        Service = 1
    }

    public enum enumLaunch : int
    {
        LaunchOnce = 0,
        Monitor = 1
    }

    public enum EnumDisplayMessageType
    {
        Alarm = 0,
        Warning = 1,
        Information = 2
    }

    public enum enumInchMetric
    {
        MM = 0,
        Inch = 1
    }

    public enum AlarmLevelEnum : int
    {
        [Description("Alarm A")] A = 4,
        [Description("Alarm P")] P = 5,
        [Description("Alarm B")] B = 3,
        [Description("Alarm C")] C = 2,
        [Description("Alarm D")] D = 1
    }

    public enum SpindleStateEnum
    {
        // Fields
        CCW = 2,
        CW = 1,
        Indexing = 3,
        Stop = 0
    }

    // Public Enum enumMachineType As Integer
    // Lathe = 0
    // Mill = 1
    // Sim = 2
    // S = 3
    // Grinder = 4
    // End Enum

    public enum DataUnitEnum
    {
        // Fields
        Unit_Inch = 1,
        Unit_mm = 0
    }

    public enum AxisEnum
    {
        X = 0,
        Y = 1,
        Z = 2,
        A = 3,
        B = 4,
        C = 5
    }

    public enum FeedrateTypeEnum
    {
        // Fields
        PerMinute = 0,
        PerRevolution = 1
    }


    public enum ExecutionModeEnum
    {
        // Fields
        NotRun = 0,
        Running = 1
    }


    public enum PanelModeEnum
    {
        // Fields
        Auto = 0,
        MacMan = 7,
        Manual = 2,
        MDI = 1,
        ParameterSetup = 4,
        ProgramOperation = 3,
        ToolDataSetup = 6,
        ZeroSetup = 5
    }


    public enum PanelGroupEnum
    {
        // Fields
        MacManMode = 5,
        OperationMode = 0,
        ParameterMode = 2,
        ProgramMode = 1,
        ToolDataSettingMode = 4,
        ZeroSetMode = 3
    }

    public enum OSPAlarmLevelEnum
    {
        // Fields
        ALARM_A = 4,
        ALARM_B = 3,
        ALARM_C = 2,
        ALARM_D = 1,
        ALARM_P = 5,
        None = 0
    }

    public enum ReportPeriodEnum
    {
        // Fields
        PeriodReport = 2,
        PreviousDayReport = 1,
        TodayReport = 0
    }


    public enum OperationModeEnum : int
    {
        Auto = 0,
        MDI = 1,
        Manual = 2
    }


    public enum NCStatusEnum
    {
        // Fields
        Alarm = 0,
        Limit = 1,
        ProgramStop = 5,
        Run = 4,
        SlideHold = 2,
        STM = 3
    }


    public enum HourMeterEnum
    {
        // Fields
        CuttingTime = 3,
        ExternalInputTime = 4,
        NCRunningTime = 1,
        PowerOnTime = 0,
        SpindleRevolutionTime = 2
    }


    public enum IOTypeEnum : int
    {
        Input = 0,
        Output = 1
    }
    public enum OnOffStateEnum
    {
        OFF,
        ON,
    }

    public enum BitsEnum
    {
        // Fields
        Bit_0 = 0,
        Bit_1 = 1,
        Bit_10 = 10,
        Bit_11 = 11,
        Bit_12 = 12,
        Bit_13 = 13,
        Bit_14 = 14,
        Bit_15 = 15,
        Bit_2 = 2,
        Bit_3 = 3,
        Bit_4 = 4,
        Bit_5 = 5,
        Bit_6 = 6,
        Bit_7 = 7,
        Bit_8 = 8,
        Bit_9 = 9
    }

}
