using Bogus;
using desafio.warren.application.dto;
using desafio.warren.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace desafio.warren.test.unity.DataTest.Fixtures
{
    [CollectionDefinition(Collections.Collections.ContaCollection)]
    public class ContaCollection : ICollectionFixture<ContaTestsFixture>
    { }

    public class ContaTestsFixture : IDisposable
    {   
        public MovimentoTestsFixture movimentoFixture = new MovimentoTestsFixture();

        public  List<Conta> GerarContas(int qtd)
        {
            var contas = new List<Conta>();

            for (int i = 1; i <= qtd; i++)
            {
                var conta = new Faker<Conta>()
                            .RuleFor(conta => conta.Id, fake => i)
                            .RuleFor(conta => conta.Agencia, fake => fake.Random.String2(3, 3, "0123456789"))
                            .RuleFor(conta => conta.Tipo, fake => fake.Random.String2(2, 2, "0123"))
                            .RuleFor(conta => conta.Numero, fake => fake.Random.String2(8, 8, "0123456789"))
                            .RuleFor(conta => conta.Digito, fake => fake.Random.Char('0', '9'))
                            .RuleFor(conta => conta.Saldo, 5000)
                            .RuleFor(conta => conta.DataCadastro, DateTime.Now)
                            .Generate();

                conta.Movimentos.Add(movimentoFixture.GerarMovimentos(i, 1).FirstOrDefault());

                contas.Add(conta);
            }

            return contas;
        }

        public Conta GerarContaInvalida()
        {
            return null;
        }

        public List<ContaDTO> GerarContasDTO(int qtd)
        {
            var contas = new List<ContaDTO>();

            for (int i = 1; i <= qtd; i++)
            {
                var conta = new Faker<ContaDTO>()
                            .RuleFor(conta => conta.Id, fake => i)
                            .RuleFor(conta => conta.Agencia, fake => fake.Random.String2(3, 3, "0123456789"))
                            .RuleFor(conta => conta.Tipo, fake => fake.Random.String2(2, 2, "0123"))
                            .RuleFor(conta => conta.Numero, fake => fake.Random.String2(8, 8, "0123456789"))
                            .RuleFor(conta => conta.Digito, fake => fake.Random.Char('0', '9'))
                            .RuleFor(conta => conta.Saldo, 5000)
                            .RuleFor(conta => conta.DataCadastro, DateTime.Now)
                            .Generate();

                conta.Movimentos.Add(movimentoFixture.GerarMovimentosDTO(i, 1).FirstOrDefault());

                contas.Add(conta);
            }

            return contas;
        }
        
        public void Dispose()
        {
        }
    }
}
