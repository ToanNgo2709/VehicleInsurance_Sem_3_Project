namespace VehicleInsuranceSem3.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Company_expense
    {
        public int id { get; set; }

        public DateTime date { get; set; }

        public int expense_type_id { get; set; }

        public decimal? amount { get; set; }

        [StringLength(100)]
        public string description { get; set; }

        public int? customer_policy_id { get; set; }

        public virtual Customer_policy Customer_policy { get; set; }

        public virtual Expense_type Expense_type { get; set; }
    }
}
