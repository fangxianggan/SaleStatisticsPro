using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System.Text;
using FXKJ.Infrastructure.Auth.Auth;
using NetRedisUtil;
using FXKJ.Infrastructure.Auth.IBLL;
using System;

namespace FXKJ.Infrastructure.Auth.BLL
{
    public partial class TokenBLL : ITokenBLL
    {
        private readonly DoRedisList doRedisList;

        private readonly DoRedisHash doRedisHash;
        public TokenBLL()
        {
            doRedisList = new DoRedisList();
            doRedisHash = new DoRedisHash();
        }
        public string GetJWTData(AuthInfoViewModel authInfo, string secretKey)
        {
            byte[] key = Encoding.UTF8.GetBytes(secretKey);
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//加密方式
            IJsonSerializer serializer = new JsonNetSerializer();//序列化Json
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//base64加解密
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);//JWT编码
            var token = encoder.Encode(authInfo, key);//生成令牌//口令信息
            return token;
        }

        public bool SetRedisToken(string key, string value)
        {
            return doRedisHash.SetEntryInHash("auth-token", key, value);
        }

        public AuthInfoViewModel DecoderToken(string token, string secretKey)
        {
            byte[] key = Encoding.UTF8.GetBytes(secretKey);
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            //解密
            var json = decoder.DecodeToObject<AuthInfoViewModel>(token, key, verify: true);
            return json;
        }

        public bool VerifyRedisToken(string key, string value)
        {
            var flag = false;
            string token = doRedisHash.GetValueFromHash("auth-token", key);
            if (token == value)
            {
                flag = true;
            }
            return flag;
        }

        public bool RemoveRedisToken(string key, string value)
        {
            return doRedisHash.RemoveEntryFromHash("auth-token", key);
        }

        public void SetListTempToken(string key, string value, TimeSpan sp)
        {
            doRedisList.LPush(key, value, sp);
        }

        public bool GetListTempToken(string key, string value)
        {
            var flag = false;
            var list = doRedisList.Get(key);
            foreach (var item in list)
            {
                if (item == value)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

    }
}