using Cash.Machine.Data.Context;
using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cash.Machine.Repository
{
    public class OperationRepository : BaseRepository<Operation>, IOperationRepository
    {
        private readonly DataContext _dataContext;

        public OperationRepository(DataContext dataContext) 
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public override Operation Get(int operationId)
        {
            return _dataContext.Operations.Include(c => c.Movements).SingleOrDefault(c => c.Id == operationId);
        }
    }
}