namespace Football.Infrastructure.Mappings
{
    using AutoMapper;
    using Football.Domain.Entities.Football;
    using Football.Infrastructure.Entities;

    public class  MappingInfrastructureProfile : Profile
    {
        public MappingInfrastructureProfile()
        {
            CreateMap<Manager, ManagerFootball>();
            CreateMap<ManagerFootball, Manager>();
            CreateMap<Match, MatchFootball>();
            CreateMap<MatchFootball, Match>();
            CreateMap<Player, PlayerFootball>();
            CreateMap<PlayerFootball, Player>();
            CreateMap<Referee, RefereeFootball>();
            CreateMap<RefereeFootball, Referee>();
            CreateMap<PlayerMatchAway, PlayerMatchAwayFootball>();
            CreateMap<PlayerMatchAwayFootball, PlayerMatchAway>();
            CreateMap<PlayerMatchHouse, PlayerMatchHouseFootball>();
            CreateMap<PlayerMatchHouseFootball, PlayerMatchHouse>();
        }
    }
}
