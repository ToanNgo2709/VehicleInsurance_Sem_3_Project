namespace VehicleInsuranceSem3.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_policy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer_policy()
        {
            Claim_detail = new HashSet<Claim_detail>();
            Company_expense = new HashSet<Company_expense>();
            Customer_billing_info = new HashSet<Customer_billing_info>();
        }

        public int id { get; set; }

        public int? customer_id { get; set; }

        public int? policy_id { get; set; }

        public int? vehicle_id { get; set; }

        public DateTime policy_start_date { get; set; }

        public DateTime policy_end_date { get; set; }

        public DateTime create_date { get; set; }

        [Required]
        [StringLength(50)]
        public string customer_add_prove { get; set; }

        public bool? active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Claim_detail> Claim_detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Company_expense> Company_expense { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer_billing_info> Customer_billing_info { get; set; }

        public virtual Customer_info Customer_info { get; set; }

        public virtual Policy Policy { get; set; }

        public virtual Vehicle_info Vehicle_info { get; set; }
    }
}
