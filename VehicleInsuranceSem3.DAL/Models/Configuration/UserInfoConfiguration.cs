using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsuranceSem3.DAL.Models.Entities;

namespace VehicleInsuranceSem3.DAL.Models.Configuration
{
    public class UserInfoConfiguration: EntityTypeConfiguration<UserInfo>
    {
        public UserInfoConfiguration()
        {
            this.ToTable("User_info");

            this.HasKey(u => u.Id);

            this.Property(u => u.Id)
                .HasColumnName("id");

            this.Property(u => u.Username)
                .HasColumnName("username")
                .IsRequired();

            this.Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired();

            this.Property(u => u.AuthorizeToken)
                .HasColumnName("authorize_token")
                .IsRequired();

            this.Property(u => u.Active)
                .HasColumnName("active");

            this.HasRequired<UserType>(u => u.UserType)
                .WithMany(u => u.UserInfos)
                .HasForeignKey(u => u.UserTypeId);

        }
    }
}
