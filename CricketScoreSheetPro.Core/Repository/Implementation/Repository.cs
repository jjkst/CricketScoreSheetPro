using Couchbase.Lite;
using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketScoreSheetPro.Core.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private Database Database;
        internal string UUID;

        public Repository(IClient client)
        {
            Database = client.GetDatabase();
            UUID = client.GetUUID();
            GenerateViews();
        }

        public virtual T Create(Dictionary<string, object> newproperty)
        {
            if (newproperty == null) throw new ArgumentNullException($"Object to create is null");
            newproperty.Add("uuid", UUID);
            var document = Database.CreateDocument();
            Function.UpdateGenericObjectProperty(newproperty, document.Id);
            document.PutProperties(newproperty);
            var result = JsonConvert.DeserializeObject<T>(document.GetProperty("value").ToString());
            return result;
        }

        public virtual bool Update(string id, T obj)
        {
            var document = Database.GetExistingDocument(id);
            var result = document.Update((UnsavedRevision newRevision) =>
            {
                var properties = newRevision.Properties;
                properties["value"] = obj;
                return true;
            });
            return result != null;
        }

        public virtual T GetItem(string id)
        {
            var document = Database.GetExistingDocument(id);
            var result = JsonConvert.DeserializeObject<T>(document.GetProperty("value").ToString());
            return result;
        }

        public virtual IList<T> GetList()
        {
            var query = Database.GetView(typeof(T).Name).CreateQuery();
            query.Descending = true;

            var result = new List<T>();
            var rows = query.Run();
            foreach (var row in rows)
                result.Add(JsonConvert.DeserializeObject<T>(row.Value.ToString()));
            return result;
        }

        public virtual IList<T> GetListByProperty(string propertyName, string propertyValue)
        {
            GenerateViewByProperty(propertyName, propertyValue);
            var query = Database.GetView("listbyproperty").CreateQuery();
            query.Descending = true;

            var result = new List<T>();
            var rows = query.Run();
            foreach (var row in rows)
                result.Add(JsonConvert.DeserializeObject<T>(row.Value.ToString()));
            return result;
        }

        public virtual void Delete(string id)
        {
            var document = Database.GetExistingDocument(id);
            document.Delete();
        }

        public void DeleteDatabase()
        {
            var query = Database.CreateAllDocumentsQuery();
            var documents = query.Run();
            foreach (var document in documents)
                document.Document.Delete();
        }

        private void GenerateViews()
        {
            var userlist = Database.GetView(typeof(T).Name);
            userlist.SetMap((doc, emit) =>
            {
                if (doc.ContainsKey("uuid") && doc["uuid"].ToString() == UUID &&
                    doc.ContainsKey("type") && doc["type"].ToString() == typeof(T).Name)
                    emit(doc["type"], doc["value"]);
            }, "1");
        }

        private void GenerateViewByProperty(string propertyName, string propertyValue)
        {
            var listByProperty = Database.GetView("listbyproperty");
            listByProperty.SetMap((doc, emit) =>
            {
                if (doc.ContainsKey(propertyName) && doc[propertyName].ToString() == propertyValue &&
                    doc.ContainsKey("type") && doc["type"].ToString() == typeof(T).Name)
                    emit(doc["type"], doc["value"]);
            }, "1");
        }
    }
}
