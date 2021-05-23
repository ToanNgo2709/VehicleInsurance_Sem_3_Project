﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.Feature;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.Models;

namespace VehicleInsuranceSem3.Controllers
{
    public class DashBoardController : Controller
    {

        ReportFeature report = new ReportFeature();
        // GET: DashBoard
        public ActionResult DashIndex()
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            List<PolicyTypeCountModel> thisMonthList = report.CountPolicyTypeSellByMonth(firstDayOfMonth, lastDayOfMonth);

            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var item in thisMonthList)
            {
                dataPoints.Add(new DataPoint(item.Name, item.Number));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            ///------------------------------------
            List<InsuranceCustomerPolicyMonthlyViewModel> model = report.ShowCustomerPolicyByDate(firstDayOfMonth, lastDayOfMonth);
            ViewBag.insuranceList = model;

            ///-------------------------------------------

            List<BrandInsuranceSellViewModel> brandList = report.CountBrandInsuranceSellByMonth(firstDayOfMonth, lastDayOfMonth);
            List<DataPoint> brandDataPoints = new List<DataPoint>();
            foreach (var item in brandList)
            {
                brandDataPoints.Add(new DataPoint(item.BrandName, item.Amount));
            }
            ViewBag.BrandDataPoints = JsonConvert.SerializeObject(brandDataPoints);

            return View();
        }
    }
}