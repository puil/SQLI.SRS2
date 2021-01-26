using Prism.Mvvm;
using SQLI.SRS2.Modules.Menu.Controls;
using System.Collections.ObjectModel;

namespace SQLI.SRS2.Modules.Menu.ViewModels
{
    public class InspectorViewModel : BindableBase
    {
        public ObservableCollection<InspectorItem> Items { get; set; } = new ObservableCollection<InspectorItem>();

        public InspectorViewModel()
        {
            GetInspectorItems();
        }

        private void GetInspectorItems()
        {
            Items.Add(CreateInspectorItem("../Assets/Close.svg", "Close"));
            Items.Add(CreateInspectorItem("../Assets/New Window.svg", "Open in new window"));
            Items.Add(CreateInspectorItem("../Assets/New file.svg", "New file"));
            Items.Add(CreateInspectorItem("../Assets/Block OFF.svg", "Block edition"));
            Items.Add(CreateInspectorItem("../Assets/Import.svg", "Import"));
            Items.Add(CreateInspectorItem("../Assets/New Window.svg", "Open in new window"));
            Items.Add(CreateInspectorItem("../Assets/New file.svg", "New file"));
            Items.Add(CreateInspectorItem("../Assets/Block OFF.svg", "Block edition"));
            Items.Add(CreateInspectorItem("../Assets/Import.svg", "Import"));
        }

        private InspectorItem CreateInspectorItem(string iconUri, string description) => new InspectorItem
        {
            IconUri = iconUri,
            Description = description
        };
    }
}
