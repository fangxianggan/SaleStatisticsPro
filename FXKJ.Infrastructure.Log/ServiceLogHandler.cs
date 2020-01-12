﻿using FXKJ.Infrastructure.Log.LogModel;

namespace FXKJ.Infrastructure.Log
{
    /// <summary>
    /// 服务日志
    /// </summary>
    public class ServiceLogHandler : BaseHandler<ServiceLog>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceLogHandler()
            : base("ServiceToDatabase")
        {

        }
    }
}
