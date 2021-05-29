using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.DAO;
using VehicleInsuranceSem3.BLL.ViewModel;

namespace VehicleInsuranceSem3.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult ContactRequest()
        {
            ContactDAORequest request = new ContactDAORequest();
            List<ContactViewModel> model = request.GetAll();
            return View(model);
        }

        public ActionResult SendEmail(int id)
        {
            ContactDAORequest request = new ContactDAORequest();
            ContactViewModel model = request.GetContactById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult SendEmailToCustomer()
        {
            string from = "toanngongo97@gmail.com";
            string cusEmail = Request.Params["cusEmail"];
            string subject = Request.Params["subject"];
            string body = Request.Params["message"];

            if (CheckNull(cusEmail, subject, body))
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(cusEmail);
                mail.From = new MailAddress(from);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("toanngongo97@gmail.com", "Toan.123"); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return RedirectToAction("ContactRequest");
            }
            else
            {
                return Json("Send email Fail");
            }
            
        }

        public bool CheckNull(string cusEmail, string subject, string body)
        {
            if (cusEmail == null || subject == null || body == null)
            {
                return false;
            }
            return true;
        }
    }
}