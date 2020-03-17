using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
    public class FileStreamViewModel
    {
        /// <summary>
        /// 文件内容
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { set; get; }
    }
}
