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
    /// 操作日志
    /// </summary>
    [Table("Log_OperateLog")]
    public class OperateLog : BaseEntityLog
    {
        [DisplayName("主键Id"), Key]
        public Guid OperationLogId { set; get; }

      
        [DisplayName("客户端"), MaxLength(128)]
        public string ClientHost { get; set; }

       
        [DisplayName("服务端IP地址"), MaxLength(128)]
        public string ServerHost { get; set; }

       
        [DisplayName("请求内容大小")]
        public int? RequestContentLength { get; set; }

        /// <summary>
        ///     请求类型 get or post
        /// </summary>
        /// 
        [DisplayName("请求类型"), MaxLength(16)]
        public string RequestType { get; set; }

       
        [DisplayName("当前请求Url信息"), MaxLength(512)]
        public string Url { get; set; }

        /// <summary>
        ///     版本
        /// </summary>
        /// 
        [DisplayName("版本"), MaxLength(512)]
        public string Version { get; set; }

        /// <summary>
        ///     请求数据
        /// </summary>
        /// 
        [DisplayName("请求数据")]
        public string RequestData { get; set; }

        /// <summary>
        ///     浏览器代理信息
        /// </summary>
        /// 
        [DisplayName("浏览器代理信息"), MaxLength(512)]
        public string UserAgent { get; set; }

        /// <summary>
        ///     控制器名称
        /// </summary>
        /// 
        [DisplayName("控制器名称"), MaxLength(64)]
        public string ControllerName { get; set; }

        /// <summary>
        ///     操作名称
        /// </summary>
        /// 
        [DisplayName("操作名称"), MaxLength(128)]
        public string ActionName { get; set; }

        /// <summary>
        ///     Action执行时间(秒)
        /// </summary>
        /// 
        [DisplayName("Action执行时间(秒)")]
        public double ActionExecutionTime { get; set; }

        /// <summary>
        ///     页面展示时间(秒)
        /// </summary>
        /// 
        [DisplayName("页面展示时间(秒)")]
        public double ResultExecutionTime { get; set; }

        /// <summary>
        ///     响应状态
        /// </summary>
        /// 
        [DisplayName("响应状态"), MaxLength(128)]
        public string ResponseStatus { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        /// 
        [DisplayName("描述")]
        public string Describe { get; set; }
    }
}
