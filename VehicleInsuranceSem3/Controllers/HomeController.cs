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
            CustomerinfoViewModel newCustomer = new CustomerinfoViewModel() {
                name = "toanngo",
                active = true,
                address = "dap da",
                dob = DateTime.Parse("1997/09/27"),
                email = "ngotoanlibra@gmail.com",
                password = "toanngo",
                phone = "0984685751",
                username = "toanngo",
                user_type_id = 2
            };

            CustomerinfoDAORequest context = new CustomerinfoDAORequest();
            context.Add(newCustomer);

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