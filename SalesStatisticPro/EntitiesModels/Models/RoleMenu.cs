using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.Models
{

    [Table("RoleMenu")]
    public partial class RoleMenu : BaseEntity
    {
        public string RoleCode { set; get; }

        public int MenuId { set; get; }

    }
}
