using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class CompanyExpense
    {
        public int Id { get; set; }
        public DateTime DateInfo { get; set; }

        public int ExpenseTypeId { get; set; }
        public ExpenseType ExpenseType { get; set; }

        public decimal Amount { get; set; }

        public int CustomerPolicyId { get; set; }
        public CustomerPolicy CustomerPolicy { get; set; }

        public string Description { get; set; }


    }
}
