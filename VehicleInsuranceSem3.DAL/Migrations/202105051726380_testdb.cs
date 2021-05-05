namespace VehicleInsuranceSem3.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Model",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BrandId = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Brand", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Vehicle_info",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        address = c.String(nullable: false, maxLength: 50),
                        owner_name = c.String(nullable: false, maxLength: 50),
                        BrandId = c.Int(nullable: false),
                        ModelInfoId = c.Int(nullable: false),
                        version = c.String(nullable: false, maxLength: 50),
                        frame_number = c.String(nullable: false, maxLength: 50),
                        engine_number = c.String(nullable: false, maxLength: 50),
                        vehicle_number = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Brand", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Model", t => t.ModelInfoId, cascadeDelete: true)
                .Index(t => t.BrandId)
                .Index(t => t.ModelInfoId);
            
            CreateTable(
                "dbo.Customer_policy",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CustomerInfoId = c.Int(nullable: false),
                        PolicyId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        policy_start_date = c.DateTime(nullable: false),
                        policy_end_date = c.DateTime(nullable: false),
                        create_date = c.DateTime(nullable: false),
                        customer_add_prove = c.String(nullable: false, maxLength: 50),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customer_info", t => t.CustomerInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Policy", t => t.PolicyId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicle_info", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.CustomerInfoId)
                .Index(t => t.PolicyId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.Claim_detail",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        claim_number = c.String(nullable: false, maxLength: 50),
                        CustomerPolicyId = c.Int(nullable: false),
                        place_accident = c.String(nullable: false, maxLength: 200),
                        date_accident = c.DateTime(nullable: false),
                        insured_amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        claimable_amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customer_policy", t => t.CustomerPolicyId, cascadeDelete: true)
                .Index(t => t.CustomerPolicyId);
            
            CreateTable(
                "dbo.Company_expense",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        ExpenseTypeId = c.Int(nullable: false),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerPolicyId = c.Int(nullable: false),
                        description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customer_policy", t => t.CustomerPolicyId, cascadeDelete: true)
                .ForeignKey("dbo.Expense_type", t => t.ExpenseTypeId, cascadeDelete: true)
                .Index(t => t.ExpenseTypeId)
                .Index(t => t.CustomerPolicyId);
            
            CreateTable(
                "dbo.Expense_type",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Customer_info",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        dob = c.DateTime(nullable: false),
                        address = c.String(nullable: false),
                        phone = c.String(nullable: false, maxLength: 10),
                        email = c.String(nullable: false, maxLength: 50),
                        active = c.Boolean(nullable: false),
                        UserInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Estimate",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        estimate_number = c.String(nullable: false, maxLength: 50),
                        CustomerId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        vehicle_warranty = c.DateTime(nullable: false),
                        PolicyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customer_info", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Policy", t => t.PolicyId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicle_info", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.VehicleId)
                .Index(t => t.PolicyId);
            
            CreateTable(
                "dbo.Policy",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        policy_number = c.String(nullable: false, maxLength: 50),
                        policy_date = c.DateTime(nullable: false),
                        policy_duration = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                        PolicyTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Policy_type", t => t.PolicyTypeId, cascadeDelete: true)
                .Index(t => t.PolicyTypeId);
            
            CreateTable(
                "dbo.Policy_type",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.User_info",
                c => new
                    {
                        id = c.Int(nullable: false),
                        username = c.String(nullable: false),
                        password = c.String(nullable: false),
                        authorize_token = c.String(nullable: false),
                        active = c.Boolean(nullable: false),
                        UserTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.User_type", t => t.UserTypeId)
                .ForeignKey("dbo.Customer_info", t => t.id)
                .Index(t => t.id)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.User_type",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Customer_billing_info",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerPolicyId = c.Int(nullable: false),
                        customer_add_prove = c.String(nullable: false, maxLength: 50),
                        bill_number = c.String(nullable: false, maxLength: 50),
                        create_date = c.DateTime(nullable: false),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer_policy", t => t.CustomerPolicyId, cascadeDelete: true)
                .Index(t => t.CustomerPolicyId);
            
            CreateTable(
                "dbo.Google_map",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        city_name = c.String(maxLength: 50),
                        latitude = c.String(),
                        longitude = c.String(),
                        description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicle_info", "ModelInfoId", "dbo.Model");
            DropForeignKey("dbo.Customer_policy", "VehicleId", "dbo.Vehicle_info");
            DropForeignKey("dbo.Customer_policy", "PolicyId", "dbo.Policy");
            DropForeignKey("dbo.Customer_billing_info", "CustomerPolicyId", "dbo.Customer_policy");
            DropForeignKey("dbo.Customer_policy", "CustomerInfoId", "dbo.Customer_info");
            DropForeignKey("dbo.User_info", "id", "dbo.Customer_info");
            DropForeignKey("dbo.User_info", "UserTypeId", "dbo.User_type");
            DropForeignKey("dbo.Estimate", "VehicleId", "dbo.Vehicle_info");
            DropForeignKey("dbo.Estimate", "PolicyId", "dbo.Policy");
            DropForeignKey("dbo.Policy", "PolicyTypeId", "dbo.Policy_type");
            DropForeignKey("dbo.Estimate", "CustomerId", "dbo.Customer_info");
            DropForeignKey("dbo.Company_expense", "ExpenseTypeId", "dbo.Expense_type");
            DropForeignKey("dbo.Company_expense", "CustomerPolicyId", "dbo.Customer_policy");
            DropForeignKey("dbo.Claim_detail", "CustomerPolicyId", "dbo.Customer_policy");
            DropForeignKey("dbo.Vehicle_info", "BrandId", "dbo.Brand");
            DropForeignKey("dbo.Model", "BrandId", "dbo.Brand");
            DropIndex("dbo.Customer_billing_info", new[] { "CustomerPolicyId" });
            DropIndex("dbo.User_info", new[] { "UserTypeId" });
            DropIndex("dbo.User_info", new[] { "id" });
            DropIndex("dbo.Policy", new[] { "PolicyTypeId" });
            DropIndex("dbo.Estimate", new[] { "PolicyId" });
            DropIndex("dbo.Estimate", new[] { "VehicleId" });
            DropIndex("dbo.Estimate", new[] { "CustomerId" });
            DropIndex("dbo.Company_expense", new[] { "CustomerPolicyId" });
            DropIndex("dbo.Company_expense", new[] { "ExpenseTypeId" });
            DropIndex("dbo.Claim_detail", new[] { "CustomerPolicyId" });
            DropIndex("dbo.Customer_policy", new[] { "VehicleId" });
            DropIndex("dbo.Customer_policy", new[] { "PolicyId" });
            DropIndex("dbo.Customer_policy", new[] { "CustomerInfoId" });
            DropIndex("dbo.Vehicle_info", new[] { "ModelInfoId" });
            DropIndex("dbo.Vehicle_info", new[] { "BrandId" });
            DropIndex("dbo.Model", new[] { "BrandId" });
            DropTable("dbo.Google_map");
            DropTable("dbo.Customer_billing_info");
            DropTable("dbo.User_type");
            DropTable("dbo.User_info");
            DropTable("dbo.Policy_type");
            DropTable("dbo.Policy");
            DropTable("dbo.Estimate");
            DropTable("dbo.Customer_info");
            DropTable("dbo.Expense_type");
            DropTable("dbo.Company_expense");
            DropTable("dbo.Claim_detail");
            DropTable("dbo.Customer_policy");
            DropTable("dbo.Vehicle_info");
            DropTable("dbo.Model");
            DropTable("dbo.Brand");
        }
    }
}
