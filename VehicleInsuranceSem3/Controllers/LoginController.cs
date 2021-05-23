using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.BLL.DAO;
using System.Text;
using VehicleInsuranceSem3.DAL.Model;

namespace VehicleInsuranceSem3.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            if (TempData["Alert"] != null)
            {
                ViewBag.Alert = TempData["Alert"];
                TempData.Remove("Alert");
            }
            return View(new CustomerinfoViewModel());
        }


        public ActionResult LoginDB(CustomerinfoViewModel uv)
        {
            using (var ctx = new InsuranceDbContext())
            {
                string pw = uv.password;
                if (Session["id"] == null)
                {
                    var checkus = ctx.Customer_Info
                        .Where(a => a.username.Equals(uv.username))
                        .FirstOrDefault();
                    if (checkus != null)
                    {
                        var obj = ctx.Customer_Info.Where(a => a.username.Equals(uv.username) && a.password.Equals(pw)).FirstOrDefault();
                        if (obj != null)
                        {
                            var ut = ctx.User_Type.Where(t => t.id == obj.user_type_id).Select(t => t.name).FirstOrDefault().ToString();
                            var sta = obj.active == true ? "Unlock" : "Lock";
                            if (sta == "Unlock")
                            {
                                if (ut == "Admin")
                                {
                                    Session["id"] = obj.id;
                                    Session["username"] = obj.username;
                                    Session["User_Type"] = ctx.User_Type.Where(utt => utt.id == obj.id).Select(utt => utt.name).FirstOrDefault();
                                    //TempData["Alert"] = "Welcome admin!";
                                    return RedirectToAction("DashIndex", "DashBoard");
                                }
                                else
                                {
                                    Session["id"] = obj.id;
                                    Session["username"] = obj.username;
                                    Session["User_Types"] = ctx.User_Type.Where(utt => utt.id == obj.id).Select(utt => utt.name).FirstOrDefault();
                                    //TempData["Alert"] = "Have a nice day!";
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                            else
                            {
                                TempData["Alert"] = "Your account has been locked!";
                                return RedirectToAction("Login", "Login");
                            }
                        }
                        else
                        {
                            TempData["Alert"] = "Your password is wrong!";
                            return RedirectToAction("Login", "Login");
                        }
                    }
                    else
                    {
                        TempData["Alert"] = "Your account not exist!";                    
                        return RedirectToAction("Login", "Login");
                    }

                }
                else
                {
                    TempData["Alert"] = "Please log out to be able to log in with another account!";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        public JsonResult CheckValidUser(CustomerinfoViewModel model)
        {
            using (var ctx = new InsuranceDbContext())
            {
                string result = "Fail";
                var DataItem = ctx.Customer_Info.Where(x => x.username == model.username && x.password == model.password).SingleOrDefault();
                if (DataItem != null)
                {
                    Session["id"] = DataItem.id.ToString();
                    Session["username"] = DataItem.username.ToString();
                    result = "Success";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AfterLogin()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult LogoutDB()
        {
            if (Session["id"] != null)
            {
                Session["id"] = null;
                Session["Username"] = null;
                Session["User_Type"] = null;
                TempData["Alert"] = "Logout successful!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Alert"] = "You not log in with any account!";
                return RedirectToAction("Index", "Home");
            }
        }


        //===============================================================================
        public static string Encrypt(string text)
        {
            string key = "A!9HHhi%XjjYY4YP2@Nob009X";
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string Decrypt(string cipher)
        {
            string key = "A!9HHhi%XjjYY4YP2@Nob009X";
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}