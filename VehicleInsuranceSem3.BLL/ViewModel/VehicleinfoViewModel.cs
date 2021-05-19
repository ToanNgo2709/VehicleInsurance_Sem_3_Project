using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
  public  class VehicleinfoViewModel
    {
        public int id { get; set; }
        public string address { get; set; }
        public string ownername { get; set; }
        public int brandid { get; set; }
        public int modelid { get; set; }
        public string version { get; set; }
        public string framenumber { get; set; }
        public string eginenumber { get; set; }
        public string vehiclenumber { get; set; }
        public int vehiclecondition { get; set; }
        public decimal? ratebycondition { get; set; }
    }
}
