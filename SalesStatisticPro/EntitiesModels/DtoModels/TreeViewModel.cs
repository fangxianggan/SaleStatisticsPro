using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
    /// <summary>
    ///  tree
    /// </summary>
    [NotMapped]
    public partial class TreeViewModel
    {
        public int ID { set; get; }

        public string Label { set; get; }

        public virtual List<TreeViewModel> Children { set; get; }
    }
}
