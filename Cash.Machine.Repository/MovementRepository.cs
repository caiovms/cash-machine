using Cash.Machine.Data.Context;
using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Domain.Entities;

namespace Cash.Machine.Repository
{
    public class MovementRepository : BaseRepository<Movement>, IMovementRepository
    {
        private readonly DataContext _dataContext;

        public MovementRepository(DataContext dataContext) 
            : base(dataContext)
        {
            _dataContext = dataContext;
        } 
    }
}