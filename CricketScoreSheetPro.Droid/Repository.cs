using Couchbase.Lite;
using CricketScoreSheetPro.Core.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CricketScoreSheetPro.Droid
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private Database Database;
        public string GetUUID()
        {
            return Singleton.Instance.UniqueUserId;
        }

        public Repository()
        {
            var manager = new Manager(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToLower()), Manager.DefaultOptions);
            Database = manager.GetDatabase("cricketscoresheetprodb");
            GenerateViews();
        }

        public virtual T Create(Dictionary<string, object> newproperty)
        {
            if (newproperty == null) throw new ArgumentNullException($"Object to create is null");
            var document = Database.CreateDocument();
            var propertyupdatedwithId = UpdateGenericObjectProperty(newproperty, document.Id);
            document.PutProperties(newproperty);
            var result = JsonConvert.DeserializeObject<T>(document.GetProperty("value").ToString());
            return result;
        }

        public T ImportCreate(Dictionary<string, object> property)
        {
            if (property == null) throw new ArgumentNullException($"Object to create is null");
            var document = Database.CreateDocument();
            document.PutProperties(property);
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

        public virtual T GetChildItem(string id)
        {
            GenerateChildView(id);
            var query = Database.GetView(typeof(T).Name).CreateQuery();
            var rows = query.Run();

            T result = null;
            foreach (var row in rows)
            {
                result = JsonConvert.DeserializeObject<T>(row.Document.GetProperty("value").ToString());
            }
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

        public virtual void Delete(string id)
        {
            var doc = Database.GetExistingDocument(id);
            doc.Delete();
            GenerateChildView(id);
            var query = Database.GetView(typeof(T).Name).CreateQuery();
            var rows = query.Run();
            foreach (var row in rows)
            {
                row.Document.Delete();
            }
        }

        public void DeleteDatabase()
        {
            var query = Database.CreateAllDocumentsQuery();
            var documents = query.Run();
            foreach (var document in documents)
                document.Document.Delete();
        }

        private static Dictionary<string, object> UpdateGenericObjectProperty(Dictionary<string, object> obj, object value)
        {
            Type t = obj["value"].GetType();
            foreach (var propInfo in t.GetProperties())
            {
                if (propInfo.Name == "Id")
                {
                    propInfo.SetValue(obj["value"], value, null);
                    break;
                }
            }
            return obj;
        }

        private void GenerateViews()
        {
            var tournamentlist = Database.GetView(typeof(T).Name);
            tournamentlist.SetMap((doc, emit) =>
            {
                if (doc.ContainsKey("uuid") && doc["uuid"].ToString() == GetUUID() &&
                    doc.ContainsKey("type") && doc["type"].ToString() == typeof(T).Name)
                    emit(doc["type"], doc["value"]);
            }, "1");
        }

        private void GenerateChildView(string parentId)
        {
            var tournamentdetail = Database.GetView(typeof(T).Name);
            tournamentdetail.SetMap((doc, emit) =>
            {
                if (doc.ContainsKey("parent_id") && doc["parent_id"].ToString() == parentId &&
                    doc.ContainsKey("type") && doc["type"].ToString() == typeof(T).Name)
                    emit(doc["type"], doc["value"]);
            }, "1");
        }
    }
}
