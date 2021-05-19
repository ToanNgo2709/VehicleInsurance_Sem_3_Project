using System;
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

        public int CountCustomerPolicySellByMonth(int month)
        {
            int count = context.Customer_Policy.Where(c => c.create_date.Month == month).Count();
            return count;
        }

        public IQueryable<PolicyTypeCountModel> CountPolicyTypeSellByMonth(int month)
        {

            var obj = from pt in context.Policy_Type
                      join p in context.Policies on pt.id equals p.policy_type_id
                      join c in context.Customer_Policy on p.id equals c.policy_id
                      where c.create_date.Month == month
                      group pt by pt.name into g
                      select new PolicyTypeCountModel
                      {
                          Name = g.Key,
                          Number = g.Count()
                      };
            return obj;
            
            

        }

    }
}
