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
    public class PolicyManagerController : Controller
    {
        // GET: PolicyManager
        public PolicyTypeDAORequest pl = new PolicyTypeDAORequest();
        public PolicyDAORequest c = new PolicyDAORequest();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PolicyTypeManager(int page = 1 , int pageSize=10)
        {
            List<PolicytypeViewModel> ListPoliType = new List<PolicytypeViewModel>();
            PagedList<PolicytypeViewModel> PageListPLT;
            if (Session["PLTSearch"] !=null)
            {
                ListPoliType = (List<PolicytypeViewModel>)Session["PLTSearch"];
                PageListPLT = new PagedList<PolicytypeViewModel>(ListPoliType, page, pageSize);
            }
            else
            {
                ListPoliType = pl.GetAll();
                PageListPLT = new PagedList<PolicytypeViewModel>(ListPoliType, page, pageSize);
            }

            return View(PageListPLT);
        }
        public ActionResult PolicyManager(int page = 1 , int pageSize = 10)
        {
            List<PolicyViewModel> x = c.GetAll();
            Session["AllPolicy"] = x;
            List<PolicytypeViewModel> m = pl.GetAll();
            Session["AllPolicyType"] = m;

            List<PolicyViewModel> ListPoli = new List<PolicyViewModel>();
            PagedList<PolicyViewModel> PageListPL;
            if (Session["PLSearch"]!= null)
            {
                ListPoli = (List<PolicyViewModel>)Session["PLSearch"];
                PageListPL = new PagedList<PolicyViewModel>(ListPoli, page, pageSize);

            }
            else
            {
                ListPoli = c.GetAll();
                PageListPL = new PagedList<PolicyViewModel>(ListPoli, page, pageSize);

            }
            return View(PageListPL);
        }

        [HttpGet]

        public ActionResult PolicyTypeViewAll()
        {
            List<PolicytypeViewModel> m = pl.GetAll();
            Session["AllPolicyType"] = m;

            Session["StringSearch"] = null;
            Session["PLTSearch"] = null;
            return RedirectToAction("PolicyTypeManager");
        }

        [HttpGet]
        public ActionResult PolicyViewAll()
        {
            List<PolicyViewModel> x = c.GetAll();
            Session["AllPolicy"] = x;
            List<PolicytypeViewModel> m = pl.GetAll();
            Session["AllPolicyType"] = m;

            Session["StringSearch"] = null;
            Session["PLSearch"] = null;
            return RedirectToAction("PolicyManager");
        }

        public ActionResult PolicySearch()
        {
            var keyword = Request.Form["tbSearch"];
            if (keyword !=null)
            {
                Session["StringSearch"] = keyword;
            }
            List<PolicyViewModel> cc = c.Search(1, 10, (String)Session["StringSearch"]);
            Session["PLSearch"] = cc;
            List<PolicytypeViewModel> m = pl.GetAll();
            Session["AllPolicyType"] = m;

            return RedirectToAction("PolicyManager");
        }


        public ActionResult PolicyTypeSearch()
        {
            var keyword = Request.Form["tbSearch"];
            if (keyword != null)
            {
                Session["StringSearch"] = keyword;
            }
            List<PolicytypeViewModel> m = pl.Search(1, 10, (String)Session["StringSearch"]);
            Session["PLTSearch"] = m;

            return RedirectToAction("PolicyTypeManager");
        }

        public ActionResult EditPolicyType(int id)
        {
            PolicytypeViewModel m = pl.GetEdit(id);
            ViewData["AllPolicyType"] = m;
            return View();
        }

        public ActionResult EditPolicy(int id)
        {
            PolicyViewModel s = c.GetEdit(id);
            ViewData["AllPolicy"] = s;
            List<PolicytypeViewModel> m = pl.GetAll();
            Session["AllPolicyType"] = m;
            return View();
        }
        [HttpPost]
        public ActionResult NewPolicy(PolicyViewModel nn)
        {
            c.Update(nn);
            List<PolicyViewModel> j = c.GetAll();
            Session["AllPolicy"] = j;
            if (Session["PLSearch"] == null)
            {
                return RedirectToAction("PolicyViewAll");
            }
            return RedirectToAction("PolicySearch");
        }


        [HttpPost]
        public ActionResult NewPolicyType(PolicytypeViewModel ps)
        {
            pl.Update(ps);
            List<PolicytypeViewModel> m = pl.GetAll();
            Session["AllPolicyType"] = m;
            if (Session["PLTSearch"] == null )
            {
                return RedirectToAction("PolicyTypeViewAll");
            }
            return RedirectToAction("PolicyTypeSearch");
        }

        public ActionResult CreatePolicyType()
        {
            List<PolicytypeViewModel> m = pl.GetAll();
            Session["AllPolicyType"] = m;
            return View();
        }
        public ActionResult CreatePolicy()
        {
            List<PolicyViewModel> x = c.GetAll();
            Session["AllPolicy"] = x;
            List<PolicytypeViewModel> m = pl.GetAll();
            Session["AllPolicyType"] = m;
            return View();

        }

       [HttpPost]
        public ActionResult AddPolicy(PolicyViewModel xx)
        {
            c.Add(xx);
            List<PolicyViewModel> xc = c.GetAll();
            Session["AllPolicy"] =xc;
        
            return RedirectToAction("PolicyViewAll");
        }

        [HttpPost]
        public ActionResult AddPolicyType(PolicytypeViewModel ps)
        {
            pl.Add(ps);
            List<PolicytypeViewModel> m = pl.GetAll();
            Session["AllPolicyType"] = m;
            return RedirectToAction("PolicyTypeViewAll");

        }

        [HttpPost]
        public ActionResult DeletePolocy(int id)
        {
            c.Delete(id);
            List<PolicyViewModel> xc = c.GetAll();
            Session["AllPolicy"] = xc;
            return RedirectToAction("PolicyViewAll");
        }

        [HttpPost]
        public ActionResult DeletePolicyType(int id)
        {
            pl.Delete(id);
            List<PolicytypeViewModel> m = pl.GetAll();
            Session["AllPolicyType"] = m;
            return RedirectToAction("PolicyTypeViewAll");
        }
    }
}