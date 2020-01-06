using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Entities.Enum;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using FXKJ.Infrastructure.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.IBLL
{
    /// <summary>
    /// 销售订单的接口
    /// </summary>
    public partial interface ISaleOrderBLL 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operateType"></param>
        /// <param name="saleOrderModels"></param>
        /// <returns></returns>
        Task<HttpReponseModel<SaleOrder>> AddOrEditSaleOrderList(OperateType operateType, List<RequestSaleOrderModel> saleOrderModels);
    }
}
