using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SchoolManagementSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SchoolManagementSystem.ViewModels
{
    public class ViewViewModel
    {
        public string ViewDisplay { get; set; }

        public Type ViewType { get; set; }

        public Type ViewModelType { get; set; }

        public UserControl View { get; set; }

        public RelayCommand Navigation { get; set; }

        public ViewViewModel()
        {
            Navigation = new RelayCommand(NavigationExecute);
        }

        public void NavigationExecute()
        {
            if(View == null && ViewType != null)
            {
                View = (UserControl)Activator.CreateInstance(ViewType);
            }

            var message = new Navigation
            {
                View = View,
                ViewModelType = ViewModelType,
                ViewType = ViewType
            };

            Messenger.Default.Send<Navigation>(message);
        }
    }
}
