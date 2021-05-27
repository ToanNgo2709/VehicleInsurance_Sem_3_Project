using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class ClaimableAmountByMonthViewModel
    {
        public int Id { get; set; }
        public int CustomerPolicyId { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal InsuredAmount { get; set; }
        public decimal ClaimableAmount { get; set; }
    }
}
