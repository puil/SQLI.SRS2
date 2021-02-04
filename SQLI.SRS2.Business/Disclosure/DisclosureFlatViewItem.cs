using SQLI.SRS2.Business.Core;

namespace SQLI.SRS2.Business.Disclosure
{
    public class DisclosureFlatViewItem : BusinessBase
    {
        private int internalId;
        private string name;
        private double percentage;
        private string functions;
        private string cas;
        private string fema;
        private string coe;
        private string enbr;

        public int InternalId { get => internalId; set => SetProperty(ref internalId, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public double Percentage { get => percentage; set => SetProperty(ref percentage, value); }
        public string Functions { get => functions; set => SetProperty(ref functions, value); }
        public string Cas { get => cas; set => SetProperty(ref cas, value); }
        public string Fema { get => fema; set => SetProperty(ref fema, value); }
        public string Coe { get => coe; set => SetProperty(ref coe, value); }
        public string Enbr { get => enbr; set => SetProperty(ref enbr, value); }
    }
}
