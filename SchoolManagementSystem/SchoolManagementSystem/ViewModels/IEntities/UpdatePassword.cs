using SchoolManagementSystem.Models;
using SchoolManagementSystem.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.ViewModels.IEntities
{
    public class UpdatePassword : BaseModel, INotifyDataErrorInfo
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

        private string _newpassword;
        public string NewPassword
        {
            get { return this._newpassword; }
            set
            {
                if (!String.Equals(this._newpassword, value))
                {
                    this._newpassword = value;
                    ValidateProperty("NewPassword");
                    NotifyPropertyChanged("NewPassword");
                }
            }
        }

        private string _repeatpassword;
        public string RepeatPassword
        {
            get { return this._repeatpassword; }
            set
            {
                if(!String.Equals(this._repeatpassword, value))
                {
                    this._repeatpassword = value;
                    ValidateProperty("RepeatPassword");
                    NotifyPropertyChanged("RepeatPassword");
                }
            }
        }
        #endregion

        #region Implements Abstract Methods
        public override void ValidateAllProperties()
        {
            ValidateProperty("Username");
            ValidateProperty("NewPassword");
            ValidateProperty("RepeatPassword");
        }

        public override void ValidateProperty(string propertyName)
        {
            string error = "";

            switch(propertyName)
            {
                case "Username":
                    if (!AdminValidationHelper.IsValidUsernameFormat(Username))
                    {
                        error = AdminValidationHelper.ERROR_USERNAME_FORMAT;
                    }
                    else if(!this.CheckIfUsernameExists())
                    {
                        error = AdminValidationHelper.ERROR_NOT_EXISTS_IN_DATABASE;
                    }
                    break;
                case "NewPassword":
                    if (!AdminValidationHelper.IsValidPasswordFormat(NewPassword))
                    {
                        error = Validations.AdminValidationHelper.ERROR_PASSWORD_FORMAT;
                    }
                    break;
                case "RepeatPassword":
                    if(!AdminValidationHelper.IsValidPasswordFormat(RepeatPassword))
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
        #endregion

        #region Check if Username exists in database
        private string RetrieveUsernameFromDatabase()
        {
            string databaseUsername = "";

            // Refactor code to retrieve username from database
            using (var db = new SMSContext())
            {
                var admins = db.Admins.Where(u => u.username.Trim() == Username.Trim()).ToList();

                foreach (var a in admins)
                {
                    databaseUsername = a.username.Trim();
                }
            }

            return databaseUsername.Trim();
        }

        public bool CheckIfUsernameExists()
        {
            string databaseUsername = this.RetrieveUsernameFromDatabase();

            return AdminValidationHelper.IsUsernameMatch(Username, databaseUsername);
        }
        #endregion

        #region Check if New Password and Reenter Password match
        public bool CheckTwoPasswordMatch()
        {
            return AdminValidationHelper.IsPasswordMatch(NewPassword, RepeatPassword);
        }
        #endregion

        #region Update Password for that Username
        [ExcludeFromCodeCoverage]
        public void Update()
        {
            using(var db = new SMSContext())
            {
                var admin = db.Admins.FirstOrDefault(a => a.username == Username);

                admin.password = NewPassword.Trim();

                db.SaveChanges();
            }
        }
        #endregion
    }
}
