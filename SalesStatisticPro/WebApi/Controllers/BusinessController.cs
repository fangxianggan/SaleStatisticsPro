using EntitiesModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.IBLL;

namespace WebApi.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    /// 
    [RoutePrefix("dev-api/Business")]
    public class BusinessController : ApiController
    {
       
        private readonly IBusinessBLL _businessBLL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessBLL"></param>
        public BusinessController(IBusinessBLL businessBLL)
        {
            _businessBLL = businessBLL;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<HttpReponseModel<List<Business>>> GetPageList(QueryModel query)
        {
            return await _businessBLL.GetPageListLogic(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/SaleOrder/5
        public async Task<HttpReponseModel<Business>> Get(int id)
        {
            HttpReponseModel<Business> response = new HttpReponseModel<Business>();
            // response.Data= await _businessBLL.GetByIdAsync(id);
            return response;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/SaleOrder
        public async Task<HttpReponseModel<Business>> Post(Business business)
        {
            HttpReponseModel<Business> response = new HttpReponseModel<Business>();
            // response.Data = await _businessBLL.InsertAsync(business);
            return response;
        }

        // PUT: api/SaleOrder/5
        public async Task<HttpReponseModel<Business>> Put(int id, [FromBody]string value)
        {
            HttpReponseModel<Business> response = new HttpReponseModel<Business>();
            return response;
        }

        // DELETE: api/SaleOrder/5
        public async Task<HttpReponseModel<Business>> Delete(int id)
        {
            HttpReponseModel<Business> response = new HttpReponseModel<Business>();
            return response;
        }
    }
}
