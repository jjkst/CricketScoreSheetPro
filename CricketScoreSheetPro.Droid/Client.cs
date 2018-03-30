using Couchbase.Lite;
using CricketScoreSheetPro.Core;
using System;
using System.IO;

namespace CricketScoreSheetPro.Droid
{
    public class Client : IClient
    {
        private Database _database;
        private string _uuid;

        public Database GetDatabase()
        {
            if (_database == null)
            {
                var manager = new Manager(new DirectoryInfo(Helper.InternalPath), Manager.DefaultOptions);
                _database = manager.GetDatabase("cricketscoresheetprodb");
            }
            return _database;
        }

        public string GetUUID()
        {
            if (string.IsNullOrEmpty(_uuid)) _uuid = Singleton.Instance.UniqueUserId;
            return _uuid;
        }

        public void SetDatabase(Database database)
        {
            this._database = database;
        }

        public void SetUUID(string uuid)
        {
            this._uuid = uuid;
        }
        
    }
}