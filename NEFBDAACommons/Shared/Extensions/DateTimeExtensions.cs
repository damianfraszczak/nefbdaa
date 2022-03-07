using System;

namespace NEFBDAACommons.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime SetTime(this DateTime dateTime, int hours, int minutes, int seconds)
        {
            if (hours < 0 || hours > 23)
            {
                return dateTime;
            }

            if (minutes < 0 || minutes > 59)
            {
                return dateTime;
            }

            if (seconds < 0 || seconds > 59)
            {
                return dateTime;
            }

            return dateTime.Date + new TimeSpan(hours, minutes, seconds);
        }
    }
}
