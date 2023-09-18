using Cash.Machine.Domain.Entities;

namespace Cash.Machine.Domain.Core.Abstracts.Services
{
    public interface IMovementService : IServiceBase<Movement>
    {
        void Withdraw(int accountId, int operationId, decimal amount);
        void Deposit(int accountId, int operationId,  decimal amount);
        void Payment(int accountId, int operationId, decimal amount, string barCode);
        void Monetize(int accountId, int operationId, decimal tax);
    }
}