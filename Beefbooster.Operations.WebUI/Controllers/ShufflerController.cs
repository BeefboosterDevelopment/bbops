using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Beefbooster.Bull.Entities.Models;
using Beefbooster.Operations.Service;
using Beefbooster.Operations.WebUI.Models.Shuffler;

namespace Beefbooster.Operations.WebUI.Controllers
{
    public class ShufflerController : Controller
    {
        private readonly IShufflerService _shufflerService;
        private readonly ISpringSaleService _springSaleService;

        public ShufflerController(ISpringSaleService springSaleService, IShufflerService shufflerService)
        {
            _springSaleService = springSaleService;
            _shufflerService = shufflerService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var vm = new InputVM {SaleYear = DateTime.Now.Year};
            List<SpringSaleDate> sales = _springSaleService.CustomerSaleDatesForYear(vm.SaleYear).ToList();
            vm.SaleDays = sales.Select(
                s =>
                    new SaleDayVM
                    {
                        SaleDay = s.SaleDate.ToString("ddd MMM dd"),
                        SpringSaleDateSN = s.SpringSaleDateSN,
                        StrainCode = s.StrainCode
                    }).ToList();
            return View(vm);
        }

        [HttpPost]
        public JsonResult Shuffle(int? selectedSaleDaySN)
        {
            if (!selectedSaleDaySN.HasValue)
                throw new ArgumentNullException("selectedSaleDaySN");
            var drawVM = new DrawVM {SelectionOrder = new List<SelectionVM>()};
            int order = 0;
            string[] shuffledNames = _shufflerService.BingoDraw(selectedSaleDaySN.Value);
            foreach (string shuffledName in shuffledNames)
            {
                drawVM.SelectionOrder.Add(new SelectionVM
                {
                    CustomerName = shuffledName,
                    SelectionNumber = ++order
                });
            }
            return Json(drawVM);
        }
    }
}