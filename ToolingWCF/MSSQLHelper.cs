using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolingWCF.Utilities;
using ClassLibrary.Data;

namespace ToolingWCF
{
    public class MSSqlHelper
    {
        private static string host;
        private static string db;
        private static string user;
        private static string pass;

        private static string connstr;

        public static string Connstr
        {
            get
            {
                if (connstr == null)
                {
                    MSSqlHelper.InitConfig();
                    MSSqlHelper.connstr = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                        MSSqlHelper.host,
                        MSSqlHelper.db,
                        MSSqlHelper.user,
                        MSSqlHelper.pass);
                }
                return MSSqlHelper.connstr;
            }
        }

        private static void InitConfig()
        {
            ConfigUtil config = new ConfigUtil("ConnectionString", "conn.ini");
            MSSqlHelper.host = config.Get("Host");
            MSSqlHelper.db = config.Get("DB");
            MSSqlHelper.user = config.Get("User");
            MSSqlHelper.pass = config.Get("Pass");
        }

        public static ToolManDataContext DataContext()
        {
            return new ToolManDataContext(MSSqlHelper.Connstr);
        }
    }
}
