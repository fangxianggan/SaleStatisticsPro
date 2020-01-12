using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Log.LogModel
{
    /// <summary>
    ///     登录日志
    /// </summary>
    public class LoginLog
    {
        /// <summary>
        ///     主键Id
        /// </summary>
        public Guid LoginLogId { get; set; }

        /// <summary>
        ///     Ip对应地址
        /// </summary>
        public string IpAddressName { get; set; }

        /// <summary>
        ///     服务器主机名
        /// </summary>
        public string ServerHost { get; set; }

        /// <summary>
        ///     客户端主机名
        /// </summary>
        public string ClientHost { get; set; }

        /// <summary>
        ///     浏览器信息
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        ///     操作系统
        /// </summary>
        public string OsVersion { get; set; }

        /// <summary>
        ///     登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        ///     退出时间
        /// </summary>
        public DateTime? LoginOutTime { get; set; }

        /// <summary>
        ///     停留时间(分钟)
        /// </summary>
        public double? StandingTime { get; set; }

        /// <summary>
        ///     创建人员
        /// </summary>
        public Guid? CreateUserId { get; set; }

        /// <summary>
        ///     创建人员登录代码
        /// </summary>
        public string CreateUserCode { get; set; }

        /// <summary>
        ///     创建人员名字
        /// </summary>
        public string CreateUserName { get; set; }


    }
}
