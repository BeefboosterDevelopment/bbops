using System.Collections.Generic;
using Beefbooster.Operations.PredictabullServices.Models;

namespace Beefbooster.Operations.PredictabullServices.PredictabullRepositories
{
    public interface IPercentileRepository
    {
        List<string> PercentileColumnNames { get; }
        List<StrainPercentiles> Calculate(string strain, int saleYear);
        StrainPercentiles Get(string strain, int saleYear, string colName);
        //List<ColumnPercentilesVM> GetForStrain(string strain, int saleYear);
    }
}