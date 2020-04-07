using EntitiesModels.Models.SysModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    [Table("Log_SqlLog")]
    public class SqlLog: BaseEntityLog
    {
        /// <summary>
        ///     sql日志Id
        /// </summary>
        [DisplayName("主键Id"), Key]
        public Guid SqlLogId { get; set; }


        [DisplayName("操作sql"), MaxLength]
        public string OperateSql { get; set; }

       
        [DisplayName("结束时间")]
        public DateTime EndDateTime
        {
            set;
            get;
        }

        [DisplayName("耗时")]
        public double ElapsedTime { get; set; }

        
        [DisplayName("参数"), MaxLength]
        public string Parameter { get; set; }

    }
}
