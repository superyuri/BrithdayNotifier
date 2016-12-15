using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApp.Domain
{
    public class BirthdayDoamin
    {
        static BirthdayDoamin()
        {
            string[] collection = File.ReadAllLines(Filename);
            foreach (var item in collection)
            {
                birthdays.Add(new Birthday(item));
            }
        }
        private static List<Birthday> birthdays = new List<Birthday>();
        private static string Filename = AppDomain.CurrentDomain.BaseDirectory+"\\data.data";
        
        public static List<Birthday>  GetBirthdays()
        {
            return birthdays;
        }
        /// <summary>
        /// 获得最近30天过生日的弟兄姐妹
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public static IEnumerable<Birthday> GetRecentBrithdays(int days)
        {
            //foreach (var item in birthdays)
            //{
            //    item.SolarCalender - DateTime.Now
            //}
            return birthdays.Where(c => (c.SolarCalender - DateTime.Now).Days > 0 && (c.SolarCalender - DateTime.Now).Days < days)
                .Union(birthdays.Where(c => (c.LunarCalendar - DateTime.Now).Days > 0 && (c.LunarCalendar - DateTime.Now).Days < days))
                .Union(birthdays.Where(c => (c.NextLunarCalendar - DateTime.Now).Days > 0 && (c.NextLunarCalendar - DateTime.Now).Days < days))
                .Union(birthdays.Where(c => (c.NextSolarCalender - DateTime.Now).Days > 0 && (c.NextSolarCalender - DateTime.Now).Days < days));
        }
        /// <summary>
        /// 获得某年某月过生日的弟兄姐妹
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public static IEnumerable<Birthday> GetBrithdaysByMonth(int year ,int month)
        {
            //foreach (var item in birthdays)
            //{
            //    item.SolarCalender - DateTime.Now
            //}
            return birthdays.Where(c => c.SolarCalender.Month == month && c.SolarCalender.Year == year)
                .Union(birthdays.Where(c => c.LunarCalendar.Month == month && c.LunarCalendar.Year == year))
                .Union(birthdays.Where(c => c.NextLunarCalendar.Month == month && c.NextLunarCalendar.Year == year))
                .Union(birthdays.Where(c => c.NextSolarCalender.Month == month && c.NextSolarCalender.Year == year));
        }
        /// <summary>
        /// 集合格式化为字符串
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string TransformString(IEnumerable<Birthday> collection)
        {
            StringBuilder sb = new StringBuilder();
            int index = 1;
            foreach (var item in collection)
            {
                sb.Append(string.Format("{0}:{1}\r\n", index++,item.ToString()));
            }
            return sb.ToString();
        }
    }
}