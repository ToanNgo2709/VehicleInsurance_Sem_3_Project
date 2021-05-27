using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class PolicytypeViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool? active { get; set; }
        public decimal? price { get; set; }
        public decimal? libilityLevel { get; set; }

    }
}
