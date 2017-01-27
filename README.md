# EmployeeWebApp
EmployeeWebApllication. More about every task:

 - CRUD was used for model Employee. While employee controller has  Read, Update and Delete methods, 
for Create method additional EmployeeService was created since im creating Employees when someone registers.
Also Delete method only deletes Employee, but not the related User, this creates some holes.

 - For list filtering and paging im using jQuery datatables. List can only be viewed by admin and manager at /Employee/Read

 - When Employee is created his income is set to 0. Only admin can change this. Admin and manager can see both Income and Gross Income in /Employee/Read
Any simple user trying to access /Employee/Read will be redirected to /Employee/Details where the user can view his own salary.

 - Admin can edit employees income. Typing in a value and then clicking away will show how tax adds up. 
In Employee/Read and Employee/Details required values are calculated in controller and then passed to the view.

 - Database use only one additional model Employee who has first name, last name, income, image path and user id.

Additional tasks:

 - Web app is installed in AppHarbor and can be accessed by link http://employeeweb.apphb.com/
 
 - Any user while registering can upload a photo which then can be seen at Employee/Details. This work only locally.

 - For logging im using Nlog. Logging details can be seen in Logs->Logs.log file.

 - Login using facebook and twitter can not be done locally.
