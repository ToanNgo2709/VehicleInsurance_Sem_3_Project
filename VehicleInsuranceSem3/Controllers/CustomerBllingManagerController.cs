using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.DAO;
using VehicleInsuranceSem3.BLL.ViewModel;
using PagedList;
using PagedList.Mvc;
using VehicleInsuranceSem3.DAL.Model;

namespace VehicleInsuranceSem3.Controllers
{
    public class CustomerBllingManagerController : Controller
    {
        public CustomerBillingInfoDAORequest csb = new CustomerBillingInfoDAORequest();
        public CustomerpolicyDAORequest csp = new CustomerpolicyDAORequest();
        // GET: CustomerBllingManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CusTomerBillManagerl(int page = 1, int pageSize = 10)
        {
            List<CustomerbillinginfoViewModel> x = csb.GetAll();
            Session["csbAllView"] = x;
            List<CustomerpolicyViewModel> z = csp.GetAll();
            Session["cspAllView"] = z;

            List<CustomerbillinginfoViewModel> ListCusbill = new List<CustomerbillinginfoViewModel>();
            PagedList<CustomerbillinginfoViewModel> PageListCusBill;
            if (Session["CusbillSearch"] != null)
            {
                ListCusbill = (List<CustomerbillinginfoViewModel>)Session["CusbillSearch"];
                PageListCusBill = new PagedList<CustomerbillinginfoViewModel>(ListCusbill, page, pageSize);

            }
            else
            {
                ListCusbill = csb.GetAll();

                PageListCusBill = new PagedList<CustomerbillinginfoViewModel>(ListCusbill, page, pageSize);
            }

            return View(PageListCusBill);

        }

        [HttpGet]
        public ActionResult CustomerbillViewAll()
        {

            List<CustomerbillinginfoViewModel> x = csb.GetAll();
            Session["csbAllView"] = x;
            List<CustomerpolicyViewModel> z = csp.GetAll();
            Session["cspAllView"] = z;

            Session["StringSearch"] = null;
            Session["CusbillSearch"] = null;
            return RedirectToAction("CusTomerBillManagerl");

        }
        public ActionResult customerSearch()
        {
            var keyword = Request.Form["tbSearch"];
            if (keyword != null)
            {
                Session["StringSearch"] = keyword;
            }
            List<CustomerbillinginfoViewModel> c = csb.Search(1, 10, (String)Session["StringSearch"]);

            Session["CusbillSearch"] = c;

            List<CustomerpolicyViewModel> z = csp.GetAll();
            Session["cspAllView"] = z;
            return RedirectToAction("CusTomerBillManagerl");
        }

        [HttpPost]
        public ActionResult FilterBillByDate()
        {
            DateTime startDate = DateTime.Parse(Request.Params["startDate"]);
            DateTime endDate = DateTime.Parse(Request.Params["endDate"]);

            List<CustomerbillinginfoViewModel> list = csb.FilterByDate(startDate, endDate, 1, 10);
            Session["CusbillSearch"] = list;

            return RedirectToAction("CusTomerBillManagerl");
        }

        public ActionResult EditCustomerBill(int id)
        {
            CustomerbillinginfoViewModel c = csb.GetEdit(id);
            ViewData["csbAllView"] = c;
            List<CustomerpolicyViewModel> z = csp.GetAll();
            Session["cspAllView"] = z;
            return View();
        }
        [HttpPost]
        public ActionResult NewCustomerBill(CustomerbillinginfoViewModel xx)
        {
            csb.Update(xx);
            List<CustomerbillinginfoViewModel> s = csb.GetAll();
            Session["csbAllView"] = s;
            if (Session["CusbillSearch"] == null)
            {
                return RedirectToAction("CustomerbillViewAll");

            }

            return RedirectToAction("customerSearch");
        }



        public ActionResult CreateCustomerBill()
        {
            List<CustomerbillinginfoViewModel> x = csb.GetAll();
            Session["csbAllView"] = x;
            List<CustomerpolicyViewModel> z = csp.GetAll();
            Session["cspAllView"] = z;
            return View();
        }

        public ActionResult CreateBill(int id)
        {
            int customerPolicyId = id;
            CustomerpolicyDAORequest request = new CustomerpolicyDAORequest();
            CustomerpolicyViewModel model = request.GetCustomerPolicyById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateBillForCustomer()
        {
            int customerPolicyId = int.Parse(Request.Params["customerPolicyId"]);
            string customerProve = Request.Params["customeraddprove"];
            string billNumber = Request.Params["bill_number"];
            DateTime createDate = DateTime.Parse(Request.Params["createDate"]);
            decimal amount = decimal.Parse(Request.Params["amount"]);
            bool active = Request.Params["active"] == "TRUE" ? true : false;

            CustomerbillinginfoViewModel model = new CustomerbillinginfoViewModel()
            {
                customerpolicyid = customerPolicyId,
                active = active,
                amount = amount,
                bill_number = billNumber,
                createdate = createDate,
                customeraddprove = customerProve
            };

            CustomerBillingInfoDAORequest request = new CustomerBillingInfoDAORequest();
            request.Add(model);
            return RedirectToAction("CustomerPolicyBillView", model);
        }

        public ActionResult BillDetail(int id)
        {
            CustomerBillingInfoDAORequest request = new CustomerBillingInfoDAORequest();
            CustomerbillinginfoViewModel model = request.GetBillById(id);
            return RedirectToAction("CustomerPolicyBillView", model);
        }

        public ActionResult CustomerPolicyBillView(CustomerbillinginfoViewModel model)
        {
            var context = new InsuranceDbContext();
            var item = context.Customer_Billing_Info
                .Where(c => c.customer_policy_id == model.customerpolicyid)
                .Select(c => new CustomerPolicyBillViewModel
                {
                    Amount = c.amount,
                    BillNumber = c.bill_number,
                    CreateDate = c.create_date,
                    CustomerName = c.Customer_Policy.Customer_Info.name,
                    CustomerPolicyId = (int)c.customer_policy_id,
                    EndDate = c.Customer_Policy.policy_end_date,
                    PolicyName = c.Customer_Policy.Policy.policy_number,
                    StartDate = c.Customer_Policy.policy_start_date,
                    VehicleName = c.Customer_Policy.Vehicle_Info.Brand.name + " " + c.Customer_Policy.Vehicle_Info.Model.name
                }).FirstOrDefault();
            return View(item);
        }




        [HttpPost]
        public ActionResult AddCustomerBill(CustomerbillinginfoViewModel l)
        {
            List<CustomerbillinginfoViewModel> model = csb.CheckCustomerPolicyExist(l.customerpolicyid);
            if (model.Count > 0)
            {
                TempData["message"] = "Customer Policy already had a bill, please check Customer Billing Info";
                return RedirectToAction("CusTomerBillManagerl", new { page = 1, pageSize = 10 });
            }
            else
            {
                csb.Add(l);
                List<CustomerbillinginfoViewModel> x = csb.GetAll();
                Session["csbAllView"] = x;
                return RedirectToAction("CustomerbillViewAll");
            }          
        }

        [HttpPost]
        public ActionResult DeleteCustomerBill(int id)
        {
            csb.Delete(id);
            List<CustomerbillinginfoViewModel> x = csb.GetAll();
            Session["csbAllView"] = x;
            return RedirectToAction("CustomerbillViewAll");
        }

        [HttpPost]
        public ActionResult SearchCustomerPolicy(int id)
        {
            CustomerpolicyViewModel model = csp.GetCustomerPolicyById(id);
            return Json(model.TotalPayment, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckBillExist(CustomerbillinginfoViewModel cb)
        {
            CustomerBillingInfoDAORequest dao = new CustomerBillingInfoDAORequest();
            List<CustomerbillinginfoViewModel> model = dao.CheckCustomerPolicyExist(cb.id);
            if (model.Count > 0)
            {
                TempData["message"] = "Customer Policy already had a bill, please check Customer Billing Info";
                return RedirectToAction("CusTomerBillManagerl", new { page = 1, pageSize = 10 });
            }
            else
            {
                return RedirectToAction("AddCustomerBill", new { l = cb });
            }

        }


    }
}