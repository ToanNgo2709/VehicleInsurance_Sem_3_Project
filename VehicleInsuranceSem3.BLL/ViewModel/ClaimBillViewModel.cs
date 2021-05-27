using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class ClaimBillViewModel
    {
        public int CustomerPolicyId { get; set; }
        public string CustomerName { get; set; }
        public string PolicyName { get; set; }
        public string VehicleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ClaimName { get; set; }
        public string AccidentPlace { get; set; }
        public DateTime AccidentDate { get; set; }
        public decimal Insured { get; set; }
        public decimal Claim { get; set; }
    }
}
