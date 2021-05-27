using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
   public class CustomerpolicyViewModel
    {
        public int id { get; set; }
        public int customerid { get; set; }
        public int policyid { get; set; }
        public int vehicleid { get; set; }
        public DateTime policystartdate { get; set; }
        public DateTime policyenddate { get; set; }
        public DateTime createdate { get; set; }
        public string customeraddprove { get; set; }
        public bool? active { get; set; }
        public decimal TotalPayment { get; set; }

    }
}
