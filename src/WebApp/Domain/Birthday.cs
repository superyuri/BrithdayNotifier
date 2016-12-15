using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApp.Domain
{
    public class Birthday
    {
        public Birthday(string RawString)
        {
            string[] strings = RawString.Split('\t');
            if (strings.Length<4)
            {
                throw new IndexOutOfRangeException(RawString+"\tBirthday:Birthday Object Create Failed");
            }

            Name = strings[0];
            LunarCalendarString = strings[1];
            SolarCalenderString = strings[2];
            Tel = strings[3];
            if (!LunarCalendarString.Equals(String.Empty))
            {
                ChineseLunisolarCalendar clc = new ChineseLunisolarCalendar();
                LunarCalendar = clc.ToDateTime(DateTime.Now.Year, GetMonthFromString(strings[1]), GetDateFromString(strings[1]), 0, 0, 0, 0);
                NextLunarCalendar = clc.ToDateTime(DateTime.Now.Year+1, GetMonthFromString(strings[1]), GetDateFromString(strings[1]), 0, 0, 0, 0);
            }
            if (!SolarCalenderString.Equals(String.Empty))
            {
                SolarCalender = new DateTime(DateTime.Now.Year, GetMonthFromString(strings[2]), GetDateFromString(strings[2]));
                NextSolarCalender = new DateTime(DateTime.Now.Year+1, GetMonthFromString(strings[2]), GetDateFromString(strings[2]));
            }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 阴历日期
        /// </summary>
        public string LunarCalendarString { get; }
        /// <summary>
        /// 今年阴历生日
        /// </summary>
        public DateTime LunarCalendar { get; }
        /// <summary>
        /// 明年阴历生日
        /// </summary>
        public DateTime NextLunarCalendar { get; }
        /// <summary>
        /// 阳历生日
        /// </summary>
        public string SolarCalenderString { get; }
        /// <summary>
        /// 今年阳历生日
        /// </summary>
        public DateTime SolarCalender { get; }
        /// <summary>
        /// 明年阳历生日
        /// </summary>
        public DateTime NextSolarCalender { get; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; }

        private int GetMonthFromString(string str)
        {
            return int.Parse(Regex.Match(str, @"(\d+)月(\d+)日").Groups[1].Value);
        }
        private int GetDateFromString(string str)
        {
            return int.Parse(Regex.Match(str, @"(\d+)月(\d+)日").Groups[2].Value);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0},", Name));
            if (!string.IsNullOrEmpty(SolarCalenderString))
            {
                sb.Append(string.Format("阳历{0},", SolarCalenderString));
            }
            if (!string.IsNullOrEmpty(LunarCalendarString))
            {
                sb.Append(string.Format("阴历{0},", LunarCalendarString));
            }
            sb.Append(string.Format("电话{0}", Tel));
            return sb.ToString();
        }
    }
}