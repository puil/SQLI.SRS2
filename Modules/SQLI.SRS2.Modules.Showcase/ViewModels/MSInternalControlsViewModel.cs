using Prism.Regions;
using SQLI.SRS2.Core.Mvvm;

namespace SQLI.SRS2.Modules.Showcase.ViewModels
{
    public class MSInternalControlsViewModel : RegionViewModelBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public MSInternalControlsViewModel(IRegionManager regionManager) : base(regionManager)
        {
            Title = "MS Internal Controls";
            Message = "MSInternalControlsViewv";
        }
    }
}
