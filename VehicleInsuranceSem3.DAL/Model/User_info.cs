namespace VehicleInsuranceSem3.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_info()
        {
            Customer_info = new HashSet<Customer_info>();
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
        public virtual ICollection<Customer_info> Customer_info { get; set; }

        public virtual User_type User_type { get; set; }
    }
}
