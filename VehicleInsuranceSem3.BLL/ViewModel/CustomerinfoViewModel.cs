using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public   class CustomerinfoViewModel
    {
        public int id { get; set; }
        public string name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dob { get; set; }

        public string address { get; set; }
        public string phone { get; set; }
        public string   email { get; set; }
        public bool active { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int user_type_id { get; set; }

    }
}
