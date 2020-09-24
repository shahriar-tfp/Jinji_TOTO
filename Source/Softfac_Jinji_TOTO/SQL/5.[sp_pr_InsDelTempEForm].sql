Drop Proc [dbo].[sp_pr_InsDelTempEForm]  
GO
/*  
Alter By : SY Tey  
Alter Date : 28/02/2017  
Version : 6.0  
Problem : New EA 2016.
*/
/*
Alter BY : SY Tey
Alter Date : 26/12/2012
Version : 1.0
Problem : Use for Division statutory.
*/
/*
Alter BY : SY Tey
Alter Date : 19/01/2010
Version : 1.0
Problem : New E Form, filter by company.
*/
/*
Alter BY : SY Tey
Alter Date : 19/01/2010
Version : 1.0
Problem : New E Form
*/    
Create Proc [dbo].[sp_pr_InsDelTempEForm]  
@Company_Profile_Code    nvarchar(50),      
@datesubmit nvarchar(50),      
@Year      int,      
@PIC     nvarchar(50),      
@SPID   int,      
@Action     nvarchar(50),
@OCP_ID nvarchar(50)= ''    
      
As      
      
if @OCP_ID is null
	select @OCP_ID = '' 
	  
declare @Employee_Profile_ID       nvarchar(50),      
 @Empname      nvarchar(80),      
 @Join_Date     nvarchar(14),      
 @Resign_Date   Nvarchar(14),      
 @PCBCredit    decimal(15,2),      
 @CP38Credit   decimal(15,2),      
 @Serial_No     nvarchar(50),      
 @TotalPCB     decimal(15,2),      
 @TotalSalary  decimal(15,2),      
 @TotalDeduct  decimal(15,2),      
 @OldICNo      nvarchar(30),      
 @NewICNo      nvarchar(30),       
 @PassportNo   nvarchar(50),      
 @PCBNo       nvarchar(30),      
 @PICName      nvarchar(80),      
 @PICJobTitle  nvarchar(50),      
 @Serial_No_2    nvarchar(50),      
 @Count       int,      
 @Count_D      int,      
        @noofChild    int,      
        @OtherCount   int,      
 @OtherSAlary  decimal(15,2)      
      
  Set Nocount on      
      
  Select @Count_D = 1      
      
  Select @PICName = Employee_Profile_Vw."Name"      
     From Employee_Profile_Vw      
     Where ID = @PIC      
      
  Select @PICJobTitle = Organisation_Code_Profile_Vw."Name"      
     From Employee_Status,Organisation_Code_Profile_Vw      
     Where Employee_Status.Company_Profile_Code = @Company_Profile_Code       
     And Organisation_Code_Profile_Vw.Company_Profile_Code = @Company_Profile_Code      
     And Employee_Profile_ID = @PIC      
     And Organisation_Code_Profile_Vw.ID = Employee_Status.OCP_ID_Job_Title    
  And Organisation_Code_Profile_Vw.Option_Type = 'JOB_TITLE'      
      
  Select @PICName = @PIC Where @PICName Is Null      
  Select @PICJobTitle = '' Where @PICJobTitle Is Null      
    
      
IF @Action = 'ADD'      
BEGIN      
  DECLARE PCBPointer CURSOR       
  FOR       
  Select Employee_Resign.Employee_Profile_ID       
    From Employee_Resign,Employee_Status,Employee_Profile_Vw      
    Where Employee_Profile_Vw.Company_Profile_Code = @Company_Profile_Code      
    And Employee_Status.Employee_Profile_ID = Employee_Resign.Employee_Profile_ID      
    And Employee_Status.Employee_Profile_ID = Employee_Profile_Vw.ID      
    And Employee_Status.Employee_Profile_ID in (Select Employee_Profile_ID 
    from Employee_Payroll_Header where year = @year
    and (ocp_id_division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null))      
    Order by Employee_Profile_Vw."Name"      
      
  OPEN PCBPointer      
  FETCH NEXT FROM PCBPointer INTO @Employee_Profile_ID      
      
    WHILE (@@fetch_status <> -1)      
    Begin      
 if (@@fetch_status <> -2)      
    Begin      
       Select @Empname     = '',      
       @Join_Date    = '19000101000000',      
       @Resign_Date  = '19000101000000',      
       @PCBCredit   = 0,      
       @CP38Credit  = 0,      
       @Serial_No   = '',      
       @Serial_No_2   = '',      
       @TotalPCB    = 0,      
       @TotalSalary = 0,      
       @OldICNo     = '',      
       @NewICNo     = '',      
       @PassportNo  = '',      
       @PCBNo   = '',      
--lai       
                     @noofChild   = 0      
       
      
       Select @empname = Employee_Profile_Vw."Name",      
       --@OldICNo   = oldnric,      
       @NewICNo   = Identity_Card_No,      
       @PassportNo = '',      
       @PCBNo  = Employee_Statutory."Value"      
    From Employee_Profile_Vw,Employee_Statutory      
    Where Employee_Profile_Vw.ID = @Employee_Profile_ID      
    And Employee_Statutory.Employee_Profile_ID = @Employee_Profile_ID    
 And Employee_Statutory.Option_Type_Statutory = 'PCB'      
      
	  select @PassportNo = DOCUMENT_NO 
	  from Employee_Document
	  where employee_profile_id = @Employee_Profile_ID
	  and option_type = 'PASSPORT'

       Select @Join_Date   = Join_Date,      
       @Resign_Date = Resign_Date      
    From Employee_Resign      
    Where Employee_Profile_ID = @Employee_Profile_ID      
    
      
              Select @noofChild = Non_Work_Child      
                  From Employee_No_Of_Child      
    Where Employee_Profile_ID = @Employee_Profile_ID 
	
	declare @maxMonth int
   select @maxMonth = max(month)
   from Employee_PCB_Formula_Value
   where Employee_Profile_ID = @Employee_Profile_ID      
   and year = @Year

   select @NoOfChild = Child_Below + Child_Diploma + Child_Disable + Child_Disable_Diploma
   from Employee_PCB_Formula_Value
   where Employee_Profile_ID = @Employee_Profile_ID      
   and year = @Year
    and month = @maxMonth         
      
      
  If (@Resign_Date = '19000101000000' or @Resign_Date > dbo.fn_UnDisplayDate(convert(nvarchar, '31/12/' + convert(nvarchar,@year),503),@Company_Profile_Code,'PAYROLL')) And dbo.fn_Datepart('year',@Join_Date) < @year      
   Select @Serial_No = '(a)'      
      
  If dbo.fn_Datepart('year',@Join_Date) = @year      
   Select @Serial_No = '(b)'      
      
  If dbo.fn_Datepart('year',@Resign_Date) = @year      
   Select @Serial_No = '(c)'      
      
       Select @PCBCredit = Sum(Employee_Credit)      
   From Employee_Payroll_Details,Employee_Profile_Vw,Employee_Payroll_Header      
   Where Employee_Profile_Vw.Company_Profile_Code = @Company_Profile_Code      
   And Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID    
   And Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID
   and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   And OCP_ID_Salary in (Select OCP_ID_Salary from Salary_Profile where Option_Type = 'PCB')
   And Employee_Payroll_Details.year = @year      
             
                Select @CP38Credit = Sum(Employee_Credit)      
   From Employee_Payroll_Details,Employee_Profile_Vw,Employee_Payroll_Header      
   Where Employee_Profile_Vw.Company_Profile_Code = @Company_Profile_Code      
   And Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID    
   And Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID
   and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)    
   And OCP_ID_Salary in (Select OCP_ID_Salary from Salary_Profile where Option_Type = 'CP38')    
   And Employee_Payroll_Details.year = @year      
                  
              Select @TotalSalary = isnull(sum(Employee_Debit),0)      
              From Employee_Payroll_Details, Payroll_EAReport_Field,Employee_Profile_Vw,Employee_Payroll_Header      
     Where Employee_Profile_Vw.Company_Profile_Code  = @Company_Profile_Code      
     And Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID    
     And Employee_Profile_Vw.ID       = Employee_Payroll_Details.Employee_Profile_ID        
              And   Payroll_EAReport_Field.Company_Profile_Code     = @Company_Profile_Code      
              And   Payroll_EAReport_Field.Option_Allowance_Deduction = 'ALLOWANCE'      
              And   Employee_Payroll_Details.OCP_ID_Salary   = Payroll_EAReport_Field.OCP_ID_Salary      
              And Employee_Payroll_Details.Employee_Profile_ID    = @Employee_Profile_ID      
              And   Employee_Payroll_Details.year                        = @Year
              and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)       
      
       Select @TotalDeduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)      
  From Employee_Payroll_Details, Payroll_EAReport_Field,Employee_Profile_Vw,Employee_Payroll_Header      
     Where Employee_Profile_Vw.Company_Profile_Code  = @Company_Profile_Code      
     And Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID    
     And Employee_Profile_Vw.ID       = Employee_Payroll_Details.Employee_Profile_ID          
              And   Payroll_EAReport_Field.Company_Profile_Code       = @Company_Profile_Code      
              And   Payroll_EAReport_Field.Option_Allowance_Deduction          = 'DEDUCTION'      
              And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary      
              And   Employee_Payroll_Details.Employee_Profile_ID                       = @Employee_Profile_ID      
              And   Employee_Payroll_Details.year                        = @Year
              and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)       
      
             If @TotalSalary > 0      
               Select @TotalSalary = @TotalSalary - @TotalDeduct      
             Else      
               Select @TotalSalary = @TotalDeduct      
      
       Select @PCBCredit = 0 Where @PCBCredit Is Null      
       Select @CP38Credit = 0 Where @CP38Credit Is Null      
       Select @TotalSalary = 0 Where @TotalSalary Is Null      
      
       Select @TotalPCB = @PCBCredit --+ @CP38Credit      
      
--               If @TotalSalary >= 25000      
--                   Insert Into Payroll_EForm values      
--    (@Company_Profile_Code,@Employee_Profile_ID,@empname,Convert(datetime,@datesubmit,503),      
--     Convert(datetime,GetDate(),503),@Serial_No,'',@TotalPCB,@TotalSalary,@Year,      
--     @OldICNo,@NewICNo,@PassportNo,@PCBNo,@picname,@picjobtitle,@SPID, @noofChild,0,0)      
--       
--   Else      
--      Begin      
   If Exists (Select * from Employee_Payroll_Details,Employee_Profile_Vw,Employee_Payroll_Header      
     Where Employee_Profile_Vw.Company_Profile_Code  = @Company_Profile_Code      
     And Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID    
     And Employee_Profile_Vw.ID       = Employee_Payroll_Details.Employee_Profile_ID          
           And   Employee_Payroll_Details.year                        = @Year
              and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)        
        And (OCP_ID_Salary in (Select OCP_ID_Salary from Salary_Profile where Option_Type = 'PCB') )--or OCP_ID_Salary = '~~CP38')      
           And (Employee_Credit > 0 or Company_Credit > 0))      
      Insert Into Payroll_EForm values      
    (@Company_Profile_Code,@Employee_Profile_ID,@datesubmit,      
    dbo.fn_GetCurrentDate(getdate()),@Serial_No,'',@TotalPCB,@TotalSalary,@Year,      
    @NewICNo,@PassportNo,@PCBNo,@picname,@picjobtitle,@SPID, @noofChild,0,0,@OCP_ID)      
   Else      
      Begin      
    Select @Serial_No = '(d)'      
    Select @Serial_No_2 = @Serial_No + Convert(nvarchar, @Count_D)      
          
    Insert Into Payroll_EForm values      
       (@Company_Profile_Code,@Employee_Profile_ID,@datesubmit,      
        dbo.fn_GetCurrentDate(getdate()),@Serial_No,@Serial_No_2,@TotalPCB,@TotalSalary,      
        @Year,@NewICNo,@PassportNo,@PCBNo,@picname,@picjobtitle,0,@noofChild,0,0,@OCP_ID)      
      
    Select @Count_D = @Count_D + 1      
      End      
--     End      
      
     FETCH NEXT FROM PCBPointer INTO @Employee_Profile_ID      
      
     End      
  End      
      
  CLOSE PCBPointer      
  DEALLOCATE PCBPointer      
      
  Select @Count = 1      
  Select @Employee_Profile_ID = null      
      
  DECLARE EmpPointer CURSOR       
  FOR       
  Select Employee_Profile_ID      
    From Payroll_EForm , Employee_Profile_Vw    
 where Payroll_EForm.Employee_Profile_ID = Employee_Profile_Vw.ID
 and (OCP_ID = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)      
    Order by Serial_No,Employee_Profile_Vw."Name"      
      
  OPEN EmpPointer      
  FETCH NEXT FROM EmpPointer INTO @Employee_Profile_ID      
    WHILE (@@fetch_status <> -1)      
    Begin      
 if (@@fetch_status <> -2)      
    Begin      
  Update Payroll_EForm      
  Set Serial_No_2 = Serial_No + Convert(nvarchar, @Count)      
  Where Employee_Profile_ID = @Employee_Profile_ID      
  And   SPID <> 0      
      
  Select @Count = @Count + 1      
      
  FETCH NEXT FROM EmpPointer INTO @Employee_Profile_ID      
     End      
    End      
  CLOSE EmpPointer      
  DEALLOCATE EmpPointer      
      
  select @OtherCount = count(*),       
         @OtherSalary = sum(Total_Salary)      
  from Payroll_EForm      
  where SPID = 0      
  and   year     = @Year      
  and   Serial_No = '(d)'      
      
  update Payroll_EForm      
  set Other_Count = @OtherCount,      
      Other_Salary = @OtherSAlary      
      
END      
      
IF @Action = 'DEL'      
   Delete From Payroll_EForm where Company_Profile_Code = @Company_Profile_Code and year = @year
   and OCP_ID = @OCP_ID
      
IF @Action = 'SEL'      
   Select Employee_Status.Employee_Profile_ID + Space(50) + '(' + Space(1) + Employee_Profile_Vw."Name" + Space(1) + ')'      
     From Employee_Status,Employee_Profile_Vw,Employee_Resign      
     Where Employee_Profile_Vw.Company_Profile_Code = @Company_Profile_Code      
     And Employee_Status.Employee_Profile_ID = Employee_Profile_Vw.ID      
     And Employee_Status.Employee_Profile_ID = Employee_Resign.Employee_Profile_ID      
     And   (Resign_Date = '19000101000000'       
     Or Resign_Date > dbo.fn_DateAdd('month', 1,dbo.fn_GetCurrentDate(getdate())))      
     Order By Employee_Status.Employee_Profile_ID      
      
  Set Nocount off      

