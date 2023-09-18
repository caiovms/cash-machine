using Cash.Machine.Data.Context;
using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cash.Machine.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly DataContext _dataContext;

        public AccountRepository(DataContext dataContext) 
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public override List<Account> List()
        {
            return _dataContext.Accounts.Include(c => c.Movements).ToList();
        }

        public override Account Get(int accountId)
        {
            return _dataContext.Accounts.Include(c => c.Movements).SingleOrDefault(c => c.Id == accountId);
        }
    }
}