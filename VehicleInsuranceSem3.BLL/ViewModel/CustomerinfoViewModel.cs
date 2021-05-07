using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public   class CustomerinfoViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime dob { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string   email { get; set; }
        public bool? active { get; set; }
        public int userinfoid { get; set; }

    }
}
