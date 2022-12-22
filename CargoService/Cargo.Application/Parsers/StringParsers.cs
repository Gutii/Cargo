using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Parsers
{
    public static class StringParsers
    {

        public static string[] ParsingSting(string parsSting)
        {
            if (string.IsNullOrEmpty(parsSting))
                return null;

            string[] separators = new string[] { @"\", ";", "/" };
            string[] result = new string[] { "" };
            foreach (var separator in separators)
            {
                result = parsSting.Split(separator);
                if (result.Length > 1)
                {
                    return result.Where(c => !string.IsNullOrEmpty(c)).ToArray();
                }
            }
            return result.Where(c => !string.IsNullOrEmpty(c)).ToArray();
        }

        public static bool IncludedDayOfWeek(string strDayOfWeek, DateTime dateStart, DateTime dateEnd)
        {
            string[] daysOfWeekStr = ParsingSting(strDayOfWeek);

            if (daysOfWeekStr == null ||
                dateStart == DateTime.UnixEpoch || dateEnd == DateTime.UnixEpoch ||
                dateStart > dateEnd)
                return false;
            int[] daysOfWeek = new int[0];

            try
            {
                daysOfWeek = daysOfWeekStr.Select(d => int.Parse(d.ToString())).ToArray();
            }
            catch (Exception ex)
            {
                return false;
            }

            DateTime dateTime = dateStart;
            int DayOfWeek = 0;
            for (int i = 0; i < 7 && dateTime < dateEnd; i++)
            {
                if (dateTime.DayOfWeek == 0)
                {
                    DayOfWeek = 7;
                }
                else
                {
                    DayOfWeek = (int)dateTime.DayOfWeek;
                }

                if (daysOfWeek.Contains(DayOfWeek))
                {
                    return true;
                }
                dateTime = dateTime.AddDays(1);
            }
            return false;
        }

    }
}
