namespace VehicleInsuranceSem3.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_Policy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer_Policy()
        {
            Claim_Detail = new HashSet<Claim_Detail>();
            Company_Expense = new HashSet<Company_Expense>();
            Customer_Billing_Info = new HashSet<Customer_Billing_Info>();
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

        public decimal total_payment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Claim_Detail> Claim_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Company_Expense> Company_Expense { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer_Billing_Info> Customer_Billing_Info { get; set; }

        public virtual Customer_Info Customer_Info { get; set; }

        public virtual Policy Policy { get; set; }

        public virtual Vehicle_Info Vehicle_Info { get; set; }
    }
}
