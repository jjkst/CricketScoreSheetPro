using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Model
{
    public class User
    {
        public string Id { get; set; }

        public List<object> Entity { get; set; }

        public DateTime AddDate { get; set; }

        public User()
        {
            Entity = new List<object>();
        }

    }
}
