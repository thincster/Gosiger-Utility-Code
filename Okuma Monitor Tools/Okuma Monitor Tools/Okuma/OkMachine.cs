using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using Scout=Okuma.Scout;
using Okuma_Monitor_Tools.Enums;
using Okuma_Monitor_Tools.Utilities;

namespace Okuma_Monitor_Tools.Okuma
{

    public class OkMachine
    {
        private static MachineInformation _machineInfo;

        public static MachineInformation MachineInfo
        {
            get
            {
                if (_machineInfo == null)
                    /* TODO ERROR: Skipped WarningDirectiveTrivia */
                    _machineInfo = GetMachineInfo();
                return _machineInfo;
            }
        }

        [Obsolete("Use MachineInfo Property")]
        public static MachineInformation GetMachineInfo()
        {
            MachineInformation rtnMachineInfo = new MachineInformation();

            return rtnMachineInfo;
        }

        [Obsolete("Use MachineType Property")]
        public static enumMachineType GetMachineType()
        {
            return MachineInfo.MachineType;
        }

        public static enumMachineType MachineType
        {
            get { return MachineInfo.MachineType; }
        }
    }
    
    public class MachineInformation
    {
        public Scout.VersionInformation APIVersion
        {
            get { return Scout.ThincApi.InstallVersion;}
            
        }

        public Scout.Enums.ApiStatus ApiAvailable
        {
            get { return Scout.ThincApi.ApiAvailable; }
        }

        public bool IsMachine
        {
            get { return Scout.Platform.Machine != Scout.Enums.MachineType.PC; }
        }

        public bool IsNcRunning
        {
            get { return Scout.OspProcessInfo.EbiStartRunning; }
        }

       

        public enumMachineType MachineType
        {
            get
            {
                switch (Scout.Platform.BaseMachineType)
                {
                    case  Scout.Enums.MachineType.M:
                    {
                        return enumMachineType.Mill;
                        
                    }

                    case  Scout.Enums.MachineType.L:
                    {
                        return enumMachineType.Lathe;
                    }

                    default:
                    {
                        return enumMachineType.Sim;
                    }
                }
            }
        }

        public bool IsPCSim
        {
            get
            {
                var mtype = Scout.Platform.Machine;
                return mtype == Scout.Enums.MachineType.NCM_L || mtype == Scout.Enums.MachineType.NCM_M ||
                       mtype == Scout.Enums.MachineType.PCNCM_L || mtype == Scout.Enums.MachineType.PCNCM_M;
            }
        }

        public Scout.Enums.ControlType ControlType
        {
            get { return Scout.Platform.Control; }
        }

        public string FullControlName
        {
            get { return Scout.DMC.PLCControl; }
        }

        public bool IsSControl
        {
            get { return Scout.Platform.Control == Scout.Enums.ControlType.P300S; }
        }

        public string SerialNumber
        {
            get
            {
                if (IsPCSim)
                    return "99999";
                return Scout.DMC.SerialNumber;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLineFormat("Machine type: {0}", MachineType);
            sb.AppendLineFormat("Control type: {0}", ControlType);
            sb.AppendLineFormat("Full Control Name: {0}", FullControlName);
            sb.AppendLineFormat("Is 'S' control?: {0}", IsSControl);
            return sb.ToString();
        }
    }


    public enum OspControlType : int
    {
        P100 = 100,
        P200 = 200,
        P300 = 300
    }
}
