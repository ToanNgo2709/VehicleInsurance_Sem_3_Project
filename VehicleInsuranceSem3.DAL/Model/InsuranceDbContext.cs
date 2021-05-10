using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace VehicleInsuranceSem3.DAL.Model
{
    public partial class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext()
            : base("data source=.;initial catalog=Vehicle_Insurance_Sem_3;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Claim_Detail> Claim_Detail { get; set; }
        public virtual DbSet<Company_Expense> Company_Expense { get; set; }
        public virtual DbSet<Customer_Billing_Info> Customer_Billing_Info { get; set; }
        public virtual DbSet<Customer_Info> Customer_Info { get; set; }
        public virtual DbSet<Customer_Policy> Customer_Policy { get; set; }
        public virtual DbSet<Estimate> Estimates { get; set; }
        public virtual DbSet<Expense_Type> Expense_Type { get; set; }
        public virtual DbSet<Google_Map> Google_Map { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<Policy_Type> Policy_Type { get; set; }
        public virtual DbSet<User_Type> User_Type { get; set; }
        public virtual DbSet<Vehicle_Info> Vehicle_Info { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Models)
                .WithRequired(e => e.Brand)
                .HasForeignKey(e => e.brand_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Vehicle_Info)
                .WithRequired(e => e.Brand)
                .HasForeignKey(e => e.brand_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Claim_Detail>()
                .Property(e => e.claim_number)
                .IsUnicode(false);

            modelBuilder.Entity<Claim_Detail>()
                .Property(e => e.insured_amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Claim_Detail>()
                .Property(e => e.claimable_amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Company_Expense>()
                .Property(e => e.amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Customer_Billing_Info>()
                .Property(e => e.customer_add_prove)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Billing_Info>()
                .Property(e => e.bill_number)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Billing_Info>()
                .Property(e => e.amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Customer_Info>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Info>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Info>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Info>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Info>()
                .HasMany(e => e.Customer_Policy)
                .WithOptional(e => e.Customer_Info)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<Customer_Info>()
                .HasMany(e => e.Estimates)
                .WithRequired(e => e.Customer_Info)
                .HasForeignKey(e => e.customer_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer_Policy>()
                .Property(e => e.customer_add_prove)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Policy>()
                .Property(e => e.total_payment)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Customer_Policy>()
                .HasMany(e => e.Claim_Detail)
                .WithOptional(e => e.Customer_Policy)
                .HasForeignKey(e => e.customer_policy_id);

            modelBuilder.Entity<Customer_Policy>()
                .HasMany(e => e.Company_Expense)
                .WithOptional(e => e.Customer_Policy)
                .HasForeignKey(e => e.customer_policy_id);

            modelBuilder.Entity<Customer_Policy>()
                .HasMany(e => e.Customer_Billing_Info)
                .WithOptional(e => e.Customer_Policy)
                .HasForeignKey(e => e.customer_policy_id);

            modelBuilder.Entity<Estimate>()
                .Property(e => e.estimate_number)
                .IsUnicode(false);

            modelBuilder.Entity<Expense_Type>()
                .HasMany(e => e.Company_Expense)
                .WithRequired(e => e.Expense_Type)
                .HasForeignKey(e => e.expense_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Google_Map>()
                .Property(e => e.latitude)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Google_Map>()
                .Property(e => e.longitude)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Model>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Model>()
                .Property(e => e.highest_rate)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Vehicle_Info)
                .WithRequired(e => e.Model)
                .HasForeignKey(e => e.model_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.policy_number)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .HasMany(e => e.Customer_Policy)
                .WithOptional(e => e.Policy)
                .HasForeignKey(e => e.policy_id);

            modelBuilder.Entity<Policy_Type>()
                .Property(e => e.liability_level)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Policy_Type>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Policy_Type>()
                .HasMany(e => e.Policies)
                .WithRequired(e => e.Policy_Type)
                .HasForeignKey(e => e.policy_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User_Type>()
                .HasMany(e => e.Customer_Info)
                .WithRequired(e => e.User_Type)
                .HasForeignKey(e => e.user_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle_Info>()
                .Property(e => e.frame_number)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle_Info>()
                .Property(e => e.engine_number)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle_Info>()
                .Property(e => e.vehicle_number)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle_Info>()
                .Property(e => e.rate_by_condition)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Vehicle_Info>()
                .HasMany(e => e.Customer_Policy)
                .WithOptional(e => e.Vehicle_Info)
                .HasForeignKey(e => e.vehicle_id);

            modelBuilder.Entity<Vehicle_Info>()
                .HasMany(e => e.Estimates)
                .WithRequired(e => e.Vehicle_Info)
                .HasForeignKey(e => e.vehicle_id)
                .WillCascadeOnDelete(false);
        }
    }
}
