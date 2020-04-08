
using EntitiesModels.DtoModels;
using EntitiesModels.HttpResponse;
using EntitiesModels.QueryModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.IBLL
{
    /// <summary>
    ///   业务层接口——PurchaseOrder
    /// </summary>
    public partial interface IPurchaseOrderBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        HttpReponseModel<PurchaseOrderViewModel> SaveData(PurchaseOrderViewModel model);

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        HttpReponseModel<int> GetDel(string orderNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        HttpReponseModel<List<PurchaseOrderViewModel>> GetPurchaseOrderPageList(QueryModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        HttpReponseModel<List<ProductStockViewModel>> GetProductStockViewModelList(string keyName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="saleCount"></param>
        /// <returns></returns>
        HttpReponseModel<int> CheckProductStock(string productCode, int saleCount);

        HttpReponseModel<int> GetPurchaseOrderInfoDoSign(string orderNumber);
        HttpReponseModel<int> GetPurchaseOrderInfoLock(string orderNumber);

        HttpReponseModel<int> GetPOrderNumIsExist(int id, string pOrderNum);


        HttpReponseModel<FileStreamViewModel> GetPurchaseListViewModelExportExcel(QueryModel model);



    }
}

