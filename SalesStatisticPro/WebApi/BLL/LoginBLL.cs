using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Core.Extensions;
using FXKJ.Infrastructure.Core.Util;
using FXKJ.Infrastructure.DataAccess;
using EntitiesModels.Enum;
using EntitiesModels.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.IBLL;
using FXKJ.Infrastructure.Auth.IBLL;
using FXKJ.Infrastructure.Auth.Auth;
using FXKJ.Infrastructure.Log;
using System.Diagnostics;
using FXKJ.Infrastructure.Log.LogModel;
using FXKJ.Infrastructure.Log.Log4NetWrite;

namespace WebApi.BLL
{

    /// <summary>
    /// 
    /// </summary>
    public partial class LoginBLL : ILoginBLL
    {
       
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

      // private readonly IDapperRepository<MerchantInfo> _merchantInfoEFRepository;
        


        /// <summary>
        /// 秘钥
        /// </summary>
        private readonly string secretKey = ConfigUtils.GetKey(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Web.config", "JWTSecretKey");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenBLL"></param>
        /// <param name="merchantInfoEFRepository"></param>
        /// <param name="roleEFRepository"></param>
        /// <param name="merchantRoleEFRepository"></param>
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
        public HttpReponseModel<string> CheckLogin(LoginRequestViewModel loginRequest)
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
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var ent = _merchantInfoEFRepository.GetEntity(p => p.MerchantPhone == loginRequest.UserName);
                stopwatch.Stop();
                var ss1 = stopwatch.Elapsed.TotalSeconds;
                LogWriter.WriteLog(FolderName.Info, "f1---" + ss1.ToString());
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
                       
                        AuthInfoViewModel authInfo = new AuthInfoViewModel
                        {
                            PhoneNumber = loginRequest.UserName,
                            MerchantNo = ent.MerchantNo,
                            Roles = roles,
                            IsAdmin = isAdmin,
                            //ExpiryDateTime = DateTime.Now.AddSeconds(30),
                            //RefreshDateTime = DateTime.Now.AddSeconds(10)
                            ExpiryDateTime = DateTime.Now.AddHours(3),
                            RefreshDateTime = DateTime.Now.AddMinutes(30),

                        };
                      
                        //口令加密秘钥
                        var data = _tokenBLL.GetJWTData(authInfo, secretKey);
                        
                        if (!string.IsNullOrEmpty(data))
                        {
                            //登录日志
                            LoginLogHandler loginLog = new LoginLogHandler();
                            loginLog.WriteLog();

                            //存储redis
                             _tokenBLL.SetRedisToken(ent.MerchantPhone, data);


                            httpReponse.Code = StatusCode.OK;
                            httpReponse.Token = data;
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
        /// 获取商户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>GetMerchantInfo
        public HttpReponseModel<MerchantInfoViewModel> GetMerchantInfo(string token)
        {
            var authInfo = _tokenBLL.DecoderToken(token, secretKey);
            HttpReponseModel<MerchantInfoViewModel> httpReponse = new HttpReponseModel<MerchantInfoViewModel>();
            MerchantInfoViewModel merchantInfoView = new MerchantInfoViewModel();
            merchantInfoView.Avatar = authInfo.Avatar;
            merchantInfoView.Name = authInfo.Name;
            merchantInfoView.Roles = _merchantRoleEFRepository.GetList(p => p.MerchantNo == authInfo.MerchantNo).Select(p => p.RoleCode).ToList();
            merchantInfoView.Introduction = authInfo.Introduction;
            httpReponse.Data = merchantInfoView;
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
            var authInfo = _tokenBLL.DecoderToken(token, secretKey);
            _tokenBLL.RemoveRedisToken(authInfo.PhoneNumber, token);
            return httpReponse;
        }

        /// <summary>
        /// 商户注册
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        /// 
        public HttpReponseModel<bool> Register(RegisterViewModel register)
        {
            HttpReponseModel<bool> httpReponse = new HttpReponseModel<bool>();
            MerchantInfo merchantInfo = new MerchantInfo();
            merchantInfo.ID = Guid.NewGuid();
            merchantInfo.MerchantName = register.NickName;
            merchantInfo.MerchantNo = RandomExtension.GetRandomNumberString(new Random(), 8);
            while (IsExistMerchantNo(merchantInfo.MerchantNo).Data)
            {
               merchantInfo.MerchantNo = RandomExtension.GetRandomNumberString(new Random(), 8);
            }
            merchantInfo.MerchantPhone = register.PhoneNumber;
            merchantInfo.MerchantPassword= DEncryptUtil.Md5Encrypt(register.Password);
            httpReponse.Data= _merchantInfoEFRepository.Add(merchantInfo,true);

            MerchantRole merchantRole = new MerchantRole();
            merchantRole.MerchantNo = merchantInfo.MerchantNo;
            merchantRole.RoleCode = "merchant_1";//普通商户角色
            _merchantRoleEFRepository.Add(merchantRole,true);

            if (httpReponse.Data) {
                httpReponse.Message = "注册成功!";
            }
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