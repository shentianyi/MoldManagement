using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolingManWPF.ConditionServiceReference;
using ToolingManWPF.Utilities;

namespace ToolingManWPF.Helper
{
  public static  class ClientSettingUpdateHelper
    {
      //public static void UpdateClientSetting()
      //{
      //    using (ConditionServiceClient conditionClient = new ConditionServiceClient())
      //    {
      //        List<ClientSetting> clientSettings = conditionClient.UpdateSettings();

      //        List<string> settingGroup = clientSettings.Select(c => c.SettingGroup).Distinct().ToList();

      //        ConfigUtil config = new ConfigUtil();
      //        foreach (string sp in settingGroup)
      //        {
      //            config.Update(sp);

      //            List<ClientSetting> css = clientSettings.Where(cs => cs.SettingGroup.Equals(sp)).ToList();
      //            foreach (ClientSetting cs in css)
      //            {
      //                config.Set(cs.Key, cs.Value);
      //            }
      //            config.Save();
      //        }
      //    }
      //}
    }
}
