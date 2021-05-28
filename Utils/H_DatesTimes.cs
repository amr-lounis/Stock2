using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class H_DatesTimes
    {
        //-------------------------------------------------------------------------------------------------------------- date and time
        #region date and time
        public static DateTime DateTimeFromString(string _DateTime)
        {
            try
            {
                return DateTime.ParseExact(_DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
        public static string DateTimeToString(DateTime _DateTime)
        {
            return _DateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }
        #endregion
    }
}
