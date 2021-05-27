using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.DAL.Model;

namespace VehicleInsuranceSem3.Models
{
    public class CheckoutInfo
    {
        public Vehicle_Info Vehicle { get; set; }
        public Customer_Policy CustomerPolicy { get; set; }
    }
}