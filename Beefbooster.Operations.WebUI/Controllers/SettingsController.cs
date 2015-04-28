using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Beefbooster.Operations.PredictabullServices.PredictabullRepositories;

namespace Beefbooster.Operations.WebUI.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IPercentileRepository _percentileRepository;

        public SettingsController(IPercentileRepository percentileRepository)
        {
            _percentileRepository = percentileRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPut]
        public JsonResult RecalculatePercentiles(int saleYear)
        {
            var strains = new List<string> {"M1", "M2", "M3", "M4", "TX"};
            strains.ForEach(x => _percentileRepository.Calculate(x, Convert.ToInt16(saleYear)));
            return Json(true);
        }
    }
}