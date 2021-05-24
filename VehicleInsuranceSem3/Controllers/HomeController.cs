using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.DAO;
using VehicleInsuranceSem3.BLL.Feature;
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

        public ActionResult InsurancePlan()
        {
            PolicyDAORequest request = new PolicyDAORequest();
            List<PolicyViewModel> model = request.GetAll();
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //===============================================================================================

        public ActionResult CreateCustomerPolicy()
        {
            if (Session["id"] != null)
            {
                //Get Customer
                int id = (int)Session["id"];
                CustomerinfoDAORequest request = new CustomerinfoDAORequest();
                CustomerinfoViewModel customer = request.GetCustomerById(id);
                ViewData["Customers"] = customer;

                //Get Policy
                int policyID = (int)TempData["PolicyID"];
                PolicyDAORequest request1 = new PolicyDAORequest();
                PolicyViewModel policy = request1.GetPolicyById(policyID);
                ViewData["Policies"] = policy;
                ViewData["Brands"] = GetBrandList();

                //Get Total Payment
                PolicyTypeDAORequest request2 = new PolicyTypeDAORequest();
                PolicytypeViewModel type = request2.GetTypeById(policy.policytypeid);
                ViewBag.Price = (decimal)(type.price * policy.policyduration);

                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewCustomerPolicy()
        {
            int cusId = (int)Session["id"];
            int policyID = int.Parse(Request.Params["idPolicyHidden"]);
            int modelId = int.Parse(Request.Params["cbVehicleModel"]);
            ModelDAORequest dao = new ModelDAORequest();
            ModelViewModel model = dao.GetModelById(modelId);
            int vehicleCondition = int.Parse(Request.Params["condition"]);

            Vehicle_Info newVehicle = new Vehicle_Info()
            {
                brand_id = int.Parse(Request.Form["cbVehicleBrand"]),
                model_id = int.Parse(Request.Form["cbVehicleModel"]),
                address = Request.Form["address"].ToString(),
                owner_name = Request.Form["ownerName"].ToString(),
                version = Request.Params["version"],
                frame_number = Request.Params["frameNumber"],
                engine_number = Request.Params["engineNumber"],
                vehicle_number = Request.Params["vehicleNumber"],
                vehicle_condition = vehicleCondition,
                rate_by_condition = (vehicleCondition * model.rate) / 100
            };
            using (var ctx = new InsuranceDbContext())
            {
                ctx.Vehicle_Info.Add(newVehicle);
                ctx.SaveChanges();
            }

            CustomerpolicyViewModel newCustomerPolicy = new CustomerpolicyViewModel()
            {
                customerid = cusId,
                policyid = policyID,
                vehicleid = newVehicle.id,
                policystartdate = DateTime.Parse(Request.Params["startDate"]),
                policyenddate = DateTime.Parse(Request.Params["endDate"]),
                createdate = DateTime.Parse(Request.Params["createDate"]),
                customeraddprove = "sdfhshf",
                TotalPayment = decimal.Parse(Request.Params["totalPayment"]),
                active = true
            };

            TempData["checkout"] = newCustomerPolicy;
            //CustomerpolicyDAORequest request = new CustomerpolicyDAORequest();
            //request.Add(newCustomerPolicy);
            return RedirectToAction("CheckOutPage", "Home");
        }

        public List<BrandViewModel> GetBrandList()
        {
            BrandDAORequest request = new BrandDAORequest();
            return request.GetAll();
        }

        [HttpPost]
        public ActionResult GetModelList(int brandId)
        {
            ModelDAORequest request = new ModelDAORequest();
            return PartialView("ModelList_PartialPage", request.GetByBrandId(brandId));
        }

        public ActionResult CheckOutPage()
        {
            CustomerpolicyViewModel newCustomerPolicy = (CustomerpolicyViewModel)TempData["checkout"];
            return View(newCustomerPolicy);
        }

        //===============================================================================================
        [HttpPost]
        public ActionResult GetPolicyId(int policyId)
        {
            TempData["PolicyID"] = policyId;
            return Json("success");
        }


        //===============================================================================================
        public ActionResult CustomerHistory()
        {
            CustomerpolicyDAORequest request = new CustomerpolicyDAORequest();
            int id = (int)Session["id"];
            List<CustomerHistoryModelView> list = request.GetCustomerPolicyHistory(id);
            return View(list);
        }


        //===============================================================================================
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

        public ActionResult ChangePassword()
        {
            if (TempData["Alert"] != null)
            {
                ViewBag.Alert = TempData["Alert"];
                TempData.Remove("Alert");
            }
            return View();
        }

        public ActionResult UpdatePassword()
        {
            int id = (int)Session["id"];
            string oldPassword = Request.Params["oldPassword"];
            string newPassword = Request.Params["newPassword"];
            string reNewPassword = Request.Params["reNewPassword"];
            CustomerinfoDAORequest request = new CustomerinfoDAORequest();
            var customer = request.GetCustomerById(id);
            if (CheckNullField(oldPassword, newPassword, reNewPassword))
            {
                if (CheckOldPassword(customer.password, oldPassword))
                {
                    if (CheckMatchNewPassword(newPassword, reNewPassword))
                    {
                        customer.password = newPassword;
                        request.Update(customer);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Alert"] = "New password not match";
                        return RedirectToAction("ChangePassword");
                    }
                }
                else
                {
                    TempData["Alert"] = "Old Password is not corrent";
                    return RedirectToAction("ChangePassword");
                }
            }
            else
            {
                TempData["Alert"] = "Please Enter All Of Field";
                return RedirectToAction("ChangePassword");
            }


        }


        //===============================================================================================
        public bool CheckOldPassword(string dbPassword, string enterPassword)
        {
            if (dbPassword.Equals(enterPassword))
            {
                return true;
            }
            return false;
        }

        public bool CheckMatchNewPassword(string newPassword, string reNewPassword)
        {
            if (newPassword.Equals(reNewPassword))
            {
                return true;
            }
            return false;
        }

        public bool CheckNullField(string oldPassword, string newPassword, string rePassword)
        {
            if (oldPassword == null || newPassword == null || rePassword == null)
            {
                return true;
            }
            return false;
        }

    }
}