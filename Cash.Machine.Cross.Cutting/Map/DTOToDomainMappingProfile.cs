using AutoMapper;
using Cash.Machine.Application.DTO;
using Cash.Machine.Domain.Entities;

namespace Cash.Machine.Cross.Cutting.Map
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<AccountDTO, Account>();
            CreateMap<MovementDTO, Movement>();
            CreateMap<OperationDTO, Operation>();
        }
    }
}