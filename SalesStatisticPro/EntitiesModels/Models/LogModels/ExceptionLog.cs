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
    /// 异常记录日志
    /// </summary>
   [Table("Log_ExceptionLog")]
   public class ExceptionLog: BaseEntityLog
    {
        
        [DisplayName("主键Id"), Key]
        public Guid ExceptionLogId { get; set; }

        /// <summary>
        ///     消息
        /// </summary>
        [DisplayName("消息")]
        public string Message { get; set; }

        /// <summary>
        ///     堆栈信息
        /// </summary>
        /// 
        [DisplayName("堆栈信息")]
        public string StackTrace { get; set; }

        /// <summary>
        ///     内部信息
        /// </summary>
        /// 
        [DisplayName("内部信息")]
        public string InnerException { get; set; }

        /// <summary>
        ///     异常类型
        /// </summary>
        /// 
        [DisplayName("异常类型"), MaxLength(128)]
        public string ExceptionType { get; set; }

        /// <summary>
        ///     服务器
        /// </summary>
        /// 
        [DisplayName("服务器"), MaxLength(128)]
        public string ServerHost { get; set; }

        /// <summary>
        ///     客户端
        /// </summary>
        /// 
        [DisplayName("客户端"), MaxLength(128)]
        public string ClientHost { get; set; }

        /// <summary>
        ///     运行环境
        /// </summary>
        /// 
        [DisplayName("客户端"), MaxLength(128)]
        public string Runtime { get; set; }

        /// <summary>
        ///     请求Url
        /// </summary>
        /// 
        [DisplayName("请求Url"), MaxLength(128)]
        public string RequestUrl { get; set; }

        /// <summary>
        ///     请求数据
        /// </summary>
        /// 
        [DisplayName("请求数据")]
        public string RequestData { get; set; }

        /// <summary>
        ///     浏览器代理
        /// </summary>
        /// 
        [DisplayName("浏览器代理"),MaxLength(128)]
        public string UserAgent { get; set; }

        /// <summary>
        ///     请求方式
        /// </summary>
        /// 
        [DisplayName("浏览器代理"), MaxLength(16)]
        public string HttpMethod { get; set; }
    }
}
