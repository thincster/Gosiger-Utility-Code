using Okuma_Monitor_Tools.Enums;
using System;

namespace Okuma_Monitor_Tools.Okuma
{

    public class APIUtilities
    {
        /// <summary>
        ///     ''' Returns the Version of the THINC API; Returns nothing if THINC API is not installed
        ///     ''' </summary>
        public static Version GetTHiNCAPIVersion()
        {
            Version _return = null;
            string _uninstallkey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(_uninstallkey);
            Microsoft.Win32.RegistryKey sk;
          var skName = rk.GetSubKeyNames();
            for (int counter = 0; counter <= skName.Length - 1; counter++)
            {
                sk = rk.OpenSubKey(skName[counter]);
                if (sk.GetValue("DisplayName") != "")
                {
                    if (sk.GetValue("DisplayName").ToString().ToUpper().Contains("THINC-API"))
                    {
                        string dv = sk.GetValue("DisplayVersion").ToString();
                        if (dv != null)
                            _return = new Version(dv);
                    }
                }
            }

            return _return;
        }

        public static APIInializeException CheckAPI(enumMachineType MachineType)
        {
            return CheckAPI(MachineType, "1.15.0");
        }

        public static APIInializeException CheckAPI(enumMachineType MachineType, string RequiredAPIVersion)
        {
            APIInializeException __rtn = new APIInializeException("", EnumAPIProblem.NoError);

            var version = new Version(RequiredAPIVersion);

            if (MachineType == enumMachineType.Lathe || MachineType == enumMachineType.Mill)
            {
                var __APIVersion = GetTHiNCAPIVersion();
                if (__APIVersion == null)
                    __rtn = new APIInializeException("API not installed on this machine", EnumAPIProblem.NoAPI);
                // Compare the version number to the required version number .CompareTo returns -1 if older, 0 if same and 1 if newer
                var __vc = __APIVersion.CompareTo(version);
                if (__vc == -1)
                    __rtn = new APIInializeException(
                        string.Format("API version {0} or greater required", RequiredAPIVersion),
                        EnumAPIProblem.OldAPI);
            }

            return __rtn;
        }
    }

    public class APIInializeException : Exception
    {
        public readonly EnumAPIProblem APIProblem;

        public APIInializeException(string Message, EnumAPIProblem APIProblem) : this(Message, APIProblem, null)
        {
        }

        public APIInializeException(string Message, EnumAPIProblem APIProblem, Exception InnerExecption) : base(Message,
            InnerExecption)
        {
            this.APIProblem = APIProblem;
        }
    }

    public enum EnumAPIProblem
    {
        Offline = 0,
        NoAPI = 1,
        OldAPI = 2,
        Exception = 3,
        NoError = 4
    }
}
