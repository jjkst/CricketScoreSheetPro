using Couchbase.Lite;
using Couchbase.Lite.Query;
using CricketScoreSheetPro.Core.Service.Interface;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Service.Implementation
{
    public class DataSeedService<T> : IDataSeedService<T>
    {
        private Database Database;
        internal string UUID;

        public DataSeedService(IClient client)
        {
            if (client == null) throw new ArgumentNullException("Database client is not set.");
            Database = client.GetDatabase() ?? throw new ArgumentNullException("Database is not set.");
            UUID = client.GetUUID();
            if(string.IsNullOrEmpty(UUID)) throw new ArgumentException("UUID is not set.");
        }

        public virtual string Create(T obj)
        {
            var mutableDoc = new MutableDocument();
            mutableDoc.SetString("uuid", UUID);
            mutableDoc.SetString("type", typeof(T).Name);                       
            Database.Save(mutableDoc);

            //Update Id
            var objwithId = Helper.Function.UpdateGenericObjectProperty(obj, mutableDoc.Id);
            mutableDoc.SetValue("value", JsonConvert.SerializeObject(objwithId));
            Database.Save(mutableDoc);
            return mutableDoc.Id;
        }

        public virtual string Create(T obj, params KeyValuePair<string, string>[] pairs)
        {
            var mutableDoc = new MutableDocument();
            mutableDoc.SetString("uuid", UUID);
            mutableDoc.SetString("type", typeof(T).Name);
            foreach(var pair in pairs)
                mutableDoc.SetString(pair.Key, pair.Value);
            Database.Save(mutableDoc);

            //Update Id
            var objwithId = Helper.Function.UpdateGenericObjectProperty(obj, mutableDoc.Id);
            mutableDoc.SetValue("value", JsonConvert.SerializeObject(objwithId));
            Database.Save(mutableDoc);
            return mutableDoc.Id;
        }

        public virtual void Delete(string id)
        {
            var document = Database.GetDocument(id);
            if (document == null) throw new ArgumentNullException($"Document does not exist.");
            Database.Delete(document);
        }

        public virtual T GetItem(string id)
        {
            var document = Database.GetDocument(id);
            if (document == null) throw new ArgumentNullException($"Document does not exist.");
            var rawvalue = document.ToMutable().GetValue("value");
            if (string.IsNullOrEmpty(rawvalue.ToString())) return default(T);
            var result = JsonConvert.DeserializeObject<T>(rawvalue.ToString());
            return result;
        }

        public virtual IList<T> GetList()
        {
            var query = QueryBuilder.Select(SelectResult.Property("value"))
                .From(DataSource.Database(Database))
                .Where(Expression.Property("uuid").EqualTo(Expression.String(UUID))
                    .And(Expression.Property("type").EqualTo(Expression.String(typeof(T).Name))));

            var result = new List<T>();
            foreach (var row in query.Execute())
            {
                var rawvalue = row.GetValue("value");
                result.Add(JsonConvert.DeserializeObject<T>(rawvalue.ToString()));
            }
            return result;
        }

        public virtual bool Update(string id, T obj)
        {
            bool result;
            try
            {
                var document = Database.GetDocument(id);
                var mutableDoc = document.ToMutable();
                mutableDoc.SetValue("value", JsonConvert.SerializeObject(obj));
                Database.Save(mutableDoc);
                result = true;
            }
            catch(Exception e)
            {
                result = false;
            }
            return result;
        }

        public void DeleteDatabase()
        {
            var query = QueryBuilder.Select(SelectResult.Property("_id")).From(DataSource.Database(Database));
            foreach (var row in query.Execute())
            {
                var document = Database.GetDocument(row["_id"].ToString());
                Database.Delete(document);
            }
        }

        public IList<T> GetFilteredList(IExpression filter)
        {
            var query = QueryBuilder.Select(SelectResult.Property("value")).From(DataSource.Database(Database));
                //.Where(filter);                

            var result = new List<T>();
            foreach (var row in query.Execute())
            {
                var rawvalue = row.GetValue("value");
                result.Add(JsonConvert.DeserializeObject<T>(rawvalue.ToString()));
            }
            return result;
        }
    }
}
