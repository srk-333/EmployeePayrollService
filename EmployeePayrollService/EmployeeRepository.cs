using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollService
{
    class EmployeeRepository
    {
        //SqlServer Connection String
        public static string connectionString = "Data Source=SAURAVSHARMA;Initial Catalog=EmployeePayrollService;Persist Security Info=True;User ID=saurav;Password=Saurav78#$";
        readonly SqlConnection connection = new SqlConnection(connectionString);
        //Method to Get All Records from DB.
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
                    //Open Connection of Database
                    this.connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //Read Records from DB Rows Wise.
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
                    //Close Connection of database
                    this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //Close Connection of database
                this.connection.Close();
            }
        }
        //Method to add a employee detail in database using stored Procedure
        public void AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spAddEmployeeDetails", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", model.StartDate);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@Country", model.Country);
                    //Open Connection of Database
                    this.connection.Open();
                    //Executes Sql statement and return no of rows affected.
                    var rows = command.ExecuteNonQuery();
                    //Close Connection of database
                    this.connection.Close();
                    if (rows != 0)
                        Console.WriteLine("Inserted in DataBase");
                    else
                        Console.WriteLine(rows);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Close Connection of database
                this.connection.Close();
            }
        }
        //Method to Update BasePay for a Employee
        public void UpdateBasicPay(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spUpdateEmployeeBasicPay", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    //Open Connection of Database
                    this.connection.Open();
                    //Executes Sql statement to Update in Db.
                    var rows = command.ExecuteNonQuery();
                    //Close Connection of database
                    this.connection.Close();
                    if (rows != 0)
                        Console.WriteLine("Updated in Db");
                    else
                        Console.WriteLine(rows);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        //Method to Update BasePay for a Employee using Prepared Statement.
        public void UpdateBasicPayByPreparedStatement(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("update Employee_Payroll set BasicPay = @BasicPay where EmployeeName = @EmployeeName;", this.connection);
                    command.Prepare();
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    //Open Connection of Database
                    this.connection.Open();
                    //Executes Sql statement to Update in Db.
                    var rows = command.ExecuteNonQuery();
                    //Close Connection of database
                    this.connection.Close();
                    if (rows != 0)
                        Console.WriteLine("Updated in Db");
                    else
                        Console.WriteLine(rows);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        /// <summary>
        /// get employee details between start and end date
        /// </summary>
        public void GetEmployeeDetailsByDate()
        {
            EmployeeModel employee = new EmployeeModel();
            DateTime startDate = new DateTime(2010, 07, 06);
            DateTime endDate = new DateTime(2022, 06, 05);
            try
            {
                this.connection.Open();
                SqlCommand sqlCommand = new SqlCommand("spGetDataByDate", this.connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee.EmployeeId = reader.GetInt32(0);
                        employee.EmployeeName = reader.GetString(1);
                        employee.PhoneNumber = reader.GetInt64(2);
                        employee.Address = reader.GetString(3);
                        employee.Department = reader.GetString(4);
                        employee.Gender = Convert.ToChar(reader.GetString(5));
                        employee.BasicPay = reader.GetDouble(6);
                        employee.Deductions = reader.GetDouble(7);
                        employee.TaxablePay = reader.GetDouble(8);
                        employee.Tax = reader.GetDouble(9);
                        employee.NetPay = reader.GetDouble(10);
                        employee.StartDate = reader.GetDateTime(11);
                        employee.City = reader.GetString(12);
                        employee.Country = reader.GetString(13);
                        //// display the payroll data from database
                        Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}", employee.EmployeeId, employee.EmployeeName,
                            employee.Gender, employee.PhoneNumber, employee.Address, employee.Department, employee.BasicPay, employee.Deductions,
                            employee.TaxablePay, employee.Tax, employee.NetPay, employee.StartDate, employee.City, employee.Country);
                        Console.WriteLine("\n");
                    }
                }
                else
                {
                    Console.WriteLine("No record found");
                }
                reader.Close();
                this.connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        //Method to find sum,avg,Min,Max and count from a DB.
        public void DatabaseFunction()
        {
            try
            {
                DBFunctionsOperation df = new DBFunctionsOperation();
                using (this.connection)
                {
                    string queryDb = @"SELECT gender,COUNT(BasicPay) AS TotalCount,
                                   SUM(BasicPay) AS TotalSum, 
                                   AVG(BasicPay) AS AverageValue, 
                                   MIN(BasicPay) AS MinValue, 
                                   MAX(BasicPay) AS MaxValue
                                   FROM Employee_Payroll GROUP BY Gender;";
                    //define SqlCommand Object
                    SqlCommand cmd = new SqlCommand(queryDb, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            df.Gender = Convert.ToString(dr["Gender"]);
                            df.Count = Convert.ToInt32(dr["TotalCount"]);
                            df.TotalSum = Convert.ToDouble(dr["TotalSum"]);
                            df.Avg = Convert.ToDouble(dr["AverageValue"]);
                            df.Min = Convert.ToDouble(dr["MinValue"]);
                            df.Max = Convert.ToDouble(dr["MaxValue"]);
                            Console.WriteLine("Gender: {0}, TotalCount: {1}, TotalSalary: {2}, AvgSalary:  {3}, MinSalary:  {4}, MinSalary:  {5}",
                                                df.Gender, df.Count, df.TotalSum, df.Avg, df.Min, df.Max);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Rows doesn't exist!");
                    }
                    dr.Close();
                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);               
            }
            finally
            {
                this.connection.Close();
            }
            Console.WriteLine();
        }
    }
}