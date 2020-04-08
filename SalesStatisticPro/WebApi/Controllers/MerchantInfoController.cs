
using EntitiesModels.HttpResponse;
using System.Web.Http;
using WebApi.IBLL;
using FXKJ.Infrastructure.WebApi.Filter;
using EntitiesModels.DtoModels;

namespace WebApi.Controllers

{
    /// <summary>
    /// MerchantInfo 控制器api 代码自动生成
    /// </summary>

    public partial class MerchantInfoController : ApiController
    {
        private readonly IMerchantRoleBLL _merchantRoleBLL;
        /// <summary>
        /// 构造器 依赖注入
        /// </summary>
        /// <param name="merchantInfoBLL"></param>
        public MerchantInfoController(IMerchantRoleBLL merchantRoleBLL, IMerchantInfoBLL merchantInfoBLL) : this(merchantInfoBLL)
        {
            _merchantRoleBLL = merchantRoleBLL;
        }



        /// <summary>
        /// 设置商户角色权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ApiDTC]
        [HttpPost]
        [Route("SetMerchantRolePermission")]
        public HttpReponseModel<bool> SetMerchantRolePermission(MerchantRoleViewModel model)
        {
            return _merchantRoleBLL.SetMerchantRolePermission(model);
        }


        /// <summary>
        /// 获取商户的角色
        /// </summary>
        /// <param name="merchantNo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRoleMenuPermission")]
        public HttpReponseModel<string[]> GetMerchantRolePermission([FromBody]string merchantNo)
        {
            return _merchantRoleBLL.GetMerchantRolePermission(merchantNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantNo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRoleTransferData")]
        public HttpReponseModel<SetTransferViewModel> GetRoleTransferData([FromBody]string merchantNo)
        {
            return _merchantRoleBLL.GetRoleTransferData(merchantNo);
        }

    }
}

