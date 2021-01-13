using System.Collections.Generic;

namespace SQLI.SRS2.Modules.Menu.Models
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MenuItem Parent { get; set; }
        public List<MenuItem> Children { get; set; }
        public string ViewName { get; set; }
        public bool IsSelected { get; set; }

        public bool HasParent => Parent != null;
        public bool HasChildren => Children != null && Children.Count > 0;

        public MenuItem()
        {
            this.Children = new List<MenuItem>();
        }
    }
}
