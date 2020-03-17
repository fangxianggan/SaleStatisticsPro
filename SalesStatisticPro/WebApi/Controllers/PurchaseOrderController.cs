using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.IBLL;

namespace WebApi.Controllers
{
    /// <summary>
    /// 销售订单
    /// </summary>
    /// 

    public partial class PurchaseOrderController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IPurchaseOrderInfoBLL _purchaseOrderInfoBLL;


      




        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseOrderInfoBLL"></param>
        /// <param name="purchaseOrderBLL"></param>
        public PurchaseOrderController(IPurchaseOrderInfoBLL purchaseOrderInfoBLL, IPurchaseOrderBLL purchaseOrderBLL) 
            : this(purchaseOrderBLL)
        {
            _purchaseOrderInfoBLL = purchaseOrderInfoBLL;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPurchaseOrderPageList")]
        public  HttpReponseModel<List<PurchaseOrderViewModel>> GetPurchaseOrderPageList(QueryModel model)
        {
            return  _purchaseOrderBLL.GetPurchaseOrderPageList(model);
        }

        /// <summary>
        /// 提交一个订单详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveData")]
        public HttpReponseModel<PurchaseOrderViewModel> Post(PurchaseOrderViewModel model)
        {
            return  _purchaseOrderBLL.SaveData(model);
        }


        /// <summary>
        /// 删除一个订单详情
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDel")]
        public HttpReponseModel<int> Del(string orderNumber)
        {
            return  _purchaseOrderBLL.GetDel(orderNumber);
        }

        /// <summary>
        /// 获取一个订单详情
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPurchaseOrderInfoViewModelList")]
        public HttpReponseModel<List<PurchaseOrderInfoViewModel>> GetPurchaseOrderInfoViewModelList(string orderNumber)
        {
            return _purchaseOrderInfoBLL.GetPurchaseOrderInfoViewModelList(orderNumber);
        }

        /// <summary>
        /// 获取库存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProductStockViewModelList")]
        public HttpReponseModel<List<ProductStockViewModel>> GetProductStockViewModelList(string keyName)
        {
            return _purchaseOrderBLL.GetProductStockViewModelList(keyName);
        }


        /// <summary>
        /// 校验库存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckProductStock")]
        public HttpReponseModel<int> CheckProductStock(string productCode,int saleCount)
        {
            return _purchaseOrderBLL.CheckProductStock(productCode, saleCount);
        }

        /// <summary>
        /// 判断一个订单详情里单子是不是都已经签收了
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPurchaseOrderInfoDoSign")]
        public HttpReponseModel<int> GetPurchaseOrderInfoDoSign(string orderNumber)
        {
            return _purchaseOrderBLL.GetPurchaseOrderInfoDoSign(orderNumber);
        }

        /// <summary>
        /// 锁定这个销售订单号
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPurchaseOrderInfoLock")]
        public HttpReponseModel<int> GetPurchaseOrderInfoLock(string orderNumber)
        {
            return _purchaseOrderBLL.GetPurchaseOrderInfoLock(orderNumber);
        }

        /// <summary>
        /// 验证输入的订单号是否已经存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pOrderNum"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPOrderNumIsExist")]
        public HttpReponseModel<int> GetPOrderNumIsExist(int id, string pOrderNum)
        {
            return _purchaseOrderBLL.GetPOrderNumIsExist(id, pOrderNum);
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetPurchaseListViewModelExportExcel")]
        public HttpReponseModel<FileStreamViewModel> GetPurchaseListViewModelExportExcel(QueryModel model)
        {
            return _purchaseOrderBLL.GetPurchaseListViewModelExportExcel(model);
        }

    }
}
