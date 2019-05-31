using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolManagementSystem.ViewModels.IEntities;
using SchoolManagementSystem.Models;

namespace SMSUnitTest.UnitTest
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void GettersAndSettersTest()
        {
            LoginAdmin admin = new LoginAdmin
            {
                Username = "admin",
                Password = "admin123"
            };

            RegisterAdmin admins = new RegisterAdmin
            {
                Username = "admintesting",
                Password = "admin123"
            };

            UpdatePassword upt = new UpdatePassword
            {
                Username = "admin",
                NewPassword = "john123",
                RepeatPassword = "john123"
            };
        }

        [TestMethod]
        public void GettersAndSettersWithErrorTest()
        {
            LoginAdmin admin = new LoginAdmin
            {
                Username = "adm",
                Password = "admin"
            };

            RegisterAdmin admins = new RegisterAdmin
            {
                Username = "adm",
                Password = "admin"
            };

            UpdatePassword upt = new UpdatePassword
            {
                Username = "john",
                NewPassword = "john",
                RepeatPassword = "john"
            };
        }

        [TestMethod]
        public void ValidatePropertiesTest()
        {
            LoginAdmin admin = new LoginAdmin
            {
                Username = "admin",
                Password = "admin123"
            };

            RegisterAdmin admins = new RegisterAdmin
            {
                Username = "admintesting",
                Password = "admin123"
            };

            UpdatePassword upt = new UpdatePassword
            {
                Username = "johnbloggs",
                NewPassword = "john123",
                RepeatPassword = "john123"
            };

            admins.ValidateAllProperties();
            admin.ValidateAllProperties();
            upt.ValidateAllProperties();
        }

        [TestMethod]
        public void TwoPasswordMatchTest()
        {
            UpdatePassword upt = new UpdatePassword
            {
                Username = "johnbloggs",
                NewPassword = "john123",
                RepeatPassword = "john123"
            };

            Assert.IsTrue(upt.CheckTwoPasswordMatch());
        }

        [TestMethod]
        public void DuplicateUsernameTest()
        {
            RegisterAdmin admin = new RegisterAdmin
            {
                Username = "admin",
                Password = "admin123"
            };

            Assert.IsTrue(admin.CheckForUsernameDuplicates());
        }

        [TestMethod]
        public void MatchingUsernameAndPasswordMatch()
        {
            LoginAdmin admin = new LoginAdmin
            {
                Username = "admin",
                Password = "admin123"
            };

            Assert.IsTrue(admin.CheckUsernameAndPasswordMatch());
        }
    }
}
