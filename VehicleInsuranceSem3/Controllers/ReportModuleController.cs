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

        //REPORT 1
        public ActionResult InsuranceMonthlySellReport()
        {
            
            var today = DateTime.Now;
            var defaultStartDate = StartEndDateOfCurrentMonth.GetStartDateOfMonth(today);
            var defaultEndDate = StartEndDateOfCurrentMonth.GetEndDateOfMonth(defaultStartDate);
            List<InsuranceCustomerPolicyMonthlyViewModel> model = report.ShowCustomerPolicyByDate(defaultStartDate, defaultEndDate);
            return View(model);
        }


        [HttpPost]
        public ActionResult InsuranceMonthlySellReport(string start, string end)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            List<InsuranceCustomerPolicyMonthlyViewModel> model = report.ShowCustomerPolicyByDate(startDate, endDate);
            return View(model);
        }

        public ActionResult SearchByDate()
        {
            DateTime startDay = DateTime.Parse(Request.Params["StartDay"]);
            DateTime endDay = DateTime.Parse(Request.Params["EndDay"]);
            return View();
        }

        public ActionResult ClaimaAmountMonthly()
        {
            ReportFeature report = new ReportFeature();
            //var list = report.ShowClaimableReportByMonth(startDate, endDate);
            return View();
        }


        [HttpPost]
        public ActionResult ClaimaAmountMonthly(string start, string end)
        {
            ReportFeature report = new ReportFeature();
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            var list = report.ShowClaimableReportByMonth(startDate, endDate);
            return View(list);
        }

        public ActionResult VehicleWise()
        {
            BrandDAORequest request = new BrandDAORequest();
            Session["brandList"] = request.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult VehicleWise(string start, string end, int id)
        {
            ReportFeature report = new ReportFeature();
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            BrandDAORequest request = new BrandDAORequest();
            Session["brandList"] = request.GetAll();
            ViewBag.modelReport = report.CountModelWithBrandSellByMonth(startDate, endDate, id);
            return View();
        }

        public ActionResult PolicyDueReport()
        {
            return View();
        }
    }
}