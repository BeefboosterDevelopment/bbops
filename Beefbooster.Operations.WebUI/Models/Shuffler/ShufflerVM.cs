using System.Collections.Generic;

namespace Beefbooster.Operations.WebUI.Models.Shuffler
{
    public class SaleDayVM
    {
        public int SpringSaleDateSN { get; set; }
        public string SaleDay { get; set; }
        public string StrainCode { get; set; }
    }

    public class InputVM
    {
        public int SaleYear { get; set; }
        public List<SaleDayVM> SaleDays { get; set; }
    }

    public class SelectionVM
    {
        public int SelectionNumber { get; set; }
        public string CustomerName { get; set; }
    }

    public class DrawVM
    {
        public List<SelectionVM> SelectionOrder { get; set; }
    }
}