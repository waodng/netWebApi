using BSF.BaseService.OpenApi.Attributes;
using BSF.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ApiView.Code
{
    public class OpenApi
    {
        public OpenApi() { }
        public MethodInfo Method;
        public OpenDllInfo OpenDll { get; set; }
        public override string ToString()
        {
            return DllView.GetOpenApiAttribute(Method).MethodName;
        }

        public string RelateUrl()
        {
            return Method.DeclaringType.Name.Replace("Controller", "") + "/" + Method.Name;
        }

        public string ID
        {
            get { return Method.DeclaringType.FullName + "." + Method.Name; }
        }

    }

    public class OpenDllInfo
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public OpenDllInfo() { }
        public OpenDllInfo(string infostr)
        {
            string[] ss = infostr.Trim(';').Split(';');
            Path = ss[0];
            Name = ss[1];
            Url = ss[2];
        }
    }
    public class DllView
    {
        public static List<OpenApi> AllApis = new List<OpenApi>();
        public static List<OpenDllInfo> AllDllInfo = new List<OpenDllInfo>();
        //public static List<OpenDllInfo> OldAllDllInfo = new List<OpenDllInfo>();
        public static List<OpenApi> OldAllApis = new List<OpenApi>();
        public static DateTime lastupdatetime = new DateTime();
        private static object _lock = new object();

        public static void Load()
        {
            lock (_lock)
            {
                foreach (var p in DllPaths())
                {
                    string newdlldir = AppDomain.CurrentDomain.BaseDirectory.Trim('\\') + "\\dll文档\\";
                    //当前文件夹
                    string dlldir = System.IO.Path.GetDirectoryName(p.Path);
                    string dllfilename = System.IO.Path.GetFileName(p.Path);
                    //拷贝文件夹
                    string copydlldir = newdlldir + p.Name + "\\";
                    string copydllpath = copydlldir + dllfilename;
                    //目录拷贝
                    IOHelper.CopyDirectory(dlldir, copydlldir);
                    //路径指向当前拷贝文件夹
                    p.Path = copydllpath;

                    AllApis.AddRange(GetApis(p));
                }
                lastupdatetime = DateTime.Now;

                //foreach (var p in DllPaths())
                //{
                //    string newdlldir = AppDomain.CurrentDomain.BaseDirectory.Trim('\\') + "\\旧版本dll文档\\";
                //    //当前文件夹
                //    string dlldir = System.IO.Path.GetDirectoryName(p.Path);
                //    string dllfilename = System.IO.Path.GetFileName(p.Path);
                //    //拷贝文件夹
                //    string copydlldir = newdlldir + p.Name + "\\";
                //    string copydllpath = copydlldir + dllfilename;

                //    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(newdlldir);
                //    if (!dir.Exists || (DateTime.Now - dir.CreationTime) > TimeSpan.FromDays(7))
                //    {
                //        //目录拷贝
                //        IOHelper.CopyDirectory(dlldir, copydlldir);
                //    }

                //    //路径指向当前拷贝文件夹
                //    p.Path = copydllpath;

                //    OldAllApis.AddRange(GetApis(p));
                //}
            }
        }

        public static List<OpenDllInfo> DllPaths()
        {
            if (AllDllInfo == null || AllDllInfo.Count == 0)
            {
                var rs = new List<OpenDllInfo>();
                foreach (var r in System.Configuration.ConfigurationManager.AppSettings.AllKeys)
                {
                    if (r.Contains("DllPath"))
                    {
                        var dllinfo = new OpenDllInfo(System.Configuration.ConfigurationManager.AppSettings[r] as string);

                        rs.Add(dllinfo);
                    }
                }
                AllDllInfo = rs;
            }
            return AllDllInfo;
        }

        public static List<OpenApi> GetApis(OpenDllInfo info)
        {
            //AppDomain ad = AppDomain.CreateDomain(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            var assembly = Assembly.LoadFrom(info.Path);
            var rs = new List<OpenApi>();
            foreach (var t in (assembly.GetTypes()))
            {
                foreach (var m in t.GetMethods())
                {
                    var find = GetOpenApiAttribute(m);
                    if (find != null)
                        rs.Add(new OpenApi() { Method = m, OpenDll = info });

                }
            }
            return rs;
        }

        public static OpenDocAttribute GetOpenApiAttribute(MethodInfo m)
        {
            return (from a in m.GetCustomAttributes(false) where a is BSF.BaseService.OpenApi.Attributes.OpenDocAttribute select a as OpenDocAttribute).SingleOrDefault();
        }

        public static bool IfNewVersion(OpenApi api)
        {
            var newapi = (from o in AllApis where o.ID == api.ID select o).SingleOrDefault();
            var oldapi = (from o in OldAllApis where o.ID == api.ID select o).SingleOrDefault();
            if (oldapi == null)
                return true;
            var newm = DllView.GetOpenApiAttribute(newapi.Method);
            var oldm = DllView.GetOpenApiAttribute(oldapi.Method);
            if (newm.MethodDescription != oldm.MethodDescription
                   || newm.MethodName != oldm.MethodName
                   || newm.ParamDescription != oldm.ParamDescription
                   || newm.ResultDescription != oldm.ResultDescription
                   || newm.ResultStateDescription != oldm.ResultStateDescription
                   || newm.Note != oldm.Note
                   || newm.Author != oldm.Author)
            {
                return true;
            }
            return false;
        }
    }
}