using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiView.Code
{
    /// <summary>
    /// 用于保存测试api的测试数据到本地文件
    /// </summary>
    [Serializable]
    public class TestData
    {
        public List<Dic> Data { get; set; }
        public string Api { get; set; }

        public void Save(TestData td)
        {
            BSF.Serialization.XmlProvider<TestData> xml = new BSF.Serialization.XmlProvider<TestData>();
            string str = xml.Serializer(td);
            System.IO.File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath("~/TestData/" + Api + ".xml"), str);

        }

        public TestData Get(string api)
        {
            BSF.Serialization.XmlProvider<TestData> xml = new BSF.Serialization.XmlProvider<TestData>();
            if (!System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/TestData/" + api + ".xml")))
                return null;
            var str = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/TestData/" + api + ".xml"));
            return xml.Deserialize(str);
        }

        public string GetData(string key)
        {
            if (Data == null)
                return "";
            foreach (var d in Data)
            {
                if (d.Key == key)
                    return d.Value;
            }
            return "";
        }

    }
    [Serializable]
    public class Dic
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}