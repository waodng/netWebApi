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
