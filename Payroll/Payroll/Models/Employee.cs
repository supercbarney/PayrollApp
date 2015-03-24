using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class Employee : Person
    {
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal GrossAnnualPay { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal NetAnnualPay { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal GrossBiWeeklyPay { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal NetBiWeeklyPay { get; set; }

        public List<Dependant> Dependants { get; set; }
    }
}