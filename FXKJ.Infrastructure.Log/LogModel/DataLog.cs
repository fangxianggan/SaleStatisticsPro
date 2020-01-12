using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Log.LogModel
{
    /// <summary>
    ///     数据日志
    /// </summary>
    public class DataLog
    {
        public Guid DataLogId { get; set; }

        /// <summary>
        ///     操作类型:0新增/2编辑/3删除
        /// </summary>
        public string OperateType { get; set; }

        /// <summary>
        ///     操作表
        /// </summary>
        public string OperateTable { get; set; }

        /// <summary>
        ///     操作前数据:若为新增，删除等数据
        /// </summary>
        public string OperationBefore { get; set; }

        /// <summary>
        ///     操作后数据:编辑操作
        /// </summary>
        public string OperationAfterData { get; set; }

        /// <summary>
        ///     创建人员
        /// </summary>
        public Guid CreateUserId { get; set; }

        /// <summary>
        ///     创建人员登录代码
        /// </summary>
        public string CreateUserCode { get; set; }

        /// <summary>
        ///     创建人员名字
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime OperateTime { get; set; }
    }

}
