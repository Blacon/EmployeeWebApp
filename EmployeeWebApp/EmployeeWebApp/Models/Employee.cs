using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string LastName { get; set; }


        [Display(Name = "Net income")]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage="Emplyees income cannot be below 0.")]
        [DataType(DataType.Currency, ErrorMessage ="Value {1} is not an number. Please enter a number.")]
        public double Income { get; set; }

        public string Name
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public string ImagePath { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
    }
}