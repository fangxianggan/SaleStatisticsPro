﻿using FXKJ.Infrastructure.Core.Util;
using FXKJ.Infrastructure.Entities.Enum;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.WebApi.IBLL;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Http;

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
            //注入
            var _tokenBLL = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(ITokenBLL)) as ITokenBLL;
            if (_tokenBLL != null)
            {
                //前端请求api时会将token存放在名为"auth"的请求头中
                var authHeader = from t in actionContext.Request.Headers where t.Key == "X-Token" select t.Value.FirstOrDefault();
                if (authHeader != null)
                {
                    //口令加密秘钥
                    string secretKey = ConfigUtils.GetKey(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Web.config", "JWTSecretKey");
                    string token = authHeader.FirstOrDefault();//获取token
                    if (!string.IsNullOrEmpty(token))
                    {
                        try
                        {
                            var req = _tokenBLL.DecoderToken(token, secretKey);
                            if (req.Flag)
                            {
                                var json = req.Data;
                                if (json != null)
                                {
                                    //判断口令过期时间
                                    if (json.ExpiryDateTime < DateTime.Now && json.RefreshDateTime < DateTime.Now)
                                    {
                                        return false;
                                    }
                                    else if (json.ExpiryDateTime < DateTime.Now && json.RefreshDateTime > DateTime.Now)
                                    {
                                        //验证token是不是合法的
                                        var flag = _tokenBLL.VerifyRedisToken(json.UserName, token).Data;
                                        if (flag)
                                        {
                                            json.ExpiryDateTime = DateTime.Now.AddMinutes(15);
                                            json.RefreshDateTime = DateTime.Now.AddHours(3);
                                            //重新生成token 
                                            var fefreshToken = _tokenBLL.GetJWTData(json, secretKey).Data;
                                            //存储redis
                                            _tokenBLL.SetRedisToken(json.UserName, fefreshToken);
                                            //返回到页面上 并且重新赋值
                                            actionContext.RequestContext.RouteData.Values.Add("X-Token", fefreshToken);
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {

                                        return true;
                                    }
                                }
                            }
                            return false;
                        }
                        catch (Exception ex)
                        {
                            return false;
                        }
                    }
                }
                return false;
            }
            else
            {
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