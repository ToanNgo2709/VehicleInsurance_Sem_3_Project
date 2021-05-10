using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VehicleInsuranceSem3.Areas.ClientArea.Controllers
{
    public class HomePageController : Controller
    {
        // GET: ClientArea/HomePage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}