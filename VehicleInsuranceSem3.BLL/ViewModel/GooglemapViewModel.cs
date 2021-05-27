using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class GooglemapViewModel
    {
        public int id { get; set; }
        public string cityname { get; set; }
        public decimal latitude { get; set; }
        public decimal longtitude { get; set; }
        public string description { get; set; }
    }
}
