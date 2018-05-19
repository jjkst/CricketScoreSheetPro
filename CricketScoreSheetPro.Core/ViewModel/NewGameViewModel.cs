using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Implementation;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class NewGameViewModel
    {
        private readonly bool _isTournament;        

        private readonly IDataSeedService<Match> _matchService;

        private readonly IDataSeedService<Team> _teamService;

        private readonly IDataSeedService<Location> _locationService;

        private readonly IDataSeedService<Umpire> _umpireService;

        private readonly IClient _client;

        public List<Team> Teams => _teamService.GetList().ToList();

        public List<Location> Locations => _locationService.GetList().ToList();

        public List<Umpire> Umpires => _umpireService.GetList().ToList();

        public NewGameViewModel(IClient client, IDataSeedService<Match> matchService, IDataSeedService<Team> teamService, 
            IDataSeedService<Location> locationService, IDataSeedService<Umpire> umpireService, bool isTournament = false)
        {
            this._client = client;
            this._matchService = matchService;
            this._teamService = teamService;
            this._locationService = locationService;
            this._umpireService = umpireService;
            this._isTournament = isTournament;
        }  

        public string AddUmpire(string umpirename)
        {
            return _umpireService.Create(new Umpire
            {
                Name = umpirename,
                AddDate = DateTime.Now
            });
        }

        public string AddLocation(string locationname)
        {
            return _locationService.Create(new Location
            {
                Name = locationname,
                AddDate = DateTime.Now
            });
        }

        public string AddTeam(string teamName)
        {
            var newteam = _teamService.Create(new Team
            {
                Name = teamName,
                AddDate = DateTime.Today
            });
            return newteam;
        }

        private IDataSeedService<Tournament> _tournamentService;

        public List<Tournament> Tournaments(IDataSeedService<Tournament> tournamentService)
        {
            this._tournamentService = tournamentService;
            return _tournamentService.GetList().ToList();
        }

        public Match AddMatch(string hometeamid, string awayteamid, string oversOrTournamentId, string location, 
            string primaryumpire, string secondaryumpire)
        {
            var hometeam = _teamService.GetItem(hometeamid);
            if (hometeam == null) throw new NullReferenceException("Home team name is not added");
            var awayteam = _teamService.GetItem(awayteamid);
            if (awayteam == null) throw new NullReferenceException("Away team name is not added");

            int overs;
            string tournamentId = string.Empty;

            if (_isTournament)
            {
                tournamentId = oversOrTournamentId;
                overs = _tournamentService.GetItem(tournamentId).TotalOvers;
            }
            else
                overs = int.Parse(oversOrTournamentId);

            var match = new Match
            {
                HomeTeam = new TeamInning(),
                AddDate = DateTime.Now,
                AwayTeam = new TeamInning(),
                Location = location,
                PrimaryUmpire = primaryumpire,
                SecondaryUmpire = secondaryumpire,
                TotalOvers = overs
            };

            var matchId = _matchService.Create(match);

            var matchIdKey = new KeyValuePair<string, string>("matchId", matchId);
            var tournamentIdKey = new KeyValuePair<string, string>("tournamentId", tournamentId);

            // Creating team innings and player innings and update match
            var teamInningService = new DataSeedService<TeamInning>(_client);
            var playerInningService = new DataSeedService<PlayerInning>(_client);

            var hometeaminningId = teamInningService.Create(new TeamInning
            {
                TeamName = hometeam.Name,
            }, new KeyValuePair<string, string>[]
                {
                    matchIdKey, tournamentIdKey,
                    new KeyValuePair<string, string>("teamId", hometeam.Id)
                });

            foreach (var homeplayer in hometeam.Players)
            {
                var homeplayerinningId = playerInningService.Create(new PlayerInning
                {
                    PlayerName = homeplayer.Name,
                }, new KeyValuePair<string, string>[]
                    {
                        matchIdKey, tournamentIdKey,
                        new KeyValuePair<string, string>("teamId", hometeam.Id),
                        new KeyValuePair<string, string>("playerId", homeplayer.Id)
                    });
                var d = playerInningService.GetItem(homeplayerinningId);
            }

            var awayteaminningId = teamInningService.Create(new TeamInning
            {
                TeamName = awayteam.Name,
            }, new KeyValuePair<string, string>[]
                {
                    matchIdKey, tournamentIdKey,
                    new KeyValuePair<string, string>("teamId", awayteam.Id)
                });

            foreach (var awayplayer in awayteam.Players)
            {
                var awayplayerinningId = playerInningService.Create(new PlayerInning
                {
                    PlayerName = awayplayer.Name,
                }, new KeyValuePair<string, string>[]
                    {
                        matchIdKey, tournamentIdKey,
                        new KeyValuePair<string, string>("teamId", awayteam.Id),
                        new KeyValuePair<string, string>("playerId", awayplayer.Id)
                    });
            }

            //Update Match
            var updatematch = _matchService.GetItem(matchId);
            updatematch.HomeTeam = teamInningService.GetItem(hometeaminningId);
            updatematch.AwayTeam = teamInningService.GetItem(awayteaminningId);
            _matchService.Update(matchId, updatematch);

            return updatematch;
        }
    }
}
