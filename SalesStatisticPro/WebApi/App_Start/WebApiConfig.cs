using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //跨域访问 全局
            var allowOrigins = ConfigurationManager.AppSettings["cors_allowOrigins"];
            var allowHeaders = ConfigurationManager.AppSettings["cors_allowHeaders"];
            var allowMethods = ConfigurationManager.AppSettings["cors_allowMethods"];
            var globalCors = new EnableCorsAttribute(allowOrigins, allowHeaders, allowMethods);
            config.EnableCors(globalCors);


            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            InitJsonConfig.Init(config);

        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class InitJsonConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Init(HttpConfiguration config)
        {

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings()
            {
                // 解决json序列化时的循环引用问题
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                // 对 JSON 数据使用混合大小写。驼峰式,但是是javascript 首字母小写形式.
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                //日期格式化问题
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
            };

           // config.Formatters.FormUrlEncodedFormatter.MediaTypeMappings.Add(new QueryStringMapping("t", "steam", "application/octet-stream"));


        }
    }




    /// <summary>
    /// 
    /// </summary>
    public class NullToEmptyStringResolver : DefaultContractResolver
    {
        /// <summary>
        /// 
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties()
                .Select(p =>
                {
                    var jp = base.CreateProperty(p, memberSerialization);
                    jp.ValueProvider = new NullToEmptyStringValueProvider(p);
                    return jp;
                }).ToList();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class NullToEmptyStringValueProvider : IValueProvider
    {
        PropertyInfo _MemberInfo;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
        {
            _MemberInfo = memberInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            object result = _MemberInfo.GetValue(target);
            if (_MemberInfo.PropertyType == typeof(string) && result == null) result = "";
            else if (_MemberInfo.PropertyType == typeof(String[]) && result == null) result = new string[] { };
            else if (_MemberInfo.PropertyType == typeof(Nullable<Int32>) && result == null) result = 0;
            else if (_MemberInfo.PropertyType == typeof(Nullable<Decimal>) && result == null) result = 0.00M;
            //if (result == null) 
            //	result = "";
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetValue(object target, object value)
        {
            _MemberInfo.SetValue(target, value);
        }

    }




}
