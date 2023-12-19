using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Helpers
{
    public static class TimeStampToDateTime
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dat_Time = new System.DateTime(1965, 1, 1, 0, 0, 0, 0);
            dat_Time = dat_Time.AddSeconds(unixTimeStamp);
            string print_the_Date = dat_Time.ToShortDateString() +" "+ dat_Time.ToShortTimeString();
            DateTime date = Convert.ToDateTime(print_the_Date);
            return date;

        }
    }
}
