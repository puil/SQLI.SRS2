using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SQLI.SRS2.Modules.Menu.Controls;
using SQLI.SRS2.Modules.Menu.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SQLI.SRS2.Modules.Menu.ViewModels
{
    public class InspectorViewModel : BindableBase
    {
        public ObservableCollection<InspectorItem> Items { get; set; } = new ObservableCollection<InspectorItem>();

        public InspectorViewModel(IEventAggregator eventAggregator)
        {
            //GetInspectorItems();

            eventAggregator.GetEvent<InspectorMenuItemsEvent>().Subscribe(OnInspectorItemsReceived);
        }

        private void OnInspectorItemsReceived(IEnumerable<InspectorItem> inspectorItems)
        {
            //Items.AddRange(inspectorItems);
            Items = new ObservableCollection<InspectorItem>(inspectorItems);
            this.RaisePropertyChanged(nameof(Items));
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

        private DelegateCommand<object> itemCommand;
        public DelegateCommand<object> ItemCommand => itemCommand ??= new DelegateCommand<object>(ExecuteItemCommand);

        void ExecuteItemCommand(object parameter)
        {

        }
    }
}
