using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
   public class ClaimDetailViewModel
    {
        public int id { get; set; }
        public string claimnumber { get; set; }
        public int? customerpolicyid { get; set; }
        public string placeaccident { get; set; }
        public DateTime dateaccident { get; set; }
        public decimal insuredamount { get; set; }
        public decimal claimableamount { get; set; }
    }
}
