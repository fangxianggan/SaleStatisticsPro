using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
    /// <summary>
    /// element ui select 模型
    /// </summary>
    [NotMapped]
    public partial class SelectViewModel
    {
        public string Key { set; get; }

        public string Label { set; get; }

        public string Value { set; get; }
    }
}
