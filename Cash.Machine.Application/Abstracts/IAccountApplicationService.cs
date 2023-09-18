using Cash.Machine.Application.DTO;
using System.Collections.Generic;

namespace Cash.Machine.Application.Abstracts
{
    public interface IAccountApplicationService
    {
        IEnumerable<AccountDTO> List();

        AccountDTO Get(int accountId);

        void Add(AccountDTO accountDTO);

        void Update(AccountDTO accountDTO);

        void Delete(int accountId);
    }
}