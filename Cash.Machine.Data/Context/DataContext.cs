using Cash.Machine.Data.EntitiesMappings;
using Cash.Machine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cash.Machine.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Movement> Movements { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AccountMap());
            builder.ApplyConfiguration(new MovementMap());
            builder.ApplyConfiguration(new OperationMap());
        }
    }
}