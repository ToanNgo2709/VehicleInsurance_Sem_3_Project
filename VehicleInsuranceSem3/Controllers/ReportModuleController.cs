using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.DAO;
using VehicleInsuranceSem3.BLL.Feature;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.Models;
using VehicleInsuranceSem3.Utilities.Others;

namespace VehicleInsuranceSem3.Controllers
{
    public class ReportModuleController : Controller
    {

        ReportFeature report = new ReportFeature();
        // GET: ReportModule
        public ActionResult DashBoard()
        {
            return View();
        }

        //INSURANCE MONTH SELL REPORT
        public ActionResult InsuranceMonthlySellReport()
        {         
            var today = DateTime.Now;
            var defaultStartDate = StartEndDateOfCurrentMonth.GetStartDateOfMonth(today);
            var defaultEndDate = StartEndDateOfCurrentMonth.GetEndDateOfMonth(defaultStartDate);
            List<InsuranceCustomerPolicyMonthlyViewModel> model = report.ShowCustomerPolicyAllTime();
            TempData["customerPurchase"] = report.ShowPurchaseAllTime();



            return View(model);
        }


        [HttpPost]
        public ActionResult GetInsuranceListByDate(string start, string end)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            List<InsuranceCustomerPolicyMonthlyViewModel> model = report.ShowCustomerPolicyByDate(startDate, endDate);
            return PartialView("InsuranceMonthly_PartialPage",model);
        }

        [HttpPost]
        public ActionResult GetPurchaseListByDate(string start, string end)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            List<CustomerPurchaseAmountViewModel> countList = report.ShowPurchaseAmountByDate(startDate, endDate);
            return PartialView("CustomerPurchaseTable_PartialPage", countList);
        }

        //------------------------------------------------
        [HttpGet]
        public ActionResult GetDataPointForCustomerPurchaseAll()
        {          
            List<CustomerPurchaseAmountViewModel> countList = report.ShowPurchaseAmountAll();

            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var item in countList)
            {
                dataPoints.Add(new DataPoint(item.CustomerName, item.Amount));
            }
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints);
            return PartialView("CustomerPurchaseReportChart_PartialPage", ViewBag.DataPoints2);
        }


        [HttpGet]
        public ActionResult GetDataPointForCustomerPurchase(string start, string end)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            List<CustomerPurchaseAmountViewModel> countList = report.ShowPurchaseAmountByDate(startDate, endDate);

            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var item in countList)
            {
                dataPoints.Add(new DataPoint(item.CustomerName, item.Amount));
            }
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints);
            return PartialView("CustomerPurchaseReportChart_PartialPage", ViewBag.DataPoints2);
        }

        //---------------------------------------------------------
        [HttpGet]
        public ActionResult GetDataPointForInsuranceReportAll()
        {
            List<PolicyTypeCountModel> countList = report.CountPolicyTypeSellAllTime();

            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var item in countList)
            {
                dataPoints.Add(new DataPoint(item.Name, item.Number));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return PartialView("InsuranceReportChart_PartialPage", ViewBag.DataPoints);
        }

        [HttpGet]
        public ActionResult GetDataPointForInsuranceReport(string start, string end)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            List<PolicyTypeCountModel> countList = report.CountPolicyTypeSellByMonth(startDate, endDate);

            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var item in countList)
            {
                dataPoints.Add(new DataPoint(item.Name, item.Number));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return PartialView("InsuranceReportChart_PartialPage", ViewBag.DataPoints);
        }

        //--------------------------------------------------------------------------------------

        //CLAIMABLE LIST REPORT
        public ActionResult ClaimaAmountMonthly()
        {
            List<ClaimableAmountByMonthViewModel> model = report.ShowAllClaimableReport();
            List<CompanyexpenseViewModel> model2 = report.ShowCompanyExpenseAllTime();

            TempData["expense"] = model2;
            return View(model);
        }


        [HttpPost]
        public ActionResult ClaimaAmountReportMonthly(string start, string end)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            var list = report.ShowClaimableReportByMonth(startDate, endDate);
            return PartialView("ClaimReport_PartialPage",list);
        }

        [HttpPost]
        public ActionResult ShowExpenseWithDate(string start, string end)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            List<CompanyexpenseViewModel> model2 = report.ShowCompanyExpenseByDate(startDate, endDate);
            return PartialView("Expense_PartialPage", model2);
        }

        [HttpGet]
        public ActionResult GetDataPointForCompanyExpenseReportAll()
        {
            List<ExpenseSumViewModel> sumList = report.ShowSumCompanyExpenseAllTime();

            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var item in sumList)
            {
                dataPoints.Add(new DataPoint(item.ExpenseTypeName, (double)item.Amount));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return PartialView("ExpenseSumChart_PartialPage", ViewBag.DataPoints);
        }

        [HttpGet]
        public ActionResult GetDataPointForCompanyExpenseReport(string start, string end)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            List<ExpenseSumViewModel> sumList = report.ShowSumCompanyExpenseByDate(startDate, endDate);

            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var item in sumList)
            {
                dataPoints.Add(new DataPoint(item.ExpenseTypeName, (double)item.Amount));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return PartialView("ExpenseSumChart_PartialPage", ViewBag.DataPoints);
        }





        //--------------------------------------------------------------------------------------

        //VEHICLE WISE REPORT
        public ActionResult VehicleWise()
        {
            BrandDAORequest request = new BrandDAORequest();
            Session["brandList"] = request.GetAll();
            return View();
        }

        //-------------------------------------------------
        [HttpGet]
        public ActionResult GetDataPointBrandReportAll()
        {
            List<BrandInsuranceSellViewModel> brandList = report.CountBrandInsuranceSellAllTime();
            List<DataPoint> brandDataPoints = new List<DataPoint>();
            foreach (var item in brandList)
            {
                brandDataPoints.Add(new DataPoint(item.BrandName, item.Amount));
            }
            ViewBag.BrandDataPoints = JsonConvert.SerializeObject(brandDataPoints);
            return PartialView("VehicleWiseChart_PartialPage", ViewBag.BrandDataPoints);
        }


        [HttpGet]
        public ActionResult GetDataPointBrandReport(string start, string end)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            List<BrandInsuranceSellViewModel> brandList = report.CountBrandInsuranceSellByMonth(startDate, endDate);
            List<DataPoint> brandDataPoints = new List<DataPoint>();
            foreach (var item in brandList)
            {
                brandDataPoints.Add(new DataPoint(item.BrandName, item.Amount));
            }
            ViewBag.BrandDataPoints = JsonConvert.SerializeObject(brandDataPoints);
            return PartialView("VehicleWiseChart_PartialPage", ViewBag.BrandDataPoints);
        }


        //------------------------------------------------------
        [HttpGet]
        public ActionResult GetDataPointModelReport(string start, string end, int id)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            List<ModelInsuranceViewModel> model = report.CountModelWithBrandSellByMonth(startDate, endDate, id);
            List<DataPoint> ModeldDataPoints = new List<DataPoint>();
            foreach (var item in model)
            {
                ModeldDataPoints.Add(new DataPoint(item.ModelName, item.Amount));
            }
            ViewBag.ModelDataPoints = JsonConvert.SerializeObject(ModeldDataPoints);
            return PartialView("VehicleModelReport_PartialPage", ViewBag.ModelDataPoints);
        }

        [HttpPost]
        public ActionResult GetReportModelAllTime(int id)
        {
            List<ModelInsuranceViewModel> model = report.CountModelWithBrandSellAllTime( id);
            return PartialView("VehicleWise_PartialPage", model);
        }

        [HttpPost]
        public ActionResult GetReportModelByBrand(string start, string end, int id)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            List<ModelInsuranceViewModel> model = report.CountModelWithBrandSellByMonth(startDate, endDate, id);
            return PartialView("VehicleWise_PartialPage", model);
        }



        //--------------------------------------------------------------------------------------

        //POLICY NEWARAL AND DUE REPORT
        public ActionResult PolicyDueReport()
        {
            ReportFeature report = new ReportFeature();

                    
            CustomerpolicyDAORequest request = new CustomerpolicyDAORequest();
            List<CustomerpolicyViewModel> list = request.GetLapsePolicy(DateTime.Today);
            foreach (var item in list)
            {
                item.active = false;
                request.Update(item);
            }

            //one month due Policy
            //date of last month
            DateTime oneMonthLaterDate = DateTime.Today.AddMonths(1);

            ViewBag.duePolicy = report.ShowPolicyDue(oneMonthLaterDate);

            //Lapsed Policy
            ViewBag.lapsedPolicy = report.ShowLapsedPolicyDue(false);
            return View();
        }

        //--------------------------------------------------------------------------------------
    }
}