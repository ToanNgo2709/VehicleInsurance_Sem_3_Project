using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class CompanyexpenseViewModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int? expensetypeid { get; set; }
        public string expenseTypeName { get; set; }
        public decimal amount { get; set; }
        public string description { get; set; }
        public int? customerpolicyid { get; set; }

    }
}
