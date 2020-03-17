
using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.IBLL
{
    /// <summary>
    ///   业务层接口——SaleOrder
    /// </summary>
    public partial interface ISaleOrderBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        HttpReponseModel<SaleOrderViewModel> SaveData(SaleOrderViewModel model);

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        HttpReponseModel<List<SaleOrderViewModel>> GetSaleOrderViewModePageList(QueryModel query);

        /// <summary>
        /// /
        /// </summary>
        /// <param name="sOrderNum"></param>
        /// <returns></returns>
        HttpReponseModel<int> GetDel(string sOrderNum);

        /// <summary>
        /// 订单详情是不是已经签收
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        HttpReponseModel<int> GetSaleOrderInfoDoSign(string orderNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        HttpReponseModel<int> GetSaleOrderInfoLock(string orderNumber);

        HttpReponseModel<FileStreamViewModel> GetSaleListViewModelExportExcel(QueryModel model);
    }
}

