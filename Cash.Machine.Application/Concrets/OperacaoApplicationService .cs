using AutoMapper;
using Cash.Machine.Application.Abstracts;
using Cash.Machine.Application.DTO;
using Cash.Machine.Domain.Core.Abstracts.Services;
using Cash.Machine.Domain.Entities;
using System.Collections.Generic;

namespace Cash.Machine.Application.Concrets
{
    public class OperationApplicationService : IOperationApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IOperationService _operationService;

        public OperationApplicationService(IOperationService operationService, IMapper mapper)
        {
            _mapper = mapper;
            _operationService = operationService;
        }

        public IEnumerable<OperationDTO> List()
        {
            var operations = _operationService.List();

            return _mapper.Map<List<OperationDTO>>(operations);
        }

        public OperationDTO Get(int operationId)
        {
            var operation = _operationService.Get(operationId);

            return _mapper.Map<OperationDTO>(operation);
        }

        public void Add(OperationDTO operationDTO)
        {
            var operation = _mapper.Map<Operation>(operationDTO);

            _operationService.Add(operation);
        }

        public void Update(OperationDTO operationDTO)
        {
            var operation = _mapper.Map<Operation>(operationDTO);

            _operationService.Update(operation);
        }

        public void Delete(int operationId)
        {
            _operationService.Delete(operationId);
        } 
    }
}