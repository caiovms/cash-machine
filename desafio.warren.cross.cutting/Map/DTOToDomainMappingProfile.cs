using AutoMapper;
using desafio.warren.application.dto;
using desafio.warren.domain.Entities;

namespace desafio.warren.cross.cutting.Map
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<ContaDTO, Conta>();
            CreateMap<MovimentoDTO, Movimento>();
            CreateMap<OperacaoDTO, Operacao>();
        }
    }
}
