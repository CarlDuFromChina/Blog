using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.Common.Utils
{
    public static class CronUtil
    {
        #region 初始化Cron对象
        private static void Seconds(Cron c, string str)
        {
            if (str == "*")
            {
                for (int i = 0; i < 60; i++)
                {
                    c.Seconds[i] = 1;
                }
            }
            else if (str.Contains('-'))
            {
                int begin = int.Parse(str.Split('-')[0]);
                int end = int.Parse(str.Split('-')[1]);
                for (int i = begin; i <= end; i++)
                {
                    c.Seconds[i] = 1;
                }
            }
            else if (str.Contains('/'))
            {
                int begin = int.Parse(str.Split('/')[0]);
                int interval = int.Parse(str.Split('/')[1]);
                while (true)
                {
                    c.Seconds[begin] = 1;
                    if ((begin + interval) >= 60)
                        break;
                    begin += interval;
                }
            }
            else if (str.Contains(','))
            {

                for (int i = 0; i < str.Split(',').Length; i++)
                {
                    c.Seconds[int.Parse(str.Split(',')[i])] = 1;
                }
            }
            else
            {
                c.Seconds[int.Parse(str)] = 1;
            }
        }
        private static void Minutes(Cron c, string str)
        {
            if (str == "*")
            {
                for (int i = 0; i < 60; i++)
                {
                    c.Minutes[i] = 1;
                }
            }
            else if (str.Contains('-'))
            {
                int begin = int.Parse(str.Split('-')[0]);
                int end = int.Parse(str.Split('-')[1]);
                for (int i = begin; i <= end; i++)
                {
                    c.Minutes[i] = 1;
                }
            }
            else if (str.Contains('/'))
            {
                int begin = int.Parse(str.Split('/')[0]);
                int interval = int.Parse(str.Split('/')[1]);
                while (true)
                {
                    c.Minutes[begin] = 1;
                    if ((begin + interval) >= 60)
                        break;
                    begin += interval;
                }
            }
            else if (str.Contains(','))
            {

                for (int i = 0; i < str.Split(',').Length; i++)
                {
                    c.Minutes[int.Parse(str.Split(',')[i])] = 1;
                }
            }
            else
            {
                c.Minutes[int.Parse(str)] = 1;
            }
        }
        private static void Hours(Cron c, string str)
        {
            if (str == "*")
            {
                for (int i = 0; i < 24; i++)
                {
                    c.Hours[i] = 1;
                }
            }
            else if (str.Contains('-'))
            {
                int begin = int.Parse(str.Split('-')[0]);
                int end = int.Parse(str.Split('-')[1]);
                for (int i = begin; i <= end; i++)
                {
                    c.Hours[i] = 1;
                }
            }
            else if (str.Contains('/'))
            {
                int begin = int.Parse(str.Split('/')[0]);
                int interval = int.Parse(str.Split('/')[1]);
                while (true)
                {
                    c.Hours[begin] = 1;
                    if ((begin + interval) >= 24)
                        break;
                    begin += interval;
                }
            }
            else if (str.Contains(','))
            {

                for (int i = 0; i < str.Split(',').Length; i++)
                {
                    c.Hours[int.Parse(str.Split(',')[i])] = 1;
                }
            }
            else
            {
                c.Hours[int.Parse(str)] = 1;
            }
        }
        private static void Month(Cron c, string str)
        {
            if (str == "*")
            {
                for (int i = 0; i < 12; i++)
                {
                    c.Month[i] = 1;
                }
            }
            else if (str.Contains('-'))
            {
                int begin = int.Parse(str.Split('-')[0]);
                int end = int.Parse(str.Split('-')[1]);
                for (int i = begin; i <= end; i++)
                {
                    c.Month[i - 1] = 1;
                }
            }
            else if (str.Contains('/'))
            {
                int begin = int.Parse(str.Split('/')[0]);
                int interval = int.Parse(str.Split('/')[1]);
                while (true)
                {
                    c.Month[begin - 1] = 1;
                    if ((begin + interval) >= 12)
                        break;
                    begin += interval;
                }
            }
            else if (str.Contains(','))
            {

                for (int i = 0; i < str.Split(',').Length; i++)
                {
                    c.Month[int.Parse(str.Split(',')[i]) - 1] = 1;
                }
            }
            else
            {
                c.Month[int.Parse(str) - 1] = 1;
            }
        }
        private static void Year(Cron c, string str)
        {
            if (str == null || str == "*")
            {
                for (int i = 0; i < 80; i++)
                {
                    c.Year[i] = 1;
                }
            }
            else if (str.Contains('-'))
            {
                int begin = int.Parse(str.Split('-')[0]);
                int end = int.Parse(str.Split('-')[1]);
                for (int i = begin - 2019; i <= end - 2019; i++)
                {
                    c.Year[i] = 1;
                }
            }
            else
            {
                c.Year[int.Parse(str) - 2019] = 1;
            }
        }
        private static void Days(Cron c, string str, int len, DateTime now)
        {
            for (int i = 0; i < 7; i++)
            {
                c.Weeks[i] = 1;
            }
            if (str == "*" || str == "?")
            {
                for (int i = 0; i < len; i++)
                {
                    c.Days[i] = 1;
                }
            }
            else if (str.Contains('-'))
            {
                int begin = int.Parse(str.Split('-')[0]);
                int end = int.Parse(str.Split('-')[1]);
                for (int i = begin; i <= end; i++)
                {
                    c.Days[i - 1] = 1;
                }
            }
            else if (str.Contains('/'))
            {
                int begin = int.Parse(str.Split('/')[0]);
                int interval = int.Parse(str.Split('/')[1]);
                while (true)
                {
                    c.Days[begin - 1] = 1;
                    if ((begin + interval) >= len)
                        break;
                    begin += interval;
                }
            }
            else if (str.Contains(','))
            {
                for (int i = 0; i < str.Split(',').Length; i++)
                {
                    c.Days[int.Parse(str.Split(',')[i]) - 1] = 1;
                }
            }
            else if (str.Contains('L'))
            {
                int i = str.Replace("L", "") == "" ? 0 : int.Parse(str.Replace("L", ""));
                c.Days[len - 1 - i] = 1;
            }
            else if (str.Contains('W'))
            {
                c.Days[len - 1] = 1;
            }
            else
            {
                c.Days[int.Parse(str) - 1] = 1;
            }
        }
        private static void Weeks(Cron c, string str, int len, DateTime now)
        {
            if (str == "*" || str == "?")
            {
                for (int i = 0; i < 7; i++)
                {
                    c.Weeks[i] = 1;
                }
            }
            else if (str.Contains('-'))
            {
                int begin = int.Parse(str.Split('-')[0]);
                int end = int.Parse(str.Split('-')[1]);
                for (int i = begin; i <= end; i++)
                {
                    c.Weeks[i - 1] = 1;
                }
            }
            else if (str.Contains(','))
            {
                for (int i = 0; i < str.Split(',').Length; i++)
                {
                    c.Weeks[int.Parse(str.Split(',')[i]) - 1] = 1;
                }
            }
            else if (str.Contains('L'))
            {
                int i = str.Replace("L", "") == "" ? 0 : int.Parse(str.Replace("L", ""));
                if (i == 0)
                {
                    c.Weeks[6] = 1;
                }
                else
                {
                    c.Weeks[i - 1] = 1;
                    c.Days[GetLastWeek(i, now) - 1] = 1;
                    return;
                }
            }
            else if (str.Contains('#'))
            {
                int i = int.Parse(str.Split('#')[0]);
                int j = int.Parse(str.Split('#')[1]);
                c.Weeks[i - 1] = 1;
                c.Days[GetWeek(i - 1, j, now)] = 1;
                return;
            }
            else
            {
                c.Weeks[int.Parse(str) - 1] = 1;
            }
            //week中初始化day，则说明day没要求
            for (int i = 0; i < len; i++)
            {
                c.Days[i] = 1;
            }
        }
        #endregion

        public static bool IsOrNoOne(string cron)
        {
            if (cron.Contains("-") || cron.Contains(",") || cron.Contains("/") || cron.Contains("*"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取最后一个星期几的day
        /// </summary>
        /// <param name="i">星期几</param>
        /// <param name="now"></param>
        /// <returns></returns>
        private static int GetLastWeek(int i, DateTime now)
        {
            DateTime d = now.AddDays(1 - now.Day).Date.AddMonths(1).AddSeconds(-1);
            int DayOfWeek = ((((int)d.DayOfWeek) + 6) % 7) + 1;
            int a = DayOfWeek >= i ? DayOfWeek - i : 7 + DayOfWeek - i;
            return DateTime.DaysInMonth(now.Year, now.Month) - a;
        }
        /// <summary>
        /// 获取当月第几个星期几的day
        /// </summary>
        /// <param name="i">星期几</param>
        /// <param name="j">第几周</param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static int GetWeek(int i, int j, DateTime now)
        {
            int day = 0;
            DateTime d = new DateTime(now.Year, now.Month, 1);
            int DayOfWeek = ((((int)d.DayOfWeek) + 6) % 7) + 1;
            if (i >= DayOfWeek)
            {
                day = (7 - DayOfWeek + 1) + 7 * (j - 2) + i;
            }
            else
            {
                day = (7 - DayOfWeek + 1) + 7 * (j - 1) + i;
            }
            return day;
        }

        /// <summary>
        /// Cron表达式转换（自定义开始时间）
        /// </summary>
        /// <param name="cron">表达式</param>
        /// <param name="now">开始时间</param>
        /// <returns>最近要执行的时间字符串</returns>
        public static string GetNextDateTime(string cron, DateTime now)
        {
            try
            {
                string[] arr = cron.Split(' ');
                if (IsOrNoOne(cron))
                {
                    string date = arr[6] + "/" + arr[4] + "/" + arr[3] + " " + arr[2] + ":" + arr[1] + ":" + arr[0];
                    if (DateTime.Compare(Convert.ToDateTime(date), now) > 0)
                    {
                        return date;
                    }
                    else
                    {
                        return null;
                    }
                }
                Cron c = new Cron();
                Seconds(c, arr[0]);
                Minutes(c, arr[1]);
                Hours(c, arr[2]);
                Month(c, arr[4]);
                if (arr.Length < 7)
                {
                    Year(c, null);
                }
                else
                {
                    Year(c, arr[6]);
                }
                int addtime = 1;
                while (true)
                {
                    if (c.Seconds[now.Second] == 1 && c.Minutes[now.Minute] == 1 && c.Hours[now.Hour] == 1 && c.Month[now.Month - 1] == 1 && c.Year[now.Year - 2019] == 1)
                    {
                        if (arr[3] != "?")
                        {
                            Days(c, arr[3], DateTime.DaysInMonth(now.Year, now.Month), now);
                            int DayOfWeek = (((int)now.DayOfWeek) + 6) % 7;
                            if (c.Days[now.Day - 1] == 1 && c.Weeks[DayOfWeek] == 1)
                            {
                                return now.ToString("yyyy/MM/dd HH:mm:ss");
                            }
                        }
                        else
                        {
                            Weeks(c, arr[5], DateTime.DaysInMonth(now.Year, now.Month), now);
                            int DayOfWeek = (((int)now.DayOfWeek) + 6) % 7;
                            if (c.Days[now.Day - 1] == 1 && c.Weeks[DayOfWeek] == 1)
                            {
                                return now.ToString("yyyy/MM/dd HH:mm:ss");
                            }
                        }
                    }
                    c.Init();
                    if (!arr[1].Contains('-') && !arr[1].Contains(',') && !arr[1].Contains('*') && !arr[1].Contains('/'))
                    {
                        if (now.Minute == int.Parse(arr[1]))
                        {
                            addtime = 3600;
                        }
                    }
                    else if (arr[0] == "0" && now.Second == 0)
                    {
                        addtime = 60;
                    }
                    now = now.AddSeconds(addtime);
                }
            }
            catch
            {
                return null;
            }
        }
    }

    class Cron
    {
        private int[] seconds = new int[60];
        private int[] minutes = new int[60];
        private int[] hours = new int[24];
        private int[] days = new int[31];
        private int[] month = new int[12];
        private int[] weeks = new int[7];
        //2019-2099年
        private int[] year = new int[80];

        public int[] Seconds { get => seconds; set => seconds = value; }
        public int[] Minutes { get => minutes; set => minutes = value; }
        public int[] Hours { get => hours; set => hours = value; }
        public int[] Days { get => days; set => days = value; }
        public int[] Month { get => month; set => month = value; }
        public int[] Weeks { get => weeks; set => weeks = value; }
        public int[] Year { get => year; set => year = value; }

        public Cron()
        {
            for (int i = 0; i < 60; i++)
            {
                seconds[i] = 0;
                minutes[i] = 0;
            }
            for (int i = 0; i < 24; i++)
            {
                hours[i] = 0;
            }
            for (int i = 0; i < 31; i++)
            {
                days[i] = 0;
            }
            for (int i = 0; i < 12; i++)
            {
                month[i] = 0;
            }
            for (int i = 0; i < 7; i++)
            {
                weeks[i] = 0;
            }
            for (int i = 0; i < 80; i++)
            {
                year[i] = 0;
            }
        }

        public void Init()
        {
            for (int i = 0; i < 7; i++)
            {
                weeks[i] = 0;
            }
            for (int i = 0; i < 31; i++)
            {
                days[i] = 0;
            }
        }
    }
}
