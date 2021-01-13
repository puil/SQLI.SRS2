using SQLI.SRS2.Business.Core;

namespace SQLI.SRS2.Business.Disclosure
{
    public class DisclosureMaterialHeader : BusinessBase
    {
        private int id;
        private string name;
        private HistoryStatusEnum historyStatus;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public HistoryStatusEnum HistoryStatus { get => historyStatus; set => SetProperty(ref historyStatus, value); }
    }
}
