using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
  public  class EstimateViewModel
    {
        public int id { get; set; }
        public int customerid { get; set; }
        public string estimatenumber { get; set; }
        public int vehicleid { get; set; }
        public DateTime vehiclewarranty { get; set; }
        public int policyid { get; set; }

    }
}
