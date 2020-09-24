--alter table Payroll_E8DFORM add TaxPay int
go
Drop proc [dbo].[sp_pr_rptE8D]  
GO
/*  
Alter By : SY Tey  
Alter Date : 05/04/2017 
Version : 7.0  
Problem : New EA 2016.
*/
/*  
Alter By : SY Tey  
Alter Date : 28/02/2017  
Version : 6.0  
Problem : New EA 2016.
*/
/*
Alter By : SY Tey
Alter Date : 26/02/2016
Problem : EA Manual.
*/
/*
Alter By : SY Tey
Alter Date : 08/03/2013
Problem : not include bonus_details.
*/
/*
Alter By : SY Tey
Alter Date : 15/03/2010
Version : 1.0
Problem : F Columns is same as EA Sum allowance 1 until Allowance 19.
*/
/*
Alter By : SY Tey
Alter Date : 24/02/2010
Version : 1.0
Problem : Change the data to become the format at excel.
*/

Create proc [dbo].[sp_pr_rptE8D]  
@Company_Profile_Code nvarchar(50),                
@year    nvarchar(4),                
@PicID   nvarchar(50),                
@type    nvarchar(50),
@OCP_ID nvarchar(50) = '',
@Report nvarchar(50) = ''    
  
as  

if @Report is null
	select @Report = ''

declare  
  
@Count int,  
@Count1 int,  
@Employee_Profile_ID  nvarchar(50),  
@partG          decimal(15,2),  
@CP38           decimal(15,2),  
@grossamt1 decimal(15,2),  
@grossamt2 decimal(15,2),  
@grossamt3 decimal(15,2),  
@grossamt4 decimal(15,2),  
@grossamt5 decimal(15,2),  
@grossamt6 decimal(15,2),  
@grossamt7 decimal(15,2),  
@grossamt8 decimal(15,2),  
@grossamt9 decimal(15,2),  
@grossamt10 decimal(15,2),  
@grossamt11 decimal(15,2),  
@grossamt12 decimal(15,2),  
@tolsalary decimal(15,2),  
@Earn      decimal(15,2),    
@Deduct      decimal(15,2),  
@n int,            
@pgtole decimal (15,2),            
@pgtolf decimal (15,2),   
@pgtolg decimal (15,2),  
@pgtolh decimal (15,2),           
@sume decimal (15,2),            
@sumf decimal(15,2),  
@sumg decimal(15,2),  
@sumh decimal(15,2),
   @Socso     decimal(15,6),
 @TP1		decimal(15,6),
 @TP1Zakat  decimal(15,6),
 @ChildAmt  decimal(15,6),
 @Zakat     decimal(15,2),
 @EPF		decimal(15,2),
 @Bik		decimal(15,2),
 @Vola      decimal(15,2),
 @Category int
  
create table #tempE8D  
( Company_Profile_Code nvarchar(50),  
 Employee_Profile_ID nvarchar(50),  
 empname nvarchar(80),  
 compname nvarchar(50),  
 compcbno nvarchar(15),  
 icno    nvarchar(30),  
 pcbno   nvarchar(30),
 nontaxallw decimal(15,2),  
 pcbamount decimal(15,2),  
 cp38amount decimal(15,2),  
 jan decimal(15,2),  
 feb decimal(15,2),  
 mar decimal(15,2),  
 apr decimal(15,2),  
 may decimal(15,2),  
 june decimal(15,2),  
 july decimal(15,2),  
 aug decimal(15,2),  
 sept decimal(15,2),  
 oct decimal(15,2),  
 nov decimal(15,2),  
 dec decimal(15,2),  
 tolgross decimal(15,2),  
 pagecount  int,
 OCP_ID nvarchar(50),
 Category nvarchar(20),
 noofchild int,
 totalChild int,
 BIK decimal(15,2),
 vola decimal(15,2),
 ESOS decimal(15,2),
  EPF decimal(15,2),
 SOCSO decimal(15,2),
 Zakat decimal(15,2),
  TP1 decimal(15,2),
 TP1Zakat decimal(15,2),
 PayTax int  
)   
 Set Nocount on    
  
Select @Earn   = 0    
Select @Deduct = 0   
select @Count1 = 0  
  
insert into #tempE8D  
select  @Company_Profile_Code,a.Employee_Profile_ID,b.Name,c.Name,d.Value,
case when a.Identity_Card_No = '' then a.Passport_No else a.Identity_Card_No end,a.Pcb_No,0,a.Total_Pcb,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,a.OCP_ID,'',a.no_of_child,0,0,0,0,0,0,0,0,0,2  
from Payroll_EForm a, employee_personal_vw b, Company_Profile c,
Company_Statutory d  
where a.Employee_Profile_ID = b.ID  
and a.Company_Profile_Code = @Company_Profile_Code  
and c.Code = @Company_Profile_Code
and c.code = d.company_profile_code
and d.Option_Type = 'PCB_NO'  
and year = @year  
and Option_Location = 'PENINSULA'
and a.OCP_ID = @OCP_ID
  

select @Count = 1  
  
  DECLARE ptrE8D CURSOR     
  FOR     
  Select Employee_Profile_ID    
    From Payroll_EForm    
    where year = @year  
    order by Serial_No,Employee_Profile_ID  
    
    
  OPEN ptrE8D    
  FETCH NEXT FROM ptrE8D INTO @Employee_Profile_ID    
    WHILE (@@fetch_status <> -1)    
    Begin    
 if (@@fetch_status <> -2)    
    Begin    
   select @Socso     =0,
 @TP1		=0,
 @TP1Zakat  =0,
 @ChildAmt  =0,
 @zakat = 0,
 @EPF = 0,
 @Bik = 0,
 @Vola = 0,
 @Category = 0
  
select @grossamt1 = isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)               
  From Employee_Payroll_Details , Payroll_EAReport_Field             
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '01'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
  And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary   
  
  select @grossamt1 = @grossamt1 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 1

Select @grossamt2 = isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Payroll_Details , Payroll_EAReport_Field
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '02'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  select @grossamt2 = @grossamt2 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 2

Select @grossamt3 = isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Payroll_Details , Payroll_EAReport_Field
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '03'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  select @grossamt3 = @grossamt3 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 3

Select @grossamt4 = isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Payroll_Details , Payroll_EAReport_Field
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '04'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  select @grossamt4 = @grossamt4 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 4

Select @grossamt5 = isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Payroll_Details , Payroll_EAReport_Field
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '05'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  select @grossamt5 = @grossamt5 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 5

Select @grossamt6 = isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Payroll_Details , Payroll_EAReport_Field
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '06'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  select @grossamt6 = @grossamt6 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 6

Select @grossamt7 = isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Payroll_Details , Payroll_EAReport_Field
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '07'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  select @grossamt7 = @grossamt7 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 7

Select @grossamt8 = isnull(sum(Employee_Debit),0)  - isnull(sum(Employee_Credit),0)                              
  From Employee_Payroll_Details , Payroll_EAReport_Field
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '08'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  select @grossamt8 = @grossamt8 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 8

Select @grossamt9 = isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Payroll_Details , Payroll_EAReport_Field
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '09'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  select @grossamt9 = @grossamt9 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 9

Select @grossamt10 = isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Payroll_Details , Payroll_EAReport_Field
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '10'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  select @grossamt10 = @grossamt10 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 10

Select @grossamt11 = isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Payroll_Details , Payroll_EAReport_Field
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '11'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  select @grossamt11 = @grossamt11 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 11

Select @grossamt12 = isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Payroll_Details , Payroll_EAReport_Field
  Where Employee_Payroll_Details.year  = @Year                 
  And   Employee_Payroll_Details.month = '12'  
  and   Employee_Payroll_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  select @grossamt12 = @grossamt12 + case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and convert(int,month) = 12

select @grossamt1 = @grossamt1 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)               
  From Employee_Bonus_Details , Payroll_EAReport_Field             
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '01'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
  And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary   
  
Select @grossamt2 = @grossamt2 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Bonus_Details , Payroll_EAReport_Field
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '02'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
Select @grossamt3 = @grossamt3 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Bonus_Details , Payroll_EAReport_Field
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '03'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
Select @grossamt4 = @grossamt4 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Bonus_Details , Payroll_EAReport_Field
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '04'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
Select @grossamt5 = @grossamt5 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Bonus_Details , Payroll_EAReport_Field
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '05'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
Select @grossamt6 = @grossamt6 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Bonus_Details , Payroll_EAReport_Field
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '06'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
Select @grossamt7 = @grossamt7 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Bonus_Details , Payroll_EAReport_Field
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '07'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
Select @grossamt8 = @grossamt8 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Bonus_Details , Payroll_EAReport_Field
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '08'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
Select @grossamt9 = @grossamt9 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Bonus_Details , Payroll_EAReport_Field
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '09'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
Select @grossamt10 = @grossamt10 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Bonus_Details , Payroll_EAReport_Field
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '10'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
Select @grossamt11 = @grossamt11 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Bonus_Details , Payroll_EAReport_Field
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '11'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
Select @grossamt12 = @grossamt12 + isnull(sum(Employee_Debit),0) - isnull(sum(Employee_Credit),0)                               
  From Employee_Bonus_Details , Payroll_EAReport_Field
  Where Employee_Bonus_Details.year  = @Year                 
  And   Employee_Bonus_Details.month = '12'  
  and   Employee_Bonus_Details.Employee_Profile_ID  = @Employee_Profile_ID
And   Payroll_EAReport_Field.Company_Profile_Code  = @Company_Profile_Code          
    And   Payroll_EAReport_Field.Option_Field  in ('1','2','3','4','5','6','7','8','9','10',
   '11','12','13')
    And   Employee_Bonus_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary     
  
  
update #tempE8D set jan = @grossamt1 where Employee_Profile_ID = @Employee_Profile_ID   
update #tempE8D set feb = @grossamt2 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set mar = @grossamt3 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set apr = @grossamt4 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set may = @grossamt5 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set june = @grossamt6 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set july = @grossamt7 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set aug = @grossamt8 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set sept = @grossamt9 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set oct = @grossamt10 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set nov = @grossamt11 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set dec = @grossamt12 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set pagecount = @Count where Employee_Profile_ID = @Employee_Profile_ID  
  
select @tolsalary = @grossamt1 + @grossamt2 + @grossamt3 + @grossamt4 + @grossamt5 + @grossamt6 +   
@grossamt7 + @grossamt8 + @grossamt9 + @grossamt10 + @grossamt11 + @grossamt12  
  
update #tempE8D set tolgross = @tolsalary where Employee_Profile_ID = @Employee_Profile_ID  
  
-- CP38 amount  
  
Select @CP38 = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0) From Employee_Payroll_Details     
Where ocp_id_salary in (
select ocp_id_salary 
from salary_Profile
where option_type = 'CP38'
and ocp_id_salary in (
select id from organisation_code_profile_vw
where company_profile_Code = @Company_Profile_Code))
And   Employee_Profile_ID = @Employee_Profile_ID    
And   year = @Year  

Select @socso = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0) From Employee_Payroll_Details     
Where ocp_id_salary in (
select ocp_id_salary 
from salary_Profile
where option_type = 'SOCSO_EMPLOYEE'
and ocp_id_salary in (
select id from organisation_code_profile_vw
where company_profile_Code = @Company_Profile_Code))
And   Employee_Profile_ID = @Employee_Profile_ID    
And   year = @Year

Select @EPF = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0) From Employee_Payroll_Details     
Where ocp_id_salary in (
select ocp_id_salary 
from salary_Profile
where option_type = 'EPF_EMPLOYEE'
and ocp_id_salary in (
select id from organisation_code_profile_vw
where company_profile_Code = @Company_Profile_Code))
And   Employee_Profile_ID = @Employee_Profile_ID    
And   year = @Year

Select @EPF = @EPF + isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0) From Employee_Bonus_Details     
Where ocp_id_salary in (
select ocp_id_salary 
from salary_Profile
where option_type = 'EPF_EMPLOYEE'
and ocp_id_salary in (
select id from organisation_code_profile_vw
where company_profile_Code = @Company_Profile_Code))
And   Employee_Profile_ID = @Employee_Profile_ID    
And   year = @Year

Select @zakat = isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0) From Employee_Payroll_Details     
Where ocp_id_salary in (
select ocp_id_salary 
from salary_Profile
where option_type = 'ZAKAT'
and ocp_id_salary in (
select id from organisation_code_profile_vw
where company_profile_Code = @Company_Profile_Code))
And   Employee_Profile_ID = @Employee_Profile_ID    
And   year = @Year

select @TP1 =  isnull(sum(Amount),0)
	from Employee_PCB_Deduction
	where employee_profile_id = @Employee_Profile_ID
	and year = @Year
	and PCB_DEDUCTION_PROFILE_CODE <> 'PBS13'

	select @TP1Zakat =  isnull(sum(Amount),0)
	from Employee_PCB_Deduction
	where employee_profile_id = @Employee_Profile_ID
	and year = @Year
	and PCB_DEDUCTION_PROFILE_CODE = 'PBS13'
	  
	  declare @maxMonth int,@NoOfChild int
   select @maxMonth = max(month)
   from Employee_PCB_Formula_Value
   where Employee_Profile_ID = @Employee_Profile_ID      
   and year = @Year

   select @NoOfChild = Child_Below + Child_Diploma + Child_Disable + Child_Disable_Diploma, @ChildAmt = C,
   @Category =  case when Marital_Status = 'SINGLE' then 1
   when S > 0 then 2
   else 3 end
   from Employee_PCB_Formula_Value
   where Employee_Profile_ID = @Employee_Profile_ID      
   and year = @Year
    and month = @maxMonth

	select @Bik = case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and option_type like 'BIK%' 

	select @Vola = case when isnull(SUM(amount),0) is null then 0 else isnull(SUM(amount),0) end
   from BENEFITS_IN_KIND where COMPANY_PROFILE_CODE = @Company_Profile_Code
   and EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
    and YEAR = @year   
	and option_type like 'VOLA%' 

update #tempE8D set cp38amount = @CP38 where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set socso = @Socso where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set epf = @EPF where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set zakat = @zakat where Employee_Profile_ID = @Employee_Profile_ID  
update #tempE8D set TP1 = @TP1 where Employee_Profile_ID = @Employee_Profile_ID 
update #tempE8D set TP1zakat = @TP1Zakat where Employee_Profile_ID = @Employee_Profile_ID 
update #tempE8D set noofchild = @NoOfChild,totalChild=@ChildAmt,category=@Category where Employee_Profile_ID = @Employee_Profile_ID 
update #tempE8D set bik = @Bik,vola=@Vola where Employee_Profile_ID = @Employee_Profile_ID 
update #tempE8D set PayTax = 1 from EMPLOYEE_REMARKS where REMARK1 = 'PCB' and #tempE8D.Employee_Profile_ID = @Employee_Profile_ID  
and #tempE8D.Employee_Profile_ID = EMPLOYEE_REMARKS.EMPLOYEE_PROFILE_ID
  
-- Part G amount  
  
/***** Alter By SY Tey On 07/01/2010 *****/
  select b.pcb_exclude_code, a.limit_Amount, isnull(sum(Convert(decimal(18,2),c.Employee_Debit)),0) as "Total" into #PCBY1
	from pcb_exclude a, pcb_exclude_details b, Employee_Payroll_Details c
	where a.code = b.pcb_exclude_code and b.ocp_id_salary = c.ocp_id_salary
	   and Employee_Profile_ID = @Employee_Profile_ID      
       and   year      = @year                  
	group by b.pcb_exclude_code, a.limit_Amount  

   select @tolsalary = @tolsalary + isnull(sum(Total - limit_amount),0)
   from #PCBY1
   where Total - limit_amount > 0
   and limit_amount > 0

   select @partG = isnull(sum(limit_amount),0)
   from #PCBY1
   where Total - limit_amount > 0
   and limit_amount > 0

   select @partG = @partG + isnull(Sum(Total),0)
   from #PCBY1
   where Total - limit_amount <= 0
   and limit_amount <> 0

   select @partG = @partG + isnull(Sum(Total),0)
   from #PCBY1
   where limit_amount = 0

   drop table #PCBY1

   select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_1 is not null )--and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_1 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_1 is not null --and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '1'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_2 is not null)-- and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_2 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_2 is not null --and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '2'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_3 is not null )--and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_3 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_3 is not null --and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '3'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_4 is not null)-- and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_4 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_4 is not null-- and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '4'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_5 is not null)-- and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_5 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_5 is not null --and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '5'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_6 is not null)-- and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_6 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_6 is not null-- and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '6'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_7 is not null)-- and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_7 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_7 is not null-- and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '7'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_8 is not null)-- and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_8 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_8 is not null-- and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '8'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   update #tempE8D set BIK = @Earn where Employee_Profile_ID = @Employee_Profile_ID
   select @tolsalary = @tolsalary - @Deduct + @Earn - @Bik
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_9 is not null)-- and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_9 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_9 is not null-- and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '9'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   update #tempE8D set vola = @Earn where Employee_Profile_ID = @Employee_Profile_ID
   select @tolsalary = @tolsalary - @Deduct + @Earn - @Vola
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_10 is not null )--and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_10 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_10 is not null --and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '10'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_11 is not null)-- and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_11 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_11 is not null-- and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '11'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_12 is not null )--and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_12 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_12 is not null --and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '12'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_13 is not null )--and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_13 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_13 is not null--and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '13'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_14 is not null )--and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_14 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_14 is not null --and (OCP_ID = @OCP_ID or @OCP_ID = '')

	select @Socso = @Earn
	--Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
 --   From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
 --   Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
 --   And   Payroll_EAReport_Field.Option_Field  = '14'              
 --   And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
 --   And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
 --   And   Employee_Payroll_Details.year = @Year  
 --   and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
 --  and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
 --  and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
 --  and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   --select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_15 is not null )--and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_15 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_15 is not null --and (OCP_ID = @OCP_ID or @OCP_ID = '')

	--Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
 --   From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
 --   Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
 --   And   Payroll_EAReport_Field.Option_Field  = '15'              
 --   And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
 --   And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
 --   And   Employee_Payroll_Details.year = @Year  
 --   and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
 --  and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
 --  and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
 --  and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
 --  --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
 --  select @tolsalary = @tolsalary - @Deduct + @Earn
 select @TP1 = @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_16 is not null )--and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_16 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_16 is not null --and (OCP_ID = @OCP_ID or @OCP_ID = '')

	--Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
 --   From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
 --   Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
 --   And   Payroll_EAReport_Field.Option_Field  = '16'              
 --   And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
 --   And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
 --   And   Employee_Payroll_Details.year = @Year  
 --   and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
 --  and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
 --  and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
 --  and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
 --  --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
 --  select @tolsalary = @tolsalary - @Deduct + @Earn
 select @TP1Zakat = @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_17 is not null )--and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_17 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_17 is not null --and (OCP_ID = @OCP_ID or @OCP_ID = '')

	--Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
 --   From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
 --   Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
 --   And   Payroll_EAReport_Field.Option_Field  = '17'              
 --   And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
 --   And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
 --   And   Employee_Payroll_Details.year = @Year  
 --   and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
 --  and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
 --  and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
 --  and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
 --  --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
 --  select @tolsalary = @tolsalary - @Deduct + @Earn
	select @ChildAmt = @Earn
end
/*
select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_18 is not null )--and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_18 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_18 is not null--and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '18'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end

select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_19 is not null)-- and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_19 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_19 is not null-- and (OCP_ID = @OCP_ID or @OCP_ID = '')

	Select @Deduct = isnull(sum(Employee_Debit),0) - (isnull(sum(Employee_Credit),0) + isnull(sum(Company_Credit),0))      
    From Employee_Payroll_Details, Payroll_EAReport_Field, Employee_Profile_Vw,Employee_Payroll_Header      
    Where Employee_Profile_Vw.ID = Employee_Payroll_Details.Employee_Profile_ID                                    
    And   Payroll_EAReport_Field.Option_Field  = '19'              
    And   Employee_Payroll_Details.OCP_ID_Salary = Payroll_EAReport_Field.OCP_ID_Salary              
    And   Employee_Payroll_Details.Employee_Profile_ID = @Employee_Profile_ID              
    And   Employee_Payroll_Details.year = @Year  
    and Employee_Payroll_Details.Employee_Profile_ID = Employee_Payroll_Header.Employee_Profile_ID  
   and Employee_Payroll_Details.Month = Employee_Payroll_Header.Month  
   and Employee_Payroll_Details.Year = Employee_Payroll_Header.Year  
   and Employee_Payroll_Details.Option_Pay_Cycle = Employee_Payroll_Header.Option_Pay_Cycle  
   --and (Employee_Payroll_Header.OCP_ID_Division = @OCP_ID or @OCP_ID = '' or @OCP_ID is null)
   
   select @tolsalary = @tolsalary - @Deduct + @Earn
end
*/
select @Earn = 0,@Deduct = 0
if exists(select * from EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_22 is not null )--and (OCP_ID = @OCP_ID or @OCP_ID = ''))
begin
	select @Earn = Allowance_22 from  EA_FORM_MANUAL where EMPLOYEE_PROFILE_ID = @Employee_Profile_ID
and YEAR = @year and Allowance_22 is not null --and (OCP_ID = @OCP_ID or @OCP_ID = '')

   select @partG = @Earn
end
   /***** Alter End By SY Tey On 07/01/2010 *****/
update #tempE8D set nontaxallw = @partG where Employee_Profile_ID = @Employee_Profile_ID 
update #tempE8D set tolgross = @tolsalary where Employee_Profile_ID = @Employee_Profile_ID 
    
  Select @Count = @Count + 1    
    
  FETCH NEXT FROM ptrE8D INTO @Employee_Profile_ID    
     End    
    End    
  CLOSE ptrE8D    
  DEALLOCATE ptrE8D   
  
delete from Payroll_E8DFORM where Company_Profile_Code = @Company_Profile_Code and year = @Year
if @year < 2012  
insert into Payroll_E8DFORM  
select Company_Profile_Code,Employee_Profile_ID,empname,icno,pcbno,nontaxallw,pcbamount,
cp38amount,@year,compname,compcbno,jan,feb,mar,apr,may,june,july,aug,sept,oct,nov,dec,tolgross,0,ocp_id,
category,noofchild,totalchild, BIK, vola, ESOS,EPF,SOCSO,Zakat,TP1,TP1Zakat,PayTax
from #tempE8D  
where jan > 2500   
or feb > 2500  
or mar > 2500  
or apr > 2500  
or may > 2500  
or june > 2500  
or july > 2500  
or aug > 2500  
or sept > 2500  
or oct > 2500  
or nov > 2500  
or dec > 2500  
or tolgross > 30000   
else if @year < 2016
insert into Payroll_E8DFORM  
select Company_Profile_Code,Employee_Profile_ID,empname,icno,pcbno,nontaxallw,pcbamount,cp38amount,
@year,compname,compcbno,jan,feb,mar,apr,may,june,july,aug,sept,oct,nov,dec,tolgross,0,ocp_id,
category,noofchild,totalchild, BIK, vola, ESOS,EPF,SOCSO,Zakat,TP1,TP1Zakat,PayTax
 from #tempE8D  
where jan > 2800   
or feb > 2800  
or mar > 2800  
or apr > 2800  
or may > 2800  
or june > 2800  
or july > 2800  
or aug > 2800  
or sept > 2800  
or oct > 2800  
or nov > 2800  
or dec > 2800  
or tolgross > 34000 
else
insert into Payroll_E8DFORM  
select Company_Profile_Code,Employee_Profile_ID,empname,icno,pcbno,nontaxallw,pcbamount,cp38amount,
@year,compname,compcbno,jan,feb,mar,apr,may,june,july,aug,sept,oct,nov,dec,tolgross,0,ocp_id,
category,noofchild,totalchild, BIK, vola, ESOS,EPF,SOCSO,Zakat,TP1,TP1Zakat,PayTax
 from #tempE8D     
--select * into #page from Payroll_E8DFORM            
--            
--  select @Count1 = 1            
--            
--  set rowcount 22            
--            
--  WHILE (select count(*) from #page) <> 0            
--  begin            
--    set rowcount 22            
--    update Payroll_E8DFORM            
--    set Payroll_E8DFORM.pageno = @Count1            
--    from #page, Payroll_E8DFORM            
--    where #page.Employee_Profile_ID = Payroll_E8DFORM.Employee_Profile_ID            
--             
--    set rowcount 0            
--    delete from #page            
--      from Payroll_E8DFORM            
--      Where #page.Employee_Profile_ID = Payroll_E8DFORM.Employee_Profile_ID             
--      And   Payroll_E8DFORM.year = @Year            
--      And   Payroll_E8DFORM.pageno > 0            
--    set rowcount 22            
--    select @Count1 = @Count1 + 1            
--             
--    CONTINUE            
--  end            
--            
--  set rowcount 0    
------------------------------------------------------------  
--  delete from Payroll_E8DFORMSUM where year = @year  
--  
--  insert into Payroll_E8DFORMSUM            
--  select Company_Profile_Code, pageno,sum(tolgross),sum(nontaxallw),sum(pcbamount),sum(cp38amount),0,0,0,0,0,0,0,0,@year             
--  from Payroll_E8DFORM            
--  where Company_Profile_Code = @Company_Profile_Code            
--  and year = @year            
--  group by Company_Profile_Code, pageno            
--            
--  select @n = 0            
--            
--  while @n <= (select max(pageno) from Payroll_E8DFORMSUM where Company_Profile_Code = @Company_Profile_Code and year = @year)            
--  begin            
--    select @sume = isnull(sumGrossSalary,0),@sumf = isnull(sumEAPartG,0),@sumg = isnull(sumPCB,0),@sumh = isnull(sumCP38,0)  
--    from Payroll_E8DFORMSUM             
--    where Company_Profile_Code = @Company_Profile_Code and pageno = @n and year = @year            
--            
--    if @sume IS NULL            
--    begin            
--      select @sume = 0            
--      select @sumf = 0   
--      select @sumg = 0  
--      select @sumh = 0           
--    end            
--            
--    update Payroll_E8DFORMSUM            
--    set pagetotalGrossSalary = @sume,            
--        pagetotalEAPartG = @sumf,      
-- pagetotalPCB = @sumg,  
-- pagetotalCP38 = @sumh        
--    where Company_Profile_Code = @Company_Profile_Code             
--    and pageno = @n            
--    and year = @year            
--             
--    if @n > 1             
--    begin            
--      select @sume = isnull(sumGrossSalary,0),@sumf = isnull(sumEAPartG,0),@sumg = isnull(sumPCB,0),@sumh = isnull(sumCP38,0)  
--      from Payroll_E8DFORMSUM             
--      where Company_Profile_Code = @Company_Profile_Code             
--      and pageno = @n            
--      and year = @year            
--            
--      select @pgtole = isnull(pagetotalGrossSalary,0),@pgtolf = isnull(pagetotalEAPartG,0),  
--      @pgtolg = isnull(pagetotalPCB,0),@pgtolh = isnull(pagetotalCP38,0)  
--      from Payroll_E8DFORMSUM             
--      where Company_Profile_Code = @Company_Profile_Code             
--      and pageno = @n - 1            
--      and year = @year            
--            
--      select @pgtole = @pgtole + @sume            
--      select @pgtolf = @pgtolf + @sumf  
--      select @pgtolg = @pgtolg + @sumg  
--      select @pgtolh = @pgtolh + @sumh            
--            
--      select @sume = isnull(pagetotalGrossSalary,0), @sumf = isnull(pagetotalEAPartG,0),   
--        @sumg = isnull(pagetotalPCB,0), @sumh = isnull(pagetotalCP38,0)   
--      from Payroll_E8DFORMSUM             
--      where Company_Profile_Code = @Company_Profile_Code             
--      and pageno = @n - 1            
--      and year = @year            
--            
--      select @suma,@sumb,@v, @vv,@n            
--            
--      update Payroll_E8DFORMSUM            
--      set nextpagetotalGrossSalary = @sume,            
--          nextpagetotalEAPartG = @sumf,  
--   nextpagetotalPCB = @sumg,            
--          nextpagetotalCP38 = @sumh,             
--          pagetotalGrossSalary = @pgtole,   
--          pagetotalEAPartG = @pgtolf,  
--          pagetotalPCB = @pgtolg,           
--          pagetotalCP38 = @pgtolh            
--      where Company_Profile_Code = @Company_Profile_Code             
--      and pageno = @n            
--      and year = @year            
--    end            
--            
--    select @n = @n + 1            
--    continue            
--  end   
if @Report = ''
begin   
if @type = 'HEADER'
	select distinct Company_Profile_Code,convert(varchar(80),comname) as 'comname',replace(replace(compcbno,'-',''),'E','') as 'compcbno' from Payroll_E8DFORM
	where Company_Profile_Code = @Company_Profile_Code and year = @Year
	and ocp_id = @OCP_ID
else
	select ROW_NUMBER() over(order by empname) as 'Row',company_profile_Code,
	employee_profile_id,
	convert(varchar(60),empname) as 'empname',replace(icno,'-','') as 'icno',
	replace(replace(replace(replace(replace(replace(replace(replace(replace(pcbno,'N',''),'G',''),'I',''),'L',''),'S',''),'O',''),'-',''),'(',''),')','') as 'pcbno',
	parsename(nontaxallw,2) as 'nontaxallw',
    pcbamount as 'pcbamount',
    cp38amount as 'cp38amount',
	year,convert(varchar(80),comname) as 'comname',replace(replace(compcbno,'-',''),'E','') as 'compcbno',
	jan,feb,mar,apr,may,june,july,aug,sept,oct,nov,dec,
    parsename(tolgross,2) as 'tolgross',pageno,
	category,noofchild,totalchild, parsename(BIK,2) as 'BIK', parsename(vola,2) as 'vola', parsename(ESOS,2) as 'ESOS',
	parsename(EPF,2) as 'EPF',SOCSO,
	Zakat,parsename(TP1,2) as 'TP1',TP1Zakat,TaxPay
    from Payroll_E8DFORM
	where Company_Profile_Code = @Company_Profile_Code and year = @Year
	and ocp_id = @OCP_ID
	order by empname
end  
else
if @Report = 'TXT1'
begin
select distinct convert(varchar(10),Rtrim(ltrim(replace(replace(compcbno,'E',''),'-',''))))
--+'|'+convert(varchar(80),comname)+'|'+convert(varchar(4),year)
from Payroll_E8DFORM
where company_profile_code = @Company_Profile_Code
and Year = @year
and OCP_ID = @OCP_ID

end
else
if @Report = 'TXT2'
begin
select convert(varchar(60),empname)+'|'+convert(varchar(11),replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(pcbno,'N',''),'G',''),'I',''),'L',''),'S',''),'O',''),'-',''),'(',''),')',''),'E',''),' ',''))+
'|'+CONVERT(varchar(12),replace(isnull(icno,''),'-',''))+'|'+Category+'|'+convert(nvarchar,TaxPay)+'|'+convert(nvarchar,noofchild)+'|'+
convert(nvarchar,totalChild)+'|'+CONVERT(varchar(11),parsename(tolgross,2))+'|'+
CONVERT(varchar(11),case when parsename(BIK,2) = 0 then '' else parsename(BIK,2) end)+'|'+
CONVERT(varchar(11),case when parsename(vola,2)= 0 then '' else parsename(vola,2) end)+'|'+
CONVERT(varchar(11),case when parsename(ESOS,2)= 0 then '' else parsename(ESOS,2) end)+'|'+
CONVERT(varchar(11),parsename(nontaxallw,2))+'|'+
CONVERT(varchar(11),case when parsename(TP1,2)= 0 then '' else parsename(TP1,2) end)+'|'+
CONVERT(varchar(11),case when CONVERT(varchar(11),convert(decimal(11,2),TP1Zakat))= '0.00' then '' else CONVERT(varchar(11),convert(decimal(11,2),TP1Zakat)) end)+'|'+
CONVERT(varchar(11),parsename(EPF,2))+'|'+
CONVERT(varchar(11),case when CONVERT(varchar(11),convert(decimal(11,2),Zakat))= '0.00' then '' else CONVERT(varchar(11),convert(decimal(11,2),Zakat)) end)+'|'+
CONVERT(varchar(11),convert(decimal(11,2),pcbamount))+'|'+
case when CONVERT(varchar(11),convert(decimal(11,2),cp38amount)) = '0.00' then '' else CONVERT(varchar(11),convert(decimal(11,2),cp38amount)) end
from Payroll_E8DFORM
where company_profile_code = @Company_Profile_Code
and Year = @year
and OCP_ID = @OCP_ID
order by empname
end    
  
  
  




