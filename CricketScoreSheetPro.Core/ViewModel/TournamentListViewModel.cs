using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.ViewModel
{
    public class TournamentListViewModel
    {
        private readonly ITournamentService _tournamentService;
        private readonly IAccessService _accessService;

        public TournamentListViewModel(ITournamentService tournamentService, IAccessService accessService)
        {
            _tournamentService = tournamentService ?? throw new ArgumentNullException($"TournamentService is null, cannot get tournaments.");
            _accessService = accessService ?? throw new ArgumentNullException($"AccessService is null, cannot get tournaments.");
        }

        public List<Tournament> Tournaments => _tournamentService.GetTournamentList().ToList();

        public List<Tournament> ImportedTournaments()
        {
            var access = _accessService.GetAccessList().LastOrDefault(a => a.DocumentType == nameof(Tournament));
            var importedTournaments = new List<Tournament>();
            foreach(var t in access.Documents)
            {
                importedTournaments.Add(_tournamentService.GetTournament(t.Id));
            }
            return importedTournaments;
        }

        public string AddTournament(string tournamentName)
        {
            var newtournament = _tournamentService.AddTournament(new Tournament
            {
                Name = tournamentName,
                Status = "Open",
                StartDate = DateTime.Today
            });
            return newtournament;
        }

        public void DeleteTournament(string id)
        {
            _tournamentService.DeleteTournament(id);
        }

        public string AddAccess(string tournamentId, AccessType accessType)
        {
            var tournamentaccess = _accessService.AddAccess(new Access
            {
                DocumentType = nameof(Tournament),
                Documents = new List<DocumentReference>
                {
                    new DocumentReference
                    {
                        AccessType = accessType,
                        Id = tournamentId
                    }
                }
            });

            return tournamentaccess;
        }
    }
}
