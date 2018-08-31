using System;
using Foundation;

namespace OneMediPlan.iOS.Helper
{
    public static class IosExtensions
    {
        public static NSDate ToNSDate(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);
            return (NSDate)date;
        }

        public static DateTime ToDateTime(this NSDate date)
        {
            return ((DateTime)date).ToLocalTime();
        }
    }
}
