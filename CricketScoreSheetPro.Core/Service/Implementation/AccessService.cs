using CricketScoreSheetPro.Core.Model;
using CricketScoreSheetPro.Core.Repository.Interface;
using CricketScoreSheetPro.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Service.Implementation
{
    public class AccessService : IAccessService
    {
        private readonly IRepository<Access> _accessRepository;

        public AccessService(IRepository<Access> accessRepository)
        {
            _accessRepository = accessRepository ?? throw new ArgumentNullException($"accessRepository is null");
        }

        public string AddAccess(Access newAccess)
        {
            if(newAccess == null) throw new ArgumentNullException($"newAccess name is null");
            var accessId = _accessRepository.Create(newAccess);
            return accessId;
        }

        public void DeleteAccess(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Access ID is null");
            _accessRepository.Delete(id);
        }

        public IList<Access> GetAccessList()
        {
            var accessList = _accessRepository.GetList();
            return accessList;
        }

        public Access GetAccess(string accessId)
        {
            if (string.IsNullOrEmpty(accessId)) throw new ArgumentException($"Access ID is null");
            var access = _accessRepository.GetItem(accessId);
            if(access == null) throw new ArgumentException($"Document does not exist.");
            return access;
        }

        public bool UpdateAccess(Access access)
        {
            if (access == null) throw new ArgumentException($"Access is null");
            return _accessRepository.Update(access.Id, access);
        }
    }
}
