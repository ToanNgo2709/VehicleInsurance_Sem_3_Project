using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.BLL.DAO;
using VehicleInsuranceSem3.DAL.Model;
using PagedList;
using PagedList.Mvc;

namespace VehicleInsuranceSem3.Controllers
{
    public class BrandmodelManagerController : Controller
    {

        public BrandDAORequest brd = new BrandDAORequest();
        public ModelDAORequest saf = new ModelDAORequest();

        // GET: brandmodel
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult BrandManager(int page = 1, int pageSize = 5)
        {
            List<BrandViewModel> a = brd.GetAll();
            Session["LisBrandAll"] = a;

            List<BrandViewModel> ListBrand = new List<BrandViewModel>();
            PagedList<BrandViewModel> PagelistBrand;
          

            if (Session["SearchBrand"] != null)
            {
                ListBrand = (List<BrandViewModel>)Session["SearchBrand"];
                PagelistBrand = new PagedList<BrandViewModel>(ListBrand, page, pageSize);
                
            }
            else
            {
                ListBrand = brd.GetAll();
                PagelistBrand = new PagedList<BrandViewModel>(ListBrand, page, pageSize);
               

            }
            return View(PagelistBrand);
        }

        public ActionResult ModelManager(int page = 1, int pageSize = 10)
        {
            List<ModelViewModel> s = saf.GetAll();
            Session["ListAllModel"] = s;
            List<BrandViewModel> a = brd.GetAll();
            Session["LisBrandAll"] = a;

            List<ModelViewModel> ListModel = new List<ModelViewModel>();
            PagedList<ModelViewModel> PagedListModel;

            if (Session["SearchModel"] != null)
            {
                ListModel = (List<ModelViewModel>)Session["SearchModel"];
                PagedListModel = new PagedList<ModelViewModel>(ListModel, page, pageSize);
                
            }
            else
            {
                ListModel = saf.GetAll();
                PagedListModel = new PagedList<ModelViewModel>(ListModel, page, pageSize);
               
            }



            return View(PagedListModel);
        }


        /*   public ActionResult testne()
           {

           }
   */
        [HttpGet]
        public ActionResult BrandViewAll()
        {
            List<BrandViewModel> a = brd.GetAll();
            Session["LisBrandAll"] = a;
           
            Session["StringSearch"] = null;
            Session["SearchBrand"] = null;

            return RedirectToAction("BrandManager");

        }



    
        [HttpGet]
        public ActionResult ModelViewAll()
        {
            List<ModelViewModel> s = saf.GetAll();
            Session["ListAllModel"] = s;
            List<BrandViewModel> a = brd.GetAll();
            Session["LisBrandAll"] = a;

            Session["StringSearch"] = null;
            Session["SearchModel"] = null;
            return RedirectToAction("ModelManager");
        }
        public ActionResult ModelSearch()
        {
            var keyword = Request.Form["tbSearch"];
            if (keyword != null)
            {
                Session["StringSearch"] = keyword;

            }
            List<ModelViewModel> sv = saf.Search(1, 10, (String)Session["StringSearch"]);
           
            Session["SearchModel"] = sv;
            //
            List<BrandViewModel> a = brd.GetAll();
            Session["LisBrandAll"] = a;
            return RedirectToAction("ModelManager");

            //PagedList<ModelViewModel> model = new PagedList<ModelViewModel>(sv, 1, 10);
            //Session["StringSearch"];
        }

        public ActionResult BrandSearch()
        {
            var keyword = Request.Form["tbSearch"];
            if (keyword !=null)
            {
                Session["StringSearch"] = keyword;
            }
            List<BrandViewModel> s = brd.Search(1, 10, (String)Session["StringSearch"]);
            Session["SearchBrand"] = s;
            return RedirectToAction("BrandManager");
        }

        public ActionResult EditModel(int id)
        {
            ModelViewModel vv = saf.GetEdit(id);
            ViewData["ListAllModel"] = vv;
            List<BrandViewModel> a = brd.GetAll();
            Session["LisBrandAll"] = a;
            return View();
        }

        public ActionResult EditBrand(int id)
        {
            BrandViewModel s = brd.GetEdit(id);
            ViewData["LisBrandAll"] = s;
            return View();
        }
        [HttpPost]
        public ActionResult NewBrand(BrandViewModel bs)
        {
            brd.Update(bs);
            List<BrandViewModel> a = brd.GetAll();
            Session["LisBrandAll"] = a;
            if (Session["SearchBrand"] == null)
            {
                return RedirectToAction("BrandViewAll");
            }

            return RedirectToAction("BrandSearch");
        }
        [HttpPost]
        public ActionResult NewModel(ModelViewModel xx)
        {
            saf.Update(xx);
            List<ModelViewModel> n = saf.GetAll();
            Session["ListAllModel"] = n;
            if (Session["SearchModel"] == null)
            {
                return RedirectToAction("ModelViewAll");

            }
            return RedirectToAction("ModelSearch");
        }
        public ActionResult CreateModel()
        {
            List<ModelViewModel> v = saf.GetAll();
            Session["ListAllModel"] = v;
            List<BrandViewModel> a = brd.GetAll();
            Session["LisBrandAll"] = a;
            return View();
        }

        public ActionResult CreateBrand()
        {
            List<BrandViewModel> a = brd.GetAll();
            Session["LisBrandAll"] = a;
            return View();
        }

        [HttpPost]
        public ActionResult AddBrand(BrandViewModel vs)
        {
            brd.Add(vs);
            List<BrandViewModel> a = brd.GetAll();
            Session["LisBrandAll"] = a;
            return RedirectToAction("BrandViewAll");
        }

        [HttpPost]
        public ActionResult AddModel(ModelViewModel mb)
        {
            saf.Add(mb);
            List<ModelViewModel> s = saf.GetAll();
            Session["ListAllModel"] = s;
            return RedirectToAction("ModelViewAll");


        }

        [HttpPost]
        public ActionResult DeleteBrand(int id)
        {
            brd.Delete(id);
            List<BrandViewModel> a = brd.GetAll();
            Session["LisBrandAll"] = a;

            return RedirectToAction("BrandViewAll");
        }

        [HttpPost]
        public ActionResult DeleteModel(int id)
        {
            saf.Delete(id);
            List<ModelViewModel> b = saf.GetAll();
            Session["ListAllModel"] = b;
            return RedirectToAction("ModelViewAll");
        }

       
    }

}
