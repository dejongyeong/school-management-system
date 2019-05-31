using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Helpers
{
    public class TotalWeeksInMonthHelper
    {
        protected TotalWeeksInMonthHelper() { }

        /* References:
         * Number of weeks in month.
         * Source: https://stackoverflow.com/questions/23353703/calculate-the-number-of-weeks-in-a-month
         * Author: Tiago Ávila
         * Supply: stackoverflow.com
         */

        public static int GetTotalWeeksInMonth(DateTime date)
        {
            DateTime beginningOfThisMonth = new DateTime(date.Year, date.Month, 1);
            int days = DateTime.DaysInMonth(date.Year, date.Month);

            //days of week starts by default as Sunday = 0
            var firstDayOfMonth = (int)beginningOfThisMonth.DayOfWeek;
            var weeks = (int)Math.Ceiling((firstDayOfMonth + days) / 7.0);

            return weeks;
        }
    }
}
