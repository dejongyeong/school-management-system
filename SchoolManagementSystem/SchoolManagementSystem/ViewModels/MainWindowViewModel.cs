using SchoolManagementSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public ObservableCollection<CommandViewModel> Commands { get; set; }

        public ObservableCollection<ViewViewModel> Views { get; set; }

        public string Message { get; set; }

        public MainWindowViewModel()
        {
            ObservableCollection<ViewViewModel> views = new ObservableCollection<ViewViewModel>
            {
                new ViewViewModel {ViewDisplay="Admins", ViewType=typeof(RegisterAdminView), ViewModelType=typeof(AdminViewModel)}
            };

            Views = views;
            NotifyPropertyChanged("Views");
            views[0].NavigationExecute();

            ObservableCollection<CommandViewModel> commands = new ObservableCollection<CommandViewModel>
            {
               new CommandViewModel { DisplayCommand="Insert", Message=new CommandMessage{ Command = CommandType.Insert}},
            };

            Commands = commands;
            NotifyPropertyChanged("Commands");
        }
    }
}
