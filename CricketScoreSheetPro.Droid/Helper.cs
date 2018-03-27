using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CricketScoreSheetPro.Droid
{
    public class Helper
    {
        public static string DownloadPath
        {
            get
            {
                string filePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Android", "data",
                     "com.jjkst.cricketscoresheetpro", "files");
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                return filePath;
            }
        }
    }
}