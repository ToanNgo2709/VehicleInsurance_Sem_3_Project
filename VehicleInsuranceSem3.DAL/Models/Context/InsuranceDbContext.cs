using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsuranceSem3.DAL.Models.Configuration;
using VehicleInsuranceSem3.DAL.Models.Entities;

namespace VehicleInsuranceSem3.DAL.Models.Context
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext() : base("InsuranceDBConnectionString")
        {
            //Database.SetInitializer<InsuranceDbContext>(new CreateDatabaseIfNotExists<InsuranceDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserInfoConfiguration());
            modelBuilder.Configurations.Add(new UserTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ClaimDetail> ClaimDetails { get; set; }
        public DbSet<CompanyExpense> CompanyExpenses { get; set; }
        public DbSet<CustomerBillingInfo> CustomerBillingInfos { get; set; }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }
        public DbSet<CustomerPolicy> CustomerPolicies { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<GoogleMap> GoogleMaps { get; set; }
        public DbSet<ModelInfo> ModelInfos { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<PolicyType> PolicyTypes { get; set; }
        public DbSet<VehicleInfo> VehicleInfos { get; set; }


    }
}
