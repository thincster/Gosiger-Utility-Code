using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okuma_Monitor_Tools.PLC
{
 
        public class FromPLC
        {
            public static FromPLCStruct FromPlcStruct;

            public ushort BKStatus1
            {
                get
                {
                    return FromPlcStruct.BkStatus1;
                }
                set
                {
                    FromPlcStruct.BkStatus1 = value;
                }
            }

            public ushort BKStatus2
            {
                get
                {
                    return FromPlcStruct.BkStatus2;
                }
                set
                {
                    FromPlcStruct.BkStatus2 = value;
                }
            }

            public ushort BkStatus3
            {
                get
                {
                    return FromPlcStruct.BkStatus3;
                }
                set
                {
                    FromPlcStruct.BkStatus3 = value;
                }
            }

            public bool HeartbeatOk
            {
                get => FromPlcStruct.HeartbeatOk;
                set => FromPlcStruct.HeartbeatOk = value;
            }

            public bool OperationStart
            {
                get => FromPlcStruct.OperationStart;
                set => FromPlcStruct.OperationStart = value;
            }

        }

        public struct FromPLCStruct
        {
            public ushort BkStatus1;
            public ushort BkStatus2;
            public ushort BkStatus3;
            public bool HeartbeatOk;
            public bool OperationStart;
            public bool Spare2;
            public bool Spare3;
            public bool Spare4;
            public bool Spare5;
            public bool Spare6;
            public bool Spare7;
            public bool Spare8;
            public bool Spare9;
            public bool Spare10;
            public bool Spare11;
            public bool Spare12;
            public bool Spare13;
            public bool Spare14;
            public bool Spare15;
        }
    }

