using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Domain.Core.Abstracts.Services;
using Cash.Machine.Domain.Entities;
using System;

namespace Cash.Machine.Services.Services
{
    public class AccountService : ServiceBase<Account>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository) 
            : base(accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public override Account Get(int accountId)
        {
            var account = _accountRepository.Get(accountId);

            if (account is null)
            {
                throw new ApplicationException("Invalid Account.");
            }

            return account;
        }
    }
}