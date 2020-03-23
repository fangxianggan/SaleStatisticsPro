using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.Models.SysModels
{
    /// <summary>
    /// 登录日志
    /// </summary>
   [Table("Log_LoginLog")]
    public class LoginLog : BaseEntityLog
    {
       
        [DisplayName("主键Id"),Key]
        public Guid LoginLogId { get; set; }

        [DisplayName("Ip对应地址"), MaxLength(128)]
        public string IpAddressName { get; set; }

       
        [DisplayName("服务器主机名"), MaxLength(256)]
        public string ServerHost { get; set; }

        
        [DisplayName("客户端主机名"), MaxLength(256)]
        public string ClientHost { get; set; }

         
        [DisplayName("浏览器信息"), MaxLength(256)]
        public string UserAgent { get; set; }

       
        [DisplayName("操作系统"), MaxLength(64)]
        public string OsVersion { get; set; }

         
        [DisplayName("登录时间")]
        public DateTime LoginTime { get; set; }

       
        [DisplayName("退出时间")]
        public DateTime? LoginOutTime { get; set; }

        
        [DisplayName("停留时间(分钟)")]
        public double? StandingTime { get; set; }
    }
}
