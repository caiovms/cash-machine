using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Domain.Core.Abstracts.Services;
using Cash.Machine.Domain.Entities;
using System;

namespace Cash.Machine.Services.Services
{
    public class OperationService : ServiceBase<Operation>, IOperationService
    {
        private readonly IOperationRepository _operationRepository;

        public OperationService(IOperationRepository operationRepository) 
            : base(operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public override Operation Get(int operationId)
        {
            var operation = _operationRepository.Get(operationId);

            if (operation == null)
            {
                throw new ApplicationException("Invalid Operation.");
            }

            return operation;
        }
    }
}