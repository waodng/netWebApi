using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiView.Code
{
    public class Config
    {
        public static string DllPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["DllPath"]; }
        }
        public static string appurl
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["appurl"]; }
        }
    }
}