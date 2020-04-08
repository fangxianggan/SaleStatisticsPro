using FXKJ.Infrastructure.Auth.Auth;

namespace FXKJ.Infrastructure.Auth.IBLL
{
    public partial interface ITokenBLL
    {
        /// <summary>
        /// 生成jwt
        /// </summary>
        /// <param name="authInfo"></param>
        /// <returns></returns>
        string GetJWTData(AuthInfoViewModel authInfo, string secretKey);

        /// <summary>
        /// 存储redis信息
        /// </summary>
        /// <param name="authInfo"></param>
        /// <returns></returns>
        bool SetRedisToken(string key, string value);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="token"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        AuthInfoViewModel DecoderToken(string token, string secretKey);


        /// <summary>
        /// 验证token是不是真实的
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool VerifyRedisToken(string key, string value);

        /// <summary>
        /// 移除redis信息
        /// </summary>
        /// <param name="authInfo"></param>
        /// <returns></returns>
        bool RemoveRedisToken(string key, string value);


        
    }
}