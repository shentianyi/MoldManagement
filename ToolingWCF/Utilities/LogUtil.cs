using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

[assembly:log4net.Config.XmlConfigurator(Watch=true)]
namespace ToolingWCF.Utilities
{
    public static class LogUtil
    {
        public static ILog log;
        static LogUtil()
        {
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}
