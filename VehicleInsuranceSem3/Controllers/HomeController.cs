using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.DAO;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.DAL.Model;
using VehicleInsuranceSem3.Utilities.Crypto;

namespace VehicleInsuranceSem3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var list = TempData["customerList"];
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        public ActionResult ViewAll()
        {
            CustomerinfoDAORequest request = new CustomerinfoDAORequest();
            var list = request.GetAll();
            Session["customerList"] = list;
            return RedirectToAction("Index");
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult TestAdminLayout()
        {
            return View();
        }
    }
}