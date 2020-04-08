
using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using EntitiesModels.HttpResponse;
using FXKJ.Infrastructure.WebApi.Filter;
using System.Collections.Generic;
using System.Web.Http;
namespace WebApi.Controllers

{
    /// <summary>
    /// UserInfo 控制器api 代码自动生成
    /// </summary>
    [ApiAuthorize]
    public partial class UserInfoController : ApiController
    {

       
        /// <summary>
        /// 构造器 依赖注入
        /// </summary>
        /// <param name=""></param>
        public UserInfoController()
        {
           
        }



        /// <summary>
        /// 获取select数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSelectList")]
        public HttpReponseModel<List<SelectViewModel>> GetSelectList()
        {
            return _userInfoBLL.GetSelectList();
        }


        /// <summary>
        /// 获取select数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserInfoList")]
        public HttpReponseModel<List<UserInfo>> GetUserInfoList(string keyName)
        {
            return _userInfoBLL.GetUserInfoList(keyName);
        }

        /// <summary>
        /// 判断该供应商能不能删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIsDeleteFlag")]
        public HttpReponseModel<int> GetIsDeleteFlag(string phoneNumber)
        {
            return _userInfoBLL.GetIsDeleteFlag(phoneNumber);
        }


    }
}

