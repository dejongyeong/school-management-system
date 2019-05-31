using System;
using System.Windows.Input;
using SchoolManagementSystem.Models;
using SchoolManagementSystem;
using System.Windows;
using System.Data.Entity;
using SchoolManagementSystem.ViewModels.IEntities;

namespace SchoolManagementSystem.ViewModels
{
    /*
     * References:
     * GitHub, BuildSys 
     * Source: https://github.com/mdgriffin/BuildSysWPF/blob/master/BuildSys/ViewModels/MainViewModel.cs
     * Author: Michael Griffin
     * 
     * References:
     * WPF & MVVM: Get values from textboxes and send it to ViewModel
     * Source: https://stackoverflow.com/questions/17409010/wpf-mvvm-get-values-from-textboxes-and-send-it-to-viewmodel
     * Author: Reed Copsey
     */

    public class RegisterViewModel : BaseViewModel
    {
        public RegisterAdmin Iadmin { get; set; }

        public RegisterViewModel(BaseViewModel parent)
        {
            Iadmin = new RegisterAdmin();

            this.parent = parent;
        }

        #region ICommand
        public ICommand GoToLogin
        {
            get { return new RelayCommand(param => parent.NavigateTo(new LoginViewModel(parent)), param => true); }
        }

        public ICommand RegisterCommand
        {
            get { return new RelayCommand(param => SaveAdmin(), param => CanSaveAdmin()); }
        }
        #endregion

        #region Methods to determine Can Save Admin
        private void SaveAdmin()
        {
            Iadmin.ValidateAllProperties();

            // Insert and Check for Duplicates
            if (!Iadmin.HasErrors)
            {
                // Insert
                Iadmin.InsertAdmin();

                // Message
                MessageBox.Show("Successfully Registered");

                // Reset Form
                parent.NavigateTo(new RegisterViewModel(parent));
            }
        }

        private bool CanSaveAdmin()
        {
            return !Iadmin.HasErrors;
        }
        #endregion


    }
}
