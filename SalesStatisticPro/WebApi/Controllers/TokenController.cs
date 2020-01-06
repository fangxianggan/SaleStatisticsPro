using FXKJ.Infrastructure.WebApi.Models.Token;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    /// <summary>
    /// 验证token
    /// </summary>
    /// 
    [RoutePrefix("dev-api/Token")]

    //[EnableCors(origins: "http://localhost:9528/", headers: "*", methods: "GET,POST,PUT,DELETE,OPTIONS")]
    public class TokenController : ApiController
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
       
        [Route("Login")]

        public TokenInfo Login([FromBody] LoginRequest loginRequest)
        {
            TokenInfo tokenInfo = new TokenInfo();//需要返回的口令信息
            if (loginRequest != null)
            {
                string userName = loginRequest.UserName;
                string passWord = loginRequest.Password;
                bool isAdmin = (userName == "admin") ? true : false;
                //模拟数据库数据，真正的数据应该从数据库读取
                //身份验证信息
                AuthInfo authInfo = new AuthInfo { UserName = userName, Roles = new List<string> { "admin", "commonrole" }, IsAdmin = isAdmin, ExpiryDateTime = DateTime.Now.AddSeconds(1000) };
                const string secretKey = "Hello World";//口令加密秘钥
                try
                {
                    byte[] key = Encoding.UTF8.GetBytes(secretKey);
                    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//加密方式
                    IJsonSerializer serializer = new JsonNetSerializer();//序列化Json
                    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//base64加解密
                    IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);//JWT编码
                    var token = encoder.Encode(authInfo, key);//生成令牌
                    //口令信息
                    tokenInfo.Success = true;
                    tokenInfo.Token = token;
                    tokenInfo.Message = "OK";
                }
                catch (Exception ex)
                {
                    tokenInfo.Success = false;
                    tokenInfo.Message = ex.Message.ToString();
                }
            }
            else
            {
                tokenInfo.Success = false;
                tokenInfo.Message = "用户信息为空";
            }
            
            return tokenInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("Info")]
        public AuthInfo GetAuthInfo(string token)
        {
            const string secretKey = "Hello World";//加密秘钥
            byte[] key = Encoding.UTF8.GetBytes(secretKey);
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            //解密
            var json = decoder.DecodeToObject<AuthInfo>(token, key, verify: true);
            return json;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [Route("Logout")]
        public void Logout(string token)
        {
             
        }
    }
}
