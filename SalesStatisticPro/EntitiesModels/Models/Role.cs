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
    [Table("Role")]
    public partial class Role:BaseEntity
    {
        [MaxLength(32), Required, DisplayName("角色编码")]
        public string RoleCode { set; get; }

        [MaxLength(32), Required, DisplayName("角色名称")]
        public string RoleName { set; get; }
    }
}
