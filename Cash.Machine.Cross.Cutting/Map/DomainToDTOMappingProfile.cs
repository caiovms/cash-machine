using AutoMapper;
using Cash.Machine.Application.DTO;
using Cash.Machine.Domain.Entities;

namespace Cash.Machine.Cross.Cutting.Map
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Account, AccountDTO>();
            CreateMap<Movement, MovementDTO>();
            CreateMap<Operation, OperationDTO>();
        }
    }
}
