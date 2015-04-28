using Beefbooster.Operations.PredictabullServices.PredictabullRepositories;

namespace Beefbooster.Operations.PredictabullServices
{
    public interface ISelectionServices
    {
        SearchResults BullSearch(PreferencesView preferences, AvailabilityScope availabilityScope, SaleStatusScope saleStatus, int basketSize);
    }
}