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
            CreateMap<Match, MatchFootball>();
            CreateMap<Player, PlayerFootball>();
            CreateMap<Referee, RefereeFootball>();
        }
    }
}
