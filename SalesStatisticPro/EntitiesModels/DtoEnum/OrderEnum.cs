using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoEnum
{
    public class OrderEnum
    {
        public enum OrderState
        {
            /// <summary>
            /// 未完成
            /// </summary>
            FF01,
            /// <summary>
            /// 已锁定
            /// </summary>
            FF02,
             /// <summary>
             /// 订单 未发货
             /// </summary>
            SS01,
            /// <summary>
            /// 已发货
            /// </summary>
            SS02,
            /// <summary>
            /// 已签收
            /// </summary>
            SS03,
            /// <summary>
            /// 已丢失
            /// </summary>
            SS04
            
        }

       


    }
}
