using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.WebApi.Models
{
   public partial class BaseModel
    {
        public BaseModel()
        {
            Name = "Admin";
            Introduction = "dasasas";
            Avatar = "https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif";

        }
        public string Name { get; }

        public string Introduction { get; }

        public string Avatar { get; }
    }
}
