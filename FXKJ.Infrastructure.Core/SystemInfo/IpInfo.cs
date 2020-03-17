using System;
using System.Collections.Generic;
using System.Text;

namespace FXKJ.Infrastructure.Core.SystemInfo
{
   

        /// <summary>
        /// 网络 ip 地址类
        /// </summary>
        public class IpInfo
        {
            public IpInfo()
            {
            }

            public IpInfo(string ip, string mac)
            {
                m_MACAddress = mac;
                m_IPAddress = ip;
            }

            private string m_MACAddress;
            /// <summary>
            /// 物理地址
            /// </summary>
            public string MACAddress
            {
                get { return m_MACAddress; }
                set { m_MACAddress = value; }
            }

            private string m_IPAddress;
            /// <summary>
            /// IP 地址
            /// </summary>
            public string IPAddress
            {
                get { return m_IPAddress; }
                set { m_IPAddress = value; }
            }
        }
    
}
