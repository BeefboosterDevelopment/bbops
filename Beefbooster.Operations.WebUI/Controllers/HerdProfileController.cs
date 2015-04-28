using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Beefbooster.Operations.ReportingServices;
using Beefbooster.Operations.WebUI.Models;
using Beefbooster.Operations.WebUI.Models.HerdProfile;

namespace Beefbooster.Operations.WebUI.Controllers
{
    public class HerdProfileController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            //TODO: populate View Model from the database
            var viewModel = new HerdProfileVM
            {
                Years = new List<int> {2011, 2012, 2013, 2014, 2015, 2016},
                SelectedYear = DateTime.Now.Year,
                Herds = new List<HerdVM>
                {
                    new HerdVM
                    {
                        Code = "AB",
                        Description = "some description",
                        SN = 5,
                        Strain = "M1"
                    },
                    new HerdVM
                    {
                        Code = "DC",
                        Description = "GILCHRIST RANCHES LTD.",
                        SN = 22,
                        Strain = "TX"
                    }
                }
            };

            return View(viewModel);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(HerdProfileVM vm)
        {
            if (ModelState.IsValid)
            {
                // exec proc: rpt.CreateHerdProfile( @herdSN INT, @birthYear INT )
                var rptService = new HerdProfileReport();
                int profileId = rptService.Generate(vm.SelectedYear, vm.SelectedHerdSN);
                if (profileId > 0)
                    return RedirectToAction("HerdProfile", "Reports", new {profileId});
            }
            return View("Error");
        }
    }
}