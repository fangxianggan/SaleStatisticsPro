using EntitiesModels.DtoModels;
using EntitiesModels.HttpResponse;
using FXKJ.Infrastructure.WebApi.Filter;
using System.Web.Http;
using WebApi.IBLL;

namespace WebApi.Controllers
{
    /// <summary>
    /// 验证token
    /// </summary>
    /// 
    [RoutePrefix("dev-api/Token")]

    //[EnableCors(origins: "http://localhost:9528/", headers: "*", methods: "GET,POST,PUT,DELETE,OPTIONS")]
   // [ApiException]
    public class TokenController : ApiController
    {
        private readonly ILoginBLL _loginBLL;
        public TokenController(ILoginBLL loginBLL)
        {
            _loginBLL = loginBLL;
        }

        #region  登录


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public HttpReponseModel<string> Login([FromBody] LoginRequestViewModel loginRequest)
        {
            return _loginBLL.CheckLogin(loginRequest);
        }

        /// <summary>
        /// 获取商户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMerchantInfo")]
        public HttpReponseModel<MerchantInfoViewModel> GetMerchantInfo([FromBody] string token)
        {
            return _loginBLL.GetMerchantInfo(token);
        }

        /// <summary>
        /// 登出   单个参数 post 传递接受  
        /// </summary>
        [HttpPost]
        [Route("Logout")]
        public HttpReponseModel<string> Logout([FromBody]string token)
        {
            return _loginBLL.SignOut(token);
        }


        #endregion

        #region 注册
        [HttpPost]
        [Route("Register")]
        [ApiDTC]
        public HttpReponseModel<bool> Register(RegisterViewModel register)
        {
            return _loginBLL.Register(register);
        }

        /// <summary>
        /// 验证手机号是否已经存在
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("IsExistPhoneNumber")]
        public HttpReponseModel<bool> IsExistPhoneNumber(string phoneNumber)
        {
            return _loginBLL.IsExistPhoneNumber(phoneNumber);
        }
        #endregion

    }
}
