using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CashpointWPF.ViewModel
{
    public class ErrorViewModel : INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> propertyErrors = new Dictionary<string, List<string>>();

        public bool HasErrors => propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return propertyErrors.GetValueOrDefault(propertyName, null);
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if(!propertyErrors.ContainsKey(propertyName))
            {
                propertyErrors[propertyName] = new List<string>();
            }

            propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        public void ClearErrors(string propertyName)
        {
            if(propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}