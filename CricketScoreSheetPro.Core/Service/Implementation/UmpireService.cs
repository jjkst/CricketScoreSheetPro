using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Interface;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Implementation
{
    public class UmpireService : IUmpireService
    {
        private readonly IRepository<Umpire> _umpireRepository;

        public UmpireService(IRepository<Umpire> umpireRepository)
        {
            _umpireRepository = umpireRepository ?? throw new ArgumentNullException($"UmpireRepository is null");
        }

        public string AddUmpire(string umpirename)
        {
            if (string.IsNullOrEmpty(umpirename)) throw new ArgumentNullException($"Umpire name is empty");
            var newumpireproperties = new Umpire
            {
                Name = umpirename,
                AddDate = DateTime.Today
            };
            var umpireAdd = _umpireRepository.Create(newumpireproperties);
            return umpireAdd;
        }

        public void DeleteUmpire(string umpireId)
        {
            if (string.IsNullOrEmpty(umpireId)) throw new ArgumentException($"Umpire ID is null");
            _umpireRepository.Delete(umpireId);
        }

        public Umpire GetUmpire(string umpireId)
        {
            if (string.IsNullOrEmpty(umpireId)) throw new ArgumentException($"Umpire ID is null");
            var umpire = _umpireRepository.GetItem(umpireId);
            return umpire;
        }

        public IList<Umpire> GetUmpires()
        {
            var umpires = _umpireRepository.GetList();
            return umpires;
        }

        public bool UpdateUmpire(Umpire updateumpire)
        {
            if (updateumpire == null) throw new ArgumentNullException($"UserUmpire is null");
            var updatedtournament = _umpireRepository.Update(updateumpire.Id, updateumpire);
            return updatedtournament;
        }
    }
}
