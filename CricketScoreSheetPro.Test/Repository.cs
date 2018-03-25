using Couchbase.Lite;
using CricketScoreSheetPro.Core.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CricketScoreSheetPro.Test
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private Database Database;

        public Repository()
        {
            var manager = new Manager(new DirectoryInfo(Environment.CurrentDirectory.ToLower()), Manager.DefaultOptions);
            Database = manager.GetDatabase("testdb");
            GenerateViews();
        }

        public virtual T Create(T obj)
        {
            if (obj == null) throw new ArgumentNullException($"Object to create is null");
            var document = Database.CreateDocument();
            obj = UpdateGenericObjectProperty(obj, document.Id);
            var properties = new Dictionary<string, object>
            {
                { "type", typeof(T).Name },
                { "value", obj }
            };
            document.PutProperties(properties);
            var result = JsonConvert.DeserializeObject<T>(document.GetProperty("value").ToString());             
            return result;
        }

        public virtual T CreateWithParentId(string id, T obj)
        {
            if (obj == null) throw new ArgumentNullException($"Object to create is null");
            var document = Database.CreateDocument();
            var properties = new Dictionary<string, object>
            {
                { "parentId", id },
                { "type", typeof(T).Name },
                { "value", obj }
            };
            document.PutProperties(properties);
            var result = JsonConvert.DeserializeObject<T>(document.GetProperty("value").ToString());
            return result;
        }

        public virtual void Delete(string id)
        {
            var doc = Database.GetExistingDocument(id);
            doc.Delete();
        }

        public virtual T GetItem(string id)
        {
            var document = Database.GetExistingDocument(id);
            var result = JsonConvert.DeserializeObject<T>(document.GetProperty(typeof(T).Name).ToString());
            return result;
        }


        public virtual IList<T> GetList()
        {
            var query = Database.GetView("docs_by_type").CreateQuery();
            query.StartKey = typeof(T).Name;
            query.Descending = true;
            
            var result = new List<T>();
            var rows = query.Run();
            foreach (var row in rows)
                result.Add(JsonConvert.DeserializeObject<T>(row.Value.ToString()));
            return result;
        }

        public virtual bool Update(string id, T obj)
        {
            var document = Database.GetDocument(id);
            var result = document.Update((UnsavedRevision newRevision) =>
            {
                var properties = newRevision.Properties;
                properties["value"] = obj;
                return true;
            });
            return result != null;
        }

        private static T UpdateGenericObjectProperty(T obj, object value)
        {
            Type t = obj.GetType();
            foreach (var propInfo in t.GetProperties())
            {
                if (propInfo.Name == "Id")
                {
                    propInfo.SetValue(obj, value, null);
                    break;
                }
            }
            return obj;
        }

        private void GenerateViews()
        {
            var docsBytype = Database.GetView("docs_by_type");
            docsBytype.SetMap((doc, emit) =>
            {
                if (doc.ContainsKey("type"))
                {
                    emit(doc["type"], doc["value"]);
                }
            }, "1");
        }
    }
}
