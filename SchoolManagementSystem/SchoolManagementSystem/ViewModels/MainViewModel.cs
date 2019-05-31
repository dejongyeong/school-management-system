using System;
using System.Diagnostics.CodeAnalysis;

namespace SchoolManagementSystem.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            this.parent = this;
            parent.BaseModel = new LoginViewModel(this);
        }
    }
}
