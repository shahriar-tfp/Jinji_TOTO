Drop proc [dbo].[sp_pr_RptEEA]              
GO
/*  
Alter BY : SY Tey  
Alter Date : 04/04/2017  
Version : 1.0  
Problem : new 2016 format.  
*/ 
/*  
Alter BY : SY Tey  
Alter Date : 26/12/2012  
Version : 1.0  
Problem : Use for Division statutory.  
*/  
/*    
Alter By : Sy tey  
Alter Date : 03/03/2011    
Version : 1.0    
Problem : filter By User_profile_Code.  
*/  
/*    
Alter By : Sy tey  
Alter Date : 25/02/2011    
Version : 1.0    
Problem : insert blankrecord, when not found.  
*/  
/*    
Alter By : win  
Alter Date : 01/04/2010    
Version : 1.0    
Problem : change Job_Title get from Employee_Payroll_Header.  
*/  
/*    
Alter By : SY Tey    
Alter Date : 25/02/2010    
Version : 1.0    
Problem : For EA Manual.  
*/  
/*    
Alter By : SY Tey    
Alter Date : 25/02/2010    
Version : 1.0    
Problem : Add Current Date, for Print date.  
*/  
/*    
Alter By : SY Tey    
Alter Date : 07/01/2010    
Version : 1.0    
Problem : Add additional columns for Part G.    
*/      
/*      
Create By: Choe      
Date : 20/06/2008      
Version: 1.0      
*/      
  
Create proc [dbo].[sp_pr_RptEEA]              
@RegNo   nvarchar(20),              
@SpID    int,      
@Year  int,      
@bonus1  nvarchar(20),      
@bonus2  nvarchar(20),  
@User_Profile_Code nvarchar(50)      
as      


declare @PIC nvarchar(50)

select @PIC = Person_In_Charge
from Government_Location
where company_profile_code = @RegNo
and option_type = 'EA-FORM'  
  
select a.employee_profile_id,b.OCP_ID,a.year,max(a.month) as 'month'  
into #lastmonth from employee_payroll_header a, payroll_eaform b  
where b.spid = @SpID  
and a.employee_profile_id = b.employee_profile_ID  
AND a.year = @Year  
and (a.OCP_ID_Division = b.OCP_ID or b.OCP_ID = '')  
and (b.employee_profile_id in (select employee_profile_id from Employee_Group  
where Employee_Group_ID in (  
select Employee_Group_ID from User_Employee_Group where User_Profile_Code = @User_Profile_Code))  
 or b.employee_profile_id in (select employee_profile_id  
 from User_Profile where Code = @User_Profile_Code))  
group by a.employee_profile_id,b.OCP_ID,a.year  
    
 SELECT      
    Payroll_EAForm.Employee_Profile_ID as 'Employee_Profile_ID', Payroll_EAForm.Serial_No, Payroll_EAForm.Allowance_1,      
 Payroll_EAForm.Allowance_2, Payroll_EAForm.Allowance_3, Payroll_EAForm.Allowance_4,       
 Payroll_EAForm.Allowance_5, Payroll_EAForm.Allowance_6, Payroll_EAForm.Allowance_7,      
 Payroll_EAForm.Allowance_8, Payroll_EAForm.Allowance_9, Payroll_EAForm.Allowance_10,       
 Payroll_EAForm.Allowance_11, Payroll_EAForm.Allowance_12, Payroll_EAForm.Allowance_13,       
 Payroll_EAForm.Allowance_14, Payroll_EAForm.Allowance_15, Payroll_EAForm.Allowance_16,       
 Payroll_EAForm.Allowance_17, Payroll_EAForm.Allowance_18, Payroll_EAForm.Allowance_19,       
 Payroll_EAForm.Allowance_20, Payroll_EAForm.Allowance_21, Payroll_EAForm.Allowance_22, --Alter By SY Tey On 07/01/2010    
 Payroll_EAForm.EPF_Employee, Payroll_EAForm.PCB, Payroll_EAForm.CP38, Payroll_EAForm.ZAKAT,    
  dbo.fn_DisplayDate(Payroll_EAForm.Join_Date,Payroll_EAForm.Company_Profile_Code,'REPORTS') "Join_Date",     
 dbo.fn_DisplayDate(Payroll_EAForm.Resign_Date,Payroll_EAForm.Company_Profile_Code,'REPORTS') "Resign_Date",     
    Employee_Profile_Vw."Name" as 'Emp_Name', Employee_Profile_Vw.Identity_Card_No,      
    Employee_Statutory."Value" as 'EPF', Employee_Statutory_2."Value" as 'PCB1',      
    Company_Profile."Name" as 'Company_Name', Company_Address."Lot", Company_Address."Street",       
 Company_Address."Postal", Company_Address."Address_City_Code", Company_Address."Address_State_Code",       
 Company_Statutory."Value" as 'Company_PCB_No',      
    Organisation_Code_Profile_Vw."Name" as 'Position',   
 convert(varchar(12), getdate()) as 'cdate',      
 @Year as 'year',      
 @bonus1 as 'bonus1', @bonus2 as 'bonus2',  
 convert(varchar(10),'28/02/' + convert(nvarchar,@Year+1),103) as 'CurrentDate',  
 convert(nvarchar(100),'') as OTHER_SALARY,  
convert(nvarchar(100),'') as CAR_DATE,  
convert(nvarchar(100),'') as CAR_TYPE,  
convert(nvarchar(100),'') as CAR_YEAR,  
convert(nvarchar(100),'') as CAR_MODEL,  
convert(nvarchar(100),'') as HOUSE_ADDRESS,  
Preyear1,PreAllowance1,PrePayment1,PreEPF1,PreCP39A1, Payroll_EAForm.OCP_ID ,
convert(varchar(500),'') as 'Passport'
,  convert(varchar(500),'') as'orgpic',convert(varchar(1000),'') as 'orgpos',
Employee_Statutory_3."Value" as 'SOCSONO',No_Of_Child,Company_Profile.Phone_No as 'Company_Tel' 
  
into #EAForm  
FROM      
    { oj (((((((((Payroll_EAForm Payroll_EAForm     
left outer JOIN Employee_Statutory Employee_Statutory ON      
Payroll_EAForm.Employee_Profile_ID = Employee_Statutory.Employee_Profile_ID AND      
Employee_Statutory.Option_Type_Statutory = 'EPF')      
left outer JOIN Employee_Statutory Employee_Statutory_2 ON      
Payroll_EAForm.Employee_Profile_ID = Employee_Statutory_2.Employee_Profile_ID AND      
Employee_Statutory_2.Option_Type_Statutory = 'PCB')  
LEFT OUTER JOIN Employee_Statutory Employee_Statutory_3 ON    
Payroll_EAForm.Employee_Profile_ID = Employee_Statutory_3.Employee_Profile_ID AND    
Employee_Statutory_3.Option_Type_Statutory = 'SOCSO')     
--INNER JOIN Employee_Status Employee_Status ON      
--Payroll_EAForm.Employee_Profile_ID = Employee_Status.Employee_Profile_ID)      
INNER JOIN Employee_Payroll_Header Employee_Payroll_Header ON      
Payroll_EAForm.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID)      
inner join #lastmonth lastmonth on   
lastmonth.employee_profile_id = Employee_Payroll_Header.Employee_Profile_ID  
and lastmonth.year = Employee_Payroll_Header.year  
and lastmonth.month = Employee_Payroll_Header.month  
and Payroll_EAForm.ocp_id = lastmonth.ocp_id)  
INNER JOIN Company_Profile Company_Profile ON      
Payroll_EAForm.Company_Profile_Code = Company_Profile.Code)      
INNER JOIN Company_Address Company_Address ON      
Payroll_EAForm.Company_Profile_Code = Company_Address.Company_Profile_Code  
and Option_Address_type = 'REGISTERED_ADDRESS')      
INNER JOIN Company_Statutory Company_Statutory ON      
Payroll_EAForm.Company_Profile_Code = Company_Statutory.Company_Profile_Code AND      
Company_Statutory.Option_Type = 'PCB_No')      
INNER JOIN Employee_Profile_Vw Employee_Profile_Vw ON      
Payroll_EAForm.Employee_Profile_ID = Employee_Profile_Vw.ID)      
left outer JOIN Organisation_Code_Profile_Vw Organisation_Code_Profile_Vw ON      
Payroll_EAForm.Company_Profile_Code = Organisation_Code_Profile_Vw.Company_Profile_Code AND      
--Employee_Status.OCP_ID_Job_Title = Organisation_Code_Profile_Vw.ID AND      
Employee_Payroll_Header.OCP_ID_Job_Title = Organisation_Code_Profile_Vw.ID AND      
Organisation_Code_Profile_Vw.Option_Type = 'Job_Title'}      
  
where spid = @SpID and PayRoll_EAForm.Company_Profile_Code = @RegNo  
order by left(Payroll_EAForm.Serial_No,3) asc, convert(int,substring(Payroll_EAForm.Serial_No,4,len(Payroll_EAForm.Serial_No)-3)) asc  
  
delete from #EAForm
where Employee_profile_id not in (
select employee_profile_id from publish_EA
where year = @year and Option_Block = 'YES')

update #EAForm
set Passport = a.Passport_No
from Payroll_EForm a
where #EAForm.Employee_Profile_ID = a.Employee_Profile_ID
and a.Year = @Year

update #EAForm  
set Company_PCB_No = VALUE  
from DIVISION_STATUTORY  
where #EAForm.ocp_id = DIVISION_STATUTORY.OCP_ID  
and DIVISION_STATUTORY.STATUTORY_TYPE = 'PCB_No'  
  
update #EAForm  
set Company_Name = VALUE  
from DIVISION_STATUTORY  
where #EAForm.ocp_id = DIVISION_STATUTORY.OCP_ID  
and DIVISION_STATUTORY.STATUTORY_TYPE = 'COMPANY_NAME'  
  
drop table #lastmonth  
   
update #EAForm   
set OTHER_SALARY = EA_FORM_MANUAL.OTHER_SALARY,  
 CAR_DATE = EA_FORM_MANUAL.CAR_DATE,  
 CAR_TYPE = EA_FORM_MANUAL.CAR_TYPE,  
 CAR_YEAR = EA_FORM_MANUAL.CAR_YEAR,  
 CAR_MODEL = EA_FORM_MANUAL.CAR_MODEL,  
 HOUSE_ADDRESS = EA_FORM_MANUAL.HOUSE_ADDRESS  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_1 = EA_FORM_MANUAL.Allowance_1  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_1 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_2 = EA_FORM_MANUAL.Allowance_2  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_2 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_3 = EA_FORM_MANUAL.Allowance_3  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_3 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_4 = EA_FORM_MANUAL.Allowance_4  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_4 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_5 = EA_FORM_MANUAL.Allowance_5  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_5 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_6 = EA_FORM_MANUAL.Allowance_6  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_6 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_7 = EA_FORM_MANUAL.Allowance_7  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_7 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_8 = EA_FORM_MANUAL.Allowance_8  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_8 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_9 = EA_FORM_MANUAL.Allowance_9  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_9 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_10 = EA_FORM_MANUAL.Allowance_10  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_10 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_11 = EA_FORM_MANUAL.Allowance_11  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_11 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_12 = EA_FORM_MANUAL.Allowance_12  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_12 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_13 = EA_FORM_MANUAL.Allowance_13  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_13 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_14 = EA_FORM_MANUAL.Allowance_14  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_14 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_15 = EA_FORM_MANUAL.Allowance_15  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_15 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_16 = EA_FORM_MANUAL.Allowance_16  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_16 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_17 = EA_FORM_MANUAL.Allowance_17  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_17 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_18 = EA_FORM_MANUAL.Allowance_18  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_18 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_19 = EA_FORM_MANUAL.Allowance_19  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_19 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_20 = EA_FORM_MANUAL.Allowance_20  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_20 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_21 = EA_FORM_MANUAL.Allowance_21  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_21 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Allowance_22 = EA_FORM_MANUAL.Allowance_22  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.Allowance_22 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set EPF_EMPLOYEE = EA_FORM_MANUAL.EPF_EMPLOYEE  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.EPF_EMPLOYEE is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set PCB = EA_FORM_MANUAL.PCB  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.PCB is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set CP38 = EA_FORM_MANUAL.CP38  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.CP38 is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set ZAKAT = EA_FORM_MANUAL.ZAKAT  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.ZAKAT is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
  
update #EAForm   
set Join_Date = EA_FORM_MANUAL.JOIN_DATE  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.JOIN_DATE is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
and EA_FORM_MANUAL.JOIN_DATE <> ''  
  
update #EAForm   
set Resign_Date = EA_FORM_MANUAL.RESIGN_DATE  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.RESIGN_DATE is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
and EA_FORM_MANUAL.RESIGN_DATE <> ''  
  
update #EAForm   
set bonus1 = EA_FORM_MANUAL.BONUS_FROM  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.BONUS_FROM is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
and EA_FORM_MANUAL.BONUS_FROM <> ''  
  
update #EAForm   
set bonus2 = EA_FORM_MANUAL.BONUS_TO  
from EA_FORM_MANUAL  
where #EAForm.Employee_profile_id = EA_FORM_MANUAL.employee_profile_ID  
and EA_FORM_MANUAL.year = #EAForm.year  
and EA_FORM_MANUAL.year = @Year  
and EA_FORM_MANUAL.BONUS_TO is not null  
and #EAForm.ocp_id = isnull(EA_FORM_MANUAL.OCP_ID,'')  
and EA_FORM_MANUAL.BONUS_TO <> ''  

declare @maxdocument table(
empid varchar(100),
option_type nvarchar(100),
effective_date nvarchar(14))

insert into @maxdocument
select a.Employee_profile_id,option_type,max(effective_date)
from Employee_Document a, #EAForm b
where a.Employee_Profile_ID = b.Employee_Profile_ID
group by a.Employee_profile_id,option_type

update #EAForm
set Passport = a.DOCUMENT_NO
from Employee_Document a, @maxdocument b
where a.Employee_Profile_ID = b.empid
and a.Option_Type = b.option_type
and a.Effective_Date = b.effective_date
and a.Option_Type = 'PASSPORT'

Update #EAForm    
  Set orgpic = Employee_CodeName_Vw.Name,    
      orgpos = Organisation_Code_Profile_Vw.name     
  From Employee_CodeName_Vw, Organisation_Code_Profile_Vw, Employee_Status
  where Employee_CodeName_Vw.Code = @PIC
  and Employee_Status.Employee_Profile_ID = Employee_CodeName_Vw.Employee_Profile_ID
  and Employee_Status.OCP_ID_Job_Title = Organisation_Code_Profile_Vw.ID

  
Update #EAForm  
set employee_profile_id = dbo.fn_getempcode(employee_profile_id)  
  
if not exists(select * from #EAForm)  
begin  
insert into #EAForm  
select '', '', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '',  
 '', '', '', '', '', '', '', '', '', '', '', '', '', convert(varchar(12), getdate()) as 'cdate',  
 @Year as 'year', @bonus1 as 'bonus1', @bonus2 as 'bonus2', convert(varchar(10),getdate(),103) as 'CurrentDate',  
 '', '', '', '', '', '',  
    '','',0,0,0,'','','','','',0,''
   
 select * from #EAForm  
 end  
else  
select * from #EAForm   
order by left(#EAForm.Serial_No,3) asc, convert(int,substring(#EAForm.Serial_No,4,len(#EAForm.Serial_No)-3)) asc  
  
  
delete from Payroll_EAForm  
where SPID = @SpID   
and (employee_profile_id in (select employee_profile_id from Employee_Group  
where Employee_Group_ID in (  
select Employee_Group_ID from User_Employee_Group where User_Profile_Code = @User_Profile_Code))  
 or employee_profile_id in (select employee_profile_id  
 from User_Profile where Code = @User_Profile_Code))   
