namespace VehicleInsuranceSem3.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_Billing_Info
    {
        public int id { get; set; }

        public int? customer_policy_id { get; set; }

        [Required]
        [StringLength(50)]
        public string customer_add_prove { get; set; }

        [Required]
        [StringLength(50)]
        public string bill_number { get; set; }

        public DateTime create_date { get; set; }

        public decimal amount { get; set; }

        public bool? active { get; set; }

        public virtual Customer_Policy Customer_Policy { get; set; }
    }
}
