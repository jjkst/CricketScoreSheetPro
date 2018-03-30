using Couchbase.Lite;
using CricketScoreSheetPro.Core;
using System;
using System.IO;

namespace CricketScoreSheetPro.Test
{
    public class TestClient : IClient
    {
        private Database _database;
        private string _uuid;

        public Database GetDatabase()
        {
            if(_database == null)
            {
                var manager = new Manager(new DirectoryInfo(Environment.CurrentDirectory.ToLower()), Manager.DefaultOptions);
                _database = manager.GetDatabase("testdb");
            }
            return _database;
        }

        public string GetUUID()
        {
            if (string.IsNullOrEmpty(_uuid)) _uuid = "UUID";
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
