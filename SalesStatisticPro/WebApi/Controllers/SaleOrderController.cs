using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using EntitiesModels.HttpResponse;
using EntitiesModels.QueryModels;
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

    public partial class SaleOrderController : ApiController
    {
       /// <summary>
       /// 
       /// </summary>
        private readonly ISaleOrderInfoBLL _saleOrderInfoBLL;

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="saleOrderInfoBLL"></param>
        /// <param name="saleOrderBLL"></param>
        public SaleOrderController(ISaleOrderInfoBLL saleOrderInfoBLL, ISaleOrderBLL saleOrderBLL)
            : this(saleOrderBLL)
        {
            _saleOrderInfoBLL = saleOrderInfoBLL;
        }
       

        /// <summary>
        /// 提交一个订单详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveData")]
        public HttpReponseModel<SaleOrderViewModel> Post(SaleOrderViewModel model)
        {
            return  _saleOrderBLL.SaveData(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetSaleOrderViewModelPageList")]
        public HttpReponseModel<List<SaleOrderViewModel>> GetSaleOrderViewModelPageList(QueryModel query)
        {
          return   _saleOrderBLL.GetSaleOrderViewModePageList(query);
        }

        /// <summary>
        /// 删除一个订单
        /// </summary>
        /// <param name="sOrderNum"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDel")]
        public HttpReponseModel<int> Del(string sOrderNum)
        {
            return _saleOrderBLL.GetDel(sOrderNum);
        }


        /// <summary>
        /// 获取一个订单详情
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSaleOrderInfoViewModelList")]
        public HttpReponseModel<List<SaleOrderInfoViewModel>> GetSaleOrderInfoViewModelList(string orderNumber)
        {
            return _saleOrderInfoBLL.GetSaleOrderInfoViewModelList(orderNumber);
        }

        /// <summary>
        /// 判断一个订单详情里单子是不是都已经签收了
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSaleOrderInfoDoSign")]
        public HttpReponseModel<int> GetSaleOrderInfoDoSign(string orderNumber)
        {
            return _saleOrderBLL.GetSaleOrderInfoDoSign(orderNumber);
        }

        /// <summary>
        /// 锁定这个销售订单号
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSaleOrderInfoLock")]
        public HttpReponseModel<int> GetSaleOrderInfoLock(string orderNumber)
        {
            return _saleOrderBLL.GetSaleOrderInfoLock(orderNumber);
        }


        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetSaleListViewModelExportExcel")]
        public HttpReponseModel<FileStreamViewModel> GetSaleListViewModelExportExcel(QueryModel model)
        {
            return _saleOrderBLL.GetSaleListViewModelExportExcel(model);
        }

    }
}
