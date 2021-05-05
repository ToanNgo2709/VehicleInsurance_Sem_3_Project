using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class ClaimDetail
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; }

        public int CustomerPolicyId { get; set; }
        public CustomerPolicy CustomerPolicy { get; set; }

        public string PlaceAccident { get; set; }
        public DateTime DateAccident { get; set; }
        public decimal InsuredAmount { get; set; }
        public decimal ClaimableAmount { get; set; }

    }
}
