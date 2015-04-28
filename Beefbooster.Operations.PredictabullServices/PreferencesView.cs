using System.Collections.Generic;

namespace Beefbooster.Operations.PredictabullServices
{
    public class PreferencesView
    {
        public string Strain { get; set; }
        public string Username { get; set; }
        public int SaleYear { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<TraitVM> Preferences { get; set; }
    }

    public class TraitVM
    {
        public int Id { get; set; }
        public string TraitName { get; set; }
        public int Sequence { get; set; }
        public BullSaleViewNameEnum BullSaleView { get; set; }
        public string PriceCategory { get; set; }
        public bool HasRange { get; set; }
        public bool Percentile { get; set; }
        public string RangeMinValue { get; set; }
        public string RangeMaxValue { get; set; }
        public string ExactValue { get; set; }
        public string UOM { get; set; }
        public string Comment { get; set; }
    }
}