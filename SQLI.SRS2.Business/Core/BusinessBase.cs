using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SQLI.SRS2.Business.Core
{
    public class BusinessBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CopyProperties(object source)
        {
            if (source == null) return;

            foreach (var property in source.GetType().GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(this, property.GetValue(source));
                }
            }
        }

        public void CopyPropertiesTo(object target, params string[] propertiesToSkip)
        {
            if (this == null) return;

            foreach (var property in this.GetType().GetProperties())
            {
                if (propertiesToSkip.Length > 0)
                {
                    if (propertiesToSkip.FirstOrDefault(p => p == property.Name) != null)
                        continue;
                }
                if (property.CanWrite)
                {
                    property.SetValue(target, property.GetValue(this));
                }
            }
        }

        protected void SetProperty<T>(ref T oldValue, T newValue)
        {
            if ((oldValue == null && newValue == null) || (oldValue != null && oldValue.Equals(newValue)))
                return;

            oldValue = newValue;
            NotifyPropertyChanged();
        }
    }
}
