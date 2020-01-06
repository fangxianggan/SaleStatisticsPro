using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


namespace FXKJ.Infrastructure.Core.WebApi
{
    public class HttpClientUtil
    {
        /// <summary>
        /// post 提交json格式参数
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="postJson">json字符串</param>
        /// <returns></returns>
        public static string DoPost(string url, Dictionary<string, object> argDic, HttpContentTypes.HttpContentTypeEnum contentTypes = HttpContentTypes.HttpContentTypeEnum.JSON)
        {
            var jsonStr = JsonConvert.SerializeObject(argDic);
            HttpContent content = new StringContent(jsonStr);
            content.Headers.ContentType = new MediaTypeHeaderValue(HttpContentTypes.GetContentType(contentTypes));
            return DoPost(url, content);
        }

        /// <summary>
        /// post 提交json格式参数
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="argDic">参数字典</param>
        /// <param name="headerDic">请求头字典</param>
        /// <returns></returns>
        public static string DoPost(string url, Dictionary<string, object> argDic, Dictionary<string, string> headerDic, HttpContentTypes.HttpContentTypeEnum contentTypes = HttpContentTypes.HttpContentTypeEnum.JSON)
        {
            var jsonStr = JsonConvert.SerializeObject(argDic);
            HttpContent content = new StringContent(jsonStr);
            content.Headers.ContentType = new MediaTypeHeaderValue(HttpContentTypes.GetContentType(contentTypes));
            if (headerDic != null)
            {
                foreach (var item in headerDic)
                {
                    content.Headers.Add(item.Key, item.Value);
                }
            }
            return DoPost(url, content);
        }

        /// <summary>
        /// HttpClient POST 提交
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="content">HttpContent</param>
        /// <returns></returns>
        public static string DoPost(string url, HttpContent content)
        {
            try
            {
                var result = "";
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
                using (var http = new HttpClient(handler))
                {
                    if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    }

                    var response = http.PostAsync(url, content).Result;
                    //确保HTTP成功状态值
                    if (response.IsSuccessStatusCode)
                    {
                        result= response.Content.ReadAsStringAsync().Result;
                    }
                    else {
                        result = "error";
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// HttpClient实现Get请求
        /// </summary>
        public static string DoGet(string url)
        {
            try
            {
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
                using (var http = new HttpClient(handler))
                {
                    http.DefaultRequestHeaders.Add("KeepAlive", "false");
                    //GET提交 返回string
                    HttpResponseMessage response = http.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                   // response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                return ex.Message + "," + ex.Source + "," + ex.StackTrace;
            }
        }

        /// <summary>
        /// HttpClient实现Get请求
        /// <param name="arg">参数字典</param>
        /// </summary>
        public static string DoGet(string url, Dictionary<string, object> arg)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            string argStr = "?";
            foreach (var item in arg)
            {
                argStr += item.Key + "=" + item.Value + "&";
            }
            argStr = argStr.TrimEnd('&');
            url = url + argStr;
            return DoGet(url);
        }

        /// <summary>
        /// HttpClient Get 提交
        /// </summary>
        /// <param name="url"></param>
        /// <param name="arg">参数</param>
        /// <param name="headerDic">头文件</param>
        /// <returns></returns>
        public static string DoGet(string url, Dictionary<string, object> arg, Dictionary<string, string> headerDic)
        {
            try
            {
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
                if (arg != null && arg.Count > 0)
                {
                    string argStr = "?";
                    foreach (var item in arg)
                    {
                        argStr += item.Key + "=" + item.Value + "&";
                    }
                    argStr = argStr.TrimEnd('&');
                    url = url + argStr;
                }
                using (var http = new HttpClient(handler))
                {
                    if (headerDic != null)
                    {
                        foreach (var item in headerDic)
                        {
                            http.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                    //await异步等待回应
                    var response = http.GetStringAsync(url).Result;
                    return response;
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
