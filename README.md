# ApiView #

这个项目是从一个开源的项目apiView中进行的第二次迭代开发的，根据自己webApi需求改写的
供自己开发学习用，请大家支持原创。。。


net api的接口文档查看网站,用于解决分布式开发过程中的Api接口管理和沟通问题。<br/><br/>
- 自动生成api文档；<br/>
- 方便api调试及第三方开发人员对接，可以应用在asp.net mvc，wcf，webservice 中使用；<br/>
- 代码及原理都很简单，方便二次开发和完善。<br/><br/>

## 安装包 ##
使用git下载项目并打开目录 “\安装包\” 可直接安装使用

## 使用Demo示例 ##
<pre class="brush:c#;toolbar: true; auto-links: false;">
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BSF.BaseService.OpenApi.Attributes;
using BSF.Extensions;
using BSF.Api;

namespace ApiViewTestWeb.Controllers
{
public class DemoController:Controller
{
//
// GET: /Demo/

[OpenDoc("Index","test method","参数描述","结果描述","结果状态描述","作者",1.0,1.0,"备注")]
public string Index(int a,string b,float d)
{
return a+b.NullToEmpty()+d;
}

[OpenDoc("某一店铺的商品列表V15", "某一店铺的商品列表V15",
@"token:token(可选参数),
商户账号:shopid,
类目id:categoryid int?,
关键词:keyword,
条形码: barcode,
显示数量:pageSize ,
页码:pageIndex",
@"活动序号(Int):hdxh,
商品条码(string):sptm,
商品名称(string):spmc,
规格型号(string):ggxh,
计量单位(string):jldw,
商品图片(string):sptp,
图片修改时间(string):sptpxgsj,
原零售价(Decimal):original_lsj,
零售价(Decimal):lsj,
商品销量(Decimal):spxl,
是否活动商品(int):isActivity,
活动总数量(Decimal):hdzsl,
每人限购数量(Decimal):mrxgsl,
", "-5缺少参数", "车江毅", 1.5, 1.5, "")]
public ActionResult List(string shopid, int? categoryid, string keyword, string barcode, long? pageSize, long? pageIndex)
{
return Json( new ServiceResult() { code=1, data="测试数据", msg="成功", total=1 });
}
}
}
</pre>
## OpenApi使用说明文档 ##

1.引用BSF.dll和BSF.BaseService.OpenApi.dll<br/>
2.在MVC网站Controller下面的接口方法处添加OpenDocAttribute特性注释即可。<br/>
3.参考ApiViewTestWeb使用方式中的DemoController。<br/>
4.在AppView站点中发布OpenApi的信息。<br/>
5.点击AppView站点“帮助文档”中“重新加载”按钮生效。<br/>
  

<p><strong><span style="font-size:14px">部分截图</span></strong></p>

<p><strong><span style="font-size:14px"><img alt="" height="246" src="http://static.oschina.net/uploads/space/2016/0602/131829_cIJW_2379842.png" width="500" /></span></strong></p>

<p><img alt="" height="235" src="http://static.oschina.net/uploads/space/2016/0602/131917_G42x_2379842.png" width="500" /></p>

<p><img alt="" height="328" src="http://static.oschina.net/uploads/space/2016/0602/131944_ijJ1_2379842.png" width="500" /></p>

开源QQ群: .net 开源基础服务  **238543768**<br/>
*随手之作，希望对大家有帮助，如果有兴趣可以继续完善。<br/>*
by 车江毅<br/>
