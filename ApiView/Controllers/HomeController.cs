using ApiView.Code;
using BSF.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiView.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id, string category, int type = 0)
        {

            ViewBag.id = id;
            ViewBag.category = category;
            ViewBag.type = type;
            if (id != null)
            {
                var item = DllView.AllApis.Where(c => c.ID == id).SingleOrDefault();
                var o = DllView.GetOpenApiAttribute(item.Method);
                string info = @"【接口url       】<b>$apiurl</b><br/>
【接口名        】$apiname<br/>
【接口描述      】$apides<br/>
【传入描述      】$params<br/>
【输出描述      】$result<br/>
【输出状态码描述】$rstate<br/>
【作者          】$author<br/>
【版本          】$version<br/>
【最低兼容版本  】$minversion<br/>
【备注          】$note<br/>";
                ViewBag.Description = info.Replace("$apides", o.MethodDescription).Replace("$params", o.ParamDescription).Replace("$result", o.ResultDescription)
                    .Replace("$author", o.Author).Replace("$version", o.Version + "").Replace("$minversion", o.MinVersion + "").Replace("$note", o.Note + "")
                    .Replace("$apiurl", Config.appurl + item.RelateUrl() + "").Replace("$apiname", item.ToString()).Replace("$rstate", (string.IsNullOrEmpty(o.ResultStateDescription) ? "" : o.ResultStateDescription));
                ViewBag.OpenApi = item;
                ViewBag.ApiUrl = item.OpenDll.Url.Trim('/') + "/";
                ViewBag.TestData = new TestData().Get(item.ID);
            }
            return View();
        }

        public string Save(string dydapiid, string dydapiurl, string dydifsave, string dydifformatjson)
        {
            var item = DllView.AllApis.Where(c => c.ID == dydapiid).SingleOrDefault();
            List<Dic> dics = new List<Dic>();
            Dictionary<string, string> ps = new Dictionary<string, string>();
            foreach (var k in Request.Form.AllKeys)
            {
                if (k != "dydapiid" && k != "dydapiurl" && k != "dydifsave" && k != "dydifformatjson")
                {
                    ps.Add(k, Request.Form[k].ToString());
                    dics.Add(new Dic() { Key = k, Value = Request.Form[k] });
                }
            }
            if (dydifsave != null && (dydifsave == "true" || dydifsave == "on"))
            {
                TestData td = new TestData();
                td.Api = item.ID;
                td.Data = dics;

                td.Save(td);
            }
            if (dydifformatjson != null && (dydifformatjson == "true" || dydifformatjson == "on"))
            {
                Session["dydifformatjson"] = "true";
            }
            else
            {
                Session["dydifformatjson"] = "false";
            }
            string text = "访问出错！";
            try
            {
               text = new BSF.Api.HttpProvider().Post(dydapiurl + item.RelateUrl(), ps);
            }
            catch (Exception exp)
            {
                text += "error:" +exp.Message;
            }
            return text;
        }

        public ActionResult Help()
        {
            return View();
        }

        public string Reload()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory.Trim('\\') + "\\".Replace("\\bin\\", "\\");
            string webconfig = dir + "Web.config";
            System.IO.File.AppendAllText(dir + "update.txt", webconfig + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            System.IO.File.AppendAllText(webconfig, " ");
            return webconfig;
        }

        public ActionResult Search(string keyword)
        {
            ViewBag.Keyword = keyword;
            List<OpenApi> openapis = new List<OpenApi>();
            if (string.IsNullOrEmpty(keyword))
            {
                ViewBag.OpenApisResult = openapis;
                return View();
            }

            DllView.AllApis.ForEach(c =>
            {
                var o = DllView.GetOpenApiAttribute(c.Method);
                if ((ContainKeywords(o.MethodDescription, keyword))
                    || (ContainKeywords(o.MethodName, keyword))
                    || (ContainKeywords(o.ParamDescription, keyword))
                    || (ContainKeywords(o.ResultDescription, keyword))
                    || (ContainKeywords(o.ResultStateDescription, keyword))
                    || (ContainKeywords(o.Note, keyword))
                    || (ContainKeywords(o.Author, keyword))
                    || (ContainKeywords(c.RelateUrl(), keyword)))
                {
                    openapis.Add(c);
                }
            });
            ViewBag.OpenApisResult = openapis;
            return View();
        }

        private bool ContainKeywords(string content, string keywords)
        {
            var ks = keywords.Trim().Split(' ');
            foreach (var k in ks)
            {
                if (content != null && content.ToLower().Contains(k.ToLower()))
                    continue;
                else
                    return false;
            }
            return true;
        }

    }
}
