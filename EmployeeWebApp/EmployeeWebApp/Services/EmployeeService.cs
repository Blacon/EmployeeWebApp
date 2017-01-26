using EmployeeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Services
{
    public class EmployeeService
    {
        private ApplicationDbContext db;

        public EmployeeService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateEmployee(string firstName, string lastName, string userId, string imagePath)
        {
            var employee = new Employee { FirstName = firstName, LastName = lastName, Income = 0, ApplicationUserId = userId, ImagePath = imagePath };
            db.Employees.Add(employee);
            db.SaveChanges();
        }
    }
}