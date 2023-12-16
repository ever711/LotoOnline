using System.Web.Mvc;
using Unity.Mvc5;
using System.Data.Entity;
using System;
using LoDeOnline.Data;
using LoDeOnline.Services;
using System.Web.Http;
using Microsoft.Practices.Unity;
using MyERP.Services;
using Microsoft.Owin.Security;
using LoDeOnline.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;

namespace LoDeOnline
{
    public static class UnityConfig
    {
        //a doi ti
        private static readonly Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterComponents()
        {
            var container = GetConfiguredContainer();
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDbContext, MyERPDbContext>(new PerRequestLifetimeManager());
            container.RegisterType<DbContext, MyERPDbContext>(new PerRequestLifetimeManager());

            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();

            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new InjectionConstructor(typeof(MyERPDbContext)));

            container.RegisterType(typeof(IRepository<>), typeof(EfRepository<>));
            container.RegisterType(typeof(IService<>), typeof(Service<>));
            container.RegisterType<IUnitOfWorkAsync, UnitOfWork>();

            container.RegisterType<ICacheManager, MemoryCacheManager>(new PerRequestLifetimeManager());


            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}