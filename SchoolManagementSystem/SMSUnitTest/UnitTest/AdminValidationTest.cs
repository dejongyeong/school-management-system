using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolManagementSystem.Validations;

namespace SMSUnitTest.UnitTest
{
    [TestClass]
    public class AdminValidationTest
    {
        #region Username Format Validation
        [TestMethod]
        public void ValidateUsernameFormatIsValid()
        {
            // Alphabets Lowercase, Not Null, 5-15 Characters

            var username = "admin";
            Boolean result = AdminValidationHelper.IsValidUsernameFormat(username);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateUsernameFormatIsNotValid()
        {
            var username = "adm";
            Boolean result = AdminValidationHelper.IsValidUsernameFormat(username);

            Assert.IsFalse(result);
        }
        #endregion

        #region Password Format Validation
        [TestMethod]
        public void ValidatePasswordFormatIsValid()
        {
            // Alphanumeric Lowercase, Not Null, 5-15 Characters

            var password = "admin123";
            Boolean result = AdminValidationHelper.IsValidPasswordFormat(password);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidatePasswordForatIsNotValid()
        {
            var password = "admin";
            Boolean result = AdminValidationHelper.IsValidPasswordFormat(password);

            Assert.IsFalse(result);
        }
        #endregion

        #region Username Match
        [TestMethod]
        public void ValidateMatchingUsername()
        {
            string username = "admin";
            string username2 = "admin";

            Assert.IsTrue(AdminValidationHelper.IsUsernameMatch(username, username2));
        }

        [TestMethod]
        public void ValidateNotMatchingUsername()
        {
            string username = "admins";
            string username2 = "admin";

            Assert.IsFalse(AdminValidationHelper.IsUsernameMatch(username, username2));
        }
        #endregion

        #region Password Matching
        [TestMethod]
        public void ValidateMatchingPassword()
        {
            string password = "admin123";
            string password2 = "admin123";

            Assert.IsTrue(AdminValidationHelper.IsPasswordMatch(password, password2));
        }

        [TestMethod]
        public void ValidateNotMatchingPassword()
        {
            string password = "admin123";
            string password2 = "admin1234";

            Assert.IsFalse(AdminValidationHelper.IsPasswordMatch(password, password2));
        }
        #endregion
    }
}
