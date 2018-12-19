using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSF.BaseService.OpenApi.Attributes
{
    /// <summary>
    /// 开发接口文档描述
    /// </summary>
    public class OpenDocAttribute : System.Attribute
    {
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 方法描述
        /// </summary>
        public string MethodDescription { get; set; }
        /// <summary>
        /// 参数描述
        /// </summary>
        public string ParamDescription { get; set; }
        /// <summary>
        /// 结果描述
        /// </summary>
        public string ResultDescription { get; set; }
        /// <summary>
        /// 结果状态描述
        /// </summary>
        public string ResultStateDescription { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public double Version { get; set; }
        /// <summary>
        /// 最低兼容的版本号
        /// </summary>
        public double MinVersion { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }


        /// <summary>
        /// 开发接口文档描述
        /// </summary>
        /// <param name="methodname">接口方法名（简短）</param>
        /// <param name="methoddescription">接口方法描述</param>
        /// <param name="paramdescription">接口参数描述</param>
        /// <param name="resultdescription">返回结果描述</param>
        /// <param name="resultstatedescription">返回结果状态码描述</param>
        /// <param name="author">作者</param>
        /// <param name="version">版本号</param>
        /// <param name="minversion">最小版本号（最低兼容版本号） 低于此版本将来会报错</param>
        /// <param name="note">其他记录</param>
        public OpenDocAttribute(string methodname, string methoddescription, string paramdescription, string resultdescription, string resultstatedescription, string author, double version, double minversion, string note)
        {
            MethodName = methodname;
            MethodDescription = methoddescription;
            ParamDescription = paramdescription;
            ResultDescription = resultdescription;
            ResultStateDescription = resultstatedescription;
            Author = author;
            Version = version;
            MinVersion = minversion;
            Note = note;
        }
    }
}
