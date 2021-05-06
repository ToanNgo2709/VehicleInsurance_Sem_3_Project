namespace VehicleInsuranceSem3.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Claim_Detail
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string claim_number { get; set; }

        public int? customer_policy_id { get; set; }

        [Required]
        [StringLength(200)]
        public string place_accident { get; set; }

        public DateTime date_accident { get; set; }

        public decimal insured_amount { get; set; }

        public decimal claimable_amount { get; set; }

        public virtual Customer_Policy Customer_Policy { get; set; }
    }
}
