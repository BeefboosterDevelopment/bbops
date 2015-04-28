using System.Collections.Generic;
using Beefbooster.Operations.PredictabullServices.Models;

namespace Beefbooster.Operations.PredictabullServices.PredictabullRepositories
{
    public interface ISaleBullRrepository
    {
        IEnumerable<SaleBull> Get(string strain, short saleYear, AvailabilityScope scope, SaleStatusScope saleStatus);
    }
}