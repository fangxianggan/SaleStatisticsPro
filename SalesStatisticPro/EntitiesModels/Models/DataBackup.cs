using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.Models
{
    [Table("DataBackup")]
    public partial  class DataBackup:BaseEntity
    {
        [DisplayName("备份名称"), MaxLength(32)]
        public string BackUpName { set; get; }


        [DisplayName("备份路径"), MaxLength(32)]
        public string Path { set; get; }
    }
}
