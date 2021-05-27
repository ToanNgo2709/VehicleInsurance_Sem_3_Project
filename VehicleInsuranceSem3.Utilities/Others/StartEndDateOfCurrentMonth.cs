using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.Utilities.Others
{
    public static class StartEndDateOfCurrentMonth
    {
        public static DateTime GetStartDateOfMonth(DateTime today)
        {
            return new DateTime(today.Year, today.Month, 1);
        }

        public static DateTime GetEndDateOfMonth(DateTime startDate)
        {
            return startDate.AddMonths(1).AddDays(-1);
        }
    }
}
