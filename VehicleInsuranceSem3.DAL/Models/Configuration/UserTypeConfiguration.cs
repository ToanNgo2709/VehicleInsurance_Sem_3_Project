using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsuranceSem3.DAL.Models.Entities;

namespace VehicleInsuranceSem3.DAL.Models.Configuration
{
    public class UserTypeConfiguration : EntityTypeConfiguration<UserType>
    {
        public UserTypeConfiguration()
        {
            this.ToTable("User_type");
            this.HasKey(u => u.Id);

            this.Property(u => u.Id)
                .HasColumnName("id");

            this.Property(u => u.Name)
                .HasColumnName("name");

            this.Property(u => u.Active)
                .HasColumnName("active");

            
        }
    }
}
