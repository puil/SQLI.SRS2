using SQLI.SRS2.Business.Core;
using System.Collections.Generic;
using System.Linq;

namespace SQLI.SRS2.Business.Menu
{
    public class MenuItem : BusinessBase
    {
        private string name;
        private string description;
        private MenuItem parent;
        private string viewName;
        private bool isSelected;
        private bool isExpanded;
        private object icon;
        private string iconUri;
        private List<ChildMenuItem> children;

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public MenuItem Parent { get => parent; set => SetProperty(ref parent, value); }
        public string ViewName { get => viewName; set => SetProperty(ref viewName, value); }
        public bool IsSelected { get => isSelected; set => SetProperty(ref isSelected, value); }
        public bool IsExpanded { get => isExpanded; set => SetProperty(ref isExpanded, value); }
        public object Icon { get => icon; set => SetProperty(ref icon, value); }
        public string IconUri { get => iconUri; set => SetProperty(ref iconUri, value); }
        public List<ChildMenuItem> Children { get => children; set => SetProperty(ref children, value); }

        public bool HasParent => Parent != null;
        public bool HasChildren => Children != null && Children.Count > 0;

        public bool IsFirstLevelExpanded => HasParent ? Parent.IsFirstLevelExpanded : IsExpanded;

        public bool IsFirstLevel => !this.HasParent;

        public int Level => 1 + (Parent?.Level ?? 0);

        public MenuItem()
        {
            this.Children = new List<ChildMenuItem>();
        }
    }
}
