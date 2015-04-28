using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Beefbooster.Bull.Entities.Models
{
    public class SpringSale : Entity
    {
        public SpringSale()
        {
            SpringSaleDates = new List<SpringSaleDate>();
            POs = new List<PO>();
        }

        public int SpringSaleSN { get; set; }
        public int CalfBirthYr_Num { get; set; }
        public string Name { get; set; }
        public int NextInvNum { get; set; }
        public bool Locked_Flag { get; set; }
        public decimal? InsuranceRate { get; set; }
        public decimal? InsuranceRate_US { get; set; }

        public virtual ICollection<SpringSaleDate> SpringSaleDates { get; set; }
        public virtual ICollection<PO> POs { get; set; }
    }
}