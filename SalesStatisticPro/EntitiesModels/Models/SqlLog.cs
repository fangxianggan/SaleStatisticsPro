using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.Models
{
    [Table("SqlLog")]
    public class SqlLog:BaseEntity
    {
        /// <summary>
        ///     sql日志Id
        /// </summary>
        public Guid SqlLogId { get; set; }

        /// <summary>
        ///     操作sql
        /// </summary>
        /// 
        [MaxLength(2000)]
        public string OperateSql { get; set; }

        /// <summary>
        ///     结束时间
        /// </summary>
        public DateTime EndDateTime
        {
            set;
            get;
        }

        /// <summary>
        ///     耗时
        /// </summary>
        public double ElapsedTime { get; set; }

        /// <summary>
        ///     参数
        /// </summary>
        /// 
        [MaxLength(500)]
        public string Parameter { get; set; }

    }
}
