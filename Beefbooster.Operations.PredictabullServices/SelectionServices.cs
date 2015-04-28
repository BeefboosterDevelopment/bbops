using System;
using System.Collections.Generic;
using System.Linq;
using Beefbooster.Operations.PredictabullServices.Models;
using Beefbooster.Operations.PredictabullServices.PredictabullRepositories;

namespace Beefbooster.Operations.PredictabullServices
{
    public class SelectionServices : ISelectionServices
    {
        private readonly IPercentileRepository _percentileRepository;
        private readonly ISaleBullRrepository _salebullRepository;

        public SelectionServices(ISaleBullRrepository salebullRepository, IPercentileRepository percentileRepository)
        {
            _salebullRepository = salebullRepository;
            _percentileRepository = percentileRepository;
        }

        public SearchResults BullSearch(PreferencesView preferences, AvailabilityScope availabilityScope, SaleStatusScope saleStatus, int basketSize)
        {
            var results = new SearchResults
                {
                    StrainPercentiles = GetPercentileViews(preferences)
                };

            var searcher = new Search(
                _salebullRepository.Get(preferences.Strain, Convert.ToInt16(preferences.SaleYear), availabilityScope, saleStatus),
                preferences.Preferences, results.StrainPercentiles
                );

            var qualifiedBulls = searcher.QualifyBulls();
            
            // set number total and sequenced matches
            foreach (var qualifiedBull in qualifiedBulls)
            {
                qualifiedBull.TotalMatches= qualifiedBull.BullTraits.Count(x => x.Qualifies);
                qualifiedBull.SequencedMatches = CountSequencedMatches(qualifiedBull.BullTraits);
            }


            //var temp = qualifiedBulls.Where(x => x.SequencedMatches > 0).ToList();

            results.QualifiedBulls = qualifiedBulls
                .OrderByDescending(x => x.SequencedMatches)
                .ThenByDescending(x => x.Bull.SEL_IDX)
                .Take(basketSize)
                .ToList();

            return results;
        }
        
        private int CountSequencedMatches(IEnumerable<BullTrait> bullTraits)
        {
            var sequencedMatches = 0;
            foreach (var trait in bullTraits.OrderBy(x => x.Trait.Sequence))
            {
                if (trait.Qualifies)
                    sequencedMatches++;
                else
                    break;
            }
            return sequencedMatches;
        }

        private StrainPercentiles GetOnePercentileView(string strain, int saleYear, BullSaleViewNameEnum e)
        {
            return _percentileRepository.Get(strain, saleYear, (Enum.GetName(typeof (BullSaleViewNameEnum), e)));
        }

        private IEnumerable<StrainPercentiles> GetPercentileViews(PreferencesView preferences)
        {
            return (from traitVM in preferences.Preferences
                    where traitVM.Percentile
                    select GetOnePercentileView(preferences.Strain, preferences.SaleYear, traitVM.BullSaleView))
                    .ToList();
        }
    }
}