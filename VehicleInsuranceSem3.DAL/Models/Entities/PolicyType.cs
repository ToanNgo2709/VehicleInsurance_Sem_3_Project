using System.Collections.Generic;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class PolicyType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public List<Policy> Policies { get; set; }
    }
}