using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.BLL.ViewModel
{
    public class InsuranceMonthlyPageListViewModel
    {
        public PagedList<InsuranceCustomerPolicyMonthlyViewModel> insurance { get; set; }
        public PagedList<CustomerPurchaseAmountViewModel> customerPurchase { get; set; }
    }
}
