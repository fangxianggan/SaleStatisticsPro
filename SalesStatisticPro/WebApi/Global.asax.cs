using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FXKJ.Infrastructure.Auth.BLL;
using FXKJ.Infrastructure.Auth.IBLL;
using FXKJ.Infrastructure.DataAccess;
using FXKJ.Infrastructure.Logic;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebApi.App_Start;


namespace WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //程序启动注入
             AutoFacBootStrapper.CoreAutoFacInit();
            //dto模型注册
            AutoMapperConfig.Register();

            //解决ef首次加载很慢的
            using (var dbcontext = new EntitiesModels.MyContext())
            {
                var objectContext = ((IObjectContextAdapter)dbcontext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }

        }

        ///// <summary>
        ///// 全局错误处理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var error = "";
        //    var errorCode = "";
        //    try
        //    {
        //        Exception exception = Server.GetLastError();
        //        error = exception != null ? exception.Message : "";
        //        Response.Clear();
        //        HttpException httpException = exception as HttpException;
        //        errorCode = (httpException != null) ? httpException.GetHttpCode().ToString() : "";
        //        // Clear the error on server.  
        //        Server.ClearError();
        //        // Call target Controller and pass the routeData. 
        //    }
        //    catch (Exception ex)
        //    {
        //        error += ex.Message;
        //    }
        //    finally
        //    {
             
        //        Response.Flush();
        //        Response.End();

        //    }

        //}




       
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
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            }

            private static void SetupResolveRules(ContainerBuilder builder)
            {

                //泛型类 接口注入方式
                builder.RegisterGeneric(typeof(AsyncLogic<>)).As(typeof(IAsyncLogic<>)).InstancePerDependency();
                builder.RegisterGeneric(typeof(AsyncEFRepository<>)).As(typeof(IAsyncEFRepository<>)).InstancePerDependency();
                builder.RegisterGeneric(typeof(Logic<>)).As(typeof(ILogic<>)).InstancePerDependency();
                builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IEFRepository<>)).InstancePerDependency();
                builder.RegisterGeneric(typeof(DapperRepository<>)).As(typeof(IDapperRepository<>)).InstancePerDependency();
                var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
                builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("BLL")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

                builder.RegisterType<TokenBLL>().As<ITokenBLL>().InstancePerLifetimeScope();
            }
        }
    }
}
