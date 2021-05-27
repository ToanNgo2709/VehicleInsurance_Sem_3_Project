using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class CustomerPolicyBillViewModel
    {
        public string BillNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public int CustomerPolicyId { get; set; }
        public string CustomerName { get; set; }
        public string PolicyName { get; set; }
        public string VehicleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
    }
}
