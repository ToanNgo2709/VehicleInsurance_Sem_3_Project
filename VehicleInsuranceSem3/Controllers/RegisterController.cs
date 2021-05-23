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
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            if (TempData["Alert"] != null)
            {
                ViewBag.Alert = TempData["Alert"];
                TempData.Remove("Alert");
            }
            return View(new CustomerinfoViewModel());
        }

        public ActionResult RegisterDb(CustomerinfoViewModel uv)
        {
            
            using (var ctx = new InsuranceDbContext())
            {
                if(Session["id"] == null)
                {
                    if (CheckNullField(uv))
                    {
                        if (CheckExistUsername(uv.username))
                        {
                            if (CheckPasswordMatch(uv.password, Request.Params["pwRePassword"]))
                            {
                                if (uv.active == true)
                                {
                                    CustomerinfoDAORequest request = new CustomerinfoDAORequest();
                                    //Usertype: Customer
                                    uv.user_type_id = 2;
                                    request.Add(uv);

                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    TempData["Alert"] = "Please Check to Accept Policy";
                                    return RedirectToAction("Register", "Register");
                                }
                               
                            }
                            else
                            {
                                TempData["Alert"] = "The Password Not Match";
                                return RedirectToAction ("Register", "Register");
                            }
                        }
                        else
                        {
                            TempData["Alert"] = "This Username Already Exist ";
                            return RedirectToAction ("Register", "Register");
                        }
                    }
                    else
                    {
                        TempData["Alert"] = "Please Enter Full Of Field";
                        return RedirectToAction("Register", "Register");
                    }

                }
                else
                {
                    TempData["Alert"] = "Please Use Another Acount";
                    return RedirectToAction("Register", "Register");
                }
            }
            
        }

        public bool CheckExistUsername(string username)
        {
            using (var ctx = new InsuranceDbContext())
            {
                var item = ctx.Customer_Info.Where(c => c.username.Equals(username)).FirstOrDefault();
                if(item == null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckPasswordMatch(string password, string rePassword)
        {
            if (password.Equals(rePassword))
            {
                return true;
            }
            return false;
        }

        public bool CheckNullField(CustomerinfoViewModel c)
        {
            if(c.name != null && c.dob != null && c.address != null && c.phone != null && c.email != null && c.address != null && c.username != null && c.password != null)
            {
                return true;
            }
            return false;
        }

        public ActionResult ForgetPassword()
        {
            return View(new CustomerinfoViewModel());
        }
    }
}