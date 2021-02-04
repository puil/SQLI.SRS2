namespace SQLI.SRS2.Core.Inspector
{
    public class ButtonInspectorItem : InspectorItem
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
