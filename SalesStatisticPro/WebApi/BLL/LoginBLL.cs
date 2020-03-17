using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Core.Extensions;
using FXKJ.Infrastructure.Core.Util;
using FXKJ.Infrastructure.DataAccess;
using FXKJ.Infrastructure.Entities.Enum;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.WebApi.IBLL;
using FXKJ.Infrastructure.WebApi.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.IBLL;

namespace WebApi.BLL
{
    public partial class LoginBLL : ILoginBLL
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEFRepository<MerchantInfo> _merchantInfoEFRepository;
        /// <summary>
        /// 
        /// </summary>
        private readonly IEFRepository<Role> _roleEFRepository;
        /// <summary>
        /// 
        /// </summary>
        private readonly IEFRepository<MerchantRole> _merchantRoleEFRepository;

        private readonly ITokenBLL _tokenBLL;

        /// <summary>
        /// 秘钥
        /// </summary>
        private readonly string secretKey = ConfigUtils.GetKey(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Web.config", "JWTSecretKey");

        public LoginBLL(ITokenBLL tokenBLL, IEFRepository<MerchantInfo> merchantInfoEFRepository, IEFRepository<Role> roleEFRepository, IEFRepository<MerchantRole> merchantRoleEFRepository)
        {
            _merchantInfoEFRepository = merchantInfoEFRepository;
            _roleEFRepository = roleEFRepository;
            _merchantRoleEFRepository = merchantRoleEFRepository;
            _tokenBLL = tokenBLL;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public HttpReponseModel<string> CheckLogin(LoginRequest loginRequest)
        {
            HttpReponseModel<string> httpReponse = new HttpReponseModel<string>();
            if (loginRequest.UserName.IsNullOrEmpty())
            {
                httpReponse.ResultSign = ResultSign.Warning;
                httpReponse.Message = "用户名不能为空";
            }
            else if (loginRequest.Password.IsNullOrEmpty())
            {
                httpReponse.ResultSign = ResultSign.Warning;
                httpReponse.Message = "密码不能为空";
            }
            else
            {
                var ent = _merchantInfoEFRepository.GetEntity(p => p.MerchantPhone == loginRequest.UserName || p.MerchantNo == loginRequest.UserName);
                if (ent == null)
                {
                    httpReponse.ResultSign = ResultSign.Warning;
                    httpReponse.Message = "用户名不存在";
                }
                else
                {
                    var password = DEncryptUtil.Md5Encrypt(loginRequest.Password);
                    if (ent.MerchantPassword == password)
                    {
                        List<string> roles = _merchantRoleEFRepository.GetList(p => p.MerchantNo == ent.MerchantNo).Select(p => p.RoleCode).ToList();
                        var isAdmin = roles.Where(p => p == "admin").Count() > 0 ? true : false;
                        AuthInfo authInfo = new AuthInfo
                        {
                            UserName = loginRequest.UserName,
                            Roles = roles,
                            IsAdmin = isAdmin,
                            ExpiryDateTime = DateTime.Now.AddMinutes(20),
                            RefreshDateTime = DateTime.Now.AddHours(3),
                            ReturnUrl = loginRequest.ReturnUrl
                        };
                        //口令加密秘钥
                        var data = _tokenBLL.GetJWTData(authInfo, secretKey);
                        if (data.Flag)
                        {
                            //存储redis
                            _tokenBLL.SetRedisToken(ent.MerchantPhone, data.Data);
                            httpReponse.Code = StatusCode.OK;
                            httpReponse.Token = data.Data;
                            httpReponse.ResultSign = ResultSign.Successful;
                            httpReponse.Data = "";
                        }
                        else
                        {
                            httpReponse.Code = StatusCode.InternalServerError;
                            httpReponse.Token = "";
                            httpReponse.ResultSign = ResultSign.Error;
                        }
                    }
                    else
                    {
                        httpReponse.ResultSign = ResultSign.Warning;
                        httpReponse.Message = "密码输入错误";
                    }
                }

            }
            return httpReponse;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public HttpReponseModel<AuthInfo> GetUserInfo(string token)
        {
            HttpReponseModel<AuthInfo> httpReponse = new HttpReponseModel<AuthInfo>();
            httpReponse.Data = _tokenBLL.DecoderToken(token, secretKey).Data;
            return httpReponse;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public HttpReponseModel<string> SignOut(string token)
        {
            HttpReponseModel<string> httpReponse = new HttpReponseModel<string>();
            //口令加密秘钥
            string secretKey = ConfigUtils.GetKey(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Web.config", "JWTSecretKey");
            var authInfo = _tokenBLL.DecoderToken(token, secretKey).Data;
            httpReponse = _tokenBLL.RemoveRedisToken(authInfo.UserName, token);
            return httpReponse;
        }

        /// <summary>
        /// 商户注册
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        /// 
        public HttpReponseModel<string> Register(RegisterViewModel register)
        {
            HttpReponseModel<string> httpReponse = new HttpReponseModel<string>();
            MerchantInfo merchantInfo = new MerchantInfo();
            merchantInfo.ID = Guid.NewGuid();
            merchantInfo.MerchantName = "";
            merchantInfo.MerchantNo = RandomExtension.GetRandomNumberString(new Random(), 8);
            while (IsExistMerchantNo(merchantInfo.MerchantNo).Data)
            {
               merchantInfo.MerchantNo = RandomExtension.GetRandomNumberString(new Random(), 8);
            }
            merchantInfo.MerchantPhone = register.PhoneNumber;
            merchantInfo.MerchantPassword= DEncryptUtil.Md5Encrypt(register.Password);
            _merchantInfoEFRepository.Add(merchantInfo);
            return httpReponse;
        }

        /// <summary>
        /// 判断手机号是不是已经存在
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public HttpReponseModel<bool> IsExistPhoneNumber(string phoneNumber)
        {
            HttpReponseModel<bool> httpReponse = new HttpReponseModel<bool>();
            var list = _merchantInfoEFRepository.GetList(p => p.MerchantPhone == phoneNumber);
            if (list.Count() > 0)
            {
                httpReponse.Data = true;
            }
            else
            {
                httpReponse.Data = false;
            }
            return httpReponse;
        }


        public HttpReponseModel<bool> IsExistMerchantNo(string merchantNo)
        {
            HttpReponseModel<bool> httpReponse = new HttpReponseModel<bool>();
            var list = _merchantInfoEFRepository.GetList(p => p.MerchantNo == merchantNo);
            if (list.Count() > 0)
            {
                httpReponse.Data = true;
            }
            else
            {
                httpReponse.Data = false;
            }
            return httpReponse;
        }
    }
}