using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
   public class PolicyViewModel
    {
        public int id { get; set; }
        public string policynumber { get; set; }
        public DateTime policydate { get; set; }
        public int policyduration { get; set; }
        public bool? active { get; set; }
        public int policytypeid { get; set; }
    }
}
