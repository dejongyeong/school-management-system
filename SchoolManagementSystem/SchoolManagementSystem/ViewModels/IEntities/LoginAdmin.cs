using System;
using System.ComponentModel;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Validations;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace SchoolManagementSystem.ViewModels.IEntities
{
    public class LoginAdmin : BaseModel, INotifyDataErrorInfo
    {

        #region Entities
        private string _username;
        public string Username
        {
            get { return this._username; }
            set
            {
                if (!String.Equals(this._username, value))
                {
                    this._username = value;
                    ValidateProperty("Username");
                    NotifyPropertyChanged("Username");
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return this._password; }
            set
            {
                if (!String.Equals(this._password, value))
                {
                    this._password = value;
                    ValidateProperty("Password");
                    NotifyPropertyChanged("Password");
                }
            }
        }
        #endregion

        #region Abstract Method Implementation
        public override void ValidateProperty(string propertyName)
        {
            string error = "";

            switch (propertyName)
            {
                case "Username":
                    if (!Validations.AdminValidationHelper.IsValidUsernameFormat(Username))
                    {
                        error = Validations.AdminValidationHelper.ERROR_USERNAME_FORMAT;
                    }
                    break;
                case "Password":
                    if (!Validations.AdminValidationHelper.IsValidPasswordFormat(Password))
                    {
                        error = Validations.AdminValidationHelper.ERROR_PASSWORD_FORMAT;
                    }
                    break;
            }

            if (error != "")
            {
                AddErrors(propertyName, error);
            }
            else
            {
                RemoveErrors(propertyName);
            }
        }

        public override void ValidateAllProperties()
        {
            ValidateProperty("Username");
            ValidateProperty("Password");
        }
        #endregion

        /*
         * Reference:
         * How to select a single column with Entity Framework?
         * Source: https://stackoverflow.com/questions/9054609/how-to-select-a-single-column-with-entity-framework
         * Author: Gary Chapman
         * 
         */

        #region Retrieve Username
        [ExcludeFromCodeCoverage]
        private string RetrieveUsernameFromDatabase()
        {
            string databaseUsername = "";

            // Refactor code to retrieve username from database
            using (var db = new SMSContext())
            {
                var admins = db.Admins.Where(u => u.username.Trim() == Username.Trim()).ToList();

                foreach(var a in admins)
                {
                    databaseUsername = a.username.Trim();
                }
            }

            return databaseUsername.Trim();
        }
        #endregion

        #region Retrieve Password
        [ExcludeFromCodeCoverage]
        private string RetrievePasswordFromDatabase()
        {
            string databasePassword = "";

            // Refactor code to retrieve password from database
            using (var db = new SMSContext())
            {
                var admins = db.Admins.Where(u => u.username.Trim() == Username.Trim()).ToList();

                foreach (var a in admins)
                {
                    databasePassword = a.password.Trim();
                }
            }

            return databasePassword.Trim();
        }
        #endregion

        #region Check if Username and Password Match
        public bool CheckUsernameAndPasswordMatch()
        {
            string databaseUsername = this.RetrieveUsernameFromDatabase();
            string databasePassword = this.RetrievePasswordFromDatabase();

            return (AdminValidationHelper.IsUsernameMatch(Username, databaseUsername) && AdminValidationHelper.IsPasswordMatch(Password, databasePassword));
        }
        #endregion
    }
}
