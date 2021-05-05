using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class CustomerBillingInfo
    {
        public int Id { get; set; }

        public int CustomerPolicyId { get; set; }
        public CustomerPolicy CustomerPolicy { get; set; }

        public string CustomerAddProve { get; set; }
        public string BillNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        public bool Active { get; set; }
    }
}
