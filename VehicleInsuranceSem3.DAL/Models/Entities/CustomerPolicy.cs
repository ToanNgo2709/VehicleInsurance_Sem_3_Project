using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class CustomerPolicy
    {
        public int Id { get; set; }

        public int CustomerInfoId { get; set; }
        public CustomerInfo CustomeInfo { get; set; }

        public int PolicyId { get; set; }
        public Policy Policy { get; set; }

        public int VehicleId { get; set; }
        public VehicleInfo VehicleInfo { get; set; }

        public DateTime PolicyStartDate { get; set; }
        public DateTime PolicyEndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CustomerAddProve { get; set; }
        public bool Active { get; set; }

        public List<CustomerBillingInfo> CustomerBillingInfos { get; set; }
        public List<CompanyExpense> CompanyExpenses { get; set; }
        public List<ClaimDetail> ClaimDetails { get; set; }
    }
}
