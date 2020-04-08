using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using EntitiesModels.HttpResponse;
using EntitiesModels.QueryModels;
using FXKJ.Infrastructure.Logic;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.IBLL;

namespace WebApi.Controllers
{


    public partial class ProductController : ApiController
    {
       



        /// <summary>
        /// 获取产品查询数据
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSelectProductDetial")]
        public HttpReponseModel<List<Product>> GetSelectProductDetial(string keyName)
        {
            return  _productBLL.GetSelectProductDetial(keyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProduct")]
        public HttpReponseModel<Product> GetProduct(string code)
        {
            return  _productBLL.GetProduct(code);
        }


       

        /// <summary>
        /// 产品列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetProductViewModelPageList")]
        public HttpReponseModel<List<ProductViewModel>> GetProductViewModelPageList(QueryModel model)
        {
            return _productBLL.GetProductViewModelPageList(model);
        }

        /// <summary>
        /// 验证输入的简码是否已经存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="simpleCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSimpleCodeIsExist")]
        public HttpReponseModel<int> GetSimpleCodeIsExist(int id, string simpleCode)
        {
            return _productBLL.GetIsExist(id,simpleCode);
        }

    }
}
