using Autofac;
using Cash.Machine.Application.Abstracts;
using Cash.Machine.Application.Concrets;
using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Domain.Core.Abstracts.Services;
using Cash.Machine.Repository;
using Cash.Machine.Services.Services;

namespace Cash.Machine.Ioc
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountApplicationService>().As<IAccountApplicationService>();
            builder.RegisterType<MovementApplicationService>().As<IMovementApplicationService>();
            builder.RegisterType<OperationApplicationService>().As<IOperationApplicationService>();

            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<MovementService>().As<IMovementService>();
            builder.RegisterType<OperationService>().As<IOperationService>();

            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<MovementRepository>().As<IMovementRepository>();
            builder.RegisterType<OperationRepository>().As<IOperationRepository>();
        }
    }
}