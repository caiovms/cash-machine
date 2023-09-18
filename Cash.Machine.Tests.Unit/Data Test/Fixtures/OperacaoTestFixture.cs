using Bogus;
using Cash.Machine.Application.DTO;
using Cash.Machine.Domain.Entities;
using System;
using Xunit;

namespace Cash.Machine.Tests.Unit.DataTest.Fixtures
{
    [CollectionDefinition(Collections.Collections.OperacaoCollection)]
    public class OperacaoCollection : ICollectionFixture<OperacaoTestFixture>
    { }
    public class OperacaoTestFixture : IDisposable
    {
        public int GerarOperacaoValida()
        {
            var operacao = new Faker<OperationDTO>()
                               .RuleFor(operacao => operacao.Id, faker => faker.Random.Int(1, 4))
                               .Generate();

            return operacao.Id;
        }

        public Operation GerarOperacaoInvalida()
        {
            return null;
        }

        public Operation GerarOperacaoPorId(int idOperacao)
        {
            var operacao = new Faker<Operation>();

            switch (idOperacao)
            {
                case (int)OperationType.WITHDRAW:
                    operacao.RuleFor(operacao => operacao.Id, idOperacao);
                    operacao.RuleFor(operacao => operacao.Description, "SAQUE")
                   .Generate();
                    break;

                case (int)OperationType.DEPOSIT:
                    operacao.RuleFor(operacao => operacao.Id, idOperacao);
                    operacao.RuleFor(operacao => operacao.Description, "DEPOSITO")
                   .Generate();
                    break;

                case (int)OperationType.PAYMENT:
                    operacao.RuleFor(operacao => operacao.Id, idOperacao);
                    operacao.RuleFor(operacao => operacao.Description, "PAGAMENTO")
                   .Generate();
                    break;

                case (int)OperationType.MONETIZE:
                    operacao.RuleFor(operacao => operacao.Id, idOperacao);
                    operacao.RuleFor(operacao => operacao.Description, "RENTABILIZACAO")
                   .Generate();
                    break;
            }

            return operacao;
        }

        public void Dispose()
        {
        }
    }
}
