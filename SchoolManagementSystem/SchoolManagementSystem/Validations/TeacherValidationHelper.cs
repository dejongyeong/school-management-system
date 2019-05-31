using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace SchoolManagementSystem.Validations
{
    public class TeacherValidationHelper
    {
        [ExcludeFromCodeCoverage]
        protected TeacherValidationHelper() { }

        // Constant Default Error Message
        public const string ERROR_SURNAME_FORMAT = "Surname Invalid. 1-25 Characters. E.g. Marry O' Sullivan";
        public const string ERROR_FORNAME_FORMAT = "Forename Invalid. 1-25 Characters. E.g. Marry O' Sullivan";
        public const string ERROR_AGE_NOTOVERAGE = "Age must be over 21";
        public const string ERROR_AGE_NOTFUTURE = "Date must not on the future";
        public const string ERROR_EMPTY_STRING = "Field must not be empty";
        public const string ERROR_PHONE_FORMAT = "Phone must be in '066<space>3423423' Format";
        public const string ERROR_HOUR_FORMAT = "Hour must be in numeric.";
        public const string ERROR_MONTH_FORMAT = "Month must be in numeric.";

        public static bool IsValidNameFormat(string name)
        {
            // Regex Pattern
            Regex pattern = new Regex(@"^[a-zA-Z''-'\s]{1,25}$");

            if (String.IsNullOrEmpty(name) || !pattern.IsMatch(name))
                return false;
            else
                return true;
        }

        public static bool IsValidPhoneFormat(string phone)
        {
            // Regex Pattern
            Regex pattern = new Regex(@"^\d{3}\s\d{7}$");

            if (String.IsNullOrEmpty(phone) || !pattern.IsMatch(phone))
                return false;
            else
                return true;
        }

        /*****************************************************
         * 
         * Title: How to calculate age from Date of Birth C#
         * Author: Upendra Pratap Shahi
         * Site owner/sponsor: c-sharpcorner.com
         * Date: 25 May 2015
         * Availability: http://www.c-sharpcorner.com/code/961/how-to-calculate-age-from-date-of-birth-in-c-sharp.aspx (Accessed 3 FEB 2017)
         * Modified: Code refactored (method renamed)
         * 
         * *****************************************************/

        public static bool IsOverAgeTwentyOne(DateTime dateTime)
        {
            int age = DateTime.Now.Year - dateTime.Year;

            if(DateTime.Now.DayOfYear < dateTime.DayOfYear)
            {
                age--;
            }

            if (age < 21)
                return false;
            else
                return true;
        }

        public static bool IsDouble(string value)
        {
            bool isDouble = false;            

            try
            {
                double output = Convert.ToDouble(value);

                if(output >= 0)
                {
                    isDouble = true;
                }
            }
            catch (Exception ex)
            {
                isDouble = false;
            }

            return isDouble;
        }
    }
}
