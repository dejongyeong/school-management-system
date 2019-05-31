using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SchoolManagementSystem.Validations;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace SchoolManagementSystem.ViewModels.IEntities
{
    public class ITeacher : BaseModel, INotifyDataErrorInfo
    {
        #region Constructor
        public ITeacher(int TeacherId, string surname, string forename, DateTime dob, string phone, char type, decimal salary, string subjects)
        {
            this.TeacherId = TeacherId;
            this._surname = surname;
            this._forename = forename;
            this._dob = dob;
            this._phone = phone;
            this._type = type;
            this._salary = salary;
            this._subjects = subjects;
        }

        [ExcludeFromCodeCoverage]
        public ITeacher()
        {
        }
        #endregion

        #region Entities
        public int TeacherId { get; set; }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                if(value != _surname)
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
                if(value != _forename)
                {
                    this._forename = value;
                    ValidateProperty("Forename");
                    NotifyPropertyChanged("Forename");
                }
            }
        }

        private DateTime _dob;
        public DateTime DOB
        {
            get { return _dob; }
            set
            {
               if(value != _dob)
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
                if(value != _phone)
                {
                    this._phone = value;
                    ValidateProperty("Phone");
                    NotifyPropertyChanged("Phone");
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

        private Decimal _salary;
        public Decimal Salary
        {
            get { return _salary; }
            set
            {
                if(value != _salary)
                {
                    this._salary = value;
                    NotifyPropertyChanged("Salary");
                }
            }
        }

        private string _subjects;
        public string Subjects
        {
            get { return _subjects; }
            set
            {
                if(value != _subjects)
                {
                    this._subjects = value;
                    ValidateProperty("Subjects");
                    NotifyPropertyChanged("Subjects");
                }
            }
        }
        #endregion

        #region Abstract Method
        public override void ValidateProperty(string propertyName)
        {
            string error = "";

            switch(propertyName)
            {
                case "Surname":
                    if(!TeacherValidationHelper.IsValidNameFormat(Surname))
                    {
                        error = TeacherValidationHelper.ERROR_SURNAME_FORMAT;
                    }
                    break;
                case "Forename":
                    if(!TeacherValidationHelper.IsValidNameFormat(Forename))
                    {
                        error = TeacherValidationHelper.ERROR_FORNAME_FORMAT;
                    }
                    break;
                case "DOB":
                    if(DOB == DateTime.Today)
                    {
                        error = TeacherValidationHelper.ERROR_EMPTY_STRING;
                    }
                    else if(DOB > DateTime.Today)
                    {
                        error = TeacherValidationHelper.ERROR_AGE_NOTFUTURE;
                    }
                    else if(!TeacherValidationHelper.IsOverAgeTwentyOne(DOB))
                    {
                        error = TeacherValidationHelper.ERROR_AGE_NOTOVERAGE;
                    }
                    break;
                case "Phone":
                    if(!TeacherValidationHelper.IsValidPhoneFormat(Phone))
                    {
                        error = TeacherValidationHelper.ERROR_PHONE_FORMAT;
                    }
                    break;
                case "Type":
                    if(Type == ' ')
                    {
                        error = TeacherValidationHelper.ERROR_EMPTY_STRING;
                    }
                    break;
                case "Subjects":
                    if(String.IsNullOrEmpty(Subjects))
                    {
                        error = TeacherValidationHelper.ERROR_EMPTY_STRING;
                    }
                    break;
            }

            if(error != "")
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
            ValidateProperty("Surname");
            ValidateProperty("Forename");
            ValidateProperty("DOB");
            ValidateProperty("Phone");
            ValidateProperty("Type");
            ValidateProperty("Subjects");
        }
        #endregion

        #region Teacher List
        [ExcludeFromCodeCoverage]
        public static ObservableCollection<ITeacher> GetTeacherList()
        {
            ObservableCollection<ITeacher> list = new ObservableCollection<ITeacher>();

            using(var db = new SMSContext())
            {
                var teachers = from t in db.Teachers select t;

                foreach(var row in teachers)
                {
                    list.Add(new ITeacher(row.teacherID, row.surname.Trim(), row.forename.Trim(), row.dob.Date, row.phone.Trim(), row.type.ToCharArray()[0], Convert.ToDecimal(row.salary), row.subjects));
                }
            }

            return list;
        }
        #endregion

        #region Update
        [ExcludeFromCodeCoverage]
        public void Update(int teacherId, string surname, string forename, DateTime dob, string phone, char type, decimal salary, string subjects)
        {
            using(var db = new SMSContext())
            {
                var teachers = db.Teachers.FirstOrDefault(a => a.teacherID == TeacherId);

                teachers.surname = surname.Trim();
                teachers.forename = forename.Trim();
                teachers.dob = dob.Date;
                teachers.type = type.ToString();
                teachers.phone = phone.Trim();
                teachers.subjects = subjects;

                db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        [ExcludeFromCodeCoverage]
        public void Delete(int teacherId)
        {
            using(var db = new SMSContext())
            {
                var teachers = db.Teachers.FirstOrDefault(a => a.teacherID == TeacherId);

                db.Teachers.Remove(teachers);

                db.SaveChanges();
            }
        }
        #endregion
    }
}
