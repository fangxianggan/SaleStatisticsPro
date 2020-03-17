using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Core.Util
{
    /// <summary>
    /// config 设置
    /// </summary>
    public static class ConfigUtils
    {
        public static string GetKey(string configPath, string key)
        {
            string text = "";
            try {
                Configuration ConfigurationInstance = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = configPath
                }, ConfigurationUserLevel.None);


                if (ConfigurationInstance.AppSettings.Settings[key] != null)
                    text= ConfigurationInstance.AppSettings.Settings[key].Value;
                else

                    text= string.Empty;

            } catch (Exception ex)
            {
                throw ex;
            }
            return text;
        }

        public static bool SetKey(string configPath, string key, string vls)
        {
            try
            {
                Configuration ConfigurationInstance = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = configPath
                }, ConfigurationUserLevel.None);

                if (ConfigurationInstance.AppSettings.Settings[key] != null)
                    ConfigurationInstance.AppSettings.Settings[key].Value = vls;
                else
                    ConfigurationInstance.AppSettings.Settings.Add(key, vls);
                ConfigurationInstance.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
