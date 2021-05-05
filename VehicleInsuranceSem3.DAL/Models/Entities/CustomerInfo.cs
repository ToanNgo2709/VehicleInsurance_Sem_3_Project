using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class CustomerInfo
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        public int UserInfoId { get; set; }
        public UserInfo UserInfo { get; set; }

        public List<CustomerPolicy> CustomerPolicies { get; set; }
    }
}
