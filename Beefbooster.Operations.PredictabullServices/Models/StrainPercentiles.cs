using System;
using System.Collections.Generic;

namespace Beefbooster.Operations.PredictabullServices.Models
{
    public class StrainPercentiles
    {
        public string Strain { get; set; }
        public int SaleYear { get; set; }
        public DateTime CalculatedOn { get; set; }
        public string ColName { get; set; }
        public IDictionary<int, decimal> Percentiles { get; set; }
    }

    public class ColumnPercentilesVM
    {
        public int Id { get; set; }
        public string Strain { get; set; }
        public int SaleYear { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class PercentileValuesVM
    {
        public int Id { get; set; }
        public string Column { get; set; }
        public int Percentile { get; set; }
        public decimal PercentileValue { get; set; }
    }

}