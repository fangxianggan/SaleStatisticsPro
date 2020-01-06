using Autofac;
using Autofac.Integration.WebApi;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
          

            //程序启动注入
            AutoFacBootStrapper.CoreAutoFacInit();


        }

        /// <summary>
        /// 
        /// </summary>
        public class AutoFacBootStrapper
        {
            /// <summary>
            /// 
            /// </summary>
            public static void CoreAutoFacInit()
            {
                var builder = new ContainerBuilder();
                HttpConfiguration config = GlobalConfiguration.Configuration;

                SetupResolveRules(builder);
                //注册所有的ApiControllers
                builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

                var container = builder.Build();
                //注册api容器需要使用HttpConfiguration对象
                config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
               
            }

            private static void SetupResolveRules(ContainerBuilder builder)
            {
            
                #region IOC注册区域
                var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
                builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("BLL")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
                #endregion

              
            }
        }
    }
}
