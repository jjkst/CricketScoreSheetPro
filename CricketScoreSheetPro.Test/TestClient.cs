using Couchbase.Lite;
using CricketScoreSheetPro.Core;
using System;
using System.IO;

namespace CricketScoreSheetPro.Test
{
    public class TestClient : IClient
    {
        private Database Database;
        private string UUID;

        public Database GetDatabase()
        {
            if(Database == null)
            {
                var manager = new Manager(new DirectoryInfo(Environment.CurrentDirectory.ToLower()), Manager.DefaultOptions);
                Database = manager.GetDatabase("testdb");
            }
            return Database;
        }

        public string GetUUID()
        {
            if (string.IsNullOrEmpty(UUID)) UUID = "UUID";
            return UUID;
        }

        public void SetDatabase(Database database)
        {
            Database = database;
        }

        public void SetUUID(string uuid)
        {
            UUID = uuid;
        }
    }
}
