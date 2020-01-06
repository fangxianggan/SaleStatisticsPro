using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Entities.Tree
{
    public class Define_Sys_Menu 
    {
        public Guid MenuId { set; get; }

        public Guid SystemId { set; get; }
        public string MenuName { set; get; }
        public string MenuTraget { set; get; } //目标

        public Guid MenuParentId { set; get; }
        public string MenuUrl { set; get; }
        public string MenuIcon { set; get; }
        public string MenuState { set; get; }
        public DateTime CreateTime { set; get; }
        public int MenuSort { set; get; }
        public string MenuNote { set; get; }

        public IList<Define_Sys_Menu> Children { set; get; }

    }
}
