using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
    /// <summary>
    /// 注册类
    /// </summary>
    public class RegisterViewModel
    {
        [DisplayName("手机号")]
        public string PhoneNumber { set; get; }

        [DisplayName("密码")]
        public string Password { set; get; }

        [DisplayName("再次密码")]
        public string PasswordTwo { set; get; }
    }
}
