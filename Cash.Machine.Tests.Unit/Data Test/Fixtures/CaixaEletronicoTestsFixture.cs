using Bogus;
using Cash.Machine.Domain.Entities;
using Cash.Machine.WebApi.Models;
using System;
using Xunit;

namespace Cash.Machine.Tests.Unit.DataTest.Fixtures
{
    [CollectionDefinition(Collections.Collections.CaixaEletronicoCollection)]
    public class CaixaEletronicoCollection : ICollectionFixture<CaixaEletronicoTestsFixture>
    { }

    public class CaixaEletronicoTestsFixture : IDisposable
    {
        public WithdrawViewModel GerarSaque()
        {
            var saque = new Faker<WithdrawViewModel>()
                .RuleFor(c => c.AccountId, m => m.Random.Int(1, 100000))
                .RuleFor(c => c.OperationId, (byte)OperationType.WITHDRAW )
                .RuleFor(c => c.OperationAmount, 500)
                .Generate();

            return saque;
        }

        public DepositViewModel GerarDeposito()
        {
            var deposito = new Faker<DepositViewModel>()
                              .RuleFor(c => c.AccountId, faker => faker.Random.Int(1, 100000))
                              .RuleFor(c => c.OperationId, (byte)OperationType.DEPOSIT)
                              .RuleFor(c => c.OperationAmount, 500)
                              .Generate();

            return deposito;
        }

        public PaymentViewModel GerarPagamento()
        {
            var pagamento = new Faker<PaymentViewModel>()
                               .RuleFor(pagamento => pagamento.AccountId, faker => faker.Random.Int(1, 100000))
                               .RuleFor(pagamento => pagamento.OperationId, (byte)OperationType.PAYMENT)
                               .RuleFor(pagamento => pagamento.OperationAmount, 500)
                               .RuleFor(pagamento => pagamento.BarCode, p => p.Random.String2(48, "1234567890"))
                               .Generate();

            return pagamento;
        }

        public MonetizeViewModel GerarRentablizacao()
        {
            var rentabilizacao = new Faker<MonetizeViewModel>()
                                    .RuleFor(rentabilizacao => rentabilizacao.AccountId, faker => faker.Random.Int(1, 100000))
                                    .RuleFor(rentabilizacao => rentabilizacao.OperationId, (byte)OperationType.MONETIZE)
                                    .RuleFor(rentabilizacao => rentabilizacao.Tax, 0.01M)
                                    .Generate();

            return rentabilizacao;
        }

        public void Dispose()
        {
        }
    }
}
