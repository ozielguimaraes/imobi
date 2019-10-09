using Autofac;
using Imobi.Services;
using Imobi.Services.Interfaces;
using Imobi.ViewModels;
using System;

namespace Imobi.IoC
{
    public class Bootstraper
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<MenuViewModel>();
            builder.RegisterType<RegisterViewModel>();
            builder.RegisterType<MyWalletViewModel>();
            builder.RegisterType<ProposalDocsViewModel>();
            builder.RegisterType<ProposalViewModel>();

            //Services
            builder.RegisterType<NavigationService>().As<INavigationService>();

            _container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}