using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.WebApi.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.IBLL
{
    public  partial interface ILoginBLL
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        HttpReponseModel<string> CheckLogin(LoginRequest loginRequest);

        HttpReponseModel<AuthInfo> GetUserInfo(string token);

        HttpReponseModel<string> SignOut(string token);

        HttpReponseModel<string> Register(RegisterViewModel register);

        HttpReponseModel<bool> IsExistPhoneNumber(string phoneNumber);

        HttpReponseModel<bool> IsExistMerchantNo(string merchantNo);

    }
}