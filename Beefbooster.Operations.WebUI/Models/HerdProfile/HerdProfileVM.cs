using System.Collections.Generic;

namespace Beefbooster.Operations.WebUI.Models.HerdProfile
{
    public class HerdProfileVM
    {
        public List<int> Years { get; set; }
        public int SelectedYear { get; set; }
        public List<HerdVM> Herds { get; set; }
        //public HerdVM SelectedHerd { get; set; }
        public int SelectedHerdSN { get; set; }
    }
}