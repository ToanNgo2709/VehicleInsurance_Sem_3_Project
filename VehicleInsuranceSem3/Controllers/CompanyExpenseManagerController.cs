using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.BLL.DAO;

using PagedList;
using PagedList.Mvc;
using VehicleInsuranceSem3.DAL.Model;

namespace VehicleInsuranceSem3.Controllers

{
    public class CompanyExpenseManagerController : Controller
    {
        public ExpensetypeDAORequest mc = new ExpensetypeDAORequest();
        public CompanyexpenseDAORequest xx = new CompanyexpenseDAORequest();
        public ClaimDetailDAORequest l = new ClaimDetailDAORequest();
        public CustomerpolicyDAORequest sxx = new CustomerpolicyDAORequest();

        // GET: CompanyExpenseManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExpenseTypeManager(int page = 1, int pageSize = 10)
        {
            List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;
            List<ExpensetypeViewModel> LisExpenseTpe = new List<ExpensetypeViewModel>();
            PagedList<ExpensetypeViewModel> PageListExpenseTpe;
            if (Session["SearchExpenseType"] != null)
            {
                LisExpenseTpe = (List<ExpensetypeViewModel>)Session["SearchExpenseType"];
                PageListExpenseTpe = new PagedList<ExpensetypeViewModel>(LisExpenseTpe, page, pageSize);
            }
            else
            {
                LisExpenseTpe = mc.GetAll();
                PageListExpenseTpe = new PagedList<ExpensetypeViewModel>(LisExpenseTpe, page, pageSize);
            }

            return View(PageListExpenseTpe);


        }
        public ActionResult CompanyExpenseManager(int page = 1, int pageSize = 10)
        {
            List<CompanyexpenseViewModel> x = xx.GetAll();
            Session["CompanyExpenseViewAll"] = x;
            List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;

            List<CompanyexpenseViewModel> ListCompanyExpense = new List<CompanyexpenseViewModel>();
            PagedList<CompanyexpenseViewModel> PageListCompanyExpense;
            if (Session["CompanyExpenseSearch"] != null)
            {
                ListCompanyExpense = (List<CompanyexpenseViewModel>)Session["CompanyExpenseSearch"];
                PageListCompanyExpense = new PagedList<CompanyexpenseViewModel>(ListCompanyExpense, page, pageSize);
            }
            else
            {
                ListCompanyExpense = xx.GetAll();
                PageListCompanyExpense = new PagedList<CompanyexpenseViewModel>(ListCompanyExpense, page, pageSize);
            }

            return View(PageListCompanyExpense);

        }
        public ActionResult CLaimDetailManager(int page = 1, int pageSize = 10)
        {
            List<ClaimDetailViewModel> s = l.GetAll();
            Session["ClaimDetailViewAll"] = s;
            List<CustomerpolicyViewModel> z = sxx.GetAll();
            Session["CusAllView"] = z;

            List<ClaimDetailViewModel> ListClaimDetail = new List<ClaimDetailViewModel>();
            PagedList<ClaimDetailViewModel> PageListClaimDetail;
            if (Session["ClaimDetailSearchs"] != null)
            {
                ListClaimDetail = (List<ClaimDetailViewModel>)Session["ClaimDetailSearchs"];
                PageListClaimDetail = new PagedList<ClaimDetailViewModel>(ListClaimDetail, page, pageSize);
            }
            else
            {
                ListClaimDetail = l.GetAll();
                PageListClaimDetail = new PagedList<ClaimDetailViewModel>(ListClaimDetail, page, pageSize);
            }
            ViewBag.message = TempData["message"];

            return View(PageListClaimDetail);
        }

        [HttpGet]
        public ActionResult ClaimDetailViewAll()
        {

            List<ClaimDetailViewModel> s = l.GetAll();
            Session["ClaimDetailViewAll"] = s;
            List<CustomerpolicyViewModel> z = sxx.GetAll();
            Session["CusAllView"] = z;

            Session["StringSearch"] = null;
            Session["ClaimDetailSearchs"] = null;

            return RedirectToAction("CLaimDetailManager");
        }

        [HttpGet]
        public ActionResult CompanyExpenseViewAll()
        {
            List<CompanyexpenseViewModel> x = xx.GetAll();
            Session["CompanyExpenseViewAll"] = x;
            List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;

            Session["StringSearch"] = null;
            Session["CompanyExpenseSearch"] = null;

            return RedirectToAction("CompanyExpenseManager");

        }

        [HttpGet]
        public ActionResult expenseTypeViewAll()
        {

            List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;

            Session["StringSearch"] = null;
            Session["SearchExpenseType"] = null;

            return RedirectToAction("ExpenseTypeManager");
        }
        public ActionResult ClaimDetailSearch()
        {
            var keywod = Request.Form["tbSearch"];
            if (keywod != null)
            {
                Session["StringSearch"] = keywod;

            }
            List<ClaimDetailViewModel> bb = l.Search(1, 10, (String)Session["StringSearch"]);
            Session["ClaimDetailSearchs"] = bb;
            List<CustomerpolicyViewModel> z = sxx.GetAll();
            Session["CusAllView"] = z;
            return RedirectToAction("CLaimDetailManager");
        }

        public ActionResult FilterClaimByDate()
        {
            DateTime startDate = DateTime.Parse(Request.Params["startDate"]);
            DateTime endDate = DateTime.Parse(Request.Params["endDate"]);
            List<ClaimDetailViewModel> model = l.FilterClaimDetailByDate(startDate, endDate, 1, 10);
            Session["ClaimDetailSearchs"] = model;
            return RedirectToAction("CLaimDetailManager");
        }



        public ActionResult ComPanySearch()
        {
            var keyword = Request.Form["tbSearch"];
            if (keyword != null)
            {
                Session["StringSearch"] = keyword;

            }
            List<CompanyexpenseViewModel> x = xx.Search(1, 10, (String)Session["StringSearch"]);
            Session["CompanyExpenseSearch"] = x;

            List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;
            return RedirectToAction("CompanyExpenseManager");
        }

        [HttpPost]
        public ActionResult FilterCompanyExpense()
        {
            DateTime startDate = DateTime.Parse(Request.Params["startDate"]);
            DateTime endDate = DateTime.Parse(Request.Params["endDate"]);
            List<CompanyexpenseViewModel> model = xx.FilterExpenseByDate(startDate, endDate, 1, 10);
            Session["CompanyExpenseSearch"] = model;
            return RedirectToAction("CompanyExpenseManager");
        }

        public ActionResult expenseTypeSearch()
        {
            var keyword = Request.Form["tbSearch"];
            if (keyword != null)
            {
                Session["StringSearch"] = keyword;
            }
            List<ExpensetypeViewModel> s = mc.Search(1, 10, (String)Session["StringSearch"]);
            Session["SearchExpenseType"] = s;
            return RedirectToAction("ExpenseTypeManager");

        }
        public ActionResult EditClaimDetail(int id)
        {
            ClaimDetailViewModel k = l.GetEdit(id);
            ViewData["ClaimDetailViewAll"] = k;
            List<CustomerpolicyViewModel> z = sxx.GetAll();
            Session["CusAllView"] = z;
            return View();
        }

        public ActionResult EditCompanyExpense(int id)
        {
            CompanyexpenseViewModel x = xx.GetEdit(id);
            ViewData["CompanyExpenseViewAll"] = x;

            List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;
            return View();
        }
        public ActionResult EditexpenseType(int id)
        {
            ExpensetypeViewModel b = mc.GetEdit(id);
            ViewData["ViewAllExpenseType"] = b;

            return View();

        }
        [HttpPost]
        public ActionResult NewClaimDetail(ClaimDetailViewModel f)
        {
            l.Update(f);
            List<ClaimDetailViewModel> s = l.GetAll();
            Session["ClaimDetailViewAll"] = s;
            if (Session["ClaimDetailSearchs"] == null)
            {
                return RedirectToAction("ClaimDetailViewAll");
            }
            return RedirectToAction("ClaimDetailSearch");

        }


        [HttpPost]
        public ActionResult NewCompanyExpense(CompanyexpenseViewModel z)
        {

            xx.Update(z);
            List<CompanyexpenseViewModel> x = xx.GetAll();
            Session["CompanyExpenseViewAll"] = x;
            if (Session["CompanyExpenseSearch"] == null)
            {
                return RedirectToAction("CompanyExpenseViewAll");
            }
            return RedirectToAction("ComPanySearch");
        }

        [HttpPost]
        public ActionResult NewexpeneseType(ExpensetypeViewModel v)
        {
            mc.Update(v);
            List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;
            if (Session["SearchExpenseType"] == null)
            {
                return RedirectToAction("expenseTypeViewAll");
            }
            return RedirectToAction("expenseTypeSearch");

        }

        public ActionResult CreateCompanyExpense()
        {
            List<CompanyexpenseViewModel> x = xx.GetAll();
            Session["CompanyExpenseViewAll"] = x;
            List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;
            return View();
        }
        public ActionResult CreateClaimDetail()
        {
            List<ClaimDetailViewModel> h = l.GetAll();
            Session["ClaimDetailViewAll"] = h;
            List<CustomerpolicyViewModel> z = sxx.GetAll();
            Session["CusAllView"] = z;

            return View();

        }

        [HttpPost]
        public ActionResult GetClaimInfo(int customerPolicyId)
        {
            var context = new InsuranceDbContext();
            var customerPolicy = context.Customer_Policy.Where(c => c.id == customerPolicyId).FirstOrDefault();

            decimal insuredAmount = (decimal)customerPolicy.Policy.Policy_Type.liability_level;
            decimal claimableAmout = (insuredAmount / 100) * (decimal)customerPolicy.Vehicle_Info.rate_by_condition;

            return Json(new { insure = insuredAmount, claim = claimableAmout }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CreateExpenseType()
        {
            List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;
            return View();
        }
        [HttpPost]
        public ActionResult AddClaimDetail(ClaimDetailViewModel ss)
        {
            CompanyexpenseDAORequest request = new CompanyexpenseDAORequest();
            List<ClaimDetailViewModel> checkLIst = l.CheckPolicyExist((int)ss.customerpolicyid);
            if (checkLIst.Count > 0)
            {
                TempData["message"] = "This customer policy has claimed, please check in claim management";
                return RedirectToAction("CLaimDetailManager", new { page = 1, pageSize = 10});
            }
            else
            {
                CompanyexpenseViewModel model = new CompanyexpenseViewModel()
                {
                    date = DateTime.Today,
                    expensetypeid = 1,
                    amount = ss.claimableamount,
                    customerpolicyid = ss.customerpolicyid,
                    description = "Chi trả bảo hiểm cho hợp đồng số: " + ss.customerpolicyid
                };
                request.Add(model);
                l.Add(ss);
                List<ClaimDetailViewModel> h = l.GetAll();
                Session["ClaimDetailViewAll"] = h;
                return RedirectToAction("ClaimDetailBill", ss);
            }
            
        }

        public ActionResult GetClaimDetail(int id)
        {
            ClaimDetailDAORequest request = new ClaimDetailDAORequest();
            var model = request.GetClaimById(id);
            return RedirectToAction("ClaimDetailBill", model);
        }

        public ActionResult ClaimDetailBill(ClaimDetailViewModel ss)
        {
            
            var context = new InsuranceDbContext();
            var claimDetail = context.Claim_Detail.Where(c => c.claim_number.Equals(ss.claimnumber))
                .Select(c => new ClaimBillViewModel
                {
                    AccidentDate = c.date_accident,
                    AccidentPlace = c.place_accident,
                    Claim = c.claimable_amount,
                    ClaimName = c.claim_number,
                    CustomerName = c.Customer_Policy.Customer_Info.name,
                    CustomerPolicyId = c.Customer_Policy.id,
                    EndDate = c.Customer_Policy.policy_end_date,
                    Insured = c.insured_amount,
                    PolicyName = c.Customer_Policy.Policy.policy_number,
                    StartDate = c.Customer_Policy.policy_start_date,
                    VehicleName = c.Customer_Policy.Vehicle_Info.Brand.name + " " + c.Customer_Policy.Vehicle_Info.Model.name
                }).FirstOrDefault();

            return View(claimDetail);
        }


        [HttpPost]
        public ActionResult AddConpanyExpesnese(CompanyexpenseViewModel z)
        {
            xx.Add(z);
            List<CompanyexpenseViewModel> x = xx.GetAll();
            Session["CompanyExpenseViewAll"] = x;
            List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;
            return RedirectToAction("CompanyExpenseViewAll");
        }

        [HttpPost]
        public ActionResult AddExpenseType(ExpensetypeViewModel b)
        {
            mc.Add(b);
            List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;
            return RedirectToAction("expenseTypeViewAll");

        }

        [HttpPost]
        public ActionResult DeleteCompanyExpense(int id)
        {
            xx.Delete(id);
            List<CompanyexpenseViewModel> x = xx.GetAll();
            Session["CompanyExpenseViewAll"] = x;
            return RedirectToAction("CompanyExpenseViewAll");
        }

        [HttpPost]
        public ActionResult DeleteExpenseType(int id)
        {
            mc.Delete(id);
             List<ExpensetypeViewModel> c = mc.GetAll();
            Session["ViewAllExpenseType"] = c;
            return RedirectToAction("expenseTypeViewAll");
        }


        [HttpPost]
        public ActionResult DeleteClaimDetail(int c)
        {
            l.Delete(c);

            List<ClaimDetailViewModel> s = l.GetAll();
            Session["ClaimDetailViewAll"] = s;
            return RedirectToAction("ClaimDetailViewAll");

        }
    }
}