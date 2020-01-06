using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FXKJ.Infrastructure.Core.Extensions;

namespace FXKJ.Infrastructure.Core.WebApi
{
    public class HttpRequestUtil
    {
        public static HttpWebRequest CreateHttpWebRequest(string url)
        {
            HttpWebRequest request;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //ServicePointManager.DefaultConnectionLimit = 1000;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version11;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Proxy = null;
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="dic">参数</param>
        /// <param name="headerDic">请求头参数</param>
        /// <returns></returns>
        public static string DoPost(string url, Dictionary<string, object> dic, Dictionary<string, string> headerDic,HttpContentTypes.HttpContentTypeEnum contentType=HttpContentTypes.HttpContentTypeEnum.JSON)
        {
            HttpWebRequest request = CreateHttpWebRequest(url);
            request.Method = "POST";
            request.Accept = "*/*";
            request.ContentType = HttpContentTypes.GetContentType(contentType);
            if (headerDic != null && headerDic.Count > 0)
            {
                foreach (var item in headerDic)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
    
            if (dic != null && dic.Count > 0)
            {

                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in dic.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, dic[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, dic[key]);
                    }
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                request.ContentLength = data.Length;
                try
                {
                    request.GetRequestStream().Write(data, 0, data.Length);
                }
                catch (WebException ex)
                {
                    throw ex;
                }
            }
            else
            {
                request.ContentLength = 0;
            }
            return HttpResponse(request);
        }

        public static string DoPost(string url, Dictionary<string, object> dic, HttpContentTypes.HttpContentTypeEnum contentType = HttpContentTypes.HttpContentTypeEnum.JSON)
        {
            return DoPost(url, dic, null,contentType);
        }

 
        public static string HttpResponse(HttpWebRequest request)
        {
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {

                    var contentEncodeing = response.ContentEncoding.ToLower();

                    if (!contentEncodeing.Contains("gzip") && !contentEncodeing.Contains("deflate"))
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        #region gzip,deflate 压缩解压
                        if (contentEncodeing.Contains("gzip"))
                        {

                            using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                            {
                                using (StreamReader reader = new StreamReader(stream))
                                {
                                    return reader.ReadToEnd();
                                }
                            }
                        }
                        else
                        {
                            using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                            {
                                using (StreamReader reader = new StreamReader(stream))
                                {
                                    return reader.ReadToEnd();
                                }
                            }
                        }
                        #endregion gzip,deflate 压缩解压
                    }
                }
               
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        public static string DoGet(string url, Dictionary<string, object> dic,HttpContentTypes.HttpContentTypeEnum contentType=HttpContentTypes.HttpContentTypeEnum.JSON)
        {
            try
            {
              
                var argStr = dic == null ? "" : dic.ToSortUrlParamString();
                argStr = string.IsNullOrEmpty(argStr) ? "" : ("?" + argStr);
                HttpWebRequest request = CreateHttpWebRequest(url + argStr);
                request.Method = "GET";
                request.ContentType = HttpContentTypes.GetContentType(contentType);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DoGet(string url, Dictionary<string, object> dic, Dictionary<string, string> headerDic, HttpContentTypes.HttpContentTypeEnum contentType = HttpContentTypes.HttpContentTypeEnum.JSON)
        {
            try
            {
                var argStr = dic == null ? "" : dic.ToSortUrlParamString();
                argStr = string.IsNullOrEmpty(argStr) ? "" : ("?" + argStr);
                HttpWebRequest request = CreateHttpWebRequest(url + argStr);
                request.Method = "GET";
                request.ContentType = HttpContentTypes.GetContentType(contentType);
                foreach (var item in headerDic)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
    }
}
