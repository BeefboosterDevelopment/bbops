using System.Collections.Generic;

namespace Beefbooster.Operations.WebUI.Models
{
    public class CustomerVM
    {
        public int UserId { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
    }
    
   public class SelectionVM
    {
        public List<string> Strains { get; set; }
        public List<int> SaleYears { get; set; }
        public List<CustomerVM> Customers { get; set; }

        public int SaleYear { get; set; }
        public string Strain { get; set; }
    }



    public class BullVM
    {
        public int CalfSN { get; set; }
        public string Id { get; set; }
        public string Pen { get; set; }
    }

    public class PreferenceVM
    {
        public int PrefId { get; set; }
        public string Strain { get; set; }
        public string Trait { get; set; }
        public string InputType { get; set; }
        public string ExactValue { get; set; }
        public string RangeMinValue { get; set; }
        public string RangeMaxValue { get; set; }
        public string Comment { get; set; }
        public int Sequence { get; set; }
    }

    public class SearchResultVM
    {
        public List<PreferenceVM> Preferences { get; set; }
        public List<BullVM> Bulls { get; set; }
    }

}