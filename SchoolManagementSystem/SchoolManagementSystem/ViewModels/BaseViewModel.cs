using System.Diagnostics.CodeAnalysis;

namespace SchoolManagementSystem.ViewModels
{
    /*
     * References:
     * Github, BuildSys  
     * Source: https://github.com/mdgriffin/BuildSysWPF/blob/master/BuildSys/ViewModels/BaseViewModel.cs
     * Author: Michael Griffin
     * 
     * References:
     * WPF Validation - using INotifyDataErrorInfo
     * Source: https://www.codeproject.com/Tips/876349/WPF-Validation-using-INotifyDataErrorInfo
     * Author: K K Srinivasan
     * Date: 15 Feb 2015
     * 
     */

    [ExcludeFromCodeCoverage]
    public abstract class BaseViewModel : ObservableObject
    {
        // Reference to parent of view model
        protected BaseViewModel parent;

        // Current View Model that display to user
        private BaseViewModel baseViewModel;
        public BaseViewModel BaseModel
        {
            get { return baseViewModel; }
            set
            {
                baseViewModel = value;
                NotifyPropertyChanged("BaseModel");
            }
        }

        // Method to navigate to specified view model
        public void NavigateTo(BaseViewModel vm)
        {
            parent.BaseModel = vm;
        }
    }
}
