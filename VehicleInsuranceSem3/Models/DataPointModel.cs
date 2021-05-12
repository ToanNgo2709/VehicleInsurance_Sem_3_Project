using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace VehicleInsuranceSem3.Models
{
    [DataContract]
    public class DataPointModel
    {
        public DataPointModel(string x, double y)
        {
            this.X = x;
            this.Y = y;
               
        }

        [DataMember(Name = "X")]
        public string X = "";

        [DataMember(Name = "Y")]
        public Nullable<double> Y = null;

    }
}