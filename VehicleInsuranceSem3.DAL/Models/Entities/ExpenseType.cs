﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public List<CompanyExpense> CompanyExpenses { get; set; }
    }
}
