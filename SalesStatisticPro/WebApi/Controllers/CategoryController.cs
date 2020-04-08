using EntitiesModels.DtoModels;
using EntitiesModels.HttpResponse;
using System.Collections.Generic;
using System.Web.Http;

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
