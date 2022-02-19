use EmployeePayrollService;
CREATE OR ALTER procedure spAddEmployeeDetails
(
 @EmployeeName varchar(255),
 @PhoneNumber bigint,
 @Address varchar(255),
 @Department varchar(100),
 @Gender char(1),
 @BasicPay float,
 @Deductions float,
 @TaxablePay float,
 @Tax float,
 @NetPay float,
 @StartDate Date,
 @City varchar(255),
 @Country varchar(100) 
 )
 as
 begin
 insert into Employee_Payroll values(@EmployeeName,@PhoneNumber,@Address,@Department,@Gender,@BasicPay,@Deductions,@TaxablePay,@Tax,@NetPay,@StartDate,@City,@Country)
 end
CREATE OR ALTER procedure spUpdateEmployeeBasicPay
(
 @EmployeeName varchar(255),
 @BasicPay float
 )
 as 
 begin
 update Employee_Payroll set BasicPay = @BasicPay where EmployeeName = @EmployeeName;
 end
CREATE OR ALTER procedure spGetDataByDate
(
	@StartDate date,
	@EndDate date
)
AS
Begin
SELECT * FROM employee_payroll WHERE StartDate BETWEEN CAST(@StartDate AS DATE ) AND @EndDate;
End