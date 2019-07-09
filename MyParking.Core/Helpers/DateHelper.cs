using System;
namespace Parking.Core.Helpers
{
    public class DateHelper
    {
        public static int GetCurrentDay(DateTimeOffset dateOfentry)
        {
            int day = (int)dateOfentry.DayOfWeek;
            return day;
        }

        public static int GetHours(DateTimeOffset dateOfEntry)
        {
            return (int)(DateTime.Now - dateOfEntry).TotalHours;
        }
    }
}
