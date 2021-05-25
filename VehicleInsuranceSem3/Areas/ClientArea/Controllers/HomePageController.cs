using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.DAO;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.DAL.Model;

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


        [HttpPost]
        public ActionResult AddContact()
        {
            string name = Request.Params["Name"];
            string email = Request.Params["Email"];
            string website = Request.Params["Website"];
            string message = Request.Params["Message"];

            if(name != null && email != null && message != null)
            {
                ContactViewModel newContact = new ContactViewModel()
                {
                    Email = email,
                    Message = message,
                    Name = name,
                    Website = website
                };

                ContactDAORequest dao = new ContactDAORequest();
                dao.Add(newContact);
            }
            return RedirectToAction("Contact");
        }
    }
}