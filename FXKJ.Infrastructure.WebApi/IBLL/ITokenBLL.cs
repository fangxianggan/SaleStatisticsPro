using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.WebApi.Models.Token;

namespace FXKJ.Infrastructure.WebApi.IBLL
{
    public partial interface ITokenBLL
    {
        /// <summary>
        /// 生成jwt
        /// </summary>
        /// <param name="authInfo"></param>
        /// <returns></returns>
        HttpReponseModel<string> GetJWTData(AuthInfo authInfo, string secretKey);

        /// <summary>
        /// 存储redis信息
        /// </summary>
        /// <param name="authInfo"></param>
        /// <returns></returns>
        HttpReponseModel<string> SetRedisToken(string key, string value);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="token"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        HttpReponseModel<AuthInfo> DecoderToken(string token, string secretKey);


        /// <summary>
        /// 验证token是不是真实的
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        HttpReponseModel<bool> VerifyRedisToken(string key, string value);

        /// <summary>
        /// 移除redis信息
        /// </summary>
        /// <param name="authInfo"></param>
        /// <returns></returns>
        HttpReponseModel<string> RemoveRedisToken(string key, string value);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="menuUrl"></param>
        /// <returns></returns>
        HttpReponseModel<bool> VerifyMenuUrl(string userName,string menuUrl);
    }
}