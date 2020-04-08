
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
    public partial interface IPurchaseOrderInfoBLL
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        HttpReponseModel<List<PurchaseOrderInfoViewModel>> GetPurchaseOrderInfoViewModelList(string orderNumber);
    }
}

