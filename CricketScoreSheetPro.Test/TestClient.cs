using Couchbase.Lite;
using CricketScoreSheetPro.Core;

namespace CricketScoreSheetPro.Test
{
    public class TestClient : IClient
    {
        private Database _database;
        private string _uuid;

        public Database GetDatabase()
        {
            if (_database == null)
            {
                Couchbase.Lite.Support.NetDesktop.Activate();
                var config = new DatabaseConfiguration();
                _database = new Database("testdb", config);
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
