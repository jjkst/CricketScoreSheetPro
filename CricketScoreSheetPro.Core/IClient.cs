using Couchbase.Lite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core
{
    public interface IClient
    {
        string GetUUID();

        void SetUUID(string uuid);

        Database GetDatabase();

        void SetDatabase(Database database);
    }
}
