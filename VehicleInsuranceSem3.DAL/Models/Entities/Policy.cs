using System;
using System.Collections.Generic;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class Policy
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime PolicyDate { get; set; }
        public int PolicyDuration { get; set; }
        public bool Active { get; set; }

        public int PolicyTypeId { get; set; }
        public PolicyType PolicyType { get; set; }

        public List<CustomerPolicy> CustomerPolicies { get; set; }
    }
}