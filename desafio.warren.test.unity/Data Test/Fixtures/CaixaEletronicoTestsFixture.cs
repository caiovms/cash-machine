using Bogus;
using desafio.warren.domain.Entities;
using desafio.warren.webapi.Models;
using System;
using Xunit;

namespace desafio.warren.test.unity.DataTest.Fixtures
{
    [CollectionDefinition(Collections.Collections.CaixaEletronicoCollection)]
    public class CaixaEletronicoCollection : ICollectionFixture<CaixaEletronicoTestsFixture>
    { }

    public class CaixaEletronicoTestsFixture : IDisposable
    {
        public SaqueViewModel GerarSaque()
        {
            var saque = new Faker<SaqueViewModel>()
                .RuleFor(c => c.IdConta, m => m.Random.Int(1, 100000))
                .RuleFor(c => c.IdOperacao, (byte)TipoOperacao.SAQUE )
                .RuleFor(c => c.ValorOperacao, 500)
                .Generate();

            return saque;
        }

        public DepositoViewModel GerarDeposito()
        {
            var deposito = new Faker<DepositoViewModel>()
                              .RuleFor(c => c.IdConta, faker => faker.Random.Int(1, 100000))
                              .RuleFor(c => c.IdOperacao, (byte)TipoOperacao.DEPOSITO)
                              .RuleFor(c => c.ValorOperacao, 500)
                              .Generate();

            return deposito;
        }

        public PagamentoViewModel GerarPagamento()
        {
            var pagamento = new Faker<PagamentoViewModel>()
                               .RuleFor(pagamento => pagamento.IdConta, faker => faker.Random.Int(1, 100000))
                               .RuleFor(pagamento => pagamento.IdOperacao, (byte)TipoOperacao.PAGAMENTO)
                               .RuleFor(pagamento => pagamento.ValorOperacao, 500)
                               .RuleFor(pagamento => pagamento.CodigoDeBarras, p => p.Random.String2(48, "1234567890"))
                               .Generate();

            return pagamento;
        }

        public RentabilizacaoViewModel GerarRentablizacao()
        {
            var rentabilizacao = new Faker<RentabilizacaoViewModel>()
                                    .RuleFor(rentabilizacao => rentabilizacao.IdConta, faker => faker.Random.Int(1, 100000))
                                    .RuleFor(rentabilizacao => rentabilizacao.IdOperacao, (byte)TipoOperacao.RENTABILIZACAO)
                                    .RuleFor(rentabilizacao => rentabilizacao.TaxaRentabilidade, 0.01M)
                                    .Generate();

            return rentabilizacao;
        }

        public void Dispose()
        {
        }
    }
}
