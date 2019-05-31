using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolManagementSystem.Helpers;

namespace SMSUnitTest.UnitTest
{
    [TestClass]
    public class TotalWeeksInMonthTest
    {
        [TestMethod]
        public void TestTotalWeeksInMonth()
        {
            int actual = TotalWeeksInMonthHelper.GetTotalWeeksInMonth(DateTime.Today);
            int expected = WeeksInMonth();

            Assert.AreEqual(expected, actual);
        }

        /*
         * References:
         * Number of Weeks in Month
         * Source: https://social.msdn.microsoft.com/Forums/en-US/ec3fde61-03d9-4609-ba02-d836a8073630/number-of-weeks-in-month?forum=vblanguage
         * Author: Joel Engineer
         * 
         */

        private int WeeksInMonth()
        {
            DateTime today = DateTime.Now;

            // Extract the Month
            int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
            DateTime firstOfMonth = new DateTime(today.Year, today.Month, 1);

            // Days of Week starts by Default as Sunday = 0
            int firstDayOfMonth = (int)firstOfMonth.DayOfWeek;
            int weeksInMonth = (int)Math.Ceiling((firstDayOfMonth + daysInMonth) / 7.0);

            return weeksInMonth;
        }
    }
}
