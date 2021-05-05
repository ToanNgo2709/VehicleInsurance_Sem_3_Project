using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class Estimate
    {
        public int Id { get; set; }
        public string EstimateNumber { get; set; }

        public int CustomerId { get; set; }
        public CustomerInfo CustomeInfo { get; set; }

        public int VehicleId { get; set; }
        public VehicleInfo VehicleInfo { get; set; }

        public DateTime VehicleWarranty { get; set; }

        public int PolicyId { get; set; }
        public Policy Policy { get; set; }


    }
}
