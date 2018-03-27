using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Model
{
    public enum AccessType
    {
        View,
        Write,
        Moderator
    }
    public enum ErrorTypes
    {
        Info,
        Warning,
        Error        
    }
}
