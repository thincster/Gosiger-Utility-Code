using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace Okuma_Monitor_Tools.PLC
{
    public class ToPLC : PropertyChangedBase
    {
        public static ToPlcStruct ToPlcStruct;

        public ushort BKCommand1
        {
            get => ToPlcStruct.BKCommand1;
            set => ToPlcStruct.BKCommand1 = value;
        }

        public ushort BKCommand2
        {
            get => ToPlcStruct.BKCommand2;
            set => ToPlcStruct.BKCommand2 = value;
        }

        public ushort BKCommand3
        {
            get => ToPlcStruct.BKCommand3;
            set => ToPlcStruct.BKCommand3 = value;
        }

        public bool ToolUnclamp
        {
            get => ToPlcStruct.ToolUnclamp;
            set => ToPlcStruct.ToolUnclamp = value;
        }

        public bool ToolClamp
        {
            get => ToPlcStruct.ToolClamp;
            set => ToPlcStruct.ToolClamp = value;
        }

        public bool Watchdog
        {
            get => ToPlcStruct.Watchdog;
            set => ToPlcStruct.Watchdog = value;
        }

        public byte MagazineType
        {
            get => ToPlcStruct.MagazineType;
            set => ToPlcStruct.MagazineType = value;
        }

        public bool PotToolDetect
        {
            get => ToPlcStruct.PotToolDetect;
            set => ToPlcStruct.PotToolDetect = value;
        }

        public bool Reset
        {
            get => ToPlcStruct.Reset;
            set => ToPlcStruct.Reset = value;
        }

        public bool DrivesOk
        {
            get => ToPlcStruct.DrivesOk;
            set => ToPlcStruct.DrivesOk = value;
        }

        public bool ToolKeyVertical
        {
            get => ToPlcStruct.ToolKeyVertical;
            set => ToPlcStruct.ToolKeyVertical = value;
        }

        public bool BKTest
        {
            get => ToPlcStruct.BKTest;
            set => ToPlcStruct.BKTest = value;
        }

        public bool BKParSetExecute
        {
            get => ToPlcStruct.BKParSetExecute;
            set => ToPlcStruct.BKParSetExecute = value;
        }

        public bool PotMagazineSide
        {
            get => ToPlcStruct.PotMagazineSide;
            set => ToPlcStruct.PotMagazineSide = value;
        }
        public bool PotSpindleSide
        {
            get => ToPlcStruct.PotSpindleSide;
            set => ToPlcStruct.PotSpindleSide = value;
        }

        public bool LargeDiaTool
        {
            get => ToPlcStruct.LargeDiaTool;
            set => ToPlcStruct.LargeDiaTool = value;
        }

        public bool BkParSetComplete
        {
            get => ToPlcStruct.BkParSetComplete;
            set => ToPlcStruct.BkParSetComplete = value;
        }

        public bool TeachCmd
        {
            get => ToPlcStruct.TeachCmd;
            set => ToPlcStruct.TeachCmd = value;
        }

        public bool MeasureCmd
        {
            get => ToPlcStruct.MeasureCmd;
            set => ToPlcStruct.MeasureCmd = value;
        }

    }


    public struct ToPlcStruct
    {
        public ushort BKCommand1;
        public ushort BKCommand2;
        public ushort BKCommand3;
        public byte MagazineType;
        public bool ToolUnclamp;
        public bool PotToolDetect;
        public bool Reset;
        public bool DrivesOk;
        public bool ToolKeyVertical;
        public bool ToolClamp;
        public bool BKTest;
        public bool BKParSetExecute;
        public bool BkParSetComplete;
        public bool LargeDiaTool;
        public bool PotMagazineSide;
        public bool PotSpindleSide;
        public bool TeachCmd;
        public bool MeasureCmd;
        public bool Spare3;
        public bool Watchdog;
    }

}
