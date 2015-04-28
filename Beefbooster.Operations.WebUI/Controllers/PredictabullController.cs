using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Beefbooster.Operations.PredictabullServices;
using Beefbooster.Operations.PredictabullServices.Models;
using Beefbooster.Operations.PredictabullServices.PredictabullRepositories;
using Newtonsoft.Json;

namespace Beefbooster.Operations.WebUI.Controllers
{
    public class PredictabullController : Controller
    {
        private readonly IPredictABullAccountServices _predictabullAccountServices;
        private readonly ISelectionServices _selectionServices;

        public PredictabullController(IPredictABullAccountServices predictabullAccountServices,
            ISelectionServices selectionServices)
        {
            _predictabullAccountServices = predictabullAccountServices;
            _selectionServices = selectionServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Customers
        [HttpPost]
        public JsonResult Customers(int saleYear, string strain)
        {
            AccountsWithPreferencesView vw = _predictabullAccountServices.AccountsWithPreferences(saleYear, strain);
            return Json(JsonConvert.SerializeObject(vw));
        }

        // POST: /Bulls
        [HttpPost]
        //public JsonResult Bulls(int saleYear, string strain, int userId, int basketSize, string scope)
        public JsonResult Bulls(int saleYear, string strain, int userId, int basketSize, string scope, string saleStatus)
        {
            SaleStatusScope saleStatusScope = saleStatus.Equals("all", StringComparison.CurrentCultureIgnoreCase)
                ? SaleStatusScope.All
                : SaleStatusScope.Classed;

            AvailabilityScope availabilityScope = scope.Equals("all", StringComparison.CurrentCultureIgnoreCase)
                ? AvailabilityScope.All
                : AvailabilityScope.AvailableOnly;

            var prefs = _predictabullAccountServices.PreferencesForUser(userId, saleYear, strain);
            if (prefs != null)
            {
                SearchResults searchResults = _selectionServices.BullSearch(prefs,
                    availabilityScope,
                    saleStatusScope,
                    basketSize);
                var vm = new BullSelectionVM
                {
                    DesiredTraits = prefs.Preferences,
                    RequiredPercentiles = ConvertPercentilesToVM(searchResults.StrainPercentiles),
                    QualifyingBulls = searchResults.QualifiedBulls
                };
                return Json(JsonConvert.SerializeObject(vm));
            }
            return null;
        }

        private static IEnumerable<PercentileVM> ConvertPercentilesToVM(IEnumerable<StrainPercentiles> strainPercentiles)
        {
            return strainPercentiles.Select(sp => new PercentileVM
            {
                ColumnName = sp.ColName,
                ColumnNameEnum = (BullSaleViewNameEnum) Enum.Parse(typeof (BullSaleViewNameEnum), sp.ColName),
                CalculatedOn = sp.CalculatedOn,
                Percentiles = MapDictionaryToList(sp.Percentiles)
            }).ToList();
        }

        private static List<PercentileValueVM> MapDictionaryToList(
            IEnumerable<KeyValuePair<int, decimal>> dicPercentiles)
        {
            List<PercentileValueVM> lst = dicPercentiles.Select(dicPercentile => new PercentileValueVM
            {
                Percentile = dicPercentile.Key,
                PercentileValue = dicPercentile.Value
            }).ToList();
            if (lst.Any()) return lst;

            // for any traits that do not have any percentiles calculated
            // e.g. RFI
            return new List<PercentileValueVM>
            {
                new PercentileValueVM {Percentile = 0, PercentileValue = 0},
                new PercentileValueVM {Percentile = 10, PercentileValue = 0},
                new PercentileValueVM {Percentile = 25, PercentileValue = 0},
                new PercentileValueVM {Percentile = 40, PercentileValue = 0},
                new PercentileValueVM {Percentile = 60, PercentileValue = 0},
                new PercentileValueVM {Percentile = 75, PercentileValue = 0},
                new PercentileValueVM {Percentile = 90, PercentileValue = 0},
                new PercentileValueVM {Percentile = 100, PercentileValue = 0},
            };
        }


        internal class BullSelectionVM
        {
            public IEnumerable<TraitVM> DesiredTraits { get; set; }
            public IEnumerable<PercentileVM> RequiredPercentiles { get; set; }
            public IEnumerable<QualifiedBull> QualifyingBulls { get; set; }
        }

        internal class PercentileVM
        {
            public string ColumnName { get; set; }
            public BullSaleViewNameEnum ColumnNameEnum { get; set; }
            public DateTime CalculatedOn { get; set; }
            public List<PercentileValueVM> Percentiles { get; set; }
        }

        internal class PercentileValueVM
        {
            public int Percentile { get; set; }
            public decimal PercentileValue { get; set; }
        }
    }
}