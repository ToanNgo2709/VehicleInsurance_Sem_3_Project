using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class ModelInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public bool Active { get; set; }
    }
}
