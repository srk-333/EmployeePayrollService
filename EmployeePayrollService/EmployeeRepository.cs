using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollService
{
    class EmployeeRepository
    {
        public static string connectionString = "Data Source=SAURAVSHARMA;Initial Catalog=EmployeePayrollService;Persist Security Info=True;User ID=saurav;Password=Saurav78#$";
        readonly SqlConnection connection = new SqlConnection(connectionString);
        public void GetAllRecords()
        {
            try
            {
                EmployeeModel model = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"SELECT EmployeeId, EmployeeName,PhoneNumber,Address,Department,Gender,BasicPay,Deductions
                                            ,TaxablePay,Tax,NetPay,StartDate,City,Country FROM Employee_Payroll;";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    this.connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.EmployeeId = reader.GetInt32(0);
                            model.EmployeeName = reader.GetString(1);
                            model.PhoneNumber = reader.GetInt64(2);
                            model.Address = reader.GetString(3);
                            model.Department = reader.GetString(4);
                            model.Gender = Convert.ToChar(reader.GetString(5));
                            model.BasicPay = reader.GetDouble(6);
                            model.Deductions = reader.GetDouble(7);
                            model.TaxablePay = reader.GetDouble(8);
                            model.Tax = reader.GetDouble(9);
                            model.NetPay = reader.GetDouble(10);
                            model.StartDate = reader.GetDateTime(11);
                            model.City = reader.GetString(12);
                            model.Country = reader.GetString(13);
                            //Print Record on Console
                            Console.WriteLine("{0},{1},{2},{3},{4},{5}",model.EmployeeId, model.EmployeeName, model.Department, model.BasicPay, model.NetPay, model.Country);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                        Console.WriteLine("No Records in Database");
                    reader.Close();
                    this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}