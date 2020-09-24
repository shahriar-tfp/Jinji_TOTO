Drop proc [dbo].[sp_pr_CalIncDeductedLeave]      
GO
/*
Alter By : SY Tey
Alter Date : 28/07/2011
Version : 1.0
Problem : create new deduction leave when leave deduct not together.
*/
/*
Alter By : SY Tey
Alter Date : 18/07/2011
Version : 1.0
Problem : create new deduction leave when leave deduct not together.
*/
/*
Alter By : SY Tey
Alter Date : 30/06/2011
Version : 1.0
Problem : Error when got increment.
*/
/*
Alter By : SY Tey
Alter Date : 06/10/2009
Version : 1.0
Problem : For new PCB
*/
/*
Alter By : SY Tey   
Date : 03/07/2008    
Version : 1.0
Problem : Deducted Leave code.  
*/
/*
Alter By : SY Tey   
Date : 29/01/2008    
Version : 1.0  
*/ 
/*       
Create/Alter By : Issac     
Create/Alter Date : 2007-03-19    
Version : 1.0                                  
Problems : -       
Description : Convert from HRES  
*/    
  
Create proc [dbo].[sp_pr_CalIncDeductedLeave]      
@OrganID    nvarchar(50),      
@empID      nvarchar(50),      
@year       nvarchar(4),      
@month      nvarchar(2),      
@week       nvarchar(20),      
@cutOff1    nvarchar(14),      
@cutOff2    nvarchar(14),      
@payType    nvarchar(10),      
@currencyID nvarchar(50),      
@type       nvarchar(50) -- 31121999 Add By HW Tang      
      
AS      
declare     
@startdate        nvarchar(14),      
@enddate          nvarchar(14),      
@tolDeductedLeave decimal(15,2),      
@MasterID         nvarchar(50),      
@resigndate    nvarchar(14),      
@effectiveDate    nvarchar(14),      
@effectiveDate1   nvarchar(14),      
@oldSalary        decimal(15,2),      
@newSalary        decimal(15,2),      
@tolDaysTaken1    decimal(5,2),      
@tolDaysTaken2    decimal(5,2),      
@tolDaysTaken3    decimal(5,2),      
@tolDaysTaken4    decimal(5,2),      
@hrp_Leave_1      decimal(25,15),      
@hrp_Leave_2      decimal(25,15),      
@codetype         nvarchar(50),      
@AutoDeduct       nvarchar(50),      
@Together         nvarchar(50),      
@LeaveID    nvarchar(50),      
@LeaveID1    nvarchar(50),      
@tolDaysTaken     decimal(25,15),      
@OneDayTime       int,      
@Description   nvarchar(200),
/***** Alter by SYTey 29/01/2008 *****/
@Non_Old_Salary	  nvarchar(50),
@Non_New_Salary	  nvarchar(50),
@Effective_Date   nvarchar(14),
/***** Alter End by SYTey 29/01/2008 *****/       
@OCP_ID_Salary nvarchar(50) -- Alter By SY Tey On 30/06/2011
      
Set Nocount on      
------------------------------------------------------------------------------------------------      
-- 11/1/2000       
  Select @resigndate = Resign_Date      
  From Employee_Resign      
  Where Employee_Profile_ID = @empID      
  And   Employee_Profile_ID In (Select Employee_Profile_ID From Employee_Status Where Company_Profile_Code = @OrganID)      
    
/***** Alter by SYTey 30/06/2011 *****/ 
select @OCP_ID_Salary = OCP_ID_Salary  
from salary_Profile, organisation_Code_Profile  
where salary_Profile.Option_Type = @Type  
and ID = OCP_ID_Salary  
and Company_profile_code = @OrganID 
/***** Alter End by SYTey 30/06/2011 *****/ 

-- 11/1/2000 (End)      
select @tolDaysTaken1    = 0,      
       @tolDaysTaken2    = 0,      
       @tolDaysTaken3    = 0,      
       @tolDaysTaken4    = 0,      
       @oldSalary        = 0,      
       @newSalary        = 0,      
       @hrp_Leave_1      = 0,      
       @hrp_Leave_2      = 0,      
       @tolDeductedLeave = 0,      
       @codetype         = 'PAYROLL',
	   @LeaveID1		 = ''      
    
-- 11/1/2000       
 if ( select count (*) from Parameter where Company_profile_Code = @OrganID and Module_Profile_Code  = 'PAYROLL'      
 and Code  = 'RESIGNPAY' and String_Value1 = 'Y') > 0       
 and @resigndate > @enddate And dbo.fn_Datepart('Month',@resigndate) = @month And dbo.fn_Datepart('Year',@resigndate) = @year      
 begin      
    select @endDate = @resigndate      
    select @cutoff2 = @resigndate      
 end      
-- 11/1/2000 (End)

/***** Alter by SYTey 29/01/2008 *****/
select Company_Profile_Code,Employee_Profile_ID,old_salary,new_Salary,Effective_Date
into #Employee_Increment1
from Employee_Increment
where Company_Profile_Code = @OrganID      
and   Employee_Profile_ID  = @empID

if  exists (select * from parameter 
			where company_profile_code = @OrganID 
			and module_profile_code = 'SYSTEM_MANAGER' 
			and code = 'ENCODE_SALARY' and string_value1 = 'Y')
begin
delete from #Employee_Increment1

DECLARE CURIncrement SCROLL CURSOR               
FOR
select Old_Salary,New_Salary,Effective_date from Employee_Increment      
            where Company_Profile_Code = @OrganID      
            and   Employee_Profile_ID  = @empID
			and   Effective_Date between @cutOff1 and @cutOff2

OPEN CURIncrement              
FETCH FIRST FROM CURIncrement INTO @Non_Old_Salary,@Non_New_Salary,@Effective_Date              
              
WHILE @@FETCH_STATUS = 0              
begin    
 IF @@FETCH_STATUS <> 0   
 Begin           
  BREAK
 End              
 ELSE
begin
	if @Non_Old_Salary <> '0'
	begin
		exec sp_sa_EDPwordv2 'DECODE',@OrganID,'',@Non_Old_Salary,@Non_Old_Salary output 
		select @oldsalary = convert(decimal(18,6),@Non_Old_Salary)
	end
	else
		select @oldsalary = 0 

	if @Non_New_Salary <> '0'
	begin
		exec sp_sa_EDPwordv2 'DECODE',@OrganID,'',@Non_New_Salary,@Non_New_Salary output 
		select @newsalary = convert(decimal(18,6),@Non_New_Salary)
	end
	else
		select @newsalary = 0 

	insert into #Employee_Increment1
	select @OrganID,@empID,@oldsalary,@newsalary,@Effective_Date

	FETCH NEXT FROM CURIncrement INTO @Non_Old_Salary,@Non_New_Salary,@Effective_Date              
	continue
end
end
close CURIncrement
deallocate CURIncrement
end

select @oldsalary = 0,
@newsalary = 0
      
/***** Alter End by SYTey 29/01/2008 *****/      
      
if exists ( select * from #Employee_Increment1      
            where company_profile_Code = @OrganID      
            and   Employee_Profile_ID   = @empID      
            and   Effective_Date between @cutOff1 and @cutOff2      
            and   Old_Salary <> New_Salary )       
begin      
  --------- Lai       
  select @Together = ISNULL(String_Value1,'Y')       
  from  Parameter      
  where  Company_Profile_Code  = @OrganID     
  and    Module_Profile_Code = 'PAYROLL'      
  and    Code = 'LTOGETHER'    
      
  ------------------------------------------------------------------------------------------------      
      
  EXEC sp_pr_GetMasterID 'DEDUCTED_LEAVE', @PayType, @MasterID output      
      
  select @AutoDeduct = String_Value3       
  from Parameter      
  where  Company_Profile_Code  = @OrganID      
  and    Module_Profile_Code = @codetype      
  and    Code = @MasterID      
      
/*    
select * from Employee_Resign    
select * from Employee_Status    
select * from Parameter Where Company_profile_Code = 'softfac' and     
Module_Profile_Code = 'PAYROLL' And Code = 'RESIGNPAY'    
Select dbo.fn_Datepart('Year',@resigndate)    
*/  
  
  if @AutoDeduct = 'Y'      
  begin      
    --EXEC sp_pr_getCutOffDate @OrganID, @month, @year, @paytype, 'LEAVE', @startdate output, @enddate output      
    EXEC sp_pr_getPayrollCutOffDate @OrganID, @week, @month, @year, @paytype, 'LEAVE', @startdate output, @enddate output      
      
   --select  @startdate,@enddate
--lai Remove Absent Before and After PH to Cal Payroll Proceesing 04/03/2002      
-- Absent Before / After Public Holoiday      
--    EXEC sp_tms_chkabsentholiday @OrganID, @empID , @startdate , @enddate       
      
    Select @effectiveDate = effective_date,       
           @oldSalary = isnull(old_salary, 0),      
           @newSalary = isnull(new_salary, 0)      
    from #employee_increment1      
    where company_profile_code = @OrganID      
    and   employee_profile_id   = @empID      
    and   effective_date between @startDate and @endDate

	drop table #employee_increment1      
          
    select @effectiveDate1 = dbo.fn_dateadd('Day', -1, @effectiveDate)      
      
    if @Together = 'Y'      
    begin      
      select @tolDaysTaken1 = isnull(sum(days), 0)      
      from employee_leave_application      
      where employee_profile_id = @empID       
      and   date_apply_for between @startdate and @effectiveDate1      
      and   option_status = 'APPROVED'      
      and   ocp_id_leave in (select ocp_id_leave from leave_profile_vw       
            where company_profile_code = @OrganID and option_deduct_pay = 'YES')
	  AND   Option_Period in (select Code from [option] where Table_Profile_Code = 'Employee_Leave_Application' and Table_Field_Code = 'OPTION_PERIOD')      
     
      select @tolDaysTaken2 = isnull(sum(days), 0)      
      from employee_leave_application      
      where employee_profile_id = @empID       
      and   date_apply_for between @effectiveDate and @endDate      
      and   option_status = 'APPROVED'      
      and   ocp_id_leave in ( select ocp_id_leave from leave_profile_vw       
            where company_profile_code = @OrganID and option_deduct_pay = 'YES')
	  AND   Option_Period not in (select Code from [option] where Table_Profile_Code = 'Employee_Leave_Application' and Table_Field_Code = 'OPTION_PERIOD')       
      
      if ( @tolDaysTaken1 + @tolDaysTaken2 ) > 0       
      begin      
        EXEC sp_pr_GetMasterID 'DEDUCTED_LEAVE', @PayType, @MasterID output      
      
        if @tolDaysTaken1 > 0      
          EXEC sp_pr_GetMasterHRP @OrganID, @empId, @year, @month, 'PAYROLL', @masterID, @oldSalary, @paytype, @hrp_Leave_1 output      
        else            
          select @hrp_Leave_1 = 0      
      
        if @tolDaysTaken2 > 0      
          EXEC sp_pr_GetMasterHRP @OrganID, @empId, @year, @month, 'PAYROLL', @masterID, @newSalary, @paytype, @hrp_Leave_2 output      
        else            
          select @hrp_Leave_2 = 0      
      
        select @tolDeductedLeave = (@tolDaysTaken1 * @hrp_Leave_1) + (@tolDaysTaken2 * @hrp_Leave_2)      
      
        if @tolDeductedLeave IS NULL      
          select @tolDeductedLeave = 0      
      
      /***** Alter by SYTey 30/06/2011 *****/ 
  --      EXEC sp_pr_InsSystemAllowDeduct @OrganID, 'DEDUCTION', 'DEDUCTED_LEAVE'
		--select @LeaveID1 = ID from Organisation_Code_Profile_Vw where Code = 'DEDUCTED_LEAVE (' + @OrganID + ')' and Option_Type = 'Salary' and Company_Profile_Code = @OrganID     
		if @type = 'DEDUCTED_LEAVE'    
		begin
        EXEC sp_pr_InsPayrollDetails @OrganID, @empID, @Year, @Month, @week, @currencyID, @OCP_ID_Salary, @tolDeductedLeave, 'DEDUCTION'      
        end
        /***** Alter end by SYTey 30/06/2011 *****/ 
      end      
    end  -- End All Leave calculate together.      
    else      
    begin      
      DECLARE LeavePointer CURSOR      
      FOR      
        Select ocp_id_leave      
        From employee_leave_application      
        Where employee_profile_id = @empID       
        And   date_apply_for between @startdate and @enddate      
        And   option_status = 'APPROVED'      
        And   ocp_id_leave in ( select ocp_id_leave from leave_profile_vw       
                           where company_profile_code = @OrganID       
                           and option_deduct_pay = 'YES')      
        Group by ocp_id_leave      
                     
      OPEN LeavePointer      
      FETCH NEXT FROM LeavePointer INTO @LeaveID      
      
      WHILE ( @@fetch_status <> -1 )      
      Begin      
        If ( @@fetch_status <> -2 )      
        Begin      

          select @tolDaysTaken1 = isnull(sum(days), 0)      
          from employee_leave_application      
          where employee_profile_id = @empID       
          and   date_apply_for between @startdate and @effectiveDate1      
          and   option_status = 'APPROVED'     
          and   ocp_id_leave = @leaveid      
          and   option_period in (Select Code from [option] where table_profile_code ='EMPLOYEE_LEAVE_APPLICATION' and Table_field_Code = 'OPTION_PERIOD')      
      
          select @tolDaysTaken3 = isnull(sum(days), 0)      
          from employee_leave_application      
          where employee_profile_id   = @empID       
          and   date_apply_for between @effectiveDate and @endDate      
          and   option_status = 'APPROVED'     
          and   ocp_id_leave = @leaveid      
          and   option_period in (Select Code from [option] where table_profile_code ='EMPLOYEE_LEAVE_APPLICATION' and Table_field_Code = 'OPTION_PERIOD')      
     
          select @tolDaysTaken2 = isnull(sum(days), 0)      
          from employee_leave_application      
          where employee_profile_id   = @empID       
          and   date_apply_for between @startdate and @effectiveDate1      
          and   option_status = 'APPROVED'      
          and   ocp_id_leave = @leaveid      
          and   option_period not in (Select Code from [option] where table_profile_code ='EMPLOYEE_LEAVE_APPLICATION' and Table_field_Code = 'OPTION_PERIOD')        
      
          select @tolDaysTaken4 = isnull(sum(days), 0)      
          from employee_leave_application      
          where employee_profile_id   = @empID       
          and   date_apply_for between @effectiveDate and @endDate      
          and   option_status = 'APPROVED'    
          and   ocp_id_leave = @leaveid      
          and   option_period not in (Select Code from [option] where table_profile_code ='EMPLOYEE_LEAVE_APPLICATION' and Table_field_Code = 'OPTION_PERIOD')        
       
          Select @OneDayTime = decimal_value1 from parameter       
          where Company_profile_code = @OrganID and code = 'LEAVEMIN'    
			and module_profile_code='LEAVE'
                         
          Select @tolDaysTaken2 = @tolDaysTaken2 / @OneDayTime      
          Select @tolDaysTaken4 = @tolDaysTaken4 / @OneDayTime      
      
--          Select @tolDaysTaken = @tolDaysTaken + @tolDaysTaken2      
--          Select @tolDeductedLeave = @tolDaysTaken * @hrp_Leave      
      
          if ( @tolDaysTaken1 + @tolDaysTaken2 + @tolDaysTaken3 + @tolDaysTaken4 ) > 0       
          begin      
            EXEC sp_pr_GetMasterID 'DEDUCTED_LEAVE', @PayType, @MasterID output      
      
            if ( @tolDaysTaken1 + @tolDaysTaken2 ) > 0      
              EXEC sp_pr_GetMasterHRP @OrganID, @empId, @year, @month, 'PAYROLL', @masterID, @oldSalary, @paytype, @hrp_Leave_1 output      
            else            
              select @hrp_Leave_1 = 0      
      
            if ( @tolDaysTaken3 + @tolDaysTaken4 ) > 0      
              EXEC sp_pr_GetMasterHRP @OrganID, @empId, @year, @month, 'PAYROLL', @masterID, @newSalary, @paytype, @hrp_Leave_2 output      
            else            
              select @hrp_Leave_2 = 0      
      
            select @tolDeductedLeave = ((@tolDaysTaken1 + @tolDaysTaken2) * @hrp_Leave_1) + ((@tolDaysTaken3 + @tolDaysTaken4)* @hrp_Leave_2)      
      
            if @tolDeductedLeave IS NULL      
              select @tolDeductedLeave = 0      
      
--            EXEC sp_pr_InsSystemAllowDeduct @OrganID, 'D', '~~DLEAVE'      
--            EXEC sp_pr_InsPayrollDetails @OrganID, @empID, @Year, @Month, @week, @currencyID, '~~DLEAVE', @tolDeductedLeave, 'D'      
          end      
      
          Select @tolDaysTaken  = 0,      
                 @tolDaysTaken1 = 0,      
                 @tolDaysTaken2 = 0,      
                 @tolDaysTaken3 = 0,      
                 @tolDaysTaken4 = 0,      
                 @OneDayTime    = 0      
      
          If @tolDeductedLeave IS NULL      
            Select @tolDeductedLeave = 0      
      
		select @LeaveID1 = null

          If @type = 'DEDUCTED_LEAVE'      
          Begin      
           Select @LeaveID1 = id
					from organisation_code_profile_vw 
					where code in (select replace(code,' (','_D (')
					from organisation_code_profile_vw 
					where id = @LeaveID  and Option_Type = 'LEAVE')
					and option_type = 'SALARY'  
                  
--select @LeaveID1,@LeaveID
          if NOT exists (select * from salary_profile where ocp_id_salary = @LeaveID1)      
            begin      
              select @description = replace(code,' (','_D (')
              from leave_profile_vw      
              where company_profile_code = @OrganID      
              and   ocp_id_leave = @LeaveID      
    
			if not exists(select * from Organisation_Code_Profile_Vw where CODE = @description
			  and Option_Type = 'SALARY')
			  begin
				insert into Organisation_Code_Profile
				select @OrganID,'SALARY','YES',@year + @month + '01000000'
				
				set rowcount 1
				select @LeaveID1 = id
				from Organisation_Code_Profile
				where Option_Type = 'SALARY'
				order by ID desc
				
				insert into OCP_Code
				select @LeaveID1,@Description,@year + @month + '01000000'
				
				insert into OCP_Name
				select @LeaveID1,'Deducted - ' + @Description,@year + @month + '01000000'
				
				insert into OCP_PIC
				select @LeaveID1,'',@year + @month + '01000000'
				
				set rowcount 0
			  end
                  
              set rowcount 1
					insert into salary_profile      
              Select @LeaveID1, 'DEDUCTION', 'DEDUCTED_LEAVE', 'FIXED', 'MONTHLY', option_epf, option_socso,       
                     option_pcb, option_hrp, option_fix, 'YES', Option_New_join_Employee, Option_Resigned_Employee,     
					option_tabung_haji, option_Amanah_saham, Option_Pay_Back_leave, option_income, Option_Pay_Cycle,
					option_bonus_formula,option_last_year      
              From salary_profile      
              Where Option_Type = 'DEDUCTED_LEAVE'
              set rowcount 0 
            end      
     
    if NOT exists ( select * from Payroll_Payslip_Order      
                            where Company_Profile_Code = @OrganID       
                            and OCP_ID_Salary = @LeaveID1      
                            and option_type = 'DEDUCTION' )     
    
              insert into Payroll_Payslip_Order      
              select @OrganID, @LeaveID1, 'DEDUCTION', isnull(max(Sequence) + 1, 0)      
              from Payroll_Payslip_Order      
              where Company_Profile_Code = @OrganID       
              and   option_type    = 'DEDUCTION'      
               
--select   @LeaveID1,@tolDeductedLeave
              EXEC sp_pr_InsPayrollDetails @OrganID, @empID, @Year, @Month, @week, @currencyID, @LeaveID1, @tolDeductedLeave, 'DEDUCTION'      
          End      
            
          FETCH NEXT FROM LeavePointer INTO @LeaveID      
        End      
      End      
      
      CLOSE LeavePointer      
      DEALLOCATE LeavePointer      
      
    end  -- End Diff Leave Type....      
  end      
end  -- End Increment      
else      
  EXEC sp_pr_CalDeductedLeave @OrganID, @empID, @year, @month, @week, @paytype, @currencyID, @type     
      
------------------------------------------------------------------------------------------------      
Set Nocount off      
      
    
/*  
exec sp_pr_CalIncDeductedLeave 'SOFTFAC','3','2007','04','MONTHLY','20070401000000','20070430000000','MONTHLY','RM','DLEAVE'    
*/










