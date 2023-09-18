using Cash.Machine.Application.DTO;
using System.Collections.Generic;

namespace Cash.Machine.Application.Abstracts
{
    public interface IOperationApplicationService
    {
        IEnumerable<OperationDTO> List();

        OperationDTO Get(int operationId);

        void Add(OperationDTO operationDTO);

        void Update(OperationDTO operationDTO);

        void Delete(int operationId);
    }
}
