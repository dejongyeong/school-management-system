using System;
using SchoolManagementSystem.ViewModels;
using System.Windows.Input;
using System.Linq;
using SchoolManagementSystem.ViewModels.IEntities;
using System.Windows;
using System.Diagnostics.CodeAnalysis;

namespace SchoolManagementSystem.ViewModels
{
    /*
     * GitHub, BuildSys 
     * Source: https://github.com/mdgriffin/BuildSysWPF/blob/master/BuildSys/ViewModels/MainViewModel.cs
     * Author: Michael Griffin
     * 
     */

    [ExcludeFromCodeCoverage]
    public class LoginViewModel : BaseViewModel
    {
        public LoginAdmin Ilogin { get; set; }

        public LoginViewModel(BaseViewModel parent)
        {
            Ilogin = new LoginAdmin();

            this.parent = parent;
        }

        public ICommand GoToRegister
        {
            get { return new RelayCommand(param => parent.NavigateTo(new RegisterViewModel(parent)), param => true); }
        }

        public ICommand GoToHome
        {
            get { return new RelayCommand(param => LoginAdmin(), param => CanLoginAdmin()); }
        }

        #region Methods to determine Can Save Admin
        private void LoginAdmin()
        {
            Ilogin.ValidateAllProperties();

            // Insert and Check if username and password match
            if (!Ilogin.HasErrors)
            {
                if (Ilogin.CheckUsernameAndPasswordMatch())
                {
                    // Navigate To HomeViewModel
                    parent.NavigateTo(new HomeViewModel(parent));
                }
                else
                {
                    // Display error message to user indicate that username and password entered does not match in database
                    MessageBox.Show(Validations.AdminValidationHelper.ERROR_INVALID_USERNAME_PASSWORD, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CanLoginAdmin()
        {
            return !Ilogin.HasErrors;
        }
        #endregion
    }
}
