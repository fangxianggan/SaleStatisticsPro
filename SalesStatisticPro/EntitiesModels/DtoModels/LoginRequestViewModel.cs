using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
  public partial  class LoginRequestViewModel
    {

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string ReturnUrl { set; get; }
    }
}
