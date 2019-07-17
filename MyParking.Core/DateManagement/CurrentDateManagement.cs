using System;
namespace MyParking.Core.Helpers
{
    public static class CurrentDateManagement
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
