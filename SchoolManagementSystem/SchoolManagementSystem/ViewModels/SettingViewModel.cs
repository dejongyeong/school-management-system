using SchoolManagementSystem.ViewModels.IEntities;
using SchoolManagementSystem.Validations;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics.CodeAnalysis;

namespace SchoolManagementSystem.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class SettingViewModel : BaseViewModel
    {
        public UpdatePassword Iupdate { get; set; }

        public SettingViewModel(BaseViewModel parent)
        {
            Iupdate = new UpdatePassword();

            this.parent = parent;
        }

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

        public ICommand Update
        {
            get { return new RelayCommand(param => UpdateAdmin(), param => CanUpdateAdmin()); }
        }

        public ICommand EditCommand
        {
            get { return new RelayCommand(param => parent.NavigateTo(new TeacherManagementViewModel(parent)), param => true); }
        }
        #endregion


        #region Update
        private void UpdateAdmin()
        {
            Iupdate.ValidateAllProperties();

            if(!Iupdate.HasErrors)
            {
                if (Iupdate.CheckTwoPasswordMatch())
                {
                    // Update Record
                    Iupdate.Update();

                    MessageBox.Show("Update Succesfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Reset UI
                    parent.NavigateTo(new SettingViewModel(parent));
                }
                else
                {
                    MessageBox.Show(AdminValidationHelper.ERROR_PASSWORD_NOT_MATCH, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CanUpdateAdmin()
        {
            return !Iupdate.HasErrors;
        }
        #endregion
    }
}
