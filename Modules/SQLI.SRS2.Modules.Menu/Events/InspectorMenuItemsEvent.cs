using Prism.Events;
using SQLI.SRS2.Modules.Menu.Controls;
using System.Collections.Generic;

namespace SQLI.SRS2.Modules.Menu.Events
{
    public class InspectorMenuItemsEvent : PubSubEvent<IEnumerable<InspectorItem>>
    {
    }
}
