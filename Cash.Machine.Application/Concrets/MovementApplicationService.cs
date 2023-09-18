using AutoMapper;
using Cash.Machine.Application.Abstracts;
using Cash.Machine.Application.DTO;
using Cash.Machine.Domain.Core.Abstracts.Services;
using Cash.Machine.Domain.Entities;
using System.Collections.Generic;

namespace Cash.Machine.Application.Concrets
{
    public class MovementApplicationService : IMovementApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IMovementService _movementService;

        public MovementApplicationService(IMovementService movementService, IMapper mapper)
        {
            _movementService = movementService;
            _mapper = mapper;
        }

        public void Withdraw(int accountId, int operationId, decimal amount)
        {
            _movementService.Withdraw(accountId, operationId, amount);
        }
        
        public void Deposit(int accountId, int operationId, decimal amount)
        {
            _movementService.Deposit(accountId, operationId, amount);
        }
        
        public void Payment(int accountId, int operationId, decimal amount, string barCode)
        {
            _movementService.Payment(accountId, operationId, amount, barCode);
        }
       
        public void Monetize(int accountId, int operationId, decimal tax)
        {
            _movementService.Monetize(accountId, operationId, tax);
        }

        public IEnumerable<MovementDTO> List()
        {
            var movements = _movementService.List();

            return _mapper.Map<List<MovementDTO>>(movements);
        }

        public MovementDTO Get(int movementId)
        {
            var movement = _movementService.Get(movementId);

            return _mapper.Map<MovementDTO>(movement);
        }

        public void Add(MovementDTO movementDTO)
        {
            var movement = _mapper.Map<Movement>(movementDTO);

            _movementService.Add(movement);
        }

        public void Update(MovementDTO movementDTO)
        {
            var movement = _mapper.Map<Movement>(movementDTO);

            _movementService.Update(movement);
        }

        public void Delete(int movementId)
        {
            _movementService.Delete(movementId);
        }
    }
}
