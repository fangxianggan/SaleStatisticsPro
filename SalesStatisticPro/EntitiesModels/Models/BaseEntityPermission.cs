using System.ComponentModel;

namespace EntitiesModels.Models
{
    public class BaseEntityPermission : BaseEntity
    {
        public BaseEntityPermission()
        {

        }
        [DisplayName("商户号")]
        public string P_MerchantNo { set; get; }


    }
}
