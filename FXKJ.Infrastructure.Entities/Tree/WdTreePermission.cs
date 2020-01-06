using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Entities.Tree
{
    /// <summary>
    /// wdTree权限信息
    /// </summary>
    public class WdTreePermission
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public string icon { get; set; }

        public IList<WdTreeEntity> tree { get; set; }
    }
}
