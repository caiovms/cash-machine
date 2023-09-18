using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Domain.Core.Abstracts.Services;
using Cash.Machine.Domain.Entities;
using System;

namespace Cash.Machine.Services.Services
{
    public class MovementService : ServiceBase<Movement>, IMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IOperationRepository _operationRepository;

        public MovementService(IOperationRepository operationRepository,
                               IAccountRepository accountRepository,
                               IMovementRepository movementRepository)
            : base(movementRepository)
        {
            _movementRepository = movementRepository;
            _accountRepository = accountRepository;
            _operationRepository = operationRepository;
        }

        public void Withdraw(int accountId, int operationId, decimal amount)
        {
            var account = _accountRepository.Get(accountId);

            ValidateOperation(account, operationId, amount, null);

            CreateMovement(account, amount, operationId, null);
        }

        public void Deposit(int accountId, int operationId, decimal amount)
        {
            var account = _accountRepository.Get(accountId);

            ValidateOperation(account, operationId, amount, null);

            CreateMovement(account, amount, operationId, null);
        }

        public void Payment(int accountId, int operationId, decimal amount, string barCode)
        {
            var conta = _accountRepository.Get(accountId);

            ValidateOperation(conta, operationId, amount, barCode);

            CreateMovement(conta, amount, operationId, barCode);
        }

        public void Monetize(int accountId, int operationId, decimal tax)
        {
            var account = _accountRepository.Get(accountId);

            ValidateOperation(account, operationId, amount: null, barCode: null);

            if (account.Balance > decimal.Zero)
            {
                var income = account.Balance * tax;

                CreateMovement(account, income, operationId, barCode: null);
            }
        }

        private void ValidateOperation(Account account, int operationId, decimal? amount, string barCode)
        {
            var operation = _operationRepository.Get(operationId);

            if (account == null)
            {
                throw new ApplicationException("Invalid Account.");
            }

            if (operation == null || amount <= decimal.Zero)
            {
                throw new ApplicationException("Invalid Operation.");
            }

            if (operation.Id == (byte)OperationType.PAYMENT && (string.IsNullOrEmpty(barCode) || barCode.Length > 48))
            {
                throw new ApplicationException("Invalid Bar Code.");
            }

            if ((operation.Id == (byte)OperationType.WITHDRAW || operation.Id == (byte)OperationType.PAYMENT) && account.Balance < amount)
            {
                throw new ApplicationException("Insuficient Balance.");
            }
        }

        private void CreateMovement(Account account, decimal amount, int operationId, string barCode)
        {
            try
            {
                var movementAmount = UpdateBalance(account, amount, operationId);

                var movement = new Movement
                {
                    AccountId = account.Id,
                    OperationId = operationId,
                    Amount = movementAmount,
                    Date = DateTime.Now,
                    BarCode = barCode
                };

                _movementRepository.Add(movement);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal UpdateBalance(Account account, decimal amount, int operationId)
        {
            switch (operationId)
            {
                case (int)OperationType.WITHDRAW:
                case (int)OperationType.PAYMENT:
                    account.Balance = account.Balance - amount;
                    amount = -Math.Abs(amount);
                    break;

                case (int)OperationType.MONETIZE:
                case (int)OperationType.DEPOSIT:
                    account.Balance = account.Balance + amount;
                    break;
            }

            _accountRepository.Update(account);

            return amount;
        }
    }
}