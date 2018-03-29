using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Helper
{
    public class Function
    {
        public static string BallsToOversValueConverter(int balls)
        {
            if (balls < 0) throw new ArgumentException("Balls cannot be negative.");
            return balls / 6 + "." + balls % 6;
        }

        public static string GetGenericObjectPropertyValue(object obj, string propertyName)
        {
            Type t = obj.GetType();
            foreach (var propInfo in t.GetProperties())
            {
                if (propInfo.Name == propertyName)
                {
                    return propInfo.GetValue(obj).ToString();
                }
            }
            return string.Empty;
        }

        public static Dictionary<string, object> UpdateGenericObjectProperty(Dictionary<string, object> obj, object value)
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

    }
}
