using Couchbase.Lite;
using CricketScoreSheetPro.Core;
using System;
using System.IO;

namespace CricketScoreSheetPro.Droid
{
    public class Client : IClient
    {
        private Database Database;
        private string UUID;

        public Database GetDatabase()
        {
            return Database;
        }

        public string GetUUID()
        {
            return UUID;
        }

        public void SetDatabase(Database database)
        {
            var manager = new Manager(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToLower()), Manager.DefaultOptions);
            Database = manager.GetDatabase("cricketscoresheetprodb");
        }

        public void SetUUID(string uuid)
        {
            UUID = Singleton.Instance.UniqueUserId;
        }
    }
}