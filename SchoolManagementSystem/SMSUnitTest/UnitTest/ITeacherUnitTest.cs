using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolManagementSystem.ViewModels.IEntities;

namespace SMSUnitTest.UnitTest
{
    [TestClass]
    public class ITeacherUnitTest
    {
        [TestMethod]
        public void MultiArgumentConstructorTest()
        {
            ITeacher teacher = new ITeacher(1, "Joe", "Bloggs", DateTime.Today, "083 4814256", 'P', 13500, "Geography");

            Assert.AreEqual(1, teacher.TeacherId);
            Assert.AreEqual("Joe", teacher.Surname);
            Assert.AreEqual("Bloggs", teacher.Forename);
            Assert.AreEqual(DateTime.Today, teacher.DOB);
            Assert.AreEqual("083 4814256", teacher.Phone);
            Assert.AreEqual('P', teacher.Type);
            Assert.AreEqual(13500, teacher.Salary);
            Assert.AreEqual("Geography", teacher.Subjects);
        }

        [TestMethod]
        public void NoArgumentConstructorAndSetterTest()
        {
            ITeacher teacher = new ITeacher
            {
                Surname = "Joe",
                Forename = "Bloggs",
                DOB = DateTime.Today,
                Phone = "083 4567823",
                Type = 'F',
                Salary = 13500,
                Subjects = "Geography"
            };
        }

        [TestMethod]
        public void ValidatePropertyAllErrorTest()
        {
            ITeacher teacher = new ITeacher
            {
                Surname = "Joe@",
                Forename = "Bloggs@",
                DOB = new DateTime(2018, 2, 21),
                Phone = "0834578345",
                Type = ' ',
                Salary = -0,
                Subjects = String.Empty
            };
        }

        [TestMethod]
        public void ValidatePropertyAllErrorForNotOverAgeTest()
        {
            ITeacher teacher = new ITeacher
            {
                Surname = "Joe@",
                Forename = "Bloggs@",
                DOB = new DateTime(2000, 2, 21),
                Phone = "0834578345",
                Type = ' ',
                Salary = -0,
                Subjects = String.Empty
            };
        }

        [TestMethod]
        public void ValidateAllPropertiesTest()
        {
            ITeacher teacher = new ITeacher
            {
                Surname = "Joe",
                Forename = "Bloggs",
                DOB = DateTime.Today,
                Phone = "083 4567823",
                Type = ' ',
                Salary = 13500,
                Subjects = "Geography"
            };

            teacher.ValidateAllProperties();
        }
    }
}
