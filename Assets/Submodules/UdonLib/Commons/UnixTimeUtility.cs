using System;

namespace UdonLib.Commons
{
    /// <summary>
    /// DateTime,UnixTimeの取得Utility
    /// </summary>
    public static class TimeUtility
    {
        private static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static long GetUnixTime()
        {
            return (long)(DateTime.Now - UnixEpoch).TotalSeconds;
        }

        public static long GetUnixTime(DateTime dateTime)
        {
            return (long)(dateTime - UnixEpoch).TotalSeconds;
        }

        public static DateTime GetDateTime(long unixTime)
        {
            return UnixEpoch.AddSeconds(unixTime);
        }
    }
}