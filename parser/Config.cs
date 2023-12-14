using System;
using System.IO;
using Newtonsoft.Json;

namespace CodeForcesDRP.parser
{
    public struct ConfigInfo
    {
        public string ApplicationID;
    }

    public class Config
    {
        private const string configFile = "config.json";

        public ConfigInfo ReadConfig()
        {
            try
            {
                return (ConfigInfo)JsonConvert.DeserializeObject<ConfigInfo>(File.ReadAllText(configFile));
            }
            catch (Exception)
            {
                return new ConfigInfo();
            }
        }

        public void WriteNewConfig()
        {
            if (File.Exists(configFile))
            {
                File.Delete(configFile);
            }
            using (StreamWriter writer = new StreamWriter(configFile))
            {
                writer.Write("{\"ApplicationID\":69420}");
            }
        }
    }
}
