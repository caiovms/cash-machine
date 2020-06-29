using desafio.warren.data.EntitiesMappings;
using desafio.warren.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace desafio.warren.data.Context
{
    public class WarrenContext : DbContext
    {
        public WarrenContext() { }

        public WarrenContext(DbContextOptions<WarrenContext> options) : base(options) { }

        public virtual DbSet<Conta> Contas { get; set; }
        public virtual DbSet<Movimento> Movimentos { get; set; }
        public virtual DbSet<Operacao> Operacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContaMap());
            builder.ApplyConfiguration(new MovimentoMap());
            builder.ApplyConfiguration(new OperacaoMap());
        }
    }
}