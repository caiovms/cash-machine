using Cash.Machine.Application.DTO;
using System.Collections.Generic;

namespace Cash.Machine.Application.Abstracts
{
    public interface IMovementApplicationService
    {
        void Withdraw(int accountId, int operationId, decimal amount);
        
        void Deposit(int accountId, int operationId, decimal amount);
        
        void Payment(int accountId, int operationId, decimal amount, string barCode);
        
        void Monetize(int accountId, int operationId, decimal tax);

        IEnumerable<MovementDTO> List();
        
        MovementDTO Get(int operationId);
        
        void Add(MovementDTO movementDTO);
        
        void Update(MovementDTO movementDTO);
        
        void Delete(int operationId);
    }
}