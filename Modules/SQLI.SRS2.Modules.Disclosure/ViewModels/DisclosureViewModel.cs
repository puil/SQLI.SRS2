using Prism.Regions;
using SQLI.SRS2.Business.Disclosure;
using SQLI.SRS2.Core.Mvvm;
using SQLI.SRS2.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SQLI.SRS2.Modules.Disclosure.ViewModels
{
    public class DisclosureViewModel : RegionViewModelBase
    {
        #region Private Fields

        private readonly IDisclosureService disclosureService;

        #endregion

        #region Public Properties

        private ObservableCollection<DisclosureMaterialHeader> disclosureMaterials;
        private DisclosureMaterialHeader selectedMaterialHeader;
        private DisclosureMaterial selectedMaterial;

        public ObservableCollection<DisclosureMaterialHeader> DisclosureMaterials { get => disclosureMaterials; set => SetProperty(ref disclosureMaterials, value); }

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

        public DisclosureViewModel(IRegionManager regionManager, IDisclosureService disclosureService) : base(regionManager)
        {
            this.disclosureService = disclosureService;

            Title = "Disclosure";

            LoadMaterialHeaders();
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

        #endregion
    }
}
