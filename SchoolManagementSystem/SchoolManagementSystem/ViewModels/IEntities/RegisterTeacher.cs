using System;
using System.ComponentModel;
using System.Linq;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SchoolManagementSystem.ViewModels.IEntities
{
    public class RegisterTeacher : BaseModel, INotifyDataErrorInfo
    {
        // Fixed Hourly Rate
        private const double WORKING_HOURLY_RATE_FOR_PARTTIME_TEACHER = 9.30;
        private const int TOTAL_MONTHS_PER_YEAR = 12;

        #region Entities
        public int TeacherID { get; set; }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (this._surname != value)
                {
                    this._surname = value;
                    ValidateProperty("Surname");
                    NotifyPropertyChanged("Surname");
                }
            }
        }


        private string _forename;
        public string Forename
        {
            get { return _forename; }
            set
            {
                if(!String.Equals(this._forename, value))
                {
                    this._forename = value;
                    ValidateProperty("Forename");
                    NotifyPropertyChanged("Forename");
                }
            }
        }

        private DateTime _dob = DateTime.Today;
        public DateTime DOB
        {
            get { return _dob; }
            set
            {
                if(!DateTime.Equals(this._dob, value))
                {
                    this._dob = value;
                    ValidateProperty("DOB");
                    NotifyPropertyChanged("DOB");
                }
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if(!String.Equals(this._phone, value))
                {
                    this._phone = value;
                    ValidateProperty("Phone");
                    NotifyPropertyChanged("Phone");
                }
            }
        }

        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set
            {
                if(!String.Equals(this._subject, value))
                {
                    this._subject = value;
                    ValidateProperty("Subject");
                    NotifyPropertyChanged("Subject");
                }
            }
        }

        private char _type;
        public char Type
        {
            get { return _type; }
            set
            {
                if(value != _type)
                {
                    this._type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        private string _salaryPartTime = "0.00";
        public string SalaryPartTime
        {
            get { return _salaryPartTime; }
            set
            {
                if(this._salaryPartTime != value)
                {
                    this._salaryPartTime = value;
                    NotifyPropertyChanged("SalaryPartTime");
                }
            }
        }

        private string _salaryFullTime = "0.00";
        public string SalaryFullTime
        {
            get { return _salaryFullTime; }
            set
            {
                if (this._salaryFullTime != value)
                {
                    this._salaryFullTime = value;
                    NotifyPropertyChanged("SalaryFullTime");
                }
            }
        }

        // Properties not in Database
        private string _monthlyFullTime;
        public string MonthlyFullTime
        {
            get { return _monthlyFullTime; }
            set
            {
                if(this._monthlyFullTime != value)
                {
                    this._monthlyFullTime = value;
                    ValidateProperty("MonthlyFullTime");
                    NotifyPropertyChanged("MonthlyFullTime");
                    CalculateAnnualSalaryFullTime();
                }
            }
        }

        private string _monthlyPartTime = "0.00";
        public string MonthlyPartTime
        {
            get { return _monthlyPartTime; }
            set
            {
                this._monthlyPartTime = value;
                NotifyPropertyChanged("MonthlyPartTime");
                CalculateAnnualSalaryPartTime();
            }
        }

        private string _hours;
        public string Hours
        {
            get { return _hours; }
            set
            {
                if(this._hours != value)
                {
                    this._hours = value;
                    ValidateProperty("Hours");
                    NotifyPropertyChanged("Hours");
                    CalculateMonthlySalaryForPartTime();
                }
            }
        }
        #endregion

        #region Get Next Row ID
        
        public int GetNextRowID()
        {
            int nextRowId = 0;

            using(var db = new SMSContext())
            {
                var max = db.Teachers.Select(id => id.teacherID).DefaultIfEmpty(0).Max();

                nextRowId = max + 1;
            }
            return nextRowId;
        }
        #endregion

        #region Absract Method Implementation
        public override void ValidateAllProperties()
        {
            ValidateProperty("Surname");
            ValidateProperty("Forename");
            ValidateProperty("DOB");
            ValidateProperty("Phone");
            ValidateProperty("Type");
            ValidateProperty("Subject");

            if(Type == 'P')
            {
                ValidateProperty("Hours");
            }
            else
            {
                ValidateProperty("MonthlyFullTime");
            }
        }

        public override void ValidateProperty(string propertyName)
        {
            string error = "";

            switch(propertyName)
            {
                case "Surname":
                    if(!Validations.TeacherValidationHelper.IsValidNameFormat(Surname))
                    {
                        error = Validations.TeacherValidationHelper.ERROR_SURNAME_FORMAT;
                    }
                    break;
                case "Forename":
                    if(!Validations.TeacherValidationHelper.IsValidNameFormat(Forename))
                    {
                        error = Validations.TeacherValidationHelper.ERROR_FORNAME_FORMAT;
                    }
                    break;
                case "DOB":
                    if(DOB == DateTime.Today)
                    {
                        error = Validations.TeacherValidationHelper.ERROR_EMPTY_STRING;
                    }
                    else if(DOB > DateTime.Today)
                    {
                        error = Validations.TeacherValidationHelper.ERROR_AGE_NOTFUTURE;
                    }
                    else if(!Validations.TeacherValidationHelper.IsOverAgeTwentyOne(DOB))
                    {
                        error = Validations.TeacherValidationHelper.ERROR_AGE_NOTOVERAGE;
                    }
                    break;
                case "Phone":
                    if(!Validations.TeacherValidationHelper.IsValidPhoneFormat(Phone))
                    {
                        error = Validations.TeacherValidationHelper.ERROR_PHONE_FORMAT;
                    }
                    break;
                case "Type":
                    if(Type == ' ')
                    {
                        error = Validations.TeacherValidationHelper.ERROR_EMPTY_STRING;
                    }
                    break;
                case "Hours":
                    if(!Validations.TeacherValidationHelper.IsDouble(Hours))
                    {
                        error = Validations.TeacherValidationHelper.ERROR_HOUR_FORMAT;
                    }
                    break;
                case "MonthlyFullTime":
                    if(!Validations.TeacherValidationHelper.IsDouble(MonthlyFullTime))
                    {
                        error = Validations.TeacherValidationHelper.ERROR_MONTH_FORMAT;
                    }
                    break;
                case "Subject":
                    if(String.IsNullOrEmpty(Subject))
                    {
                        error = Validations.TeacherValidationHelper.ERROR_EMPTY_STRING;
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

        #region Calculate Monthly Salary for Part Time
        public void CalculateMonthlySalaryForPartTime()
        {
            int totalWeeks = TotalWeeksInMonthHelper.GetTotalWeeksInMonth(DateTime.Today);

            if(!HasErrors)
            {
                MonthlyPartTime = (double.Parse(Hours) * WORKING_HOURLY_RATE_FOR_PARTTIME_TEACHER * totalWeeks).ToString("0,0.00");
            }
            else
            {
                MonthlyPartTime = "0.00";
            }
        }
        #endregion

        #region Calculate Annual Salary
        public void CalculateAnnualSalaryFullTime()
        {
            if (!HasErrors)
            {
                if (Type == 'F')
                {
                    SalaryFullTime = (double.Parse(MonthlyFullTime) * TOTAL_MONTHS_PER_YEAR).ToString("0,0.00");
                }
            }
            else
            {
                SalaryFullTime = "0.00";
            }
        }

        public void CalculateAnnualSalaryPartTime()
        {
            if(Type == 'P')
            {
                SalaryPartTime = (double.Parse(MonthlyPartTime) * TOTAL_MONTHS_PER_YEAR).ToString("0,0.00");
            }
        }
        #endregion

        #region Insert Into Database
        [ExcludeFromCodeCoverage]
        public void InsertTeachers()
        {
            using(var db = new SMSContext())
            {
                int id = this.GetNextRowID();

                if (Type == 'P')
                {
                    var teachers = new Teacher { teacherID = id, surname = Surname.Trim(), forename = Forename.Trim(), dob = DOB.Date, phone = Phone.Trim(), type = Type.ToString(), salary = Convert.ToDecimal(SalaryPartTime.Trim()), subjects = Subject.Trim() };
                    db.Teachers.Add(teachers);
                }
                else
                {
                    var teachers = new Teacher { teacherID = id, surname = Surname.Trim(), forename = Forename.Trim(), dob = DOB.Date, phone = Phone.Trim(), type = Type.ToString(), salary = Convert.ToDecimal(SalaryFullTime.Trim()), subjects = Subject.Trim() };
                    db.Teachers.Add(teachers);
                }

                db.SaveChanges();
            }
        }
        #endregion
    }
}
