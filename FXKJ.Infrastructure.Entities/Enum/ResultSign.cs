using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Entities.Enum
{
    public enum ResultSign
    {
        /// <summary>
        ///     操作成功
        /// </summary>
        Successful = 0,

        /// <summary>
        ///     警告
        /// </summary>
        Warning = 1,

        /// <summary>
        ///     操作引发错误
        /// </summary>
        Error = 2
    }
}
