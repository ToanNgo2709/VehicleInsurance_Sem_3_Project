using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.DAO;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.DAL.Model;
using VehicleInsuranceSem3.Utilities.Crypto;

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

        //==========================================================================

        public ActionResult ForgetPassword()
        {
            if (TempData["AlertForgetPw"] != null)
            {
                ViewBag.ForgetPwError = TempData["AlertForgetPw"];
                TempData.Remove("AlertForgetPw");
            }
            return View(new CustomerinfoViewModel());
        }

        [HttpPost]
        public ActionResult ForgetPasswordDB(CustomerinfoViewModel model)
        {
            CustomerinfoDAORequest request = new CustomerinfoDAORequest();           
            string username = model.username;
            string email = model.email;
            ForgetPasswordEmailViewModel emailModel = new ForgetPasswordEmailViewModel();

            CustomerinfoViewModel customer = request.GetByUsernameAndEmail(username, email);
            
            if (CheckForgotPwNull(username, email))
            {
                if (customer != null)
                {
                    string realPassword = PasswordSecurity.Decrypt(customer.password);

                    emailModel.From = "toanngongo97@gmail.com";
                    emailModel.To = email;
                    emailModel.Subject = "Kraken Force Inc - Your Password";
                    emailModel.Body = "Your Password is: " + realPassword;

                    MailMessage mail = new MailMessage();
                    mail.To.Add(emailModel.To);
                    mail.From = new MailAddress(emailModel.From);
                    mail.Subject = emailModel.Subject;
                    mail.Body = emailModel.Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("toanngongo97@gmail.com", "Toan.123"); // Enter seders User name and password  
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                    TempData["AlertForgetPw"] = "Send Email. Check Email to Get Password";
                    return RedirectToAction("ForgetPassword");

                }
                else
                {
                    TempData["AlertForgetPw"] = "Your Username and Email is Wrong. Please try again";
                    return RedirectToAction("ForgetPassword");
                }
            }
            else
            {
                TempData["AlertForgetPw"] = "Please Input full field";
                return RedirectToAction("ForgetPassword");
            }

        }

        public bool CheckForgotPwNull(string username, string email)
        {
            if (username != null || email != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //===============================================================================

        
    }
}