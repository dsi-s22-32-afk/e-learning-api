using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Utilities
{
    public class TimeUtil
    {
        public static long GetMiliseconds(DateTime dateTime)
        {
            return (long) dateTime.ToUniversalTime().Subtract(
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            ).TotalMilliseconds;
        }

        public static long GetSeconds(DateTime dateTime)
        {
            return (long) dateTime.ToUniversalTime().Subtract(
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            ).TotalSeconds;
        }

        public static DateTime FromMiliseconds(long millis)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(millis);
        }
    }
}
