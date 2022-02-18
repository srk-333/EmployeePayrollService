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