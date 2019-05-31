using System;
using System.Diagnostics;
using System.Windows.Input;

namespace SchoolManagementSystem
{
    /**
     * WPF Enterprise MVVM Session 1: Building a base ViewModel class
     * Source: https://www.youtube.com/watch?v=lool8Ut58Xw&t=1313s
     * Author: DCOM Engineering, LLC
     * 
     * Reference: 
     * Implementation of the ICommand Interface
     * Source: https://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090030
     * Author: Josh Smith
     * 
     * Reference:
     * A simple MVVM Example
     * Source: https://rachel53461.wordpress.com/2011/05/08/simplemvvmexample/amp/
     * Author: Rachel
     */

    public class RelayCommand : ICommand
    {
        private readonly Action<Object> action;
        private readonly Predicate<Object> predicate;

        // New command that can always execute
        public RelayCommand(Action<Object> action) : this(action, null)
        {
        }

        public RelayCommand(Action<Object> action, Predicate<Object> predicate)
        {
            if(action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.action = action;
            this.predicate = predicate;
        }

        #region ICommand Members
        // Defines the method that determines whether the command can execute in its current state,
        public bool CanExecute(object parameter)
        {
            return predicate == null ? true : predicate(parameter);
        }

        // Defines the method to be called when command is invoked.
        public void Execute(object parameter)
        {
            action(parameter);
        }

        public void Execute()
        {
            Execute(null);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion
    }
}
