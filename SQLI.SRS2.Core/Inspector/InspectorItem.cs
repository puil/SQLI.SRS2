using Prism.Mvvm;

namespace SQLI.SRS2.Core.Inspector
{
    public class InspectorItem : BindableBase
    {
        private string key;
        public string Key { get => key; set => SetProperty(ref key, value); }
    }
}
