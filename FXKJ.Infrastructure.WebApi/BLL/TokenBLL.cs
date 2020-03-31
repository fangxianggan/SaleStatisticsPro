using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.WebApi.Models.Token;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using NetRedisUtil;
using System;
using System.Text;
using FXKJ.Infrastructure.WebApi.IBLL;
using FXKJ.Infrastructure.DataAccess;
using EntitiesModels.Models;
using System.Collections.Generic;
using System.Linq;

namespace FXKJ.Infrastructure.WebApi.BLL
{
    public partial class TokenBLL : ITokenBLL
    {

        private readonly IEFRepository<MerchantInfo> _merchantInfoEFRepository;

        private readonly IEFRepository<Menu> _menuEFRepository;

        private readonly IEFRepository<Role> _roleEFRepository;

        private readonly IEFRepository<RoleMenu> _roleMenuEFRepository;

        private readonly IEFRepository<MerchantRole> _merchantRoleEFRepository;
        public TokenBLL(IEFRepository<MerchantInfo> merchantInfoEFRepository, IEFRepository<RoleMenu> roleMenuEFRepository, IEFRepository<Menu> menuEFRepository, IEFRepository<MerchantRole> merchantRoleEFRepository)
        {
            _menuEFRepository = menuEFRepository;
            _roleMenuEFRepository = roleMenuEFRepository;
            _merchantRoleEFRepository = merchantRoleEFRepository;
            _merchantInfoEFRepository = merchantInfoEFRepository;
        }
        public HttpReponseModel<string> GetJWTData(AuthInfo authInfo, string secretKey)
        {
            HttpReponseModel<string> httpReponse = new HttpReponseModel<string>();
            try
            {
                byte[] key = Encoding.UTF8.GetBytes(secretKey);
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//加密方式
                IJsonSerializer serializer = new JsonNetSerializer();//序列化Json
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//base64加解密
                IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);//JWT编码
                var token = encoder.Encode(authInfo, key);//生成令牌//口令信息
                httpReponse.Flag = true;
                httpReponse.Data = token;
                httpReponse.Message = "OK";
            }
            catch (Exception ex)
            {
                httpReponse.Flag = false;
                httpReponse.Data = "";
                httpReponse.Message = ex.Message.ToString();
            }
            return httpReponse;
        }

        public HttpReponseModel<string> SetRedisToken(string key, string value)
        {
            HttpReponseModel<string> httpReponse = new HttpReponseModel<string>();
            httpReponse.Flag = DoRedisHash.SetEntryInHash("auth-token", key, value);
            return httpReponse;
        }

        public HttpReponseModel<AuthInfo> DecoderToken(string token, string secretKey)
        {
            HttpReponseModel<AuthInfo> httpReponse = new HttpReponseModel<AuthInfo>();
            try
            {
                byte[] key = Encoding.UTF8.GetBytes(secretKey);
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                //解密
                var json = decoder.DecodeToObject<AuthInfo>(token, key, verify: true);
                httpReponse.Flag = true;
                httpReponse.Data = json;
                httpReponse.Message = "OK";
            }
            catch (Exception ex)
            {
                httpReponse.Flag = false;
                httpReponse.Data = null;
                httpReponse.Message = ex.Message.ToString();
            }
            return httpReponse;
        }

        public HttpReponseModel<bool> VerifyRedisToken(string key, string value)
        {
            HttpReponseModel<bool> httpReponse = new HttpReponseModel<bool>();
            string token = DoRedisHash.GetValueFromHash("auth-token", key);
            if (token == value)
            {
                httpReponse.Data = true;
            }
            else
            {
                httpReponse.Data = false;
            }
            return httpReponse;
        }

        public HttpReponseModel<string> RemoveRedisToken(string key, string value)
        {
            HttpReponseModel<string> httpReponse = new HttpReponseModel<string>();
            httpReponse.Flag = DoRedisHash.RemoveEntryFromHash("auth-token", key);
            return httpReponse;
        }

        public HttpReponseModel<bool> VerifyMenuUrl(string userName, string menuUrl)
        {
            HttpReponseModel<bool> httpReponse = new HttpReponseModel<bool>();
            string num = _merchantInfoEFRepository.GetEntity(p => p.MerchantPhone == userName).MerchantNo;
            List<string> roleCodeList = _merchantRoleEFRepository.GetList(p => p.MerchantNo == num).Select(p => p.RoleCode).Distinct().ToList();
            List<int> menuIds = _roleMenuEFRepository.GetList(p => roleCodeList.Contains(p.RoleCode)).Select(p => p.MenuId).Distinct().ToList();
            var list = _menuEFRepository.GetList(p => menuIds.Contains(p.ID)).Select(p => p.Path).Where(p => p.Contains(menuUrl));
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