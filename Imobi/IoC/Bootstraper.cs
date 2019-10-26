using Autofac;
using Imobi.Managers.File;
using Imobi.Managers.File.Interfaces;
using Imobi.Services;
using Imobi.Services.Interfaces;
using Imobi.Validations;
using Imobi.Validations.Interfaces;
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
            builder.RegisterType<ProposalListViewModel>();
            builder.RegisterType<ProposalDocsViewModel>();
            builder.RegisterType<ProposalFormViewModel>();
            builder.RegisterType<ProposalFlowViewModel>();
            builder.RegisterType<ProposalViewModel>();

            //Services
            builder.RegisterType<ExceptionService>().As<IExceptionService>();
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<ProposalFileTypeService>().As<IProposalFileTypeService>();

            //Validations
            builder.RegisterType<FileValidation>().As<IFileValidation>();
            builder.RegisterType<PermissionValidation>().As<IPermissionValidation>();

            //Managers
            builder.RegisterType<FileManager>().As<IFileManager>();

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