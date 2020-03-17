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
    [Table("MerchantInfo")]
    public partial class MerchantInfo
    {
        
        [Key]
        [Required, DisplayName("ID")]
        public Guid ID { set; get; }

        [MaxLength(32), Required, DisplayName("商户号")]
        public string MerchantNo { set; get; }

        [MaxLength(32), DisplayName("商户名")]
        public string MerchantName { set; get; }

        [MaxLength(32), Required, DisplayName("商户密码")]
        public string MerchantPassword { set; get; }

        [MaxLength(32), Required, DisplayName("商户电话")]
        public string MerchantPhone { set; get; }


    }
}
