using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nett;
using Westwind.Utilities.Configuration;


namespace Okuma_Monitor_Tools
{
  public  class ApplicationSettings : AppConfiguration
    {
        [TomlComment("Creates an AppData directory and project folder.")]
        public virtual string AppDirectory { get; set; }

        [TomlComment("Lowest API Version that works with this application.")]
        public string APIVersion { get; set; }

         [TomlComment(@"Where the Config Files are located.")]
        public virtual string ConfigDirectory { get; set; }

        public string IPSubnet { get; set; }

        [TomlComment("Okuma serial number last 4 digits.")]
        public string OkuamaLast4 { get; set; }

        [TomlComment("PLC serial number last 4 digits.")]
        public string PlcLast4 { get; set; }

        [TomlComment("Okuma last octet.")]
        public string OkumaCom { get; set; }

        [TomlComment("PLC last octet.")]
        public string PlcCom{ get; set; }

        [TomlComment("Does this appliction require use of the network.")]
        public bool NetworkRequired { get; set; }

        
        [TomlComment("Does this application use a PLC.")]
        public bool PlcRequired { get; set; }

        [TomlComment("Set 'true' to run the Wizard during next startup")]
        public bool Wizard { get; set; }

        [TomlComment("Used to set the interval for the scantimer")]
        public int ScanInterval { get; set; }

        public ApplicationSettings()
        {
            var appDataPath = Path.Combine("D:\\", "AppData");

            if (!Directory.Exists(appDataPath)) { Directory.CreateDirectory(appDataPath); }

            var gosFldr = Path.Combine("D:\\", "AppData", "Gosiger");

            if (!Directory.Exists(gosFldr)) { Directory.CreateDirectory(gosFldr); }

            AppDirectory = Path.Combine(gosFldr, Assembly.GetExecutingAssembly().GetName().Name.Replace(".exe", ""));
            if (!Directory.Exists(AppDirectory)) { Directory.CreateDirectory(AppDirectory); }

            ConfigDirectory = Path.Combine(AppDirectory, "Config");
            if (!Directory.Exists(ConfigDirectory)) { Directory.CreateDirectory(ConfigDirectory); }

            APIVersion = "1.9.1";
            NetworkRequired = true;
            PlcRequired = true;
            IPSubnet = "192.168.10";
            OkumaCom = "";
            PlcCom = "";
            OkuamaLast4 = "";
            PlcLast4 = "";
            ScanInterval = 100;
            Wizard = true;

        }

    }
    public static class My
    {

        static My()
        {
            TomlFileConfigurationProvider<ApplicationSettings> provider = new TomlFileConfigurationProvider<ApplicationSettings>();
            try
            {
                Settings = new ApplicationSettings();
                provider = new TomlFileConfigurationProvider<ApplicationSettings>()
                {
                    TomlConfigurationFile = Path.Combine(Settings.ConfigDirectory, "settings.txt"),
                    EncryptionKey = "Gosiger1234"
                };
                Settings.Initialize(provider);
                Settings.Write();

            }
            catch (Exception ex)
            {
                var mm = provider.ErrorMessage;
                var str = ex.Message;
            }


        }


        public static readonly ApplicationSettings Settings;

    }
}
