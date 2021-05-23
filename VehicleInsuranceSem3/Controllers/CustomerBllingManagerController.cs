using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.DAO;
using VehicleInsuranceSem3.BLL.ViewModel;
using PagedList;
using PagedList.Mvc;

namespace VehicleInsuranceSem3.Controllers
{
    public class CustomerBllingManagerController : Controller
    {
        public CustomerBillingInfoDAORequest csb = new CustomerBillingInfoDAORequest();
      public  CustomerpolicyDAORequest csp = new CustomerpolicyDAORequest();
        // GET: CustomerBllingManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CusTomerBillManagerl(int page = 1 , int pageSize = 10)
        {
            List<CustomerbillinginfoViewModel> ListCusbill = new List<CustomerbillinginfoViewModel>();
            PagedList<CustomerbillinginfoViewModel> PageListCusBill;
            if (Session["CusbillSearch"] !=null)
            {
                ListCusbill = (List<CustomerbillinginfoViewModel>)Session["CusbillSearch"];
                PageListCusBill = new PagedList<CustomerbillinginfoViewModel>(ListCusbill , page , pageSize);

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
            if (keyword !=null )
            {
                Session["StringSearch"] = keyword;
            }
            List<CustomerbillinginfoViewModel> c = csb.Search(1, 10, (String)Session["StringSearch"]);

            Session["CusbillSearch"] = c;
            
            List<CustomerpolicyViewModel> z = csp.GetAll();
            Session["cspAllView"] = z;
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
            return RedirectToAction("CustomerPolicyManager", "CustomerPolicyManager");
        }


        [HttpPost]
        public ActionResult AddCustomerBill(CustomerbillinginfoViewModel l)
        {
            csb.Add(l);
            List<CustomerbillinginfoViewModel> x = csb.GetAll();
            Session["csbAllView"] = x;
            return RedirectToAction("CustomerbillViewAll");
        }
         
        public ActionResult DeleteCustomerBill(int id)
        {
            csb.Delete(id);
            List<CustomerbillinginfoViewModel> x = csb.GetAll();
            Session["csbAllView"] = x;
            return RedirectToAction("CustomerbillViewAll");
        }

        

    }
}