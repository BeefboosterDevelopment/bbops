using System.Web.Mvc;
using FastReport.Web;

namespace Beefbooster.Operations.WebUI.Controllers
{
    public class ReportsController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult HerdProfile(int? profileId)
        {
            var webReport = new WebReport
            {
                Width = 800,
                Height = 800,
            };
            webReport.Report.Load(Server.MapPath("~/Reports/FR/HerdProf.frx"));
            webReport.Report.SetParameterValue("profileId", profileId);
            webReport.Prepare();

            ViewBag.Title = "Herd Profile Report";
            ViewBag.WebReport = webReport;

            return View("Report");
        }
    }
}