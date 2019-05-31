using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SchoolManagementSystem
{
    /*
     * Reference:
     * Easy MVVM Example With INotifyPropertyChanged And INotifyDataErrorInfo
     * Source: http://blog.micic.ch/net/easy-mvvm-example-with-inotifypropertychanged-and-inotifydataerrorinfo
     * Author: Darko Micic
     * 
     * Reference:
     * A Simple MVVM Example
     * Source: https://rachel53461.wordpress.com/2011/05/08/simplemvvmexample/amp/
     * Author: Rachel
     */

    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Raised when the value of a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
