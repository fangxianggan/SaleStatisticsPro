using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FXKJ.Infrastructure.WebApi.Models.Token
{
    /// <summary>
    /// 身份验证信息 模拟JWT的payload
    /// </summary>

    public class AuthInfo:BaseModel
    {

       /// <summary>
       /// 手机号
       /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MerchantNo { set; get; }

        /// <summary>
        /// 角色
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 口令过期时间
        /// </summary>
        public DateTime? ExpiryDateTime { get; set; }


        /// <summary>
        /// 刷新时间
        /// </summary>
        public DateTime? RefreshDateTime { get; set; }

       

       
    }
}