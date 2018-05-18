using Couchbase.Lite;

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
