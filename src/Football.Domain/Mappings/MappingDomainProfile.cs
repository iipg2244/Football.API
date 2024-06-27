namespace Football.Infrastructure.Mappings
{
    using AutoMapper;
    using Football.Domain.Entities.Exemples;
    using Football.Domain.Entities.Football;

    public class MappingDomainProfile : Profile
    {
        public MappingDomainProfile()
        {
            CreateMap<ManagerFootballExemple, ManagerFootball>();
            CreateMap<ManagerFootball, ManagerFootballExemple>();
            CreateMap<MatchFootballExemple, MatchFootball>();
            CreateMap<MatchFootball, MatchFootballExemple>();
            CreateMap<PlayerFootballExemple, PlayerFootball>();
            CreateMap<PlayerFootball, PlayerFootballExemple>();
            CreateMap<RefereeFootballExemple, RefereeFootball>();
            CreateMap<RefereeFootball, RefereeFootballExemple>();
            CreateMap<PlayerMatchAwayFootballExemple, PlayerMatchAwayFootball>();
            CreateMap<PlayerMatchAwayFootball, PlayerMatchAwayFootballExemple>();
            CreateMap<PlayerMatchHouseFootballExemple, PlayerMatchHouseFootball>();
            CreateMap<PlayerMatchHouseFootball, PlayerMatchHouseFootballExemple>();
        }
    }
}
