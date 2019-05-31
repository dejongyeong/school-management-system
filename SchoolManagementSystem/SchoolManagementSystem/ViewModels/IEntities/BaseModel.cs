using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SchoolManagementSystem.ViewModels.IEntities
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseModel : ObservableObject
    {
        #region INotifyDataErrorInfo
        // Stores errors raised on properties
        public Dictionary<string, List<string>> propertyErrors = new Dictionary<string, List<string>>();

        // Raised when errors changed
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get
            {
                try
                {
                    var propertyErrorsCount = propertyErrors.Values.FirstOrDefault(r => r.Count > 0);

                    if (propertyErrorsCount != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch { }

                return true;
            }
        }

        // Get list of defined errors from dictionary
        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errors = new List<string>();

            if (propertyName != null)
            {
                propertyErrors.TryGetValue(propertyName, out errors);
                return errors;
            }
            else
            {
                return null;
            }
        }

        public void AddErrors(string propertyName, string error)
        {
            this.propertyErrors[propertyName] = new List<string>() { error };
            this.NotifyErrorsChanged(propertyName);
        }

        public void RemoveErrors(string propertyName)
        {
            if (this.propertyErrors.ContainsKey(propertyName))
            {
                this.propertyErrors.Remove(propertyName);
            }

            this.NotifyErrorsChanged(propertyName);
        }

        public void NotifyErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion

        #region Abstract Method
        public abstract void ValidateProperty(string propertyName);
        public abstract void ValidateAllProperties();
        #endregion
    }
}
