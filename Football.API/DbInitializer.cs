using Football.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Football.API
{
    public static class DbInitializer
    {
        public static void Initialize(FootballContext context)
        {
            context.Database.EnsureCreated();

            if (context.Players.Any())
                return;

            context.Players.AddRange(new Player[]
            {
                new Player{ Name = "Lionel" },
                new Player{ Name = "Cristiano" },
                new Player{ Name = "Iker" },
                new Player{ Name = "Gerard" },
                new Player{ Name = "Philippe" },
                new Player{ Name = "Jordi" }
            });
            context.SaveChanges();

            context.Managers.AddRange(new Manager[]
            {
                new Manager { Name = "Alex" },
                new Manager { Name = "Zidane" },
                new Manager { Name = "Guardiola" }
            });
            context.SaveChanges();

            context.Referees.AddRange(new Referee[]
            {
                new Referee { Name = "Pierluigi" },
                new Referee { Name = "Howard" }
            });
            context.SaveChanges();

            var matches = new List<Match>();
            var referees = context.Referees.ToList();
            if (referees?.Count > 0)
            {
                foreach (var referee in referees)
                {
                    var housemanagers = context.Managers.ToList();
                    if (housemanagers?.Count >= 2)
                    {
                        foreach (var housemanager in housemanagers)
                        {
                            var awaymanager = context.Managers.Where(x => x.Id != housemanager.Id).FirstOrDefault();
                            if (awaymanager != null)
                            {
                                matches.Add(new Match()
                                {
                                    RefereeId = referee.Id,
                                    Referee = null,
                                    HouseManagerId = housemanager.Id,
                                    HouseManager = null,
                                    AwayManagerId = awaymanager.Id,
                                    AwayManager = null
                                });
                            }
                        }
                    }
                }
            }
            if (matches?.Count > 0)
            {
                context.Matches.AddRange(matches);
                context.SaveChanges();

                matches = context.Matches.ToList();
                var players = context.Players.ToList();

                Random random = new Random();
                int length = 6;
                if ((length % 2) == 0 && players?.Count >= length)
                {
                    var playerhouses = new List<PlayerMatchHouse>();
                    var playeraways = new List<PlayerMatchAway>();
                    foreach (var match in matches)
                    {
                        int[] symbols = players.Select(x => x.Id).ToArray();
                        bool[] noRepeat = new bool[symbols.Length + 1];
                        int i = 0;
                        while (i < length)
                        {
                            int indexRandom = random.Next(symbols.Length);
                            if (!noRepeat[indexRandom])
                            {
                                if (i < (length / 2))
                                {
                                    playerhouses.Add(new PlayerMatchHouse()
                                    {
                                        MatchId = match.Id,
                                        Match = null,
                                        PlayerId = players.ElementAt(indexRandom).Id,
                                        Player = null
                                    });
                                }
                                else
                                {
                                    playeraways.Add(new PlayerMatchAway()
                                    {
                                        MatchId = match.Id,
                                        Match = null,
                                        PlayerId = players.ElementAt(indexRandom).Id,
                                        Player = null
                                    });
                                }
                                noRepeat[indexRandom] = true;
                                i++;
                            }
                        }
                    }
                    context.PlayerHouses.AddRange(playerhouses);
                    context.PlayerAways.AddRange(playeraways);
                    context.SaveChanges();
                }    
            }
         
        }
    }
}
