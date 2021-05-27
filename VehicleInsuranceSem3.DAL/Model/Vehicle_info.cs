namespace VehicleInsuranceSem3.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vehicle_Info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle_Info()
        {
            Customer_Policy = new HashSet<Customer_Policy>();
            Estimates = new HashSet<Estimate>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string address { get; set; }

        [Required]
        [StringLength(50)]
        public string owner_name { get; set; }

        public int brand_id { get; set; }

        public int model_id { get; set; }

        [Required]
        [StringLength(50)]
        public string version { get; set; }

        [Required]
        [StringLength(50)]
        public string frame_number { get; set; }

        [Required]
        [StringLength(50)]
        public string engine_number { get; set; }

        [Required]
        [StringLength(50)]
        public string vehicle_number { get; set; }

        public int vehicle_condition { get; set; }

        public decimal? rate_by_condition { get; set; }

        public virtual Brand Brand { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer_Policy> Customer_Policy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Estimate> Estimates { get; set; }

        public virtual Model Model { get; set; }
    }
}
