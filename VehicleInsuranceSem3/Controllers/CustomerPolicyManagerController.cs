using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.BLL.DAO;
using PagedList;
using PagedList.Mvc;
using System.Web.UI;

namespace VehicleInsuranceSem3.Controllers
{
    public class CustomerPolicyManagerController : Controller
    {
        public CustomerpolicyDAORequest cs = new CustomerpolicyDAORequest();
        public PolicyDAORequest pl = new PolicyDAORequest();
        public VehicleinfoDAORequest vh = new VehicleinfoDAORequest();
        public CustomerinfoDAORequest csi = new CustomerinfoDAORequest();

        // GET: CustomerPolicyManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CustomerPolicyManager(int? page)
        {
            List<CustomerpolicyViewModel> ListcusPL = new List<CustomerpolicyViewModel>();
            PagedList<CustomerpolicyViewModel> PageListCusPL;

            List<PolicyViewModel> s = pl.GetAll();
            Session["PolicyAll"] = s;
            List<VehicleinfoViewModel> a = vh.GetAll();
            Session["VehicleAll"] = a;
            List<CustomerinfoViewModel> w = csi.GetAll();
            Session["customerInAll"] = w;

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (Session["CusPLSearch"] != null)
            {
                ListcusPL = (List<CustomerpolicyViewModel>)Session["CusPLSearch"];
                PageListCusPL = new PagedList<CustomerpolicyViewModel>(ListcusPL, pageNumber, pageSize);

            }
            else
            {
                ListcusPL = cs.GetAll();
                PageListCusPL = new PagedList<CustomerpolicyViewModel>(ListcusPL, pageNumber, pageSize);
            }
            TempData["currentPage"] = page;
            return View(PageListCusPL);
        }
        [HttpGet]
        public ActionResult CustomerPolicyViewAll()
        {
            List<CustomerpolicyViewModel> d = cs.GetAll();
            Session["CustomAll"] = d;
            List<PolicyViewModel> s = pl.GetAll();
            Session["PolicyAll"] = s;
            List<VehicleinfoViewModel> a = vh.GetAll();
            Session["VehicleAll"] = a;
            List<CustomerinfoViewModel> w = csi.GetAll();
            Session["customerInAll"] = w;

            Session["StringSearch"] = null;
            Session["CusPLSearch"] = null;
            return RedirectToAction("CustomerPolicyManager");
        }
        public ActionResult customerPolicySearch()
        {
            var keyword = Request.Form["tbSearch"];
            if (keyword != null)
            {
                Session["StringSearch"] = keyword;
            }
            List<CustomerpolicyViewModel> g = cs.Search(1, 10, (String)Session["StringSearch"]);
            Session["CusPLSearch"] = g;
            List<PolicyViewModel> s = pl.GetAll();
            Session["PolicyAll"] = s;
            List<VehicleinfoViewModel> a = vh.GetAll();
            Session["VehicleAll"] = a;
            List<CustomerinfoViewModel> w = csi.GetAll();
            Session["customerInAll"] = w;
            return RedirectToAction("CustomerPolicyManager");
        }

        [HttpPost]
        public ActionResult CustomerPolicySearchByDate()
        {
            DateTime startDate = DateTime.Parse(Request.Params["startDate"]);
            DateTime endDate = DateTime.Parse(Request.Params["endDate"]);

            List<CustomerpolicyViewModel> list = cs.FilterCustomerPolicyByCreateDate(startDate, endDate, 1, 10);
            Session["CusPLSearch"] = list;

            return RedirectToAction("CustomerPolicyManager");
        }
        public ActionResult EditCustomerPolicy(int id)
        {
            CustomerpolicyViewModel k = cs.GetEdit(id);
            ViewData["CustomAll"] = k;
            List<PolicyViewModel> s = pl.GetAll();
            Session["PolicyAll"] = s;
            List<VehicleinfoViewModel> a = vh.GetAll();
            Session["VehicleAll"] = a;
            List<CustomerinfoViewModel> w = csi.GetAll();
            Session["customerInAll"] = w;
            return View();

        }
        [HttpPost]
        public ActionResult NewCustomerPolicy(CustomerpolicyViewModel ff)
        {
            cs.Update(ff);
            List<CustomerpolicyViewModel> d = cs.GetAll();
            Session["CustomAll"] = d;
            if (Session["CusPLSearch"] == null)
            {
                return RedirectToAction("CustomerPolicyViewAll");

            }
            return RedirectToAction("customerPolicySearch");
        }
        public ActionResult CreateCustomerPolicy()
        {
            List<CustomerpolicyViewModel> d = cs.GetAll();
            Session["CustomAll"] = d;
            List<PolicyViewModel> s = pl.GetAll();
            Session["PolicyAll"] = s;
            List<VehicleinfoViewModel> a = vh.GetAll();
            Session["VehicleAll"] = a;
            List<CustomerinfoViewModel> w = csi.GetAll();
            Session["customerInAll"] = w;
            return View();

        }
        [HttpPost]
        public ActionResult AddCustomerPolicy(CustomerpolicyViewModel jg)
        {
            cs.Add(jg);
            List<CustomerpolicyViewModel> d = cs.GetAll();
            Session["CustomAll"] = d;
            return RedirectToAction("CustomerPolicyViewAll");
        }

        [HttpPost]
        public ActionResult DeleteCustomerPolicy(int id)
        {
            cs.Delete(id);
            List<CustomerpolicyViewModel> d = cs.GetAll();
            Session["CustomAll"] = d;
            return RedirectToAction("CustomerPolicyViewAll");
        }


        public ActionResult CreateCustomerPolicyTest()
        {
            return View();
        }

        public ActionResult CheckBillExist(int customerPolicyId)
        {
            CustomerBillingInfoDAORequest dao = new CustomerBillingInfoDAORequest();
            List<CustomerbillinginfoViewModel> model = dao.CheckCustomerPolicyExist(customerPolicyId);
            if (model.Count > 0)
            {
                TempData["message"] = "Customer Policy already had a bill, please check Customer Billing Info";
                return RedirectToAction("CustomerPolicyManager", new { page = 1});
            }
            else
            {
                return RedirectToAction("CreateBill", "CustomerBllingManager", new { id = customerPolicyId } );
            }

        }

    }
}