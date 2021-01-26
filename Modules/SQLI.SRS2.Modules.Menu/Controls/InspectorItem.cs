using Prism.Mvvm;

namespace SQLI.SRS2.Modules.Menu.Controls
{
    public class InspectorItem : BindableBase
    {
        private string iconUri;
        public string IconUri
        {
            get { return iconUri; }
            set { SetProperty(ref iconUri, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }
    }
}
