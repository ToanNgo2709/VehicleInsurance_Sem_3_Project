using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class CopyCustomerPolicyViewModel
    {
        public string CustomerName { get; set; }
        public string PolicyName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string OwnerName { get; set; }
        public string Address { get; set; }
        public string Version { get; set; }
        public int Condition { get; set; }
        public string FrameNumber { get; set; }
        public string EngineNumber { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotayPayment { get; set; }
    }
}
