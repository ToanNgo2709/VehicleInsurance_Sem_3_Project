using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.DAL;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.BLL.DAO;
using PagedList;
using PagedList.Mvc;
using VehicleInsuranceSem3.DAL.Model;

namespace VehicleInsuranceSem3.Controllers
{
    public class UserAccController : Controller
    {
        public UserTypeDAORequest cc = new UserTypeDAORequest();
        public CustomerinfoDAORequest ss = new CustomerinfoDAORequest();

        // GET: UserAcc
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccManager(int page = 1 , int pageSize = 10)
        {
            List<CustomerinfoViewModel> listcus = ss.GetAll();
            PagedList<CustomerinfoViewModel> PageListCus = new PagedList<CustomerinfoViewModel>(listcus, page, pageSize);
            if (Session["cusSearch"] != null)
            {
                listcus = (List<CustomerinfoViewModel>)Session["cusSearch"];
                PageListCus = new PagedList<CustomerinfoViewModel>(listcus, page, pageSize);
            }
            //else
            //{
            //    listcus = ss.GetAll();
            //    PageListCus = new PagedList<CustomerinfoViewModel>(listcus, page, pageSize);

            //}

            return View(PageListCus);
        }

   
        public ActionResult UserTypeManager(int page = 1 , int pageSize =10)
        {
            List<UsertypeViewModel> ListUS = new List<UsertypeViewModel>();
            PagedList<UsertypeViewModel> PageListUS;
            if (Session["UsSearch"] !=null)
            {
                ListUS = (List<UsertypeViewModel>)Session["UsSearch"];
                PageListUS = new PagedList<UsertypeViewModel>(ListUS, page, pageSize);
            }
            else
            {
                ListUS = cc.GetAll();
                PageListUS = new PagedList<UsertypeViewModel>(ListUS, page, pageSize);

            }

            return View(PageListUS);
        }
     

        #region ViewAll
        [HttpGet]
        public ActionResult ViewAllAcc()
        {
            List<CustomerinfoViewModel> c = ss.GetAll();
            Session["AllListAcc"] = c;

            Session["StringSearch"] = null;
            Session["cusSearch"] = null;
            return RedirectToAction("AccManager");
        }

        [HttpGet]
        public ActionResult ViewUserType()
        {
          List<UsertypeViewModel> q = cc.GetAll();
            Session["AllListUsType"] = q;

            Session["StringSearch"] = null;
            Session["UsSearch"] = null;
            return RedirectToAction("UserTypeManager");
        }
        #endregion

        #region Search
        public ActionResult AccSearch()
        {
            var keyword = Request.Form["tbSearch"];
            if (keyword !=null)
            {
                Session["StringSearch"] = keyword;
            }
           List<CustomerinfoViewModel> ds= ss.Search(1,10,(String)Session["StringSearch"]);
            Session["cusSearch"] = ds;
            return RedirectToAction("AccManager");
        }
        public ActionResult UserTypeSearch()
        { 
            var keword = Request.Form["tbSearch"];
            if (keword !=null)
            {
                Session["StringSearch"] = keword;
            }
            List<UsertypeViewModel> ll = cc.Search(1, 10, (String)Session["StringSearch"]);
            Session["UsSearch"] = ll ;
            return RedirectToAction("UserTypeManager");
        }
        #endregion

        #region AccEdit
        public ActionResult EditAcc(int id)
        {
            CustomerinfoViewModel asf = ss.GetEdit(id);
            List<UsertypeViewModel> q = cc.GetAll();
            Session["AllListUsType"] = q;
            ViewData["AllListAcc"] = asf;
            return View();
        }
        public ActionResult EdituserType(int id)
        {
            UsertypeViewModel mc = cc.GetEdit(id);
            ViewData["AllListUsType"] = mc;
            return View();
        }

        #endregion

        #region NewAcc
        [HttpPost]
        public ActionResult NewAcc(CustomerinfoViewModel cus)
        {
            ss.Update(cus);
            List<CustomerinfoViewModel> c = ss.GetAll();
            Session["AllListAcc"] = c;
            if (Session["cusSearch"] == null)
            {
                return RedirectToAction("ViewAllAcc");
            }
            return RedirectToAction("AccSearch");
        }
        [HttpPost]
        public ActionResult NewUsType(UsertypeViewModel csa)
        {
            cc.Update(csa);
            List<UsertypeViewModel> q = cc.GetAll();
            Session["AllListUsType"] = q;
            if (Session["UsSearch"] == null)
            {
                return RedirectToAction("ViewUserType");
            }
            return RedirectToAction("UserTypeSearch");
        }

        #endregion
        #region Create
        public ActionResult CreateAcc()
        {
            List < CustomerinfoViewModel>c=ss.GetAll();
            Session["AllListAcc"] = c;
            List<UsertypeViewModel> q = cc.GetAll();
            Session["AllListUsType"] = q;
            return View();
           
        }
        public ActionResult CreateUsType()
        {
            List<UsertypeViewModel> q = cc.GetAll();
            Session["AllListUsType"] = q;
            return View();
        }
        [HttpPost]
        public ActionResult AddUsType(UsertypeViewModel a)
        {
            cc.Add(a);
            List<UsertypeViewModel> q = cc.GetAll();
            Session["AllListUsType"] = q;
            return RedirectToAction("ViewUserType");

        }

        [HttpPost]
        public ActionResult AddACC(CustomerinfoViewModel cus)
        {
            
            
         
            ss.Add(cus);
            List<CustomerinfoViewModel> c = ss.GetAll();
            Session["AllListAcc"] = c;
            return RedirectToAction("ViewAllAcc");

        }

        public ActionResult ShowCustomerPolicyHistory(int id, string customerName)
        {
            CustomerpolicyDAORequest request = new CustomerpolicyDAORequest();
            List<CustomerHistoryModelView> list = request.GetCustomerPolicyHistory(id);
            ViewBag.customerName = customerName;
            return View(list);
        }

       
        public ActionResult ShowCustomerPolicyDetail(int customerPolicyId)
        {
            var context = new InsuranceDbContext();
            var model = context.Customer_Policy
                .Where(c => c.id == customerPolicyId)
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

        #endregion

        #region Delete
        [HttpPost]
        public ActionResult DeleteAcc(int id)
        {
            ss.Delete(id);
            List<CustomerinfoViewModel> c = ss.GetAll();
            Session["AllListAcc"] = c;
            return RedirectToAction("ViewAllAcc");
        }

        [HttpPost]
        public ActionResult DeleteUSType(int id)
        {
            cc.Delete(id);
            List<UsertypeViewModel> q = cc.GetAll();
            Session["AllListUsType"] = q;
            return RedirectToAction("ViewUserType");
        }

        #endregion
    }

}
