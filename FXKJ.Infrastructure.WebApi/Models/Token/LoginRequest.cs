using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FXKJ.Infrastructure.WebApi.Models.Token
{
    /// <summary>
    /// 登录用户信息
    /// </summary>

    public partial class LoginRequest:BaseModel
    {

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

    }
}