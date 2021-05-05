using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsuranceSem3.DAL.Models.Entities;

namespace VehicleInsuranceSem3.DAL.Models.Configuration
{
    public class CustomerInfoConfiguration : EntityTypeConfiguration<CustomerInfo>
    {
        public CustomerInfoConfiguration()
        {
            this.ToTable("Customer_info");

            this.HasKey(c => c.Id);

            
        }
        
    }
}
