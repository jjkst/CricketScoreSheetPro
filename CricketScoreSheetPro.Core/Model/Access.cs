using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Model
{
    public class Access
    {
        public string Id { get; set; }
        public string DocumentType { get; set; }
        public List<DocumentReference> Documents { get; set; }        
    }

    public class DocumentReference
    {
        public string Id { get; set; }
        public AccessType AccessType { get; set; }
    }
}
