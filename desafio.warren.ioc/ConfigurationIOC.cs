using Autofac;
using desafio.warren.application.Abstracts;
using desafio.warren.application.Concrets;
using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.domain.core.Abstracts.Services;
using desafio.warren.repository;
using desafio.warren.services.Services;

namespace desafio.warren.ioc
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContaApplicationService>().As<IContaApplicationService>();
            builder.RegisterType<MovimentoApplicationService>().As<IMovimentoApplicationService>();
            builder.RegisterType<OperacaoApplicationService>().As<IOperacaoApplicationService>();

            builder.RegisterType<ContaService>().As<IContaService>();
            builder.RegisterType<MovimentoService>().As<IMovimentoService>();
            builder.RegisterType<OperacaoService>().As<IOperacaoService>();

            builder.RegisterType<ContaRepository>().As<IContaRepository>();
            builder.RegisterType<MovimentoRepository>().As<IMovimentoRepository>();
            builder.RegisterType<OperacaoRepository>().As<IOperacaoRepository>();
        }
    }
}