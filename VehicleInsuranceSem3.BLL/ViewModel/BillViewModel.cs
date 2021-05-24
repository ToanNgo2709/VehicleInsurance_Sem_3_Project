using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class BillViewModel
    {
        public string BillNumber { get; set; }
        public string CustomerName { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPayment { get; set; }
        public DateTime BillCreteTime { get; set;  }

    }
}
