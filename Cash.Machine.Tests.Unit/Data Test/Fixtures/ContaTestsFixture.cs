using Bogus;
using Cash.Machine.Application.DTO;
using Cash.Machine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cash.Machine.Tests.Unit.DataTest.Fixtures
{
    [CollectionDefinition(Collections.Collections.ContaCollection)]
    public class ContaCollection : ICollectionFixture<ContaTestsFixture>
    { }

    public class ContaTestsFixture : IDisposable
    {   
        public MovimentoTestsFixture movimentoFixture = new MovimentoTestsFixture();

        public  List<Account> GerarContas(int qtd)
        {
            var contas = new List<Account>();

            for (int i = 1; i <= qtd; i++)
            {
                var conta = new Faker<Account>()
                            .RuleFor(conta => conta.Id, fake => i)
                            .RuleFor(conta => conta.Agency, fake => fake.Random.String2(3, 3, "0123456789"))
                            .RuleFor(conta => conta.Type, fake => fake.Random.String2(2, 2, "0123"))
                            .RuleFor(conta => conta.Number, fake => fake.Random.String2(8, 8, "0123456789"))
                            .RuleFor(conta => conta.Digit, fake => fake.Random.Char('0', '9'))
                            .RuleFor(conta => conta.Balance, 5000)
                            .RuleFor(conta => conta.CreatedOn, DateTime.Now)
                            .Generate();

                conta.Movements.Add(movimentoFixture.GerarMovimentos(i, 1).FirstOrDefault());

                contas.Add(conta);
            }

            return contas;
        }

        public Account GerarContaInvalida()
        {
            return null;
        }

        public List<AccountDTO> GerarContasDTO(int qtd)
        {
            var contas = new List<AccountDTO>();

            for (int i = 1; i <= qtd; i++)
            {
                var conta = new Faker<AccountDTO>()
                            .RuleFor(conta => conta.Id, fake => i)
                            .RuleFor(conta => conta.Agency, fake => fake.Random.String2(3, 3, "0123456789"))
                            .RuleFor(conta => conta.Type, fake => fake.Random.String2(2, 2, "0123"))
                            .RuleFor(conta => conta.Number, fake => fake.Random.String2(8, 8, "0123456789"))
                            .RuleFor(conta => conta.Digit, fake => fake.Random.Char('0', '9'))
                            .RuleFor(conta => conta.Balance, 5000)
                            .RuleFor(conta => conta.CreatedOn, DateTime.Now)
                            .Generate();

                conta.Movements.Add(movimentoFixture.GerarMovimentosDTO(i, 1).FirstOrDefault());

                contas.Add(conta);
            }

            return contas;
        }
        
        public void Dispose()
        {
        }
    }
}
