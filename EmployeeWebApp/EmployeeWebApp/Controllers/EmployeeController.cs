using EmployeeWebApp.Models;
using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeWebApp.Controllers
{
    [Authorize]
    
    public class EmployeeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            logger.Info("User {0} loaded Employee/Index", User.Identity.Name.ToString());


            return RedirectToAction("Read");
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return RedirectToAction("Register", "Account");
        }

        
        [Authorize]
        public ActionResult Read()
        {

            if(!User.IsInRole("Administrator") && !User.IsInRole("Manager"))
            {
                logger.Info("User {0} was not authorized to view Employee/Read and was redirected to siad users details.", User.Identity.Name.ToString());
                return RedirectToAction("Details");
            }
            logger.Info("User {0} was authorized to view Employee/Read.", User.Identity.Name.ToString());
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Update(int Id)
        {
            logger.Info("Administrator {0} will edit user with Id: {1}", User.Identity.Name.ToString(), Id);
            var employee = db.Employees.FirstOrDefault(e => e.Id == Id);

            return View(employee);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Employee employee)
        {
            logger.Info("Trying to save changes of user with id: {0} to database.", employee.Id.ToString() );
            if (ModelState.IsValid)
            {

                db.Employees.FirstOrDefault(e => e.Id == employee.Id).Income = employee.Income;
                db.SaveChanges();

                logger.Info("User with id: {0} updated.", employee.Id);

                return RedirectToAction("Index");
            }
            logger.Info("Input was not correct. Updating Income and reloading page.", employee.Id.ToString());
            employee.Income = db.Employees.FirstOrDefault(e => e.Id == employee.Id).Income;
            return View(employee);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int ?Id)
        {

            if(Id.HasValue)
            {
                logger.Info("Administrator {0} deleted user with Id: {1}, did not delete associated identity!", User.Identity.Name.ToString(), Id);
                var response = db.Employees.Remove(db.Employees.FirstOrDefault(e => e.Id == Id));

                db.SaveChanges();
            }
            return RedirectToAction("Read");
        }

        [Authorize]
        public ActionResult Details()
        {
            logger.Info("User {0} checking their details.", User.Identity.Name.ToString());
            var userId = User.Identity.GetUserId();

            var employee = db.Employees.FirstOrDefault(e => e.ApplicationUserId == userId);
            logger.Info("Checking if user: {0} has associated employee", User.Identity.Name.ToString());
            if(employee != null)
            {
                EmployeeDetailsModel employeeDetails = new EmployeeDetailsModel()
                {
                    Name = employee.Name,
                    Email = User.Identity.Name,
                    Income = employee.Income,
                    IncomeTax = ((employee.Income * 100) / 76) * 0.15,
                    Healthcare = ((employee.Income * 100) / 76) * 0.06,
                    Social = ((employee.Income * 100) / 76) * 0.03,
                    GrossIncome = Math.Round((employee.Income * 100) / 76, 2),
                    ImagePath = employee.ImagePath
                };

                return View(employeeDetails);
            }
            logger.Warn("User: {0} does not have an associated employee", User.Identity.Name.ToString());
            Response.StatusCode = 404;
            return RedirectToAction("Login", "Account");
        }

        //Loading data for datatables
        [Authorize(Roles = "Manager, Administrator")]
        public ActionResult LoadData()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var employees = db.Employees.ToList();
            List<EmployeeViewModel> employeesView = new List<EmployeeViewModel>();
            employees.ForEach(e => employeesView.Add(new EmployeeViewModel()
            {
                Employee = e,
                GrossIncome = Math.Round((e.Income * 100) / 76, 2)
            }));

            return Json(new { data = employeesView }, JsonRequestBehavior.AllowGet);

        }
    }
}