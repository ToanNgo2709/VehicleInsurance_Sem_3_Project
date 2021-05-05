namespace VehicleInsuranceSem3.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Policy")]
    public partial class Policy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Policy()
        {
            Customer_policy = new HashSet<Customer_policy>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string policy_number { get; set; }

        public DateTime policy_date { get; set; }

        public int policy_duration { get; set; }

        public bool? active { get; set; }

        public int policy_type_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer_policy> Customer_policy { get; set; }

        public virtual Policy_type Policy_type { get; set; }
    }
}
