using EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
    [NotMapped]
    public partial class MenuViewModel : Menu
    {
        public List<MenuViewModel> Children { set; get; }
    }
}
