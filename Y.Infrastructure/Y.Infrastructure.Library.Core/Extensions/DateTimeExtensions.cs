using System;
using System.Collections.Generic;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetDefault(this DateTime dateTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// 获取 毫秒  时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStampTicks(this DateTime dateTime)
        {
            TimeSpan ts = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return ts.Ticks;
        }

        /// <summary>
        /// 获取当前时间与1970-1-1之间的 毫秒数 总和
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetTimeStamps(this DateTime time)
        {
            TimeSpan ts = time - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }


        /// <summary>
        /// 获取当前时间与1970-1-1之间的 秒数 总和
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static double GetTimeStampToSeconds(this DateTime dateTime)
        {
            TimeSpan ts = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return ts.TotalSeconds;
        }

        /// <summary>
        /// 秒数 时间戳转换为日期
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeBySecondsTimeStamp(this double timeStamp)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return time.AddSeconds(timeStamp);
        }

        /// <summary>
        /// 毫秒数  时间戳转换为日期
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeByMillisecondsTimeStamp(this double timeStamp)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return time.AddMilliseconds(timeStamp);
        }


        /// <summary>
        /// check the expired time
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="expiredSecond"></param>
        /// <returns></returns>
        public static bool CheckExpiredTime(double timestamp, double expiredSecond)
        {
            double now_timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            return (now_timestamp - timestamp) > expiredSecond;
        }


        /// <summary>
        /// 转换为 yyyy-MM 字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToMonthString(this DateTime dateTime)
        {
            return $"{dateTime.Year.ToString()}-{dateTime.Month.ToString()}";
        }

        /// <summary>
        /// 转换为 yyyy-MM-dd 字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.Date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 转换为 yyyy-MM-dd HH.mm.ss 字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime.Date.ToString("yyyy-MM-dd HH.mm.ss");
        }

        /// <summary>
        /// 转换为 yyyy-MM-dd HH:mm:ss 字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateTimeString2(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换为 yyyyMMddHHmm 字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateTimeString3(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmm");
        }
        /// <summary>
        /// 转换为 yyyyMMdd 字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateTimeString4(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 判断今天是否是周一
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsMonday(this DateTime dateTime)
        {
            return DateTime.UtcNow.AddHours(8).DayOfWeek.GetEnumValue() == 1;
        }


        /// <summary>
        /// 获取上周一 日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetLastWeekMondayDate(this DateTime dateTime)
        {
            return DateTime.UtcNow.AddHours(8)
                .AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek))) - 7 + 1).ToDateString()
                .To<DateTime>();
        }

        /// <summary>
        /// 获取上周日 日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetLaskWeekSundayDate(this DateTime dateTime)
        {
            return DateTime.UtcNow.AddHours(8)
                .AddDays(Convert.ToDouble((6 - Convert.ToInt16(DateTime.Now.DayOfWeek))) - 7 + 1).ToDateString()
                .To<DateTime>();
        }


        /// <summary>
        /// 获取本周一
        /// </summary>
        /// <returns></returns>
        public static DateTime GetThisWeekMondayDate(this DateTime dateTime)
        {
            return DateTime.UtcNow.AddHours(8)
                .AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek))) + 1).ToDateString()
                .To<DateTime>();
        }


        /// <summary>
        /// 获取上个月第一天日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetLastMonthFirstDayDate(this DateTime dateTime)
        {
            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1).ToDateString().To<DateTime>();
        }


        /// <summary>
        /// 获取本月第一天日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetCurrentMonthFirstDayDate(this DateTime dateTime)
        {
            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).ToDateString().To<DateTime>();
        }

        /// <summary>
        /// 获取上个月最后一天日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetLastMonthLastDayDate(this DateTime dateTime)
        {
            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToDateString().To<DateTime>();
        }

        public static bool IsDayOfWeekEnd(this DateTime dateTime)
        {
            List<DayOfWeek> weekDay = new List<DayOfWeek>()
            {
                DayOfWeek.Saturday,DayOfWeek.Sunday
            };
            var day = dateTime.DayOfWeek;
            return weekDay.Contains(day);
        }

        /// <summary>
        /// 传入时间是否是股市时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsCNStockTime(this DateTime dt)
        {
            var dateTimeNow = dt;
            var time930 = dt.Date.AddHours(9.5);
            var tiem1130 = dt.Date.AddHours(11.5);
            var time1300 = dt.Date.AddHours(13);
            var time1500 = dt.Date.AddHours(15);
            var v1 = (dateTimeNow > time930) && (dateTimeNow < tiem1130);
            var v3 = (dateTimeNow > time1300) && dateTimeNow < time1500;

            return v1 || v3;
        }

        public static DateTime ToUtc8DateTime(this DateTime dt)
        {
            return DateTime.UtcNow.AddHours(8);
        }

        public static int GetDiffDay(this DateTime end, DateTime start)
        {
            TimeSpan sp = end.Subtract(start);
            return sp.Days;
        }
        public static int GetDiffMins(this DateTime end, DateTime start)
        {
            TimeSpan sp = end.Subtract(start);
            return sp.Minutes;
        }
        public static double GetDiffSeconds(this DateTime end, DateTime start)
        {
            TimeSpan sp = end.Subtract(start);
            return sp.TotalSeconds;
        }
    }
}