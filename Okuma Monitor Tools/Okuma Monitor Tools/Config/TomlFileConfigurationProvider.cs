using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nett;
using Westwind.Utilities;
using Westwind.Utilities.Configuration;

namespace Okuma_Monitor_Tools
{
    public class TomlFileConfigurationProvider<TAppConfiguration> : ConfigurationProviderBase<TAppConfiguration>
        where TAppConfiguration : AppConfiguration, new()
    {

        public string TomlConfigurationFile { get; set; }
        public override TAppConfiguration Read<TAppConfiguration>()
        {
            var result = Toml.ReadFile<TAppConfiguration>(TomlConfigurationFile) as TAppConfiguration;
            if (result != null)
                DecryptFields(result);
            return result;
        }

        public override bool Read(AppConfiguration config)
        {
            if (!File.Exists(TomlConfigurationFile))
            {
                Write(config);
                return Read(config);
            }

            var tt = Toml.ReadFile<TAppConfiguration>(TomlConfigurationFile);
            var t2 = tt is TAppConfiguration;


            if (!(Toml.ReadFile<TAppConfiguration>(TomlConfigurationFile) is TAppConfiguration newConfig))
            {
                if (Write(config))
                    return true;
                return false;
            }
            DecryptFields(newConfig);
            DataUtils.CopyObjectData(newConfig, config, "Provider,ErrorMessage");

            return true;
        }

        public override bool Write(AppConfiguration config)
        {
            EncryptFields(config);

            Toml.WriteFile(config, TomlConfigurationFile);

            // Have to decrypt again to make sure the properties are readable afterwards
            DecryptFields(config);

            return true;
        }
    }
}
