using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using IContainer = Autofac.IContainer;

namespace Services
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public static class AutofacExt
    {
        private static IContainer _container;

        public static void InitAutofac()
        {
            var builder = new ContainerBuilder();

            //注册数据库基础操作和工作单元
            builder.RegisterGeneric(typeof(SugarRepository<>)).As(typeof(ISugarRepository<>)).PropertiesAutowired();

            //注册Service层
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            // 注册controller，使用属性注入
            builder.RegisterControllers(Assembly.GetCallingAssembly()).PropertiesAutowired();

            //注册所有的ApiControllers
            builder.RegisterApiControllers(Assembly.GetCallingAssembly()).PropertiesAutowired();

            builder.RegisterModelBinders(Assembly.GetCallingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // 注册所有的Attribute
            builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.
            _container = builder.Build();

            //Set the MVC DependencyResolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));

            //Set the WebApi DependencyResolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)_container);
        }

        /// <summary>
        /// 从容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetFromFac<T>()
        {
            return _container.Resolve<T>();
            //   return (T)DependencyResolver.Current.GetService(typeof(T));
        }
    }
}
