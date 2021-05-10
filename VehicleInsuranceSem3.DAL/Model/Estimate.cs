namespace VehicleInsuranceSem3.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Estimate")]
    public partial class Estimate
    {
        public int id { get; set; }

        public int customer_id { get; set; }

        [Required]
        [StringLength(50)]
        public string estimate_number { get; set; }

        public int vehicle_id { get; set; }

        public DateTime vehicle_warranty { get; set; }

        public int policy_id { get; set; }

        public virtual Customer_Info Customer_Info { get; set; }

        public virtual Vehicle_Info Vehicle_Info { get; set; }
    }
}
