using AutoMapper;
using desafio.warren.application.dto;
using desafio.warren.domain.Entities;

namespace desafio.warren.cross.cutting.Map
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Conta, ContaDTO>();

            CreateMap<Conta, ContaDTO>();
            CreateMap<Movimento, MovimentoDTO>();
            CreateMap<Operacao, OperacaoDTO>();

        }
    }
}
