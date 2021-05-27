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
using VehicleInsuranceSem3.Models;
using VehicleInsuranceSem3.Utilities.Crypto;

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
            CustomerinfoDAORequest customerRequest = new CustomerinfoDAORequest();
            Customer_Info customerInfo = customerRequest.searchCustomerById(cusId);

            int policyID = int.Parse(Request.Params["idPolicyHidden"]);
            PolicyDAORequest policyRequest = new PolicyDAORequest();
            Policy policyInfo = policyRequest.searchPolicyById(policyID);

            int modelId = int.Parse(Request.Params["cbVehicleModel"]);
            ModelDAORequest dao = new ModelDAORequest();
            Model model1 = dao.searchModelByModel(modelId);
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
                rate_by_condition = (vehicleCondition * model.rate) / 100,
                Model = model1

            };

            Customer_Policy newCustomerPolicy = new Customer_Policy()
            {
                customer_id = cusId,
                policy_id = policyID,
                Vehicle_Info = newVehicle,
                vehicle_id = newVehicle.id,
                policy_start_date = DateTime.Parse(Request.Params["startDate"]),
                policy_end_date = DateTime.Parse(Request.Params["endDate"]),
                create_date = DateTime.Parse(Request.Params["createDate"]),
                customer_add_prove = "Proved",
                total_payment = decimal.Parse(Request.Params["totalPayment"]),
                active = true,
                Policy = policyInfo,
                Customer_Info = customerInfo
            };

            CheckoutInfo checkout = new CheckoutInfo() {
                CustomerPolicy = newCustomerPolicy,
                Vehicle = newVehicle               
            };

            Session["checkoutInfo"] = checkout;
            return RedirectToAction("CheckOutPage");
        }

        public ActionResult CheckOutPage()
        {
            CheckoutInfo checkout = (CheckoutInfo)Session["checkoutInfo"];
            return View(checkout);
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

        public ActionResult CustomerHistoryDetail(int id)
        {
            var context = new InsuranceDbContext();
            var model = context.Customer_Policy
                .Where(c => c.id == id)
                .Select(c => new CopyCustomerPolicyViewModel
                {
                    Address = c.Vehicle_Info.address,
                    BrandName = c.Vehicle_Info.Brand.name,
                    Condition = c.Vehicle_Info.vehicle_condition,
                    CreateDate = c.create_date,
                    CustomerName = c.Customer_Info.name,
                    EndDate = c.policy_end_date,
                    EngineNumber = c.Vehicle_Info.engine_number,
                    FrameNumber = c.Vehicle_Info.frame_number,
                    ModelName = c.Vehicle_Info.Model.name,
                    OwnerName = c.Vehicle_Info.owner_name,
                    PolicyName = c.Policy.policy_number,
                    StartDate = c.policy_start_date,
                    TotayPayment = c.total_payment,
                    VehicleNumber = c.Vehicle_Info.vehicle_number,
                    Version = c.Vehicle_Info.version
                }).FirstOrDefault();
            return View(model);
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
            if (!CheckNullField(oldPassword, newPassword, reNewPassword))
            {
                if (CheckOldPassword(customer.password, oldPassword))
                {
                    if (CheckMatchNewPassword(newPassword, reNewPassword))
                    {
                        customer.password = PasswordSecurity.Encrypt(newPassword);
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

            if (PasswordSecurity.Decrypt(dbPassword).Equals(enterPassword))
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

        public ActionResult ConvertPassword()
        {
            ViewBag.result = TempData["encrypt"];
            return View();
        }

        [HttpPost]
        public ActionResult Convert()
        {
            string password = Request.Params["password"];
            string encryptPw = PasswordSecurity.Encrypt(password);
            TempData["encrypt"] = encryptPw;
            return RedirectToAction("ConvertPassword", TempData["encrypt"]);
        }
    }
}