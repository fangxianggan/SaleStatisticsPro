using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Entities.Enum;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using FXKJ.Infrastructure.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using WebApi.IBLL;
using WebApi.IRepository;

namespace WebApi.BLL
{
    /// <summary>
    /// 销售订单 处理复杂的业务逻辑
    /// </summary>
    public partial class SaleOrderBLL : AsyncLogic<SaleOrder>, ISaleOrderBLL
    {
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly ISaleOrderInfoRepository _saleOrderInfoRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saleOrderRepository"></param>
        /// <param name="saleOrderInfoRepository"></param>
        public SaleOrderBLL(ISaleOrderRepository saleOrderRepository, ISaleOrderInfoRepository saleOrderInfoRepository)
        {
            _saleOrderRepository = saleOrderRepository;
            _saleOrderInfoRepository = saleOrderInfoRepository;

        }

        /// <summary>
        /// 添加或是修改购买订单  批量
        /// </summary>
        /// <returns></returns>
        public async Task<HttpReponseModel<SaleOrder>> AddOrEditSaleOrderList(OperateType operateType, List<RequestSaleOrderModel> saleOrderModels)
        {
            HttpReponseModel<SaleOrder> httpReponse = new HttpReponseModel<SaleOrder>();
            if (operateType == OperateType.Add)
            {
                foreach (var item in saleOrderModels)
                {
                    var ent = await _saleOrderRepository.Add(item.saleOrder);
                    item.saleOrderInfos.ForEach(p => p.ID = ent.ID);
                    await _saleOrderInfoRepository.AddList(item.saleOrderInfos);
                }
            }
            else
            {

            }

            return httpReponse;
        }

    }
}