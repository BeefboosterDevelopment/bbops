using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Beefbooster.Operations.PredictabullServices.Models;

namespace Beefbooster.Operations.PredictabullServices
{
    public class TraitMatcher
    {
        private readonly List<BullTrait> _bullTraits = new List<BullTrait>();

        public BullTrait FeetLegs(TraitVM trait, int front, int hind)
        {
            return new BullTrait
                {
                    Trait = trait,
                    MatchType = MatchTypeEnum.StringCompare,
                    BullValue =
                        front.ToString(CultureInfo.InvariantCulture) + "/" + hind.ToString(CultureInfo.InvariantCulture),
                    Qualifies = (
                                    ((front >= int.Parse(trait.RangeMinValue)) &&
                                     (front <= int.Parse(trait.RangeMaxValue)))
                                    &&
                                    ((hind >= int.Parse(trait.RangeMinValue)) &&
                                     (hind <= int.Parse(trait.RangeMaxValue)))
                                )
                };
        }

        public BullTrait StringMatches(TraitVM trait, string bullVal)
        {
            return new BullTrait
                {
                    Trait = trait,
                    MatchType = MatchTypeEnum.StringCompare,
                    BullValue = bullVal.ToString(CultureInfo.InvariantCulture),
                    Qualifies = trait.ExactValue.Equals(bullVal, StringComparison.InvariantCultureIgnoreCase)
                };
        }

        public BullTrait InDecimalRange(TraitVM trait, decimal bullVal)
        {
            return new BullTrait
                {
                    Trait = trait,
                    MatchType = MatchTypeEnum.InRange,
                    BullValue = bullVal.ToString(CultureInfo.InvariantCulture),
                    Qualifies =
                        (bullVal >= decimal.Parse(trait.RangeMinValue) && bullVal <= decimal.Parse(trait.RangeMaxValue))
                };
        }

        /*
         * ====================================================================================
         * if  Jennifer decides to choose percentiles ranging from 10-90 then switch to this one
         * ====================================================================================
         * 
                public string InPercentile0To100(TraitVM trait, decimal bullVal)
                {
                    // which percentile? - match column name on trait
                    string colName = (Enum.GetName(typeof (BullSaleViewNameEnum), trait.BullSaleView));

                    StrainPercentiles strainPercentile =
                        _percentiles.FirstOrDefault(x => x.ColName.Equals(colName, StringComparison.CurrentCultureIgnoreCase));
                    if (strainPercentile == null) return null;

                    int desiredPercentileVal = int.Parse(trait.ExactValue);

                    decimal pctVal = strainPercentile.Percentiles[desiredPercentileVal];
                    if ((desiredPercentileVal > 50) && (bullVal > pctVal))
                        return bullVal.ToString(CultureInfo.InvariantCulture);

                    if ((desiredPercentileVal <= 50) && (bullVal < pctVal))
                        return bullVal.ToString(CultureInfo.InvariantCulture);

                    return null;
                }
         */


        private StrainPercentiles LookupStrainPercentile(IEnumerable<StrainPercentiles> percentiles,
                                                         BullSaleViewNameEnum bullSaleViewNameEnum)
        {
            //var strainPercentile = _percentiles.FirstOrDefault(x => x.ColEnum == bullSaleViewNameEnum);
            string colName = Enum.GetName(typeof (BullSaleViewNameEnum), bullSaleViewNameEnum);
            StrainPercentiles strainPercentile = percentiles.FirstOrDefault(x => x.ColName == colName);
            if (strainPercentile == null)
                throw new Exception(string.Format("No strain percentiles found for column {0}", colName));
            return strainPercentile;
        }

        //============================================================================================
        //NOTE:  as of today (Apr 2013) they can only select desired traits for percentiles as
        //  10, 25, or 40   
        // This translates to 90, 75 or 60 
        //============================================================================================
        public BullTrait WithInPercentile(TraitVM trait, StrainPercentiles strainPercentile, decimal bullVal)
        {
            var bt = new BullTrait
                {
                    Trait = trait,
                    BullValue = bullVal.ToString(CultureInfo.InvariantCulture),
                };
            try
            {
                //int desiredPercentile = int.Parse(trait.ExactValue);
                
                // the desired percentile is stored in the "ExactValue" column
                bool upper = trait.ExactValue.Substring(0, 1) == "T";
                int desiredPercentile = int.Parse(trait.ExactValue.Substring(1));
                switch (desiredPercentile)
                {
                    case 10:
                        if (upper)
                        {
                            bt.MatchType = MatchTypeEnum.WithinPercentileTop10;
                            bt.PercentileRangeValue = strainPercentile.Percentiles[90];
                            bt.Qualifies = bullVal >= bt.PercentileRangeValue;
                        }
                        else
                        {
                            bt.MatchType = MatchTypeEnum.WithinPercentileBottom10;
                            bt.PercentileRangeValue = strainPercentile.Percentiles[10];
                            bt.Qualifies = bullVal <= bt.PercentileRangeValue;
                        }
                        break;
                    case 25:
                        if (upper)
                        {
                            bt.MatchType = MatchTypeEnum.WithinPercentileTop25;
                            bt.PercentileRangeValue = strainPercentile.Percentiles[75];
                            bt.Qualifies = bullVal >= bt.PercentileRangeValue;
                        }
                        else
                        {
                            bt.MatchType = MatchTypeEnum.WithinPercentileBottom25;
                            bt.PercentileRangeValue = strainPercentile.Percentiles[25];
                            bt.Qualifies = bullVal <= bt.PercentileRangeValue;                           
                        }
                        break;
                    case 40:
                        if (upper)
                        {
                            bt.MatchType = MatchTypeEnum.WithinPercentileTop40;
                            bt.PercentileRangeValue = strainPercentile.Percentiles[60];
                            bt.Qualifies = bullVal >= bt.PercentileRangeValue;
                        }
                        else
                        {
                            bt.MatchType = MatchTypeEnum.WithinPercentileBottom40;
                            bt.PercentileRangeValue = strainPercentile.Percentiles[40];
                            bt.Qualifies = bullVal <= bt.PercentileRangeValue;                           
                        }
                        break;
                    default:
                        throw new Exception("Desired trait percentile is not one of the calcuated percentiles.");
                }
            }
            catch (KeyNotFoundException)
            {
           
                bt.PercentileRangeValue = 0;
                bt.Qualifies = true;
            }
            return bt;
        }

        public IEnumerable<BullTrait> QualifyTraitSet(SaleBull bull, IEnumerable<TraitVM> traitSet,
                                                      IEnumerable<StrainPercentiles> percentiles)
        {
            _bullTraits.Clear();
            IList<StrainPercentiles> listOfPercentiles = (percentiles == null)
                                                             ? new List<StrainPercentiles>()
                                                             : percentiles.ToList();

            foreach (TraitVM trait in traitSet)
            {
                StrainPercentiles percentile = (trait.Percentile)
                                                   ? LookupStrainPercentile(listOfPercentiles, trait.BullSaleView)
                                                   : null;
                BullTrait bullTrait;
                switch (trait.BullSaleView)
                {
                    case BullSaleViewNameEnum.TagColour:
                        bullTrait = StringMatches(trait, bull.TagColour);                       
                        break;

                    case BullSaleViewNameEnum.HideColour_Code:
                        bullTrait = StringMatches(trait, bull.HideColour_Code);                       
                        break;

                        // Percentiles
                    case BullSaleViewNameEnum.SEL_IDX:
                        bullTrait = WithInPercentile(trait, percentile, bull.SEL_IDX);
                        break;

                    case BullSaleViewNameEnum.BW_EBV:
                        bullTrait = WithInPercentile(trait, percentile, bull.BW_EBV);
                        break;

                    case BullSaleViewNameEnum.WWD_EBV:
                        bullTrait = WithInPercentile(trait, percentile, bull.WWD_EBV);
                        break;

                    case BullSaleViewNameEnum.WWM_EBV:
                        bullTrait = WithInPercentile(trait, percentile, bull.WWM_EBV);
                        break;

                    case BullSaleViewNameEnum.YWT_EBV:
                        bullTrait = WithInPercentile(trait, percentile, bull.YWT_EBV);
                        break;

                    case BullSaleViewNameEnum.H18M_EBV:
                        bullTrait = WithInPercentile(trait, percentile, bull.H18M_EBV);
                        break;

                    case BullSaleViewNameEnum.SC_EBV:
                        bullTrait = WithInPercentile(trait, percentile, bull.SC_EBV);
                        break;

                    case BullSaleViewNameEnum.BF_EBV:
                        bullTrait = WithInPercentile(trait, percentile, bull.BF_EBV);
                        break;

                    case BullSaleViewNameEnum.MW_EBV:
                        bullTrait = WithInPercentile(trait, percentile, bull.MW_EBV);
                        break;

                        // Note: not calculating percentiles for RFI...
                    case BullSaleViewNameEnum.RFI_EBV:
                        bullTrait = WithInPercentile(trait, percentile, bull.RFI_EBV);
                        break;

                        // Adjusted
                    case BullSaleViewNameEnum.BW_ADJ:
                        bullTrait = InDecimalRange(trait, bull.BW_ADJ);
                        break;

                    case BullSaleViewNameEnum.WW_ADJ:
                        bullTrait = InDecimalRange(trait, bull.WW_ADJ);
                        break;


                    case BullSaleViewNameEnum.ADG_BW_ADJ:
                        bullTrait = InDecimalRange(trait, bull.ADG_BW_ADJ);
                        break;

                    case BullSaleViewNameEnum.YW_ADJ:
                        bullTrait = InDecimalRange(trait, bull.YW_ADJ);
                        break;

                    case BullSaleViewNameEnum.H18MW_ADJ:
                        bullTrait = InDecimalRange(trait, bull.H18MW_ADJ);
                        break;

                    case BullSaleViewNameEnum.BACKFAT_ADJ:
                        bullTrait = InDecimalRange(trait, bull.BACKFAT_ADJ);
                        break;

                    case BullSaleViewNameEnum.SCROTCIRC_ADJ:
                        bullTrait = InDecimalRange(trait, bull.SCROTCIRC_ADJ);
                        break;

                    case BullSaleViewNameEnum.Stn_ADG:
                        bullTrait = InDecimalRange(trait, bull.Stn_ADG);
                        break;

                        // Performance
                    case BullSaleViewNameEnum.Morph:
                        bullTrait = InDecimalRange(trait, bull.Morph);
                        break;

                    case BullSaleViewNameEnum.Motil:
                        bullTrait = InDecimalRange(trait, bull.Motil);
                        break;

                    case BullSaleViewNameEnum.Conc:
                        bullTrait = InDecimalRange(trait, bull.Conc);
                        break;

                    case BullSaleViewNameEnum.Disp:
                        bullTrait = InDecimalRange(trait, bull.Disp);
                        break;

                    case BullSaleViewNameEnum.FFHH:
                        bullTrait = FeetLegs(trait, bull.FF, bull.HF);
/*
                        string ff = InIntegerRange(trait, bull.FF);
                        string hf = InIntegerRange(trait, bull.HF);
                        if (!string.IsNullOrEmpty(ff) && !string.IsNullOrEmpty(hf))
                            bullValue = bull.FF.ToString(CultureInfo.InvariantCulture) + "/" +
                                        bull.HF.ToString(CultureInfo.InvariantCulture);
*/
                        break;

                    case BullSaleViewNameEnum.FLHL:
                        bullTrait = FeetLegs(trait, bull.FL, bull.HL);
/*
                        string fl = InIntegerRange(trait, bull.FL);
                        string hl = InIntegerRange(trait, bull.HL);
                        if (!string.IsNullOrEmpty(fl) && !string.IsNullOrEmpty(hl))
                            bullValue = bull.FL.ToString(CultureInfo.InvariantCulture) + "/" +
                                        bull.HL.ToString(CultureInfo.InvariantCulture);
*/
                        break;


                        // Dam
                    case BullSaleViewNameEnum.AgeOfDam:
                        bullTrait = InDecimalRange(trait, bull.AgeOfDam);
                        break;

                    case BullSaleViewNameEnum.Teat:
                        bullTrait = InDecimalRange(trait, bull.Teat);
                        break;

                    case BullSaleViewNameEnum.Udder:
                        bullTrait = InDecimalRange(trait, bull.Udder);
                        break;

                    case BullSaleViewNameEnum.Dam_Wt:
                        bullTrait = InDecimalRange(trait, bull.Dam_Wt);
                        break;

                    default:

                        throw new ArgumentException(
                            string.Format("Trait with BullSaleView = {0} was not found in bull {1}",
                                          trait.BullSaleView, bull.Calf_Id));
                }
                _bullTraits.Add(bullTrait);
            }
            return _bullTraits;
        }
    }
}