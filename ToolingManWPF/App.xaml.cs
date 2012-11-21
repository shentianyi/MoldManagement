using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using ToolingManWPF.ConditionServiceReference;
using ToolingManWPF.Properties;

using Nini.Config;
using ToolingManWPF.Utilities;
using ToolingManWPF.Helper;

namespace ToolingManWPF
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
          //  ClientSettingUpdateHelper.UpdateClientSetting();
            base.OnStartup(e);
        }
    }
}
