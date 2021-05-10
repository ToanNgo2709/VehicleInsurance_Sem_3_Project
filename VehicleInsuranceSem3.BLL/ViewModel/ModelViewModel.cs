using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class ModelViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal rate { get; set; }
        public int brandid { get; set; }
        public bool? active { get; set; }
    }
}
