Drop proc [dbo].[sp_pr_InsDelTempEAForm]            
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
Alter By : SY Tey  
Alter Date : 17/10/2011  
Version : 3.0  
Problem : add bonus split.
*/
/*  
Alter By : SY Tey  
Alter Date : 23/02/2011  
Version : 2.0  
Problem : Select By Division.
*/
/*  
Alter By : SY Tey  
Alter Date : 07/01/2010  
Version : 1.0  
Problem : Add additional columns for Part G.  
*/   
      
Create proc [dbo].[sp_pr_InsDelTempEAForm]            
@Action  nvarchar(50),            
@RegNo   nvarchar(20),            
@EmpID   nvarchar(10),            
@Year    int,            
@SpID    int            
                
As            
  Declare @Basic         decimal(15,6),            
          @Ot            decimal(15,6),            
          @Bonus         decimal(15,6),            
          @TolAllow      decimal(15,6),            
          @Epfee         decimal(15,6),             
          @Pcb           decimal(15,6),         
          @cp38          decimal(15,6),           
          @zakat          decimal(15,6),           
          @Join_Date      nvarchar(14),            
          @DateResign    nvarchar(14),              
          @PreOrg        nvarchar(50),            
          @PreOrgAddr    nvarchar(80),            
          @OrgName       nvarchar(50),            
          @OrgAddr       nvarchar(80),            
          @HusbandName   nvarchar(80),            
          @HusbandIC     nvarchar(30),            
          @HusbandPcb    nvarchar(15),                
          @NoOfChild     int,            
          @JoinYear      int,            
          @ResignYear    int,            
          @Marital       nvarchar(10),            
          @Sex           nvarchar(10),      
          @pcbno         nvarchar(30),      
          @hqpcbno         nvarchar(30),
          @PreYear1		 nvarchar(4),                      
          @PreYear2		 nvarchar(4),
          @PreAllowance1 nvarchar(2000),
          @PreAllowance2 nvarchar(2000),
          @PrePayment1   decimal(18,2),
          @PrePayment2   decimal(18,2),
          @PreEPF1	     decimal(18,2),
          @PreEPF2	     decimal(18,2),
          @PreCP39A1	 decimal(18,2),
          @PreCP39A2	 decimal(18,2),
          @OCP_ID		 nvarchar(50)
            
Declare @Allowance1  decimal(15,6),            
 @Allowance2  decimal(15,6),            
 @Allowance3  decimal(15,6),            
 @Allowance4  decimal(15,6),            
 @Allowance5  decimal(15,6),            
 @Allowance6  decimal(15,6),            
 @Allowance7  decimal(15,6),            
 @Allowance8  decimal(15,6),            
 @Allowance9  decimal(15,6),            
 @Allowance10 decimal(15,6),            
 @Allowance11 decimal(15,6),            
 @Allowance12 decimal(15,6),            
 @Allowance13 decimal(15,6),            
 @Allowance14 decimal(15,6),            
 @Allowance15 decimal(15,6),            
 @Allowance16 decimal(15,6),            
 @Allowance17 decimal(15,6),            
 @Allowance18 decimal(15,6),            
 @Allowance19 decimal(15,6),            
 @Allowance20 decimal(15,6),            
 @Allowance21 decimal(15,6),  
 @Allowance22 decimal(15,6), --Alter By SY Tey On 07/01/2010  
 @Earn      decimal(15,6),            
 @Deduct      decimal(15,6) ,
 @Socso     decimal(15,6),
 @TP1		decimal(15,6),
 @TP1Zakat  decimal(15,6),
 @ChildAmt  decimal(15,6)            
            
If @Action = 'ADD' or @Action = 'DIVISION'
Begin            
  Select @Basic = 0,            
         @Ot = 0,            
         @Bonus = 0,            
         @TolAllow = 0,            
         @Epfee = 0,             
         @Pcb = 0,        
  @cp38 = 0,        
  @zakat = 0,            
         @Join_Date = ' ',            
         @DateResign = ' ',              
         @PreOrg = ' ',            
         @PreOrgAddr = ' ',            
         @OrgName = ' ',            
         @OrgAddr = ' ',            
         @HusbandName = ' ',            
         @HusbandIC = ' ',            
         @HusbandPcb = ' ',                
         @NoOfChild = 0,
         @PreAllowance1 = '',
         @PreAllowance2 = '',
         @PreCP39A1 = 0,
         @PreCP39A2 = 0,
         @PreEPF1 = 0,
         @PreEPF2 = 0,
         @PrePayment1 = 0,
         @PrePayment2 = 0,
         @PreYear1 = '',
         @PreYear2 = ''            
            
Select  @Allowance1  = 0,            
 @Allowance2  = 0,            
 @Allowance3  = 0,            
 @Allowance4  = 0,            
 @Allowance5  = 0,            
 @Allowance6  = 0,            
 @Allowance7  = 0,            
 @Allowance8  = 0,            
 @Allowance9  = 0,            
 @Allowance10 = 0,            
 @Allowance11 = 0,            
 @Allowance12 = 0,            
 @Allowance13 = 0,            
 @Allowance14 = 0,            
 @Allowance15 = 0,            
 @Allowance16 = 0,            
 @Allowance17 = 0,            
 @Allowance18 = 0,            
 @Allowance19 = 0,            
 @Allowance20 = 0,            
 @Allowance21 = 0,  
 @Allowance22 = 0,   --Alter By SY Tey on 07/01/2010         
 @Earn      = 0,            
 @Deduct      = 0,
 @OCP_ID	 = '',
 @Socso     =0,
 @TP1		=0,
 @TP1Zakat  =0,
 @ChildAmt  =0             

 Create table #tempemp(
 employee_profile_id nvarchar(50),
 OCP_ID nvarchar(50))
                                   
 if @Action = 'DIVISION'
 begin
 
	insert into #tempemp
	select a.employee_profile_id, c.ocp_id
	from Employee_Status a, Employee_Payroll_Header b, Payroll_EForm c
	where a.Employee_Profile_ID = b.Employee_Profile_ID
	and b.Year = @Year
	and a.OCP_ID_Division = @EmpID
	and c.Company_Profile_Code = @RegNo
	and b.Employee_Profile_ID = c.Employee_Profile_ID
	
 end
 
 if @Action = 'ADD'
 begin
	insert into #tempemp
	select @EmpID, ocp_id
	From Payroll_EForm            
   Where Company_Profile_Code = @RegNo            
   And  Employee_Profile_ID   = @empid            
   And   year    = @year
       
 end
-- Start 03/01/2001            
  -- Field 1  
  while(select COUNT(*) from #tempemp) > 0
  begin
  select @EmpID = '', @OCP_ID= ''
  set rowcount  1
  select @EmpID = employee_profile_id , @OCP_ID = ocp_id from #tempemp
  set rowcount 0
  
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID            
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '1'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And   Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)         
      
      
          
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '1'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
        
      
          
  If @Earn > 0            
     Select @Allowance1 = @Earn - @Deduct            
  Else            
     Select @Allowance1 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 2            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '2'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null) 
    
  Select @Earn = @Earn + isnull(sum(Employee_Debit),0)            
    From Employee_Bonus_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Bonus_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Bonus_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '2'            
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Bonus_Details.Employee_Profile_ID = @empid            
    And   Employee_Bonus_Details.year = @Year
    and Employee_Bonus_Details.Employee_Profile_ID = Employee_Bonus_Header.Employee_Profile_ID
   and Employee_Bonus_Details.Month = Employee_Bonus_Header.Month
   and Employee_Bonus_Details.Year = Employee_Bonus_Header.Year
   and Employee_Bonus_Details.Option_Pay_Cycle = Employee_Bonus_Header.Option_Pay_Cycle
   and (Employee_Bonus_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)           
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '2'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance2 = @Earn - @Deduct            
  Else            
     Select @Allowance2 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 3            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '3'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '3'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And  Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance3 = @Earn - @Deduct            
  Else            
     Select @Allowance3 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
            
  -- Field 4            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '4'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary           
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '4'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance4 = @Earn - @Deduct            
  Else            
     Select @Allowance4 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 5            
  Select @Earn = isnull(sum(Employee_Debit),0)   
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '5'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '5'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance5 = @Earn - @Deduct            
  Else            
     Select @Allowance5 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 6            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '6'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '6'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance6 = @Earn - @Deduct            
  Else            
     Select @Allowance6 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 7            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '7'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid    
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '7'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
    Group by Employee_Payroll_Details.Employee_Profile_ID            
            
  If @Earn > 0            
     Select @Allowance7 = @Earn - @Deduct            
  Else            
   Select @Allowance7 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 8            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '8'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '8'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance8 = @Earn - @Deduct            
  Else            
     Select @Allowance8 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 9            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '9'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '9'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance9 = @Earn - @Deduct            
  Else            
     Select @Allowance9 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 10            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '10'      
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '10'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance10 = @Earn - @Deduct            
  Else            
     Select @Allowance10 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 11            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '11'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
 From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '11'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance11 = @Earn - @Deduct            
  Else            
     Select @Allowance11 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 12            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '12'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '12'         
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance12 = @Earn - @Deduct            
  Else            
     Select @Allowance12 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 13            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '13'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '13'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance13 = @Earn - @Deduct            
  Else            
     Select @Allowance13 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
  /*          
  -- Field 14            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '14'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
  
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '14'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance14 = @Earn - @Deduct            
  Else            
     Select @Allowance14 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 15            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '15'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
  
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '15'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance15 = @Earn - @Deduct            
  Else            
     Select @Allowance15 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 16            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '16'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  select @Earn = @Earn + isnull(SUM(amount),0) from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @RegNo
  and EMPLOYEE_PROFILE_ID = @EmpID
  and YEAR = @year
  
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '16'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance16 = @Earn - @Deduct            
  Else            
     Select @Allowance16 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 17            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '17'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
  And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '17'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance17 = @Earn - @Deduct            
  Else            
     Select @Allowance17 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 18            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '18'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '18'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance18 = @Earn - @Deduct            
  Else            
     Select @Allowance18 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 19            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '19'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '19'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance19 = @Earn - @Deduct            
  Else            
     Select @Allowance19 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 20            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '20'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '20'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance20 = @Earn - @Deduct            
  Else            
     Select @Allowance20 = @Deduct            
            
  Select @Earn   = 0            
  Select @Deduct = 0            
            
  -- Field 21            
  Select @Earn = isnull(sum(Employee_Debit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'ALLOWANCE'            
    And   Payroll_EAReport_Field.Option_Field  = '21'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
             
  Select @Deduct = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)            
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
    And   Payroll_EAReport_Field.Company_Profile_Code  = @RegNo            
    And   Payroll_EAReport_Field.Option_Allowance_Deduction     = 'DEDUCTION'            
    And   Payroll_EAReport_Field.Option_Field  = '21'            
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary            
    And  Employee_Payroll_Details.Employee_Profile_ID = @empid            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
            
  If @Earn > 0            
     Select @Allowance21 = @Earn - @Deduct            
  Else            
     Select @Allowance21 = @Deduct            
  */          
  Select @Earn   = 0            
  Select @Deduct = 0            
            
    
  -- Field 22  
  /***** Alter By SY Tey On 07/01/2010 *****/  
  select b.pcb_exclude_code, a.limit_Amount, isnull(sum(Convert(decimal(18,2),c.Employee_Debit)),0) as "Total" into #PCBY1  
 from pcb_exclude_ea a, pcb_exclude_details_ea b, Employee_Payroll_Details c,Employee_Payroll_Header  
 where a.code = b.pcb_exclude_code and b.ocp_id_salary = c.ocp_id_salary  
    and c.Employee_Profile_ID = @empid        
       and   c.year      = @year
       and   a.year      = b.year
       and   a.year      = c.year
       and c.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and c.Month = Employee_Payroll_Header.Month
   and c.Year = Employee_Payroll_Header.Year
   and c.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)                
 group by b.pcb_exclude_code, a.limit_Amount    
  
   select @Allowance3 = @Allowance3 + isnull(sum(Total - limit_amount),0)  
   from #PCBY1  
   where Total - limit_amount > 0  
   and limit_amount > 0  
  
   select @Allowance22 = isnull(sum(limit_amount),0)  
   from #PCBY1  
   where Total - limit_amount > 0  
   and limit_amount > 0  
  
   select @Allowance22 = @Allowance22 + isnull(Sum(Total),0)  
   from #PCBY1  
   where Total - limit_amount <= 0  
  
   select @Allowance22 = @Allowance22 + isnull(Sum(Total),0)  
   from #PCBY1  
   where limit_amount = 0  
  
   drop table #PCBY1  
   /***** Alter End By SY Tey On 07/01/2010 *****/  
-- End 03/01/2001            
               
  Select @Epfee = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)     
 From Employee_Payroll_Details, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
 And   OCP_ID_Salary in (Select OCP_ID_Salary from Salary_Profile where Option_Type = 'EPF_EMPLOYEE')    
    And   Employee_Payroll_Details.Employee_Profile_ID = @EmpID            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)            
      
  Select @Epfee = @Epfee + isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)     
 From Employee_Bonus_Details, Employee_Profile_Vw,Employee_Bonus_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Bonus_Details.Employee_Profile_ID    
 And   OCP_ID_Salary in (Select OCP_ID_Salary from Salary_Profile where Option_Type = 'EPF_EMPLOYEE')    
    And   Employee_Bonus_Details.Employee_Profile_ID = @EmpID            
    And   Employee_Bonus_Details.year = @Year
    and Employee_Bonus_Details.Employee_Profile_ID = Employee_Bonus_Header.Employee_Profile_ID
   and Employee_Bonus_Details.Month = Employee_Bonus_Header.Month
   and Employee_Bonus_Details.Year = Employee_Bonus_Header.Year
   and Employee_Bonus_Details.Option_Pay_Cycle = Employee_Bonus_Header.Option_Pay_Cycle
   and (Employee_Bonus_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)

   Select @Socso = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)     
 From Employee_Payroll_Details, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
 And   OCP_ID_Salary in (Select OCP_ID_Salary from Salary_Profile where Option_Type = 'SOCSO_EMPLOYEE')    
    And   Employee_Payroll_Details.Employee_Profile_ID = @EmpID            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)   
    
	select @TP1 =  isnull(sum(Amount),0)
	from Employee_PCB_Deduction
	where employee_profile_id = @EmpID
	and year = @Year
	and PCB_DEDUCTION_PROFILE_CODE <> 'PBS13'

	select @TP1Zakat =  isnull(sum(Amount),0)
	from Employee_PCB_Deduction
	where employee_profile_id = @EmpID
	and year = @Year
	and PCB_DEDUCTION_PROFILE_CODE = 'PBS13'

  Select @Pcb = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)     
 From Employee_Payroll_Details, Employee_Profile_Vw ,Employee_Payroll_Header   
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
 And   OCP_ID_Salary in (Select OCP_ID_Salary from Salary_Profile where Option_Type = 'PCB')            
    And   Employee_Payroll_Details.Employee_Profile_ID = @EmpID            
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null) 
    
  Select @Pcb = @Pcb + isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)     
 From Employee_Bonus_Details, Employee_Profile_Vw,Employee_Bonus_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Bonus_Details.Employee_Profile_ID    
 And   OCP_ID_Salary in (Select OCP_ID_Salary from Salary_Profile where Option_Type = 'PCB')            
    And   Employee_Bonus_Details.Employee_Profile_ID = @EmpID            
    And   Employee_Bonus_Details.year = @Year
    and Employee_Bonus_Details.Employee_Profile_ID = Employee_Bonus_Header.Employee_Profile_ID
   and Employee_Bonus_Details.Month = Employee_Bonus_Header.Month
   and Employee_Bonus_Details.Year = Employee_Bonus_Header.Year
   and Employee_Bonus_Details.Option_Pay_Cycle = Employee_Bonus_Header.Option_Pay_Cycle
   and (Employee_Bonus_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)           
        
-- add by ang 06022006-----------------------------------------------------------------                   
  Select @cp38 = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)     
 From Employee_Payroll_Details, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
 And   OCP_ID_Salary in (Select OCP_ID_Salary from Salary_Profile where Option_Type = 'CP38')                    
    And   Employee_Payroll_Details.Employee_Profile_ID = @EmpID                    
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)                    
                
  Select @zakat = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0)     
 From Employee_Payroll_Details, Employee_Profile_Vw,Employee_Payroll_Header    
    Where Employee_Profile_Vw.Company_Profile_Code = @RegNo    
 And   Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID    
 And   OCP_ID_Salary in (Select OCP_ID_Salary from Salary_Profile where Option_Type = 'ZAKAT')                    
    And   Employee_Payroll_Details.Employee_Profile_ID = @EmpID                    
    And   Employee_Payroll_Details.year = @Year
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)                    
                
-- end add by ang 06022006---------------------------              
                
  Select @JoinYear = dbo.fn_datepart('Year',Join_Date),@ResignYear = dbo.fn_datepart('Year',Resign_Date)             
    From Employee_Resign             
    Where Employee_Profile_ID = @EmpID            
              
 -- If @JoinYear = @Year             
  --  Begin            
      Select @Join_Date = Join_Date From Employee_Resign Where Employee_Profile_ID = @EmpID              
      Select @PreOrg = Company_Name,@PreOrgAddr = Lot + ' ' + Street From Employee_Experience             
        Where Employee_Profile_ID = @EmpID            
        And   Resign_Date = (Select max(Resign_Date)             
            From Employee_Experience Where Employee_Profile_ID = @EmpID)             
 --   End                   
                 
 -- If @ResignYear = @Year            
 --   Begin             
      Select @DateResign = Resign_Date From Employee_Resign Where Employee_Profile_ID = @EmpID          
      Select @OrgName = Company_Profile."Name",@OrgAddr = Lot + ' ' + Street             
        From Company_Profile,Company_Address             
        Where Company_Profile.Code = @RegNo    
  and   Company_Profile.Code = Company_Address.Company_Profile_Code    
  and   Company_Address.Option_Address_Type = 'MAILING_ADDRESS'    
                       
--    End             
    
    
   Select @Marital = Option_Marital_Status,@Sex = Option_Sex From Employee_Profile Where ID = @EmpID             
   If @Marital = 'MARRIED' And @Sex = 'FEMALE'            
      Select @HusbandName = name,@HusbandIC = Identity_Card_No,@HusbandPcb = PCB_NO From Employee_Family_Information             
        Where Employee_Profile_ID = @EmpID            
        And    Option_RelationShip = 'HUSBAND'            
                
   Select @NoOfChild = Non_Work_Child From Employee_No_Of_Child Where Employee_Profile_ID = @EmpID           
      
	  declare @maxMonth int
   select @maxMonth = max(month)
   from Employee_PCB_Formula_Value
   where Employee_Profile_ID = @EmpID      
   and year = @Year

   select @NoOfChild = Child_Below + Child_Diploma + Child_Disable + Child_Disable_Diploma, @ChildAmt = C
   from Employee_PCB_Formula_Value
   where Employee_Profile_ID = @EmpID      
   and year = @Year
    and month = @maxMonth

   select @pcbno= isnull(value,'') from Company_Statutory where      
   Company_Profile_Code =@RegNo and Option_Type = 'PCB_No'    
    
   select @hqpcbno= isnull(value,'') from Company_Statutory where      
   Company_Profile_Code = (select Company_Profile_Code_HQ from Company_Profile where code = @RegNo)    
   and Option_Type = 'PCB_No'         
           
   if dbo.fn_datepart('Year',@dateresign) > @year    
   select @dateresign = ''  
    
   if @dateresign = '19000101000000'  
 select @dateresign = ''  
       
         
  select @PreYear1 = b.YEAR,@PreAllowance1 = isnull(dbo.fn_GetOCPNameByID(b.OCP_ID_SALARY),''),
  @PreCP39A1 = d.Employee_Credit + d.Company_Credit,
  @PrePayment1 = c.Value 
  from commision_last_year b, Employee_Benefit c,employee_payroll_details d,Employee_Payroll_Header
 where b.Employee_Profile_ID = @EmpID
 and b.Employee_Profile_ID = c.Employee_Profile_ID
 and b.PAY_ON between c.Date_From and c.Date_To
 and b.OCP_ID_SALARY = c.OCP_ID_Salary
 and dbo.fn_DatePart('year',b.pay_on) = @Year
 and b.EMPLOYEE_PROFILE_ID = d.Employee_Profile_ID
 and d.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID
   and d.Month = Employee_Payroll_Header.Month
   and d.Year = Employee_Payroll_Header.Year
   and d.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle
   and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
 and d.Year = @Year
 and d.OCP_ID_Salary in (
 select id from Organisation_Code_Profile_Vw
 where Company_Profile_Code = @RegNo
 and CODE like 'CP39A%')
 and d.Employee_Credit +d.Company_Credit > 0
 
 
 exec sp_pr_CalBonusEPF @empid,@regno,'MONTHLY','01',@year,'RM',@PrePayment1,0,@preEPF1 output,0
 
  Insert into Payroll_EAForm            
 Select @RegNo,@EmpID, Serial_No_2, @Allowance1,@Allowance2,@Allowance3,@Allowance4,@Allowance5,@Allowance6,            
  @Allowance7,@Allowance8,@Allowance9,@Allowance10,@Allowance11,@Allowance12,@Allowance13,            
  @Socso,@TP1,@TP1Zakat,@ChildAmt,@Allowance18,@Allowance19,@Allowance20,@Allowance21,            
  @Allowance22,0,0,0,0,0,0,0,0,0,0,0,0,0,@Epfee,@Pcb,@cp38,@zakat,@Join_Date,@DateResign,@PreOrg,@PreOrgAddr,@OrgName,@OrgAddr, --Alter By SY Tey On 07/01/2010  
  @HusbandName,@HusbandIC,@HusbandPcb,@NoOfChild,@SpID ,@pcbno,@PreYear1,@PreAllowance1,@PrePayment1,@preEPF1,@PreCP39A1,
  '','',0,0,0, OCP_ID
   From Payroll_EForm            
   Where Company_Profile_Code = @RegNo            
   And  Employee_Profile_ID   = @empid            
   And   year    = @year
   and OCP_ID = @OCP_ID        
    
   delete from #tempemp where employee_profile_id = @empid and OCP_ID = @OCP_ID
 end 
/*     
  update pr_tempeaform set pcbno=@hqpcbno where Company_Profile_Code= @RegNo       
  andEmployee_Profile_ID in (selectEmployee_Profile_ID from is_empstatus where Company_Profile_Code=@RegNO and finid = 'UNI 0001' )      
  */    
  SELECT    
    Payroll_EAForm.Employee_Profile_ID, Payroll_EAForm.Serial_No, Payroll_EAForm.Allowance_1,    
 Payroll_EAForm.Allowance_2, Payroll_EAForm.Allowance_3, Payroll_EAForm.Allowance_4,     
 Payroll_EAForm.Allowance_5, Payroll_EAForm.Allowance_6, Payroll_EAForm.Allowance_7,    
 Payroll_EAForm.Allowance_8, Payroll_EAForm.Allowance_9, Payroll_EAForm.Allowance_10,     
 Payroll_EAForm.Allowance_11, Payroll_EAForm.Allowance_12, Payroll_EAForm.Allowance_13,     
 Payroll_EAForm.Allowance_14, Payroll_EAForm.Allowance_15, Payroll_EAForm.Allowance_16,     
 Payroll_EAForm.Allowance_17, Payroll_EAForm.Allowance_18, Payroll_EAForm.Allowance_19,     
 Payroll_EAForm.Allowance_20, Payroll_EAForm.Allowance_21, Payroll_EAForm.Allowance_22, --Alter By Sy Tey On 07/01/2010  
 Payroll_EAForm.EPF_Employee, Payroll_EAForm.PCB, Payroll_EAForm.CP38, Payroll_EAForm.ZAKAT,  
 dbo.fn_DisplayDate(Payroll_EAForm.Join_Date,Payroll_EAForm.Company_Profile_Code,'REPORTS') "Join_Date",   
 dbo.fn_DisplayDate(Payroll_EAForm.Resign_Date,Payroll_EAForm.Company_Profile_Code,'REPORTS') "Resign_Date",    
    Employee_Profile_Vw."Name" as 'Emp_Name', Employee_Profile_Vw.Identity_Card_No,    
    Employee_Statutory."Value" as 'EPF', Employee_Statutory_2."Value" as 'PCB',    
    Company_Profile."Name" as 'Company_Name', Company_Address."Lot", Company_Address."Street",     
 Company_Address."Postal", Company_Address."Address_City_Code", Company_Address."Address_State_Code",     
 Company_Statutory."Value" as 'Company_PCB_No',    
    Organisation_Code_Profile_Vw."Name" as 'Position',
    Preyear1,PreAllowance1,PrePayment1,PreEPF1,PreCP39A1    
FROM    
    { oj (((((((Payroll_EAForm Payroll_EAForm    
INNER JOIN Employee_Statutory Employee_Statutory ON    
Payroll_EAForm.Employee_Profile_ID = Employee_Statutory.Employee_Profile_ID AND    
Employee_Statutory.Option_Type_Statutory = 'EPF')    
INNER JOIN Employee_Statutory Employee_Statutory_2 ON    
Payroll_EAForm.Employee_Profile_ID = Employee_Statutory_2.Employee_Profile_ID AND    
Employee_Statutory_2.Option_Type_Statutory = 'PCB')    
INNER JOIN Employee_Status Employee_Status ON    
Payroll_EAForm.Employee_Profile_ID = Employee_Status.Employee_Profile_ID)    
INNER JOIN Company_Profile Company_Profile ON    
Payroll_EAForm.Company_Profile_Code = Company_Profile.Code)    
INNER JOIN Company_Address Company_Address ON    
Payroll_EAForm.Company_Profile_Code = Company_Address.Company_Profile_Code )    
INNER JOIN Company_Statutory Company_Statutory ON    
Payroll_EAForm.Company_Profile_Code = Company_Statutory.Company_Profile_Code AND    
Company_Statutory.Option_Type = 'PCB_No')    
INNER JOIN Employee_Profile_Vw Employee_Profile_Vw ON    
Payroll_EAForm.Employee_Profile_ID = Employee_Profile_Vw.ID)    
INNER JOIN Organisation_Code_Profile_Vw Organisation_Code_Profile_Vw ON    
Payroll_EAForm.Company_Profile_Code = Organisation_Code_Profile_Vw.Company_Profile_Code AND    
Employee_Status.OCP_ID_Job_Title = Organisation_Code_Profile_Vw.ID AND    
Organisation_Code_Profile_Vw.Option_Type = 'Job_Title'}    
    
    
    
          
End            
            
Else            
If @Action = 'DEL'            
   Delete Payroll_EAForm Where SPID = @SpID        
      
        
            
          
          
          
        
      
    
    
  