using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using SQLI.SRS2.Business.Disclosure;
using SQLI.SRS2.Core.Extensions;
using SQLI.SRS2.Core.Inspector;
using SQLI.SRS2.Core.Mvvm;
using SQLI.SRS2.Modules.Disclosure.Resources;
using SQLI.SRS2.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SQLI.SRS2.Modules.Disclosure.ViewModels
{
    public class DisclosureViewModel : RegionViewModelBase
    {
        #region Private Fields

        private readonly IDialogService dialogService;
        private readonly IDisclosureService disclosureService;

        #endregion

        #region Public Properties

        private ObservableCollection<DisclosureMaterialHeader> disclosureMaterials;
        private DisclosureMaterialHeader selectedMaterialHeader;
        private DisclosureMaterial selectedMaterial;

        public ObservableCollection<DisclosureMaterialHeader> DisclosureMaterials { get => disclosureMaterials; set => SetProperty(ref disclosureMaterials, value); }
        public ObservableCollection<InspectorItem> InspectorItems { get; set; } = new ObservableCollection<InspectorItem>();

        public DisclosureMaterialHeader SelectedMaterialHeader
        {
            get => selectedMaterialHeader;
            set
            {
                selectedMaterialHeader = value;
                OnSelectedMaterialHeaderChanged();
            }
        }

        public DisclosureMaterial SelectedMaterial { get => selectedMaterial; set => SetProperty(ref selectedMaterial, value); }

        #endregion

        #region Constructor

        public DisclosureViewModel(IRegionManager regionManager, IDialogService dialogService, IDisclosureService disclosureService) 
            : base(regionManager)
        {
            this.dialogService = dialogService;
            this.disclosureService = disclosureService;

            Title = ResourceStrings.DisclosureView_Title;

            LoadMaterialHeaders();

            this.InspectorItems = new ObservableCollection<InspectorItem>(GetInspectorItems());
        }

        #endregion

        #region Private Methods

        private void LoadMaterialHeaders()
        {
            var bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            DisclosureMaterials = new ObservableCollection<DisclosureMaterialHeader>(disclosureService.GetDisclosureMaterialsHeaders());
        }

        private void OnSelectedMaterialHeaderChanged()
        {
            if (SelectedMaterialHeader != null)
                SelectedMaterial = disclosureService.GetDisclosureMaterial(SelectedMaterialHeader.Id);
        }

        private IEnumerable<InspectorItem> GetInspectorItems() => new List<InspectorItem>
            {
                CreateInspectorItem("close", "../Assets/Close.svg", "Close"),
                CreateInspectorItem("newWindow", "../Assets/New Window.svg", "Open in new window"),
                //new SeparatorInspectorItem(),
                CreateInspectorItem("newFile", "../Assets/New file.svg", "New file"),
                CreateInspectorItem("block", "../Assets/Block OFF.svg", "Block edition"),
                CreateInspectorItem("refresh", "../Assets/Refresh Content.svg", "Refresh content"),
                CreateInspectorItem("save", "../Assets/Save.svg", "Save"),
                CreateInspectorItem("delete", "../Assets/Delete.svg", "Delete"),
                //new SeparatorInspectorItem(),
                CreateInspectorItem("import", "../Assets/Import.svg", "Import"),
                CreateInspectorItem("export", "../Assets/Export.svg", "Export"),
                //new SeparatorInspectorItem(),
                CreateInspectorItem("workflow", "../Assets/Run Workflow.svg", "Run workflow"),
                CreateInspectorItem("process", "../Assets/Process Candidates.svg", "Process candidates"),
            };

        private static InspectorItem CreateInspectorItem(string key, string iconUri, string description) => new ButtonInspectorItem
        {
            Key = key,
            IconUri = iconUri,
            Description = description
        };

        #endregion

        #region Commands

        private DelegateCommand openDocumentationCommand;
        public DelegateCommand OpenDocumentationCommand => openDocumentationCommand ??= new DelegateCommand(ExecuteOpenDocumentationCommand);

        void ExecuteOpenDocumentationCommand()
        {
            dialogService.ShowToast("Open documentation pressed");
        }

        private DelegateCommand<object> inspectorItemCommand;
        public DelegateCommand<object> InspectorItemCommand => inspectorItemCommand ??= new DelegateCommand<object>(ExecuteInspectorItemCommand);

        void ExecuteInspectorItemCommand(object parameter)
        {
            switch ((parameter as InspectorItem)?.Key)
            {
                case "close":
                    this.Close();
                    break;
                case "newWindow":
                    this.OpenInNewWindow();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Actions from inspector buttons

        private void Close()
        {
            dialogService.ShowToast("Close is pressed");

        }

        private void OpenInNewWindow()
        {
            dialogService.ShowToast("OpenInNewWindow is pressed");
        }

        #endregion
    }
}
