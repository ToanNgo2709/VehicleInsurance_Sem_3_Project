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
    public class EstimateManagerController : Controller
    {
        // GET: EstimateManager
        public EstimateDAORequest es = new EstimateDAORequest();
        public VehicleinfoDAORequest vh = new VehicleinfoDAORequest();
        public CustomerinfoDAORequest cs = new CustomerinfoDAORequest();
        public PolicyDAORequest pl = new PolicyDAORequest();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EstimateManagerNe(int page = 1 , int pageSize =10)
        {
            List<EstimateViewModel> Listesti = new List<EstimateViewModel>();
            PagedList<EstimateViewModel> PageListEsti;
            if (Session["EstiSearch"] !=null)
            {
                Listesti = (List<EstimateViewModel>)Session["EstiSearch"];
                PageListEsti = new PagedList<EstimateViewModel>(Listesti, page, pageSize);

            }
            else
            {

                Listesti = es.GetAll();
                PageListEsti = new PagedList<EstimateViewModel>(Listesti, page, pageSize);
            }
            
            return View(PageListEsti);
        }

        [HttpGet]
        public ActionResult EstimateViewAll()
        {
            List<EstimateViewModel> z = es.GetAll();
            Session["EstimatAll"] = z;
            List<VehicleinfoViewModel> x = vh.GetAll();
            Session["VehicleAll"] = x;
            List<CustomerinfoViewModel> w = cs.GetAll();
            Session["CustomerAll"] = w;
            List<PolicyViewModel> a = pl.GetAll();
            Session["PolicyAll"] = a;

            Session["StringSearch"] = null;
            Session["EstiSearch"] = null;

            return RedirectToAction("EstimateManagerNe");

        }
        public ActionResult EstimateSearch()
        {
            var keyword = Request.Form["tbSearch"];
            if (keyword !=null)
            {
                Session["StringSearch"] = keyword;
            }
            
            List<EstimateViewModel> d = es.Search(1, 10, (String)Session["StringSearch"]);
            Session["EstiSearch"] = d;
            List<VehicleinfoViewModel> x = vh.GetAll();
            Session["VehicleAll"] = x;
            List<CustomerinfoViewModel> w = cs.GetAll();
            Session["CustomerAll"] = w;
            List<PolicyViewModel> a = pl.GetAll();
            Session["PolicyAll"] = a;
            return RedirectToAction("EstimateManagerNe");
        }

        public ActionResult EditEstimate(int id)
        {
            EstimateViewModel z = es.GetEdit(id);
            ViewData["EstimatAll"] = z;
            List<VehicleinfoViewModel> x = vh.GetAll();
            Session["VehicleAll"] = x;
            List<CustomerinfoViewModel> w = cs.GetAll();
            Session["CustomerAll"] = w;
            List<PolicyViewModel> a = pl.GetAll();
            Session["PolicyAll"] = a;
            return View();
        }

        [HttpPost]
        public ActionResult NewEstimate(EstimateViewModel zz )
        {
            es.Update(zz);
            List<EstimateViewModel> z = es.GetAll();
            Session["EstimatAll"] = z;
            if (Session["EstiSearch"] == null)
            {
                return RedirectToAction("EstimateViewAll");

            }
            return RedirectToAction("EstimateSearch");
        }

        public ActionResult CreateEstimate()
        {
            List<EstimateViewModel> z = es.GetAll();
            Session["EstimatAll"] = z;
            List<VehicleinfoViewModel> x = vh.GetAll();
            Session["VehicleAll"] = x;
            List<CustomerinfoViewModel> w = cs.GetAll();
            Session["CustomerAll"] = w;
            List<PolicyViewModel> a = pl.GetAll();
            Session["PolicyAll"] = a;
            return View();

        }
            
        [HttpPost]
        public ActionResult AddEstimate(EstimateViewModel b)
        {
            es.Add(b);
            List<EstimateViewModel> z = es.GetAll();
            Session["EstimatAll"] = z;
            return RedirectToAction("EstimateViewAll");

        }
        public ActionResult DeleteEstimate(int id)
        {
            es.Delete(id);
            List<EstimateViewModel> z = es.GetAll();
            Session["EstimatAll"] = z;
            return RedirectToAction("EstimateViewAll");
        }

    }
}