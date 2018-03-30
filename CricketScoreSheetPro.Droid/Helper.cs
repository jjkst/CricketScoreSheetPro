using System.IO;
using System;

namespace CricketScoreSheetPro.Droid
{
    public class Helper
    {
        public static string InternalPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToLower();
            }
        }
    }
}