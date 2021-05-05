using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.DAL.Models.Entities
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AuthorizeToken { get; set; }
        public bool Active { get; set; }

        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }

        public virtual CustomerInfo CustomeInfo { get; set; }
    }
}
