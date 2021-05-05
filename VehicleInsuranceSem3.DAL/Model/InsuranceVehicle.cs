using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace VehicleInsuranceSem3.DAL.Model
{
    public partial class InsuranceVehicle : DbContext
    {
        public InsuranceVehicle()
            : base("name=InsuranceVehicle")
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Claim_detail> Claim_detail { get; set; }
        public virtual DbSet<Company_expense> Company_expense { get; set; }
        public virtual DbSet<Customer_billing_info> Customer_billing_info { get; set; }
        public virtual DbSet<Customer_info> Customer_info { get; set; }
        public virtual DbSet<Customer_policy> Customer_policy { get; set; }
        public virtual DbSet<Estimate> Estimates { get; set; }
        public virtual DbSet<Expense_type> Expense_type { get; set; }
        public virtual DbSet<Google_map> Google_map { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<Policy_type> Policy_type { get; set; }
        public virtual DbSet<User_info> User_info { get; set; }
        public virtual DbSet<User_type> User_type { get; set; }
        public virtual DbSet<Vehicle_info> Vehicle_info { get; set; }

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
                .HasMany(e => e.Vehicle_info)
                .WithRequired(e => e.Brand)
                .HasForeignKey(e => e.brand_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Claim_detail>()
                .Property(e => e.claim_number)
                .IsUnicode(false);

            modelBuilder.Entity<Claim_detail>()
                .Property(e => e.insured_amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Claim_detail>()
                .Property(e => e.claimable_amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Company_expense>()
                .Property(e => e.amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Customer_billing_info>()
                .Property(e => e.customer_add_prove)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_billing_info>()
                .Property(e => e.bill_number)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_billing_info>()
                .Property(e => e.amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Customer_info>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_info>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_info>()
                .HasMany(e => e.Customer_policy)
                .WithOptional(e => e.Customer_info)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<Customer_info>()
                .HasMany(e => e.Estimates)
                .WithRequired(e => e.Customer_info)
                .HasForeignKey(e => e.customer_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer_policy>()
                .Property(e => e.customer_add_prove)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_policy>()
                .HasMany(e => e.Claim_detail)
                .WithOptional(e => e.Customer_policy)
                .HasForeignKey(e => e.customer_policy_id);

            modelBuilder.Entity<Customer_policy>()
                .HasMany(e => e.Company_expense)
                .WithOptional(e => e.Customer_policy)
                .HasForeignKey(e => e.customer_policy_id);

            modelBuilder.Entity<Customer_policy>()
                .HasMany(e => e.Customer_billing_info)
                .WithOptional(e => e.Customer_policy)
                .HasForeignKey(e => e.customer_policy_id);

            modelBuilder.Entity<Estimate>()
                .Property(e => e.estimate_number)
                .IsUnicode(false);

            modelBuilder.Entity<Expense_type>()
                .HasMany(e => e.Company_expense)
                .WithRequired(e => e.Expense_type)
                .HasForeignKey(e => e.expense_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Google_map>()
                .Property(e => e.latitude)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Google_map>()
                .Property(e => e.longitude)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Model>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Model>()
                .Property(e => e.rate)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Vehicle_info)
                .WithRequired(e => e.Model)
                .HasForeignKey(e => e.model_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.policy_number)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .HasMany(e => e.Customer_policy)
                .WithOptional(e => e.Policy)
                .HasForeignKey(e => e.policy_id);

            modelBuilder.Entity<Policy_type>()
                .HasMany(e => e.Policies)
                .WithRequired(e => e.Policy_type)
                .HasForeignKey(e => e.policy_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User_info>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User_info>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User_info>()
                .Property(e => e.authorize_token)
                .IsUnicode(false);

            modelBuilder.Entity<User_info>()
                .HasMany(e => e.Customer_info)
                .WithOptional(e => e.User_info)
                .HasForeignKey(e => e.user_info_id);

            modelBuilder.Entity<User_type>()
                .HasMany(e => e.User_info)
                .WithRequired(e => e.User_type)
                .HasForeignKey(e => e.user_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle_info>()
                .Property(e => e.frame_number)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle_info>()
                .Property(e => e.engine_number)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle_info>()
                .Property(e => e.vehicle_number)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle_info>()
                .HasMany(e => e.Customer_policy)
                .WithOptional(e => e.Vehicle_info)
                .HasForeignKey(e => e.vehicle_id);

            modelBuilder.Entity<Vehicle_info>()
                .HasMany(e => e.Estimates)
                .WithRequired(e => e.Vehicle_info)
                .HasForeignKey(e => e.vehicle_id)
                .WillCascadeOnDelete(false);
        }
    }
}
