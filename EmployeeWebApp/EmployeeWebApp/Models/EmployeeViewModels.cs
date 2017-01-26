using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Models
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }

        [Display(Name = "Gross income")]
        [DataType(DataType.Currency)]
        public double GrossIncome { get; set; }
    }

    public class EmployeeDetailsModel
    {
        public string Name { get; set; }

        [Display(Name = "Your Email: ")]
        public string Email { get; set; }

        [Display(Name = "Your Net income: ")]
        [DataType(DataType.Currency)]
        public double Income { get; set; }

        [Display(Name = "Income Tax(15%): ")]
        [DataType(DataType.Currency)]
        public double IncomeTax { get; set; }

        [Display(Name = "Healthcare (6%): ")]
        [DataType(DataType.Currency)]
        public double Healthcare { get; set; }

        [Display(Name = "Social, pension(3%): ")]
        [DataType(DataType.Currency)]
        public double Social { get; set; }

        [Display(Name = "Your Gross income: ")]
        [DataType(DataType.Currency)]
        public double GrossIncome { get; set; }

        public string ImagePath { get; set; }
    }
}