using EntitiesModels.Models;
using FXKJ.Infrastructure.Core.Extensions;
using FXKJ.Infrastructure.Core.Util;
using FXKJ.Infrastructure.DataAccess;
using FXKJ.Infrastructure.WebApi;
using FXKJ.Infrastructure.WebApi.Filter;
using NetRedisUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    /// 

    // [RoutePrefix("dev-api/Values")]
    // [ApiAuthorizeAttribute]
    // [ApiExceptionAttribute]
    [ApiDTC]
    public class ValuesController : BaseApiController
    {
        private readonly IEFRepository<Business> _businessEFRepository;
        private readonly IEFRepository<Category> _categoryEFRepository;
        public ValuesController(IEFRepository<Business> businessEFRepository, IEFRepository<Category> categoryEFRepository)
        {
            _businessEFRepository = businessEFRepository;
            _categoryEFRepository = categoryEFRepository;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET api/values/5
        public string Get(int id)
        {
           

            Business business = new Business();
            business.BusinessCode = "111";
            business.BusinessName = "111";
            business.CreateTime = DateTime.Now;
            business.UpdateTime = DateTime.Now;
            var aa = _businessEFRepository.Add(business);

            Category category = new Category();
            category.CategoryCode = null;
            category.CategoryName = "1111";
            var bb = _categoryEFRepository.Add(category);

            //for (var i=0;i<10000;i++)
            //{
            //    DoRedisHash.SetEntryInHash("auth-token", i.ToString(), i.ToString());
            //}

            //int o = 0;
            //int d = 6/o;
            //return d.ToString();
            //var pathUrl = AppDomain.CurrentDomain.SetupInformation.ApplicationBase ;
            // return ConfigUtils.GetKey(pathUrl + "Web.config", "JWTSecretKey");
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }
    }
}
