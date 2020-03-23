using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models.SysModels
{
    /// <summary>
    ///  数据日志
    /// </summary>
    /// 
    [Table("Log_DataLog")]
    public class DataLog:BaseEntityLog
    {
        [DisplayName("主键Id"), Key]
        public Guid DataLogId { get; set; }

        /// <summary>
        ///  操作类型:0新增/2编辑/3删除
        /// </summary>
        /// 
        [DisplayName("操作类型"), MaxLength(16)]
        public string OperateType { get; set; }

        /// <summary>
        ///  操作表
        /// </summary>
        /// 
        [DisplayName("操作表"), MaxLength(64)]
        public string OperateTable { get; set; }

        /// <summary>
        ///  操作前数据:若为新增，删除等数据
        /// </summary>
        /// 
        [DisplayName("操作前数据")]
        public string OperationBefore { get; set; }

        /// <summary>
        /// 操作后数据:编辑操作
        /// </summary>
        /// 
        [DisplayName("操作后数据")]
        public string OperationAfterData { get; set; }

    }
}
