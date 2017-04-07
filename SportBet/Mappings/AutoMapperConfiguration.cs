using AutoMapper;

namespace SportBet.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<MappingProfile>();
            });
        }
    }
}
