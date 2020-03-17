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
    [Table("MerchantRole")]
    public partial class MerchantRole : BaseEntity
    {

        [MaxLength(32), Required, DisplayName("商户号")]
        public string MerchantNo { set; get; }

        [MaxLength(32), Required, DisplayName("角色Code")]
        public string RoleCode { set; get; }

    }
}
