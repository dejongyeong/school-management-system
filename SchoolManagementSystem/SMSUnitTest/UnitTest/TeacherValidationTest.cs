using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using SchoolManagementSystem.Validations;

namespace SMSUnitTest.UnitTest
{
    [TestClass]
    public class TeacherValidationTest
    {
        #region Surname Validation
        [TestMethod]
        public void ValidateTeacherSurnameIsValid()
        {
            var surname = "John O'";
            Boolean result = TeacherValidationHelper.IsValidNameFormat(surname);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateTeacherSurnameIsInvalid()
        {
            var surname = "John O'@";
            Boolean result = TeacherValidationHelper.IsValidNameFormat(surname);

            Assert.IsFalse(result);
        }
        #endregion

        #region Forename Validation
        [TestMethod]
        public void ValidateTeacherForenameIsValid()
        {
            var forename = "O' Sullivan";
            Boolean result = TeacherValidationHelper.IsValidNameFormat(forename);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateTeacherForenameInvalid()
        {
            var forename = "O- Sullivan";
            Boolean result = TeacherValidationHelper.IsValidNameFormat(forename);

            Assert.IsFalse(result);
        }
        #endregion

        #region Phone Number
        [TestMethod]
        public void ValidatePhoneFormatIsValid()
        {
            // Valid Format: 083 4785849
            var phone = "083 4874269";
            Boolean result = TeacherValidationHelper.IsValidPhoneFormat(phone);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidatePhoneFormatIsInvalid()
        {
            // Valid Format: 083 4759867
            var phone = "066 78222456";
            Boolean result = TeacherValidationHelper.IsValidPhoneFormat(phone);

            Assert.IsFalse(result);
        }
        #endregion

        #region Age
        [TestMethod]
        public void ValidateAgeIsOver21True()
        {
            // Valid: Must be over 21
            DateTime dateTime = new DateTime(1992, 8, 18);
            Boolean result = TeacherValidationHelper.IsOverAgeTwentyOne(dateTime);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateAgeIsOver21False()
        {
            // Valid: Must be over 21
            DateTime dateTime = new DateTime(1999, 8, 18);

            Boolean result = TeacherValidationHelper.IsOverAgeTwentyOne(dateTime);

            Assert.IsFalse(result);
        }
        #endregion

        #region Is Double Value
        [TestMethod]
        public void ValidateDoubleValueIsTrue()
        {
            // Valid
            Boolean result = TeacherValidationHelper.IsDouble("30.2");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateDoubleValueIsEmpty()
        {
            // Invalid
            Boolean result = TeacherValidationHelper.IsDouble("");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateDoubleValueIsNegative()
        {
            // Invalid
            Boolean result = TeacherValidationHelper.IsDouble("-30.2");
            Assert.IsFalse(result);
        }
        #endregion
    }
}
