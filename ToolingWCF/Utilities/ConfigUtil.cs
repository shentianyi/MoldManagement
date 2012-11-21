using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nini.Config;
using ToolingWCF.DataModel;
namespace ToolingWCF.Utilities
{
    public class ConfigUtil
    {
        private IConfig config;
        private IConfigSource source;

        public ConfigUtil()
        {
            string path=System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user.ini");
            source = new IniConfigSource(path);
        }

        public ConfigUtil(string node, string filename) {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            source = new IniConfigSource(path);
            config = source.Configs[node];
        }

        public List<ClientSetting> Get( )
        {
            List<ClientSetting> clientSetting = new List<ClientSetting>();
            for (int i = 0; i < source.Configs.Count; i++)
            {
                config = source.Configs[i];
                string[] keys = config.GetKeys();
                string[] values = config.GetValues();
                for (int j = 0; j < keys.Length; j++)
                {
                    string node = config.Name;
                    ClientSetting cs = new ClientSetting() { SettingGroup = node, Key = keys[j], Value = values[j] };
                    clientSetting.Add(cs);
                }
            }
            return clientSetting;

            //foreach (string node in nodes)
            //{

            //    config = source.Configs[node];
            //    string[] keys = config.GetKeys();
            //    string[] values = config.GetValues();
            //    for (int i = 0; i < keys.Length; i++)
            //    {
            //        ClientSetting cs = new ClientSetting() { SettingGroup = node, Key = keys[i], Value = values[i] };
            //        clientSetting.Add(cs);
            //    }
            //}

        }
        public string Get(string key) { 
        return config.Get(key);
        }
    }
}
