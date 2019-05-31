using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolManagementSystem.ViewModels.IEntities;
using SchoolManagementSystem.Models;
using System.Linq;

namespace SMSUnitTest.UnitTest
{
    [TestClass]
    public class RegisterTeacherTest
    {
        [TestMethod]
        public void GettersAndSetterFullTimeTest()
        {
            RegisterTeacher teacher = new RegisterTeacher
            {
                TeacherID = 4,
                Surname = "Ha",
                Forename = "Christina",
                DOB = new DateTime(1990, 4, 2),
                Phone = "083 4814269",
                Subject = "Geography",
                Type = 'F',
                SalaryPartTime = "13000",
                SalaryFullTime = "16000",
                MonthlyFullTime = "1300",
                MonthlyPartTime = "1600",
                Hours = "15"
            };

            teacher.ValidateAllProperties();

            Assert.AreEqual("13000", teacher.SalaryPartTime);
        }

        [TestMethod]
        public void GettersAndSetterWithErrorTest()
        {
            RegisterTeacher teacher = new RegisterTeacher
            {
                TeacherID = 4,
                Surname = "Ha@",
                Forename = "Christina@",
                DOB = DateTime.Today,
                Phone = "0834814269",
                Subject = String.Empty,
                Type = ' ',
                SalaryPartTime = "@",
                SalaryFullTime = "-10",
                MonthlyFullTime = "-10",
                MonthlyPartTime = "-10",
                Hours = "-15"
            };
        }

        [TestMethod]
        public void GetNextRowIdTest()
        {
            RegisterTeacher teacher = new RegisterTeacher();
            SMSContext ctx = new SMSContext();

            int expected = teacher.GetNextRowID();
            int actual = ctx.Teachers.Select(id => id.teacherID).DefaultIfEmpty(0).Max() + 1;

            Assert.AreEqual(expected, actual);
        }
    }
}
