﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.DAL.Model;

namespace VehicleInsuranceSem3.BLL.Feature
{
    public class ReportFeature
    {
        readonly InsuranceDbContext context = new InsuranceDbContext();

        public ReportFeature()
        {

        }


        /* REPORT 1*/
        public int CountCustomerPolicySellByMonth(int month)
        {
            int count = context.Customer_Policy.Where(c => c.create_date.Month == month).Count();
            return count;
        }

        public List<PolicyTypeCountModel> CountPolicyTypeSellByMonth(DateTime start, DateTime end)
        {

            var obj = (from pt in context.Policy_Type
                       join p in context.Policies on pt.id equals p.policy_type_id
                       join c in context.Customer_Policy on p.id equals c.policy_id
                       where c.create_date >= start && c.create_date <= end
                       group pt by pt.name into g
                       select new PolicyTypeCountModel
                       {
                           Name = g.Key,
                           Number = g.Count()
                       }).ToList();
            return obj;
        }

        public List<InsuranceCustomerPolicyMonthlyViewModel> ShowCustomerPolicyAllTime()
        {
            var i = 0;
            var list = context.Customer_Policy
                .Select(c => new InsuranceCustomerPolicyMonthlyViewModel
                {
                    Active = (bool)c.active,
                    CreatedDate = c.create_date,
                    CustomerName = c.Customer_Info.name,
                    Duration = c.Policy.policy_duration,
                    PolicyName = c.Policy.policy_number,
                    VehicleName = c.Vehicle_Info.Brand.name + " " + c.Vehicle_Info.Model.name

                }).ToList();
            return list;
        }

        public List<InsuranceCustomerPolicyMonthlyViewModel> ShowCustomerPolicyByDate(DateTime startDay, DateTime endDate)
        {

            var list = context.Customer_Policy
                .Where(c => c.create_date >= startDay && c.create_date <= endDate)
                .Select(c => new InsuranceCustomerPolicyMonthlyViewModel
                {
                    Active = (bool)c.active,
                    CreatedDate = c.create_date,
                    CustomerName = c.Customer_Info.name,
                    Duration = c.Policy.policy_duration,
                    PolicyName = c.Policy.policy_number,
                    VehicleName = c.Vehicle_Info.Brand.name + " " + c.Vehicle_Info.Model.name

                }).ToList();
            return list;
        }

        /* REPORT 2 */
        public List<BrandInsuranceSellViewModel> CountBrandInsuranceSellByMonth(DateTime startDate, DateTime endDate)
        {
            var list = (from cp in context.Customer_Policy
                        join v in context.Vehicle_Info on cp.vehicle_id equals v.id
                        join b in context.Brands on v.brand_id equals b.id
                        where cp.create_date >= startDate && cp.create_date <= endDate
                        group b by b.name into g
                        select new BrandInsuranceSellViewModel
                        {
                            BrandName = g.Key,
                            Amount = g.Count()
                        }).ToList();
            return list;
        }

        public List<ModelInsuranceViewModel> CountModelWithBrandSellByMonth(DateTime startDate, DateTime endDate, int brandId)
        {
            var list = (from cp in context.Customer_Policy
                        join v in context.Vehicle_Info on cp.vehicle_id equals v.id
                        join m in context.Models on v.model_id equals m.id
                        where cp.create_date >= startDate && cp.create_date <= endDate && v.brand_id == brandId
                        group m by m.name into g
                        select new ModelInsuranceViewModel
                        {
                            ModelName = g.Key,
                            Amount = g.Count()
                        }).ToList();
            return list;
        }

        /* REPORT 3 */

        public decimal CalculateTotalClaimAmount(DateTime startDate, DateTime endDate)
        {
            var amount = context.Claim_Detail
                .Where(c => c.date_accident >= startDate && c.date_accident <= endDate)
                .Sum(c => c.claimable_amount);
            return amount;
        }

        public decimal CalculateTotalClaimAmounAllTimet()
        {
            var amount = context.Claim_Detail
                .Sum(c => c.claimable_amount);
            return amount;
        }

        public List<ClaimableAmountByMonthViewModel> ShowAllClaimableReport()
        {
            var list = context.Claim_Detail
                .Select(c => new ClaimableAmountByMonthViewModel
                {
                    ClaimableAmount = c.claimable_amount,
                    CreateDate = c.Customer_Policy.create_date,
                    CustomerName = c.Customer_Policy.Customer_Info.name,
                    CustomerPolicyId = c.Customer_Policy.id,
                    InsuredAmount = c.insured_amount
                }).ToList();
            return list;
        }

        public List<ClaimableAmountByMonthViewModel> ShowClaimableReportByMonth(DateTime startDate, DateTime endDate)
        {
            var list = context.Claim_Detail
                .Where(c => c.Customer_Policy.create_date >= startDate && c.Customer_Policy.create_date <= endDate)
                .Select(c => new ClaimableAmountByMonthViewModel
                {
                    ClaimableAmount = c.claimable_amount,
                    CreateDate = c.Customer_Policy.create_date,
                    CustomerName = c.Customer_Policy.Customer_Info.name,
                    CustomerPolicyId = c.Customer_Policy.id,
                    InsuredAmount = c.insured_amount
                }).ToList();
            return list;
        }

        public List<PolicyDueViewModel> ShowPolicyDue(DateTime currentDate)
        {
            DateTime oneMonthAgo = currentDate.AddMonths(-1);
            var list = context.Customer_Policy
                .Where(c => c.policy_end_date >= oneMonthAgo && c.policy_end_date <= currentDate)
                .Select(p => new PolicyDueViewModel { 
                    CustomerPolicyId = p.id,
                    EndDate = p.policy_end_date,
                    PolicyType = p.Policy.Policy_Type.name,
                    StartDate = p.policy_start_date
                })
                .ToList();
            return list;
        }

        public List<PolicyDueViewModel> ShowLapsedPolicyDue(bool status)
        {
            var list = context.Customer_Policy
                .Where(c => c.active == status)
                .Select(p => new PolicyDueViewModel
                {
                    CustomerPolicyId = p.id,
                    EndDate = p.policy_end_date,
                    PolicyType = p.Policy.Policy_Type.name,
                    StartDate = p.policy_start_date
                })
                .ToList();
            return list;
        }
    }
}
