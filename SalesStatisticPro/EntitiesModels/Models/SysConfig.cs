using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.Models
{

    [Table("SysConfig")]
    public partial class SysConfig : BaseEntity
    {
        public Guid ConfigId { set; get; }


        [MaxLength(32)]
        public string Code { set; get; }


        [MaxLength(3000)]
        public string Value { set; get; }
    }
}
