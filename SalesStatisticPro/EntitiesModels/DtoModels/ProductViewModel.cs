using EntitiesModels.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.DtoModels
{
    [NotMapped]
   public partial class ProductViewModel:Product
    {
      
        [DisplayName("分类名称")]
        public string CategoryName { set; get; }
       
    }
}
