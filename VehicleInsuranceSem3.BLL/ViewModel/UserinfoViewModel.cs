using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class UserinfoViewModel
    {
        public int id { get; set; }
        public string   username { get; set; }
        public string password { get; set; }
        public string authorizetoken { get; set; }
        public bool? active { get; set; }
        public int usertypeid { get; set; }
    }
}
