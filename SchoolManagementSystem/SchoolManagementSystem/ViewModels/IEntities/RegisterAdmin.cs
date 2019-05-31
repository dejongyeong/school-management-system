using System;
using SchoolManagementSystem.ViewModels;
using System.ComponentModel;
using SchoolManagementSystem.Models;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace SchoolManagementSystem.ViewModels.IEntities
{
    public class RegisterAdmin : BaseModel, INotifyDataErrorInfo
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
                    else if (CheckForUsernameDuplicates())
                    {
                        error = Validations.AdminValidationHelper.ERROR_DUPLICATE_USERNAME;
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

        #region Insert Into Database
        [ExcludeFromCodeCoverage]
        public void InsertAdmin()
        {
            using (var db = new SMSContext())
            {
                var admin = new Admin { username = Username.Trim(), password = Password.Trim() };
                db.Admins.Add(admin);
                db.SaveChanges();
            }
        }
        #endregion

        #region Check for Duplicates
        /*
         * References:
         * Entity Framework Local Data
         * Source: https://msdn.microsoft.com/en-us/library/jj592872(v=vs.113).aspx
         * Author: Microsoft Documentation
         */
        public bool CheckForUsernameDuplicates()
        {
            bool duplicate = false;

            using (var db = new SMSContext())
            {
                // Load all admins to context
                db.Admins.Load();

                // Loop over admins data in database
                foreach (var admin in db.Admins)
                {
                    if (String.Equals(admin.username.Trim(), Username))
                    {
                        duplicate = true;
                    }
                }
            }

            return duplicate;
        }
        #endregion
    }
}
