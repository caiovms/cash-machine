using AutoMapper;
using Cash.Machine.Cross.Cutting.Map;

namespace Cash.Machine.Cross.Cutting
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
