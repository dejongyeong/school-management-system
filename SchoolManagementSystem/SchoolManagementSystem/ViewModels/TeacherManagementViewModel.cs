using SchoolManagementSystem.ViewModels.IEntities;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementSystem.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class TeacherManagementViewModel : BaseViewModel
    {
        public ITeacher Iteacher { get; set; }

        public TeacherManagementViewModel(BaseViewModel parent)
        {
            List = ITeacher.GetTeacherList();

            Iteacher = new ITeacher();

            this.parent = parent;
        }

        private ObservableCollection<ITeacher> _list;
        public ObservableCollection<ITeacher> List
        {
            get { return _list ?? (_list = new ObservableCollection<ITeacher>()); }
            set
            {
                if (value == null) return;
                _list = value;
                NotifyPropertyChanged("List");
            }
        }

        #region Selected Item
        private ITeacher _current;
        public ITeacher Current
        {
            get { return _current; }
            set
            {
                if (value != _current)
                {
                    this._current = value;
                    Iteacher = new ITeacher(Current.TeacherId, Current.Surname, Current.Forename, Current.DOB, Current.Phone, Current.Type, Current.Salary, Current.Subjects);
                    NotifyPropertyChanged("Current");
                }
            }
        }
        #endregion

        #region ICommand
        public ICommand Logout
        {
            get { return new RelayCommand(param => parent.NavigateTo(new LoginViewModel(parent)), param => true); }
        }

        public ICommand HomeCommand
        {
            get { return new RelayCommand(param => parent.NavigateTo(new HomeViewModel(parent)), param => true); }
        }

        public ICommand SettingCommand
        {
            get { return new RelayCommand(param => parent.NavigateTo(new SettingViewModel(parent)), param => true); }
        }

        public ICommand EditCommand
        {
            get { return new RelayCommand(param => parent.NavigateTo(new TeacherManagementViewModel(parent)), param => true); }
        }

        public ICommand UpdateCommand
        {
            get { return new RelayCommand(param => UpdateTeacher(), param => CanUpdateTeacher()); }
        }

        public ICommand DeleteCommand
        {
            get { return new RelayCommand(param => DeleteTeacher(), param => true); }
        }
        #endregion

        #region Private Update Method
        private void UpdateTeacher()
        {
            Iteacher.ValidateAllProperties();

            if(!Iteacher.HasErrors)
            {
                // Update Teacher
                Iteacher.Update(Current.TeacherId, Current.Surname, Current.Forename, Current.DOB, Current.Phone, Current.Type, Current.Salary, Current.Subjects);

                // Information Message
                MessageBox.Show("Update Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset UI
                parent.NavigateTo(new TeacherManagementViewModel(parent));
            }
        }

        private bool CanUpdateTeacher()
        {
            return !Iteacher.HasErrors;
        }
        #endregion

        #region Delete
        private void DeleteTeacher()
        {
            var results = MessageBox.Show("Delete Teacher " + Current.Forename + " " + Current.Forename, "Information", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if(results == MessageBoxResult.Yes)
            {
                // Delete Teacher
                Iteacher.Delete(Current.TeacherId);

                // Information Message
                MessageBox.Show("Deleted Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset UI
                parent.NavigateTo(new TeacherManagementViewModel(parent));
            }
        }
        #endregion
    }
}
