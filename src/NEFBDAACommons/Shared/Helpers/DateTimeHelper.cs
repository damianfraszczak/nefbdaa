using System;

namespace NEFBDAACommons.Shared.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetOldestDate(DateTime time1, DateTime? time2)
        {
            return time1.Date > time2?.Date ? time2.Value : time1;
        }

        
    }
}
