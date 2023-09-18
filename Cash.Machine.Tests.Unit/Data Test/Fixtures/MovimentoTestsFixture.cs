using Bogus;
using Cash.Machine.Application.DTO;
using Cash.Machine.Domain.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cash.Machine.Tests.Unit.DataTest.Fixtures
{
    [CollectionDefinition(Collections.Collections.MovimentoCollection)]
    public class MovimentoCollection : ICollectionFixture<ContaTestsFixture>
    { }
    public class MovimentoTestsFixture : IDisposable
    {
        public List<Movement> GerarMovimentos(int idConta, int qtd)
        {
            var movimentos = new List<Movement>();

            for (int i = 1; i <= qtd; i++)
            {
                var movimento = new Faker<Movement>()
                    .RuleFor(movimento => movimento.Id, fake => fake.Random.Int(1, 100000))
                    .RuleFor(movimento => movimento.AccountId, idConta)
                    .RuleFor(movimento => movimento.OperationId, fake => fake.Random.Int(1, 4))
                    .RuleFor(movimento => movimento.Amount, fake => fake.Random.Int(100, 500))
                    .RuleFor(movimento => movimento.Date, DateTime.Now)
                    .RuleFor(movimento => movimento.BarCode, fake => fake.Random.String2(48, "0123456789"))
                    .Generate();

                movimentos.Add(movimento);
            }

            return movimentos;
        }

        public List<MovementDTO> GerarMovimentosDTO(int idConta, int qtd)
        {
            var movimentos = new List<MovementDTO>();

            for (int i = 1; i <= qtd; i++)
            {
                var movimento = new Faker<MovementDTO>()
                    .RuleFor(movimento => movimento.Id, fake => fake.Random.Int(1, 100000))
                    .RuleFor(movimento => movimento.AccountId, idConta)
                    .RuleFor(movimento => movimento.OperationId, fake => fake.Random.Int(1, 4))
                    .RuleFor(movimento => movimento.Amount, fake => fake.Random.Int(100, 500))
                    .RuleFor(movimento => movimento.BarCode, fake => fake.Random.String2(48, "0123456789"))
                    .RuleFor(movimento => movimento.Date, DateTime.Now)
                    .Generate();

                movimentos.Add(movimento);
            }

            return movimentos;
        }

        public void Dispose()
        {
        }
    }
}