using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EntitiesModels.DtoModels
{
    public  class AuthInfoViewModel
    {

        public AuthInfoViewModel()
        {
            Name = "Admin";
            Introduction = "dasasas";
            Avatar = "https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif";

        }
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

        [DisplayName("名称")]
        public string Name { get; set; }

        [DisplayName("介绍")]
        public string Introduction { get; set; }

        [DisplayName("头像")]
        public string Avatar { get; set; }

        public Guid GuidId { set; get; }
        
    }
}
