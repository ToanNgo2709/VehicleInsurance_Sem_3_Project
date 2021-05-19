using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.DAO;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.DAL.Model;


namespace VehicleInsuranceSem3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
                return View();
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

        [HttpPost]
        public ActionResult CreateNewContact()
        {
            ContactViewModel newContact = new ContactViewModel()
            {
                Email = Request.Params["Email"],
                Message = Request.Params["Message"],
                Name = Request.Params["Name"],
                Website = Request.Params["Website"]
            };

            ContactDAORequest request = new ContactDAORequest();
            request.Add(newContact);

            return RedirectToAction("Contact");
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}