using System.Collections.Generic;
using System.Linq;
using Beefbooster.Operations.PredictabullServices.Models;

namespace Beefbooster.Operations.PredictabullServices
{
    public enum BullSaleViewNameEnum
    {
        Stn_ADG,
        HideColour_Code,
        TagColour,
        BW_ADJ,
        WW_ADJ,
        YW_ADJ,
        H18MW_ADJ,
        ADG_BW_ADJ,
        BACKFAT_ADJ,
        SCROTCIRC_ADJ,

        BW_EBV,
        WWD_EBV,
        YWT_EBV,
        BF_EBV,
        SC_EBV,
        WWM_EBV,
        MW_EBV,
        RFI_EBV,
        H18M_EBV,

        AgeOfDam,
        Dam_Wt,
        Teat,
        Udder,
        FFHH,
        FLHL,
        Morph,
        Motil,
        Conc,
        Disp,
        SEL_IDX
    }
    
    public enum MatchTypeEnum
    {
        StringCompare = 1,  // duplicated in the javascript (MatchTypeString)
        InRange,
        WithinPercentileTop10,
        WithinPercentileTop25,
        WithinPercentileTop40,
        WithinPercentileBottom40,
        WithinPercentileBottom25,
        WithinPercentileBottom10
    }

    public class SearchResults
    {
        public IEnumerable<StrainPercentiles> StrainPercentiles { get; set; }
        public List<QualifiedBull> QualifiedBulls { get; set; }
    }

    public class QualifiedBull
    {
        public SaleBull Bull { get; set; }
        public IEnumerable<BullTrait> BullTraits { get; set; }
        public int SequencedMatches { get; set; }
        public int TotalMatches { get; set; }
    }

    public class BullTrait
    {     
        public TraitVM Trait { get; set; }
        public string BullValue { get; set; }
        public bool Qualifies { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public decimal PercentileRangeValue { get; set; }       
    }

    public class Search
    {
        //private readonly int _basketSize;
        private readonly IEnumerable<SaleBull> _bulls;
        private readonly IEnumerable<TraitVM> _desiredTraits;
        private readonly TraitMatcher _traitMatcher;
        private readonly IEnumerable<StrainPercentiles> _percentiles;
        public Search(IEnumerable<SaleBull> bulls, IEnumerable<TraitVM> desiredTraits, IEnumerable<StrainPercentiles> percentiles/*, int basketSize*/)
        {
            _bulls = bulls;
            _desiredTraits = desiredTraits;
            //_basketSize = basketSize;
            _percentiles = percentiles;
            _traitMatcher = new TraitMatcher();
        }

        public List<QualifiedBull> QualifyBulls()
        {
            var bullSet = new List<SaleBull>(_bulls);
            var basket = new List<QualifiedBull>();
            if ((_bulls == null) || (!_bulls.Any())) return basket;
            var traitSet = new List<TraitVM>(_desiredTraits.OrderBy(x => x.Sequence));

            // attempt to qualify all bulls
            return bullSet.Select(bull => new QualifiedBull
                {
                    Bull = bull, BullTraits = _traitMatcher.QualifyTraitSet(bull, traitSet, _percentiles).ToList()
                }).ToList();


/*


            var topN = traitSet.Count;

            while ((topN > 0) && (bullSet.Count > 0))
            {
                // add any bulls where the top N traits were matched
                basket.AddRange(from bull in bullSet
                                let bullTraits = _traitMatcher.QualifyTraitSet(bull, traitSet, _percentiles).ToList()
                                where bullTraits.Count(x => x.Qualifies) == topN
                                select new QualifiedBull
                                    {
                                        Bull = bull, BullTraits = bullTraits
                                    });

                // have we found enough matches?
                if (basket.Count >= _basketSize)
                    break;

                // no, so remove least desirable trait, and try it again
                //traitSet.RemoveAt(traitSet.Count() - 1);
                topN--;

                // need to remove the matches from the set of bulls we are searching or
                // we will end up adding duplicates
                foreach (var b in basket.Where(b => bullSet.Contains(b.Bull)))
                    bullSet.Remove(b.Bull);
            }
            return basket;
 */
        }
    }
}