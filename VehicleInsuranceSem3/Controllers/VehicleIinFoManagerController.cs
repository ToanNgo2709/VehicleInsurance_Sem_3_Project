using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.BLL.DAO;
using PagedList.Mvc;
using PagedList;


namespace VehicleInsuranceSem3.Controllers
{

    public class VehicleIinFoManagerController : Controller
    {
        public VehicleinfoDAORequest lk = new VehicleinfoDAORequest();
        public BrandDAORequest br = new BrandDAORequest();
        public ModelDAORequest md = new ModelDAORequest();
        // GET: VehicleIinFoManager
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult VehicleInfoManager(int page = 1 , int pageSize =10)
        {
            List<VehicleinfoViewModel> f = lk.GetAll();
            Session["VehicleInfoAll"] = f;
            List<BrandViewModel> s = br.GetAll();
            Session["brandAll"] = s;
            List<ModelViewModel> o = md.GetAll();
            Session["modelAll"] = o;

            List<VehicleinfoViewModel> Listvh = new List<VehicleinfoViewModel>();
            PagedList<VehicleinfoViewModel> PageListVH;
            if (Session["VHSearch"] != null)
            {
                Listvh = (List<VehicleinfoViewModel>)Session["VHSearch"];
                PageListVH = new PagedList<VehicleinfoViewModel>(Listvh, page, pageSize);

            }
            else
            {
                Listvh = lk.GetAll();
                PageListVH = new PagedList<VehicleinfoViewModel>(Listvh, page, pageSize);

            }

            return View(PageListVH);

        }
        [HttpGet]
        public ActionResult VehicleInfoViewAll()
        {
            List<VehicleinfoViewModel> f = lk.GetAll();
            Session["VehicleInfoAll"] = f;
            List<BrandViewModel> s = br.GetAll();
            Session["brandAll"] = s;
            List<ModelViewModel> o = md.GetAll();
            Session["modelAll"] = o;

            Session["StringSearch"] = null;
            Session["VHSearch"] = null;
            return RedirectToAction("VehicleInfoManager");
        }
        public ActionResult VehicleSearch()
        {
            var keyword = Request.Form["tbSearch"];
            if (keyword !=null)
            {
                Session["StringSearch"] = keyword;
            }
            List<VehicleinfoViewModel> xa = lk.Search(1, 10, (String)Session["StringSearch"]);
            Session["VHSearch"] = xa;
            List<BrandViewModel> s = br.GetAll();
            Session["brandAll"] = s;
            List<ModelViewModel> o = md.GetAll();
            Session["modelAll"] = o;
            return RedirectToAction("VehicleInfoManager");
        }
        public ActionResult EditVehicleInfo(int id)
        {
            VehicleinfoViewModel a = lk.GetEdit(id);
            ViewData["VehicleInfoAll"] = a;
            List<BrandViewModel> s = br.GetAll();
            Session["brandAll"] = s;
            List<ModelViewModel> o = md.GetAll();
            Session["modelAll"] = o;
            return View();
        }
        [HttpPost]
        public ActionResult NewVehicleInfo(VehicleinfoViewModel sa)
        {
            lk.Update(sa);
            List<VehicleinfoViewModel> f = lk.GetAll();
            Session["VehicleInfoAll"] = f;
            if (Session["VHSearch"] == null)
            {
                return RedirectToAction("VehicleInfoViewAll");
            }
            return RedirectToAction("VehicleSearch");
        }

        public ActionResult Createvehicle()
        {
            List<VehicleinfoViewModel> f = lk.GetAll();
            Session["VehicleInfoAll"] = f;
            List<BrandViewModel> s = br.GetAll();
            Session["brandAll"] = s;
            List<ModelViewModel> o = md.GetAll();
            Session["modelAll"] = o;

            return View();
        }
        [HttpPost]
        public ActionResult AddVehicle(VehicleinfoViewModel ty)
        {
            lk.Add(ty);
            List<VehicleinfoViewModel> f = lk.GetAll();
            Session["VehicleInfoAll"] = f;
            return RedirectToAction("VehicleInfoViewAll");

        }

        [HttpPost]
        public ActionResult DeleteVehicle(int id)
        {
            lk.Delete(id);
            List<VehicleinfoViewModel> f = lk.GetAll();
            Session["VehicleInfoAll"] = f;
            return RedirectToAction("VehicleInfoViewAll");
        }
    }
}