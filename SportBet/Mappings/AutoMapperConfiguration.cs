using AutoMapper;

namespace SportBet.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<AdminProfile>();
                config.AddProfile<AnalyticProfile>();
                config.AddProfile<BetProfile>();
                config.AddProfile<BookmakerProfile>();
                config.AddProfile<ClientProfile>();
                config.AddProfile<CoefficientProfile>();
                config.AddProfile<CountryProfile>();
                config.AddProfile<EventProfile>();
                config.AddProfile<ParticipantProfile>();
                config.AddProfile<SportProfile>();
                config.AddProfile<TournamentProfile>();
            });
        }
    }
}
