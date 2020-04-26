using EntitiesModels.DtoModels;
using EntitiesModels.HttpResponse;
using FXKJ.Infrastructure.Core.Attributes;
using FXKJ.Infrastructure.WebApi.Filter;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    
    public partial class BusinessController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        public BusinessController()
        {
            
        }


        /// <summary>
        /// 获取转运仓select数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSelectViewModelList")]
        [ActionRecord(Describe = "获取转运仓select数据")] 
        public HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList()
        {

            return _businessBLL.GetSelectViewModelList();
        }


        /// <summary>
        /// 判断该品牌能不能删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIsDeleteFlag")]
        public HttpReponseModel<int> GetIsDeleteFlag(string code)
        {
            return _businessBLL.GetIsDeleteFlag(code);
        }


    }
}
