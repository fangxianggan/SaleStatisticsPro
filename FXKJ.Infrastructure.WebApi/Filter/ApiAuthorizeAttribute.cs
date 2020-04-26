using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Core.Util;
using System.Linq;
using EntitiesModels.HttpResponse;
using EntitiesModels.Enum;
using FXKJ.Infrastructure.Auth.Auth;
using FXKJ.Infrastructure.Auth.IBLL;
using System.Web;

namespace FXKJ.Infrastructure.WebApi.Filter
{
    /// <summary>
    /// 身份认证拦截器
    /// </summary>
    public class ApiAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {

        /// <summary>
        /// 指示指定的控件是否已获得授权
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            AuthInfoViewModel auth = FormAuthenticationExtension.CurrentAuth();
            if (auth != null)
            {
                //判断口令过期时间
                if (auth.ExpiryDateTime < DateTime.Now && auth.RefreshDateTime < DateTime.Now)
                {
                    return false;
                }
                else if (auth.ExpiryDateTime < DateTime.Now && auth.RefreshDateTime > DateTime.Now)
                {
                    //一次请求头里是不是验证过了
                    var x_token = HttpContext.Current.Request.RequestContext.RouteData.Values["X-Token"];
                    if (x_token == null)
                    {
                        var authHeader = from t in actionContext.Request.Headers where t.Key == "X-Token" select t.Value.FirstOrDefault();
                        string secretKey = ConfigUtils.GetKey(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Web.config", "JWTSecretKey");
                        string token = authHeader.FirstOrDefault();//获取token
                        var _tokenBLL = DependencyResolver.Current.GetService<ITokenBLL>();
                        //验证token是不是合法的
                        var flag = _tokenBLL.VerifyRedisToken(auth.PhoneNumber, token);
                        if (flag)
                        {

                            auth.ExpiryDateTime = DateTime.Now.AddSeconds(10);
                            auth.RefreshDateTime = DateTime.Now.AddSeconds(600);
                            //auth.ExpiryDateTime = DateTime.Now.AddMinutes(15);
                            //auth.RefreshDateTime = DateTime.Now.AddHours(3);
                            //重新生成token 
                            var fefreshToken = _tokenBLL.GetJWTData(auth, secretKey);
                            //存储redis
                            _tokenBLL.SetRedisToken(auth.PhoneNumber, fefreshToken);

                            // actionContext.RequestContext.RouteData.Values.Add("X-Token", fefreshToken);
                            //返回到页面上 并且重新赋值
                            HttpContext.Current.Request.RequestContext.RouteData.Values.Add("X-Token", fefreshToken);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else {
                return false;
            }
        }


       
        /// <summary>
        /// 处理授权失败的请求
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            HttpReponseModel httpReponse = new HttpReponseModel();
            httpReponse.Code = StatusCode.Unauthorized;
            httpReponse.Message = "未授权";
            httpReponse.Flag = true;
            httpReponse.ResultSign = ResultSign.Warning;
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, httpReponse, "application/json");
        }

        /// <summary>
        ///  为操作授权时调用
        /// </summary>
        /// <param name="actionContext"></param>
        //public override void OnAuthorization(HttpActionContext actionContext)
        //{

        //}



    }

}