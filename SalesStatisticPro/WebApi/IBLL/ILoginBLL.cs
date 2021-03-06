﻿using EntitiesModels.DtoModels;
using EntitiesModels.HttpResponse;

namespace WebApi.IBLL
{
    public   interface ILoginBLL
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        HttpReponseModel<string> CheckLogin(LoginRequestViewModel loginRequest);

        HttpReponseModel<MerchantInfoViewModel> GetMerchantInfo(string token);

        HttpReponseModel<string> SignOut(string token);

        HttpReponseModel<bool> Register(RegisterViewModel register);

        HttpReponseModel<bool> IsExistPhoneNumber(string phoneNumber);

        HttpReponseModel<bool> IsExistMerchantNo(string merchantNo);

    }
}