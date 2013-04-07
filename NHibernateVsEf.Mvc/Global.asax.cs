using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using NHibernate.Context;
using NHibernateVsEf.Core;
using NHibernateVsEf.Core.IocAttributes;
using NHibernateVsEf.Core.Repositories.NHibernate;
using NHibernateVsEf.Mvc.Controllers;
using Ninject;
using Ninject.Web.Common;
using Ninject.Extensions.Conventions;

namespace NHibernateVsEf.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        private ISessionFactory _sessionFactory;

        public MvcApplication()
        {
            EndRequest += MvcApplication_EndRequest;
        }

        void MvcApplication_EndRequest(object sender, System.EventArgs e)
        {
            if (_sessionFactory != null && CurrentSessionContext.HasBind(_sessionFactory))
            {
                ISession session = CurrentSessionContext.Unbind(_sessionFactory);
                session.Dispose();
            }
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
            );
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            _sessionFactory = new SessionFactoryBuilder().BuildSessionFactory();
            kernel.Inject(_sessionFactory);
            BindRepositories(kernel);
            BindServices(kernel);
            BindInjectableInterfaces(kernel);
            return kernel;
        }

        private static void BindInjectableInterfaces(StandardKernel kernel)
        {
            kernel.Bind
            (
                s =>
                s.From(Assembly.GetAssembly(typeof(TimerController)))
                    .SelectAllClasses()
                    .WithAttribute<IsInjectedAttribute>()
                    .BindAllInterfaces()
                    .Configure(cfg => cfg.InRequestScope())
            );
        }

        private static void BindServices(StandardKernel kernel)
        {
            kernel.Bind
            (
                s =>
                s.From(Assembly.GetAssembly(typeof (TrackRepositoryNh)))
                    .SelectAllClasses()
                    .WithAttribute<ServiceAttribute>()
                    .BindAllInterfaces()
                    .Configure(cfg => cfg.InRequestScope())
            );
        }

        private static void BindRepositories(StandardKernel kernel)
        {
            kernel.Bind
            (
                s =>
                s.From(Assembly.GetAssembly(typeof (ArtistRepositoryNh)))
                    .SelectAllClasses()
                    .WithAttribute<RepositoryAttribute>()
                    .BindAllInterfaces()
                    .Configure(cfg => cfg.InRequestScope())
            );
        }


        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}