using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.IBLL;

namespace WebApi.Controllers
{
    public partial class ExpressCompanyController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        public ExpressCompanyController()
        {

        }


        /// <summary>
        /// 获取转运仓select数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSelectViewModelList")]
        public HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList()
        {
            return _expressCompanyBLL.GetSelectViewModelList();
        }


        /// <summary>
        /// 判断该品牌能不能删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIsDeleteFlag")]
        public HttpReponseModel<int> GetIsDeleteFlag(string code)
        {
            return _expressCompanyBLL.GetIsDeleteFlag(code);
        }


    }
}
