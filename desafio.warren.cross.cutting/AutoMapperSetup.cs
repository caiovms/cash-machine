using AutoMapper;
using desafio.warren.cross.cutting.Map;

namespace desafio.warren.cross.cutting
{
    public class AutoMapperSetup
    {
       public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(autoMapperConfig =>
            {
                autoMapperConfig.AddProfile(new DTOToDomainMappingProfile());
                autoMapperConfig.AddProfile(new DomainToDTOMappingProfile());
            });
        }
    }
}
