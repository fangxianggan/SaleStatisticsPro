
using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.IBLL
{
    /// <summary>
    ///   业务层接口——SaleOrderInfo
    /// </summary>
    public partial interface ISaleOrderInfoBLL
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sOrderNum"></param>
        /// <returns></returns>
        HttpReponseModel<List<SaleOrderInfoViewModel>> GetSaleOrderInfoViewModelList(string sOrderNum);

    }
}

