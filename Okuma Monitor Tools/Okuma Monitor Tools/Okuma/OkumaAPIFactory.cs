
using Okuma_Monitor_Tools.Enums;
using Okuma_Monitor_Tools.Okuma.IOkumaAPI;
using Okuma_Monitor_Tools.Okuma.OkumaLathe;
using Okuma_Monitor_Tools.Okuma.OkumaMill;

namespace Okuma_Monitor_Tools.Okuma
{

    public class OkumaAPIFactory
    {
        private static IOkuma _genericAPI;

        public static IOkuma GetAPI()
        {
            if (_genericAPI != null)
                return _genericAPI;
            _genericAPI = new OkumaSimGeneric();
            
            var machineType = OkMachine.MachineType;

            var apiCheckResults = APIUtilities.CheckAPI(machineType, My.Settings.APIVersion);

            if (apiCheckResults.APIProblem == EnumAPIProblem.NoError)
            {
                switch (machineType)
                {
                    case enumMachineType.Mill:
                        {
                            _genericAPI = new OkumaMillGeneric();
                            break;
                        }

                    case  enumMachineType.Lathe:
                        {
                            _genericAPI = new OkumaLatheGeneric();
                            break;
                        }

                    default:
                        {
                            _genericAPI = new OkumaSimGeneric();
                            break;
                        }
                }

                return _genericAPI;
            }
            else
                throw apiCheckResults;
        }

            public static IOkumaLathe GetLatheAPI()
            {
                IOkumaLathe rtnAPI = new OkumaLatheSim();
                if (OkMachine.MachineType == enumMachineType.Lathe)
                {
                   
                    rtnAPI = new OkumaLathe.OkumaLathe();
                    //OkMachine.MachineInfo.HasSubSpindle = rtnAPI.GenericOkumaAPI.GetSpecCode(4, 1);

                    //var hasBTurret = rtnAPI.IsValidSubSystem(Okuma.CLDATAPI.Enumerations.SubSystemEnum.NC_BL);
                    //var hasCTurret = rtnAPI.IsValidSubSystem(Okuma.CLDATAPI.Enumerations.SubSystemEnum.NC_CL);
                    //var numOfTurrets = 3;

                    //if (!hasBTurret)
                    //    numOfTurrets -= 1;
                    //if (!hasCTurret)
                    //    numOfTurrets -= 1;

                    //OkMachine.MachineInfo.NumberOfTurrets = numOfTurrets;
                }


                return rtnAPI;
            }

            public static IOkumaMill GetMillAPI()
            {
                if (OkMachine.MachineType == enumMachineType.Mill)
                    return new OkumaMill.OkumaMill();
                else
                    return new OkumaMillSim();
            }
    }
}
