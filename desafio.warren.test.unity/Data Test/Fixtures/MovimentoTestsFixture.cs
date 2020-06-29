using Bogus;
using desafio.warren.application.dto;
using desafio.warren.domain.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace desafio.warren.test.unity.DataTest.Fixtures
{
    [CollectionDefinition(Collections.Collections.MovimentoCollection)]
    public class MovimentoCollection : ICollectionFixture<ContaTestsFixture>
    { }
    public class MovimentoTestsFixture : IDisposable
    {
        public List<Movimento> GerarMovimentos(int idConta, int qtd)
        {
            var movimentos = new List<Movimento>();

            for (int i = 1; i <= qtd; i++)
            {
                var movimento = new Faker<Movimento>()
                    .RuleFor(movimento => movimento.Id, fake => fake.Random.Int(1, 100000))
                    .RuleFor(movimento => movimento.IdConta, idConta)
                    .RuleFor(movimento => movimento.IdOperacao, fake => fake.Random.Int(1, 4))
                    .RuleFor(movimento => movimento.Valor, fake => fake.Random.Int(100, 500))
                    .RuleFor(movimento => movimento.Data, DateTime.Now)
                    .RuleFor(movimento => movimento.CodigoBarras, fake => fake.Random.String2(48, "0123456789"))
                    .Generate();

                movimentos.Add(movimento);
            }

            return movimentos;
        }

        public List<MovimentoDTO> GerarMovimentosDTO(int idConta, int qtd)
        {
            var movimentos = new List<MovimentoDTO>();

            for (int i = 1; i <= qtd; i++)
            {
                var movimento = new Faker<MovimentoDTO>()
                    .RuleFor(movimento => movimento.Id, fake => fake.Random.Int(1, 100000))
                    .RuleFor(movimento => movimento.IdConta, idConta)
                    .RuleFor(movimento => movimento.IdOperacao, fake => fake.Random.Int(1, 4))
                    .RuleFor(movimento => movimento.Valor, fake => fake.Random.Int(100, 500))
                    .RuleFor(movimento => movimento.CodigoBarras, fake => fake.Random.String2(48, "0123456789"))
                    .RuleFor(movimento => movimento.Data, DateTime.Now)
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