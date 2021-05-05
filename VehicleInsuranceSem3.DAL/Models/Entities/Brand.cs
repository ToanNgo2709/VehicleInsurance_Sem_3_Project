using System.Collections.Generic;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public List<VehicleInfo> VehicleInfos { get; set; }
        public List<ModelInfo> ModelInfos { get; set; }
    }
}