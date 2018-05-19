using System;

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
            foreach (var propInfo in obj.GetType().GetProperties())
            {
                if (propInfo.Name == propertyName)
                {
                    return propInfo.GetValue(obj).ToString();
                }
            }
            return string.Empty;
        }

        public static object UpdateGenericObjectProperty(object obj, object value)
        {
            foreach (var propInfo in obj.GetType().GetProperties())
            {
                if (propInfo.Name == "Id")
                {
                    propInfo.SetValue(obj, value, null);
                    break;
                }
            }
            return obj;
        }

    }
}
