using SQLI.SRS2.Business.Core;
using System;

namespace SQLI.SRS2.Business.Disclosure
{
    public class DisclosureMaterial : BusinessBase
    {
        private int id;
        private string name;
        private HistoryStatusEnum historyStatus;
        private DateTime receivedOn;
        private DateTime? validFrom;
        private DateTime? validTo;
        private bool incomplete;
        private bool tpd2Relevant;
        private bool evaluated;
        private string country;

        //public int Id
        //{
        //    get => id;
        //    set
        //    {
        //        if (this.id == value)
        //            return;

        //        id = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //public string Name
        //{
        //    get => name;
        //    set
        //    {
        //        if (this.name == value)
        //            return;

        //        name = value;
        //        NotifyPropertyChanged();
        //    }
        //}


        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public HistoryStatusEnum HistoryStatus { get => historyStatus; set => SetProperty(ref historyStatus, value); }
        public DateTime ReceivedOn { get => receivedOn; set => SetProperty(ref receivedOn, value); }
        public DateTime? ValidFrom { get => validFrom; set => SetProperty(ref validFrom, value); }
        public DateTime? ValidTo { get => validTo; set => SetProperty(ref validTo, value); }
        public bool Incomplete { get => incomplete; set => SetProperty(ref incomplete, value); }
        public bool Tpd2Relevant { get => tpd2Relevant; set => SetProperty(ref tpd2Relevant, value); }
        public bool Evaluated { get => evaluated; set => SetProperty(ref evaluated, value); }
        public string Country { get => country; set => SetProperty(ref country, value); }
    }
}
