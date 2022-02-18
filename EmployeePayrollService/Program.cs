using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollService
{
    public class Program
    {
        //Method to Invoke EmployeeRepo Class.
        public static void InvokeEmployeeRepo()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();    
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeId = 4;
            employeeModel.EmployeeName = "Vikash";
            employeeModel.PhoneNumber = 89675423;
            employeeModel.Address = "Buxar";
            employeeModel.Department = "Testing";
            employeeModel.Gender = 'M';
            employeeModel.BasicPay = 49999;
            employeeModel.Deductions = 1000;
            employeeModel.TaxablePay = 47000;
            employeeModel.Tax = 5000;
            employeeModel.StartDate = DateTime.Now;
            employeeModel.City = "Patna";
            employeeModel.Country = "India";
            employeeRepository.AddEmployee(employeeModel);
            employeeRepository.GetAllRecords();
        }
        //Entry Point
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll Service");
            InvokeEmployeeRepo();
            Console.ReadKey();
        }
    }
}