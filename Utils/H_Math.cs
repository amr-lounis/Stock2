using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class H_Math
    {
        //-------------------------------------------------------------------------------------------------------------- Math
        #region math
        public static double rnd(object _value)
        {
            return Math.Round((double)_value, 2);
        }
        public static double DoubleFromString(string _value)
        {
            try
            {
                return double.Parse(_value);
            }
            catch (Exception)
            {
                return 0f;
            }
        }
        public static long LongFromString(string _value)
        {
            try
            {
                return long.Parse(_value);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int intFromString(string _value)
        {
            try
            {
                return int.Parse(_value);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
    }
}
