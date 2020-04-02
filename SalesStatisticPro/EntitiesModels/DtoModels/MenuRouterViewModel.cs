using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
  public partial class MenuRouterViewModel
    {
        public string Path { set; get; }

        public string Name { set; get; }

        public string Component { set; get; }

        public MetaViewModel Meta { set; get; }

      //  public bool Hidden { set; get; }

      //  public string Redirect { set; get; }

       public List<MenuRouterViewModel> Children { set; get; }

    }

    public partial class MetaViewModel
    {
        public string Icon { set; get; }

        public string Title { set; get; }

       // public string[] Role { set; get; }

      //  public bool NoCache { set; get; }

       // public bool Breadcrumb { set; get; }
    }
}
