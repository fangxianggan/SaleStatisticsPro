using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Core.WebApi
{
    public class HttpContentTypes
    {
        public   enum HttpContentTypeEnum
        {
            JSON,
            FORM,
            TEXT,
            XML,
            HTML,
            JPG,
            GIF,
            PNG,
          
        }

        public static string GetContentType(HttpContentTypeEnum type)
        {
            string typeStr = "";
            switch (type)
            {
                case HttpContentTypeEnum.HTML:
                    typeStr = "text/html";
                    break;
                case HttpContentTypeEnum.JPG:
                    typeStr = "image/jpg";
                    break;
                case HttpContentTypeEnum.GIF:
                    typeStr = "image/gif";
                    break;
                case HttpContentTypeEnum.PNG:
                    typeStr = "image/png";
                    break;
                case HttpContentTypeEnum.TEXT:
                    typeStr = "text/plain";
                    break;
                case HttpContentTypeEnum.XML:
                    typeStr = "text/xml";
                    break;
                case HttpContentTypeEnum.JSON:
                    typeStr = "application/json";
                    break;
                case HttpContentTypeEnum.FORM:
                    typeStr = "application/x-www-form-urlencoded";
                    break;
            }
            return typeStr;
        }

    }
}
