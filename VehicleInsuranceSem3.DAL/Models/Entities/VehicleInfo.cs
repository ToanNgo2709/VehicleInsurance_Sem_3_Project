using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class VehicleInfo
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string OwnerName { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int ModelId { get; set; }
        public ModelInfo Model { get; set; }

        public string Version { get; set; }
        public string FrameNumber { get; set; }
        public string EngineNumber { get; set; }
        public string VehicleNumber { get; set; }

        public List<CustomerPolicy> CustomerPolicies { get; set; }
    }
}
