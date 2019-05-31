using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace SchoolManagementSystem.Validations
{
    public class AdminValidationHelper
    {
        [ExcludeFromCodeCoverage]
        protected AdminValidationHelper() { }

        // Constant Default Error Message
        public const string ERROR_USERNAME_FORMAT = "Username Format Invalid. 5-15 Lowercase Alphabets";
        public const string ERROR_PASSWORD_FORMAT = "Password Format Invalid. 5-15 Lowercase Alphanumerics";
        public const string ERROR_DUPLICATE_USERNAME = "Username exists in database";
        public const string ERROR_NOT_EXISTS_IN_DATABASE = "Username does not exists in database";
        public const string ERROR_INVALID_USERNAME_PASSWORD = "Username/Password Not Match";
        public const string ERROR_PASSWORD_NOT_MATCH = "Password does not match";

        public static Boolean IsValidUsernameFormat(string username)
        {
            // Regex Pattern
            Regex pattern = new Regex(@"^[a-z]{5,15}$");

            if (string.IsNullOrEmpty(username) || !pattern.IsMatch(username))
                return false;
            else
                return true;
        }

        public static Boolean IsValidPasswordFormat(string password)
        {
            // Regex Pattern
            // Reference: https://msdn.microsoft.com/en-us/library/ms998267.aspx
            Regex pattern = new Regex(@"(?!^[0-9]*$)(?!^[a-z]*$)^([a-z0-9]{5,15})$");

            if (String.IsNullOrEmpty(password) || !pattern.IsMatch(password))
                return false;
            else
                return true;
        }

        public static Boolean IsUsernameMatch(string inputUsername, string databaseUsername)
        {
            if (String.Equals(inputUsername, databaseUsername))
                return true;
            else
                return false;
        }

        public static Boolean IsPasswordMatch(string inputPassword, string databasePassword)
        {
            if (String.Equals(inputPassword, databasePassword))
                return true;
            else
                return false;
        }
    }
}
