using System.IO;
using System;
using Android.App;

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