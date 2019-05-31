using SchoolManagementSystem.ViewModels.IEntities;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementSystem.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        // Properties
        [ExcludeFromCodeCoverage]
        public string DisplayPartTimeSalary { get; set; }

        [ExcludeFromCodeCoverage]
        public string DisplayFullTimeSalary { get; set; }

        public RegisterTeacher Iregister { get; set; }

        [ExcludeFromCodeCoverage]
        public HomeViewModel(BaseViewModel parent)
        {
            Iregister = new RegisterTeacher();
            Iregister.PropertyChanged += OnPartTimePropertyChanged;
            Iregister.PropertyChanged += OnFullTimePropertyChanged;

            // Salary Inputs are not visible by default
            DisplayFullTimeSalary = "Collapsed";
            DisplayPartTimeSalary = "Collapsed";

            this.parent = parent;
        }

        #region Property Changed
        //Implement methods for OnTeacherTypePropertyChanged to hide properties in View
        [ExcludeFromCodeCoverage]
        private void OnPartTimePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals("Type"))
            {
                if(Iregister.Type == 'P')
                {
                    DisplayPartTimeSalary = "Visible";
                }
                else
                {
                    DisplayPartTimeSalary = "Collapsed";
                }
                NotifyPropertyChanged("DisplayPartTimeSalary");
            }
        }

        [ExcludeFromCodeCoverage]
        private void OnFullTimePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals("Type"))
            {
                if(Iregister.Type == 'F')
                {
                    DisplayFullTimeSalary = "Visible";
                }
                else
                {
                    DisplayFullTimeSalary = "Collapsed";
                }
                NotifyPropertyChanged("DisplayFullTimeSalary");
            }
        }
        #endregion

        #region ICommand
        [ExcludeFromCodeCoverage]
        public ICommand Logout
        {
            get { return new RelayCommand(param => parent.NavigateTo(new LoginViewModel(parent)), param => true); }
        }

        [ExcludeFromCodeCoverage]
        public ICommand RegisterTeacherCommand
        {
            get { return new RelayCommand(param => SaveTeacher(), param => CanSaveTeacher()); }
        }

        [ExcludeFromCodeCoverage]
        public ICommand HomeCommand
        {
            get { return new RelayCommand(param => parent.NavigateTo(new HomeViewModel(parent)), param => true); }
        }

        [ExcludeFromCodeCoverage]
        public ICommand SettingCommand
        {
            get { return new RelayCommand(param => parent.NavigateTo(new SettingViewModel(parent)), param => true); }
        }

        [ExcludeFromCodeCoverage]
        public ICommand EditCommand
        {
            get { return new RelayCommand(param => parent.NavigateTo(new TeacherManagementViewModel(parent)), param => true); }
        }
        #endregion

        #region Private Save Method
        [ExcludeFromCodeCoverage]
        private void SaveTeacher()
        {
            Iregister.ValidateAllProperties();

            // Register Teacher
            if(!Iregister.HasErrors)
            {
                // Insert
                Iregister.InsertTeachers();

                // Message
                MessageBox.Show("Successfully Registered Teacher");

                // Reset Form
                parent.NavigateTo(new HomeViewModel(parent));
            }
        }

        [ExcludeFromCodeCoverage]
        private bool CanSaveTeacher()
        {
            return !Iregister.HasErrors;
        }
        #endregion
    }
}
