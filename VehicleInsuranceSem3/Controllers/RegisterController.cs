using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.ViewModel;

namespace VehicleInsuranceSem3.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            return View(new CustomerinfoViewModel());
        }

        public ActionResult ForgetPassword()
        {
            return View(new CustomerinfoViewModel());
        }
    }
}