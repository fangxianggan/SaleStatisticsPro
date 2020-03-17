using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using FXKJ.Infrastructure.Logic;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.IBLL;

namespace WebApi.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public partial class CategoryController : ApiController
    {
      
        /// <summary>
        /// 
        /// </summary>
        public CategoryController()
        {
            
        }

        /// <summary>
        /// 获取分类select数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSelectViewModelList")]
        public HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList()
        {
            return  _categoryBLL.GetSelectViewModelList();
        }


        /// <summary>
        /// 判断该品牌能不能删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIsDeleteFlag")]
        public HttpReponseModel<int> GetIsDeleteFlag(string code)
        {
            return _categoryBLL.GetIsDeleteFlag(code);
        }

    }
}
