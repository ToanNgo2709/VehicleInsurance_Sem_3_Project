using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
   public class CustomerbillinginfoViewModel
    {
        public int id { get; set; }
        public int customerpolicyid { get; set; }
        public string customeraddprove { get; set; }
        public string bill_number { get; set; }
        public DateTime createdate { get; set; }
        public decimal amount { get; set; }
        public bool? active { get; set; }
    }
}
