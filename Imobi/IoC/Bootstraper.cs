//using Autofac;
using Imobi.Services;
using Imobi.Services.Interfaces;
using Imobi.ViewModels;
using System;
using Xamarin.Forms;

namespace Imobi.IoC
{
    public class Bootstraper
    {
        //private static IContainer _container;

        public static void RegisterDependencies()
        {
            //    var builder = new ContainerBuilder();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<INavigationService, NavigationService>();

            //    //ViewModels
            //    builder.RegisterType<MDViewModel>();
            //    builder.RegisterType<MainViewModel>();
            //    builder.RegisterType<LoginViewModel>();
            //    builder.RegisterType<RegisterViewModel>();
            //    builder.RegisterType<ItemsViewModel>();
            //    builder.RegisterType<EventCreateViewModel>();
            //    builder.RegisterType<EventListViewModel>();

            //    //services - general
            //    builder.RegisterType<NavigationService>().As<INavigationService>();
            //    builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            //    builder.RegisterType<SettingsService>().As<ISettingsService>();

            //    //Database
            //    builder.RegisterType<UserDatabase>().As<IUserDatabase>();
            //    builder.RegisterType<EventDatabase>().As<IEventDatabase>();

            //    _container = builder.Build();
            //}

            //public static object Resolve(Type typeName)
            //{
            //    return _container.Resolve(typeName);
            //}

            //public static T Resolve<T>()
            //{
            //    return _container.Resolve<T>();
            //}
        }
    }
}