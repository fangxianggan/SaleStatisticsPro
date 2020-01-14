using EntitiesModels.Models;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.IBLL;
using WebApi.Model;

namespace WebApi.Controllers
{
    /// <summary>
    /// 销售订单
    /// </summary>
    /// 
    [RoutePrefix("dev-api/SaleOrder")]
    public class SaleOrderController : ApiController
    {
        private readonly ISaleOrderBLL _saleOrderBLL;
        private readonly ISaleOrderInfoBLL _saleOrderInfoBLL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saleOrderBLL"></param>
        /// <param name="saleOrderInfoBLL"></param>
        public SaleOrderController(ISaleOrderBLL saleOrderBLL, ISaleOrderInfoBLL saleOrderInfoBLL)
        {
            _saleOrderBLL = saleOrderBLL;
            _saleOrderInfoBLL = saleOrderInfoBLL;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetList")]

        public async Task<HttpReponseModel<List<SaleOrder>>> GetPageList(QueryModel query)
        {
           
            return await _saleOrderBLL.GetPageListLogic(query);
        }


        // GET: api/SaleOrder/5
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sale"></param>
        public void Post(Test sale)
        {

        }

        // PUT: api/SaleOrder/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SaleOrder/5
        public void Delete(int id)
        {

        }
    }
}
