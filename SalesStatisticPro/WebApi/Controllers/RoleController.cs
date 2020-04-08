using EntitiesModels.HttpResponse;
using System.Web.Http;

namespace WebApi.Controllers

{
    /// <summary>
    /// Role 控制器api 代码自动生成
    /// </summary>

    public partial class RoleController : ApiController
    { 
       
        /// <summary>
        /// 构造器 依赖注入
        /// </summary>
        /// <param name="roleBLL"></param>
        public RoleController()
        {
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIsDeleteFlag")]
        public HttpReponseModel<bool> GetIsDeleteFlag(string roleCode)
        {
            return _roleBLL.GetIsDeleteFlag(roleCode);
        }
        

    }
}

