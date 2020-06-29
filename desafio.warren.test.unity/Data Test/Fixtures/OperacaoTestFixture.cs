using Bogus;
using desafio.warren.application.dto;
using desafio.warren.domain.Entities;
using System;
using Xunit;

namespace desafio.warren.test.unity.DataTest.Fixtures
{
    [CollectionDefinition(Collections.Collections.OperacaoCollection)]
    public class OperacaoCollection : ICollectionFixture<OperacaoTestFixture>
    { }
    public class OperacaoTestFixture : IDisposable
    {
        public int GerarOperacaoValida()
        {
            var operacao = new Faker<OperacaoDTO>()
                               .RuleFor(operacao => operacao.Id, faker => faker.Random.Int(1, 4))
                               .Generate();

            return operacao.Id;
        }

        public Operacao GerarOperacaoInvalida()
        {
            return null;
        }

        public Operacao GerarOperacaoPorId(int idOperacao)
        {
            var operacao = new Faker<Operacao>();

            switch (idOperacao)
            {
                case (int)TipoOperacao.SAQUE:
                    operacao.RuleFor(operacao => operacao.Id, idOperacao);
                    operacao.RuleFor(operacao => operacao.Descricao, "SAQUE")
                   .Generate();
                    break;

                case (int)TipoOperacao.DEPOSITO:
                    operacao.RuleFor(operacao => operacao.Id, idOperacao);
                    operacao.RuleFor(operacao => operacao.Descricao, "DEPOSITO")
                   .Generate();
                    break;

                case (int)TipoOperacao.PAGAMENTO:
                    operacao.RuleFor(operacao => operacao.Id, idOperacao);
                    operacao.RuleFor(operacao => operacao.Descricao, "PAGAMENTO")
                   .Generate();
                    break;

                case (int)TipoOperacao.RENTABILIZACAO:
                    operacao.RuleFor(operacao => operacao.Id, idOperacao);
                    operacao.RuleFor(operacao => operacao.Descricao, "RENTABILIZACAO")
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
