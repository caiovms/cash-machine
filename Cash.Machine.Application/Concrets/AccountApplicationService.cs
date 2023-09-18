using AutoMapper;
using Cash.Machine.Application.Abstracts;
using Cash.Machine.Application.DTO;
using Cash.Machine.Domain.Core.Abstracts.Services;
using Cash.Machine.Domain.Entities;
using System.Collections.Generic;

namespace Cash.Machine.Application.Concrets
{
    public class AccountApplicationService : IAccountApplicationService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountApplicationService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public IEnumerable<AccountDTO> List()
        {
            var accounts = _accountService.List();

            return _mapper.Map<List<AccountDTO>>(accounts);
        }

        public AccountDTO Get(int accountId)
        {
            var account = _accountService.Get(accountId);

            return _mapper.Map<AccountDTO>(account);
        }

        public void Add(AccountDTO accountDTO)
        {
            var account = _mapper.Map<Account>(accountDTO);

            _accountService.Add(account);
        }

        public void Update(AccountDTO accountDTO)
        {
            var account = _mapper.Map<Account>(accountDTO);

            _accountService.Update(account);
        }

        public void Delete(int accountId)
        {
            _accountService.Delete(accountId);
        } 
    }
}