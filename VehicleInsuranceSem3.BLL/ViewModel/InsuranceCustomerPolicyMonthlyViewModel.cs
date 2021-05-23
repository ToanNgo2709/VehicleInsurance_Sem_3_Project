using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class InsuranceCustomerPolicyMonthlyViewModel
    {
        public int orderNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string PolicyName { get; set; }
        public string VehicleName { get; set; }
        public int Duration { get; set; }
        public bool Active { get; set; }
    }
}
