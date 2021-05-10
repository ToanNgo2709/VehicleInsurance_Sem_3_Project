namespace VehicleInsuranceSem3.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Google_Map
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string city_name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal latitude { get; set; }

        [Column(TypeName = "numeric")]
        public decimal longitude { get; set; }

        [StringLength(100)]
        public string description { get; set; }
    }
}
