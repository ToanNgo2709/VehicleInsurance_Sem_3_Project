namespace VehicleInsuranceSem3.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_Info()
        {
            Customer_Info = new HashSet<Customer_Info>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string authorize_token { get; set; }

        public bool? active { get; set; }

        public int user_type_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer_Info> Customer_Info { get; set; }

        public virtual User_Type User_Type { get; set; }
    }
}
