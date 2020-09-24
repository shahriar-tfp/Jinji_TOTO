Drop proc [dbo].[sp_pr_RptEPCB2]  
go

/*
Alter By : SY Tey
Alter Date : 29/02/2016
Version : 1.0
Problem : use Division.
*/
/*
Alter By : SY Tey
Alter Date : 26/02/2016
Version : 2.0
Problem : New PCB 2012
*/
/*
Alter By : SY Tey
Alter Date : 29/09/2011
Version : 1.0
Problem : New PCB 2010
*/
/*
Alter By : SY Tey
Alter Date : 03/03/2011
Version : 1.0
Problem : Nees Filter by User.
*/
Create proc [dbo].[sp_pr_RptEPCB2]    
@Company_Profile_Code       nvarchar(50),    
@Year        int,    
@SpID        int,
@Location    nvarchar(50),
@Type		 nvarchar(50),
@User_Profile_Code nvarchar(50) = '',
@PIC		 nvarchar(50) = ''
  
        
As    
  Declare @Employee_Profile_ID     nvarchar(50),    
          @EmpNm     nvarchar(100),    
          @Nric      nvarchar(50),    
          @EmpPcbNo  nvarchar(50),          
          @Mon       int,    
          @Yr        int,    
          @StdDt     datetime,    
          @Amt       decimal(15,2),    
          @Count     int,
          @tolmon  int,
          @OCP_ID nvarchar(50)
                  
      
  Create table #pr_rptpcb2    
  (    
   Employee_Profile_ID      nvarchar(50),    
   empnm      nvarchar(100),    
   nric       nvarchar(50),    
   emppcbno   nvarchar(50),
   OCP_ID     nvarchar(50),    
   year       int,
   month      int,
   PCBamt		  decimal(15,2),
   CP38amt		  decimal(15,2),
   recp		  nvarchar(50),
   dt		  datetime,    
   orgpic     nvarchar(100),    
   orgpos     nvarchar(100),    
   orgpcbno   nvarchar(50),    
   orgtel     nvarchar(50),    
   govnm1     nvarchar(50),    
   govnm2     nvarchar(50),    
   govaddr1   nvarchar(80),    
   govaddr2   nvarchar(80),    
   govpostcd  nvarchar(5),    
   govcity    nvarchar(50),    
   govstate   nvarchar(50),    
   govpic     nvarchar(80),
   orgname    nvarchar(100),
   PreAllowance nvarchar(500),
   PreMonth nvarchar(2),
   PreYear nvarchar(4),
   CP39A decimal(15,2),
   PreReceipt nvarchar(100),
   PreDate datetime      
  )          
Declare @Caw nvarchar(50)

select @PIC = Person_In_Charge,@Caw = Name2
from Government_Location
where company_profile_code = @Company_Profile_Code
and option_type = 'PCB2'
      
  Select @StdDt = convert(datetime,'01/01/1900',103),
  @tolmon = 1    
       
  create table #Empgroup(
  employee_profile_id nvarchar(50))

  if @Type = 'ADD'
	insert into #Empgroup
	select control_id from Report_Control_Temp where Report_ID = @SpID and User_Profile_Code = @User_Profile_Code
	and (Control_ID in (select employee_profile_id from Employee_Group
where Employee_Group_ID in (
select Employee_Group_ID from User_Employee_Group where User_Profile_Code = @User_Profile_Code))
	or Control_ID in (select employee_profile_id
	from User_Profile where Code = @User_Profile_Code))

  if @Type = 'DEPARTMENT'
	insert into #EmpGroup
	select employee_profile_id from employee_status where ocp_id_department in (
	select control_id from Report_Control_Temp where Report_ID = @SpID and User_Profile_Code = @User_Profile_Code)
	and (employee_profile_id in (select employee_profile_id from Employee_Group
where Employee_Group_ID in (
select Employee_Group_ID from User_Employee_Group where User_Profile_Code = @User_Profile_Code))
	or employee_profile_id in (select employee_profile_id
	from User_Profile where Code = @User_Profile_Code))

  if @Type = 'DIVISION'
	insert into #EmpGroup
	select employee_profile_id from employee_status where ocp_id_Division in (
	select control_id from Report_Control_Temp where Report_ID = @SpID and User_Profile_Code = @User_Profile_Code)
	and (employee_profile_id in (select employee_profile_id from Employee_Group
where Employee_Group_ID in (
select Employee_Group_ID from User_Employee_Group where User_Profile_Code = @User_Profile_Code))
	or employee_profile_id in (select employee_profile_id
	from User_Profile where Code = @User_Profile_Code))

	delete from #EmpGroup
where Employee_profile_id not in (
select employee_profile_id from publish_EA
where year = @year and Option_Block = 'YES')

  Declare PrDetailPointer Cursor     
  For Select Employee_payroll_Details.Employee_Profile_ID,Employee_Codename_Vw.Name,    
        case     
          when Employee_Profile.Option_Citizenship = String_Value1 then    
                Identity_Card_No + space(5) + '( ' +  Old_Identity_Card_No + ' )'    
          else    
            isnull(DOCUMENT_NO,'')
          end nric,       
        Employee_Statutory.Value,Employee_payroll_Details.month,Employee_payroll_Details.year,convert(decimal(15,2),sum(Employee_Credit + Company_Credit )) amt,
		employee_payroll_header.OCP_ID_DIVISION     
        From Employee_payroll_Details
		left outer join Employee_Document 
		on Employee_Document.Employee_Profile_ID = Employee_payroll_Details.Employee_Profile_ID
		and Employee_Document.Option_Type = 'PASSPORT',
	    Employee_Profile,Parameter,Employee_Statutory,employee_payroll_header, Employee_Codename_Vw
 Where Employee_payroll_Details.OCP_ID_Salary 
		in (select OCP_ID_Salary from Salary_Profile where option_Type = 'PCB')
        And Employee_payroll_Details.year = @Year    
		and Employee_payroll_Details.employee_profile_id = employee_payroll_header.employee_profile_id
        and Employee_payroll_Details.month = employee_payroll_header.month
        and Employee_payroll_Details.year = employee_payroll_header.year
        and Employee_payroll_Details.option_pay_cycle = employee_payroll_header.option_pay_cycle
        And Employee_payroll_Details.Employee_Profile_ID in (select Employee_Profile_ID From #Empgroup)    
        And Employee_payroll_Details.Employee_Profile_ID = Employee_Profile.ID     
        And Employee_Profile.ID = Employee_Statutory.Employee_Profile_ID      
		And Employee_Profile.ID = Employee_Codename_Vw.Employee_Profile_ID  
        And Parameter.Company_Profile_Code = @Company_Profile_Code
        and Employee_Statutory.Option_Type_Statutory = 'PCB'
		and Employee_Statutory.Option_Location = @Location      
        And Parameter.Code = 'CITI'    
        and Employee_Credit + Company_Credit > 0  
        Group by Employee_payroll_Details.Employee_Profile_ID,Employee_Codename_Vw.Name,Identity_Card_No,
		Old_Identity_Card_No,Employee_Profile.Option_Citizenship,String_Value1,DOCUMENT_NO,Employee_Statutory.Value,
		Employee_payroll_Details.month,Employee_payroll_Details.year     ,
		employee_payroll_header.OCP_ID_DIVISION
                                  
  Open PrDetailPointer    
  Fetch next from PrDetailPointer Into @Employee_Profile_ID,@EmpNm,@Nric,@EmpPcbNo,@Mon,@Yr,@Amt,@OCP_ID    
               
  While (@@fetch_status <> -1)    
  Begin        
    if (@@fetch_status <> -2)     
     Begin    
         Select @Count = count(Employee_Profile_ID) From #pr_rptpcb2 Where Employee_Profile_ID = @Employee_Profile_ID
         and year = @Yr  and ocp_id = @OCP_ID
               
         If @Count = 0
         begin  
		   select @tolmon = 1
		   while @tolmon < 13
		   begin  
           Insert into #pr_rptpcb2(Employee_Profile_ID,empnm,nric,emppcbno,OCP_ID,year,month,PCBamt,CP38amt,recp,dt,
           orgpic,orgpos,orgpcbno,orgtel,govnm1,                  
                 govnm2,govaddr1,govaddr2,govpostcd,govcity,govstate,govpic,orgName,
                 PreAllowance,PreMonth , PreYear ,CP39A ,PreReceipt ,PreDate)    
           Values(@Employee_Profile_ID,@EmpNm,@Nric,@EmpPcbNo,@OCP_ID,@Yr,@tolmon,0,0,'',@StdDt,'','','','','',
           '','','','','','','',@Company_Profile_Code,'','','',0,'','')
           
           select @tolmon = @tolmon + 1      
           end
         end
                     
           Update #pr_rptpcb2 Set PCBamt = @Amt Where Employee_Profile_ID = @Employee_Profile_ID 
           and month = @Mon and year = @Yr and ocp_id = @OCP_ID
        
            
       End                 
    Fetch next from PrDetailPointer Into @Employee_Profile_ID,@EmpNm,@Nric,@EmpPcbNo,@Mon,@Yr,@Amt ,@OCP_ID          
  End            
                          
  Close PrDetailPointer    
  Deallocate PrDetailPointer 
  
  Declare PrDetailPointer Cursor     
  For Select Employee_bonus_Details.Employee_Profile_ID,Employee_CodeName_Vw.Name,    
        case     
          when Employee_Profile.Option_Citizenship = String_Value1 then    
                Identity_Card_No + space(5) + '( ' +  Old_Identity_Card_No + ' )'    
          else    
            isnull(DOCUMENT_NO,'')
          end nric,       
        Employee_Statutory.Value,Employee_bonus_Details.month,Employee_bonus_Details.year,convert(decimal(15,2),sum(Employee_Credit + Company_Credit )) amt     ,
		Employee_bonus_Header.OCP_ID_DIVISION
        From Employee_bonus_Details
		left outer join Employee_Document 
		on Employee_Document.Employee_Profile_ID = Employee_bonus_Details.Employee_Profile_ID
		and Employee_Document.Option_Type = 'PASSPORT',
	    Employee_Profile,Parameter,Employee_Statutory,Employee_bonus_Header, Employee_CodeName_Vw
 Where Employee_bonus_Details.OCP_ID_Salary 
		in (select OCP_ID_Salary from Salary_Profile where option_Type = 'PCB')
        And Employee_bonus_Details.year = @Year    
		and Employee_bonus_Details.employee_profile_id = Employee_bonus_Header.employee_profile_id
        and Employee_bonus_Details.month = Employee_bonus_Header.month
        and Employee_bonus_Details.year = Employee_bonus_Header.year
        and Employee_bonus_Details.option_pay_cycle = Employee_bonus_Header.option_pay_cycle
        And Employee_bonus_Details.Employee_Profile_ID in (select Employee_Profile_ID From #Empgroup)    
        And Employee_bonus_Details.Employee_Profile_ID = Employee_Profile.ID     
        And Employee_Profile.ID = Employee_Statutory.Employee_Profile_ID      
		And Employee_Profile.ID = Employee_CodeName_Vw.Employee_Profile_ID      
        And Parameter.Company_Profile_Code = @Company_Profile_Code
        and Employee_Statutory.Option_Type_Statutory = 'PCB'
		and Employee_Statutory.Option_Location = @Location      
        And Parameter.Code = 'CITI'    
        and Employee_Credit + Company_Credit > 0  
        Group by Employee_bonus_Details.Employee_Profile_ID,Employee_CodeName_Vw.Name,Identity_Card_No,
		Old_Identity_Card_No,Employee_Profile.Option_Citizenship,String_Value1,DOCUMENT_NO,Employee_Statutory.Value,
		Employee_bonus_Details.month,Employee_bonus_Details.year     ,
		Employee_bonus_Header.OCP_ID_DIVISION
                                  
  Open PrDetailPointer    
  Fetch next from PrDetailPointer Into @Employee_Profile_ID,@EmpNm,@Nric,@EmpPcbNo,@Mon,@Yr,@Amt , @OCP_ID   
               
  While (@@fetch_status <> -1)    
  Begin        
    if (@@fetch_status <> -2)     
     Begin    
         Select @Count = count(Employee_Profile_ID) From #pr_rptpcb2 Where Employee_Profile_ID = @Employee_Profile_ID
         and year = @Yr and OCP_ID = @OCP_ID 
               
         If @Count = 0
         begin  
		   select @tolmon = 1
		   while @tolmon < 13
		   begin  
           Insert into #pr_rptpcb2(Employee_Profile_ID,empnm,nric,emppcbno,OCP_ID,year,month,PCBamt,CP38amt,recp,dt,
           orgpic,orgpos,orgpcbno,orgtel,govnm1,                  
                 govnm2,govaddr1,govaddr2,govpostcd,govcity,govstate,govpic,orgName,
                 PreAllowance,PreMonth , PreYear ,CP39A ,PreReceipt ,PreDate)        
           Values(@Employee_Profile_ID,@EmpNm,@Nric,@EmpPcbNo,@OCP_ID,@Yr,@tolmon,0,0,'',@StdDt,'','','','','',
           '','','','','','','',@Company_Profile_Code,'','','',0,'','')
           
           select @tolmon = @tolmon + 1      
           end
         end
                     
           Update #pr_rptpcb2 Set PCBamt = PCBamt + @Amt Where Employee_Profile_ID = @Employee_Profile_ID 
           and month = @Mon and year = @Yr and OCP_ID = @OCP_ID 
        
            
       End                 
    Fetch next from PrDetailPointer Into @Employee_Profile_ID,@EmpNm,@Nric,@EmpPcbNo,@Mon,@Yr,@Amt,@OCP_ID         
  End            
                          
  Close PrDetailPointer    
  Deallocate PrDetailPointer   
  
  Declare PrDetailPointer Cursor     
  For Select Employee_payroll_Details.Employee_Profile_ID,Employee_CodeName_Vw.Name,    
        case     
          when Employee_Profile.Option_Citizenship = String_Value1 then    
                Identity_Card_No + space(5) + '( ' +  Old_Identity_Card_No + ' )'    
          else    
            isnull(DOCUMENT_NO,'')
          end nric,       
        Employee_Statutory.Value,Employee_payroll_Details.month,Employee_payroll_Details.year,convert(decimal(15,2),sum(Employee_Credit + Company_Credit )) amt,
		employee_payroll_header.OCP_ID_DIVISION     
        From Employee_payroll_Details
		left outer join Employee_Document 
		on Employee_Document.Employee_Profile_ID = Employee_payroll_Details.Employee_Profile_ID
		and Employee_Document.Option_Type = 'PASSPORT',
	    Employee_Profile,Parameter,Employee_Statutory,employee_payroll_header, Employee_CodeName_Vw
 Where Employee_payroll_Details.OCP_ID_Salary 
		in (select OCP_ID_Salary from Salary_Profile where option_Type = 'CP38')
        And Employee_payroll_Details.year = @Year    
		and Employee_payroll_Details.employee_profile_id = employee_payroll_header.employee_profile_id
        and Employee_payroll_Details.month = employee_payroll_header.month
        and Employee_payroll_Details.year = employee_payroll_header.year
        and Employee_payroll_Details.option_pay_cycle = employee_payroll_header.option_pay_cycle
        And Employee_payroll_Details.Employee_Profile_ID in (select Employee_Profile_ID From #Empgroup)    
        And Employee_payroll_Details.Employee_Profile_ID = Employee_Profile.ID     
        And Employee_Profile.ID = Employee_Statutory.Employee_Profile_ID      
		And Employee_Profile.ID = Employee_CodeName_Vw.Employee_Profile_ID      
        And Parameter.Company_Profile_Code = @Company_Profile_Code
        and Employee_Statutory.Option_Type_Statutory = 'PCB'
		and Employee_Statutory.Option_Location = @Location      
        And Parameter.Code = 'CITI'    
        and Employee_Credit + Company_Credit > 0  
        Group by Employee_payroll_Details.Employee_Profile_ID,Employee_CodeName_Vw.Name,Identity_Card_No,
		Old_Identity_Card_No,Employee_Profile.Option_Citizenship,String_Value1,DOCUMENT_NO,Employee_Statutory.Value,
		Employee_payroll_Details.month,Employee_payroll_Details.year     ,
		employee_payroll_header.OCP_ID_DIVISION 
        
  Open PrDetailPointer    
  Fetch next from PrDetailPointer Into @Employee_Profile_ID,@EmpNm,@Nric,@EmpPcbNo,@Mon,@Yr,@Amt , @OCP_ID   
               
  While (@@fetch_status <> -1)    
  Begin        
    if (@@fetch_status <> -2)     
     Begin    
         Select @Count = count(Employee_Profile_ID) From #pr_rptpcb2 Where Employee_Profile_ID = @Employee_Profile_ID
         and year = @Yr  and OCP_ID = @OCP_ID
               
         If @Count = 0
         begin  
		   select @tolmon = 1
		   while @tolmon < 13
		   begin  
           Insert into #pr_rptpcb2(Employee_Profile_ID,empnm,nric,emppcbno,OCP_ID,year,month,PCBamt,CP38amt,recp,dt,
           orgpic,orgpos,orgpcbno,orgtel,govnm1,                  
                 govnm2,govaddr1,govaddr2,govpostcd,govcity,govstate,govpic,orgName,
                 PreAllowance,PreMonth , PreYear ,CP39A ,PreReceipt ,PreDate)        
           Values(@Employee_Profile_ID,@EmpNm,@Nric,@EmpPcbNo,@OCP_ID,@Yr,@tolmon,0,0,'',@StdDt,'','','','','',
           '','','','','','','',@Company_Profile_Code,'','','',0,'','')
           
           select @tolmon = @tolmon + 1      
           end
         end
                     
           Update #pr_rptpcb2 Set CP38amt = @Amt Where Employee_Profile_ID = @Employee_Profile_ID 
           and month = @Mon and year = @Yr and OCP_ID = @OCP_ID 
        
            
       End                 
    Fetch next from PrDetailPointer Into @Employee_Profile_ID,@EmpNm,@Nric,@EmpPcbNo,@Mon,@Yr,@Amt, @OCP_ID           
  End            
                          
  Close PrDetailPointer    
  Deallocate PrDetailPointer         

insert into #pr_rptpcb2(Employee_Profile_ID,empnm,nric,emppcbno,OCP_ID,year,month)
Select distinct Employee_payroll_Details.Employee_Profile_ID,Employee_Codename_Vw.Name,    
        case     
          when Employee_Profile.Option_Citizenship = String_Value1 then    
                Identity_Card_No + space(5) + '( ' +  Old_Identity_Card_No + ' )'    
          else    
            isnull(DOCUMENT_NO,'')
          end nric,       
        Employee_Statutory.Value,employee_payroll_header.OCP_ID_DIVISION,Employee_payroll_Details.year,
		0
        From Employee_payroll_Details
		left outer join Employee_Document 
		on Employee_Document.Employee_Profile_ID = Employee_payroll_Details.Employee_Profile_ID
		and Employee_Document.Option_Type = 'PASSPORT',
	    Employee_Profile,Parameter,Employee_Statutory,employee_payroll_header, Employee_Codename_Vw
 Where Employee_payroll_Details.OCP_ID_Salary 
		in (select OCP_ID_Salary from Salary_Profile where option_Type = 'PCB')
        And Employee_payroll_Details.year = @Year    
		and Employee_payroll_Details.employee_profile_id = employee_payroll_header.employee_profile_id
        and Employee_payroll_Details.month = employee_payroll_header.month
        and Employee_payroll_Details.year = employee_payroll_header.year
        and Employee_payroll_Details.option_pay_cycle = employee_payroll_header.option_pay_cycle
        And Employee_payroll_Details.Employee_Profile_ID in (select Employee_Profile_ID From #Empgroup)    
        And Employee_payroll_Details.Employee_Profile_ID = Employee_Profile.ID     
        And Employee_Profile.ID = Employee_Statutory.Employee_Profile_ID      
		And Employee_Profile.ID = Employee_Codename_Vw.Employee_Profile_ID  
        And Parameter.Company_Profile_Code = @Company_Profile_Code
        and Employee_Statutory.Option_Type_Statutory = 'PCB'
		and Employee_Statutory.Option_Location = @Location      
        And Parameter.Code = 'CITI'     
        Group by Employee_payroll_Details.Employee_Profile_ID,Employee_Codename_Vw.Name,Identity_Card_No,
		Old_Identity_Card_No,Employee_Profile.Option_Citizenship,String_Value1,DOCUMENT_NO,Employee_Statutory.Value,
		Employee_payroll_Details.month,Employee_payroll_Details.year     ,
		employee_payroll_header.OCP_ID_DIVISION
       
if not exists(select * from #pr_rptpcb2)
	insert into #pr_rptpcb2 values('','','','','','',0.00,0.00,0,0,'','','','','','','',
           '','','','','','','','','','',0,'','')

declare @emp table(
Employee_Profile_ID nvarchar(50))

insert into @emp
select Distinct employee_profile_id from #pr_rptpcb2 where month > 0

delete #pr_rptpcb2 from @emp a
where #pr_rptpcb2.Employee_Profile_ID = a.Employee_Profile_ID
and #pr_rptpcb2.month = 0

declare @DateSetting nvarchar(100)
       
select @DateSetting = String_Value1 from parameter
where code = 'Regional_Setting'
and Module_Profile_Code = 'PAYROLL'
and Company_Profile_Code = @Company_Profile_Code
print  @DateSetting
if @DateSetting = 'DD/MM/YYYY'
  Update #pr_rptpcb2 set
  recp = case when month = 1 then Receipt_No_1
  when month = 2 then RECEIPT_NO_2
  when month = 3 then RECEIPT_NO_3
  when month = 4 then RECEIPT_NO_4
  when month = 5 then RECEIPT_NO_5
  when month = 6 then RECEIPT_NO_6
  when month = 7 then RECEIPT_NO_7
  when month = 8 then RECEIPT_NO_8
  when month = 9 then RECEIPT_NO_9
  when month = 10 then RECEIPT_NO_10
  when month = 11 then RECEIPT_NO_11
  when month = 12 then RECEIPT_NO_12
  else '' end,
  dt = case when month = 1 then convert(datetime,dbo.fn_displayDate(Receipt_Date_1,@Company_Profile_Code,'PAYROLL'),103)
  when month = 2 then convert(datetime,dbo.fn_displayDate(Receipt_Date_2,@Company_Profile_Code,'PAYROLL'),103)
  when month = 3 then convert(datetime,dbo.fn_displayDate(Receipt_Date_3,@Company_Profile_Code,'PAYROLL'),103)
  when month = 4 then convert(datetime,dbo.fn_displayDate(Receipt_Date_4,@Company_Profile_Code,'PAYROLL'),103)
  when month = 5 then convert(datetime,dbo.fn_displayDate(Receipt_Date_5,@Company_Profile_Code,'PAYROLL'),103)
  when month = 6 then convert(datetime,dbo.fn_displayDate(Receipt_Date_6,@Company_Profile_Code,'PAYROLL'),103)
  when month = 7 then convert(datetime,dbo.fn_displayDate(Receipt_Date_7,@Company_Profile_Code,'PAYROLL'),103)
  when month = 8 then convert(datetime,dbo.fn_displayDate(Receipt_Date_8,@Company_Profile_Code,'PAYROLL'),103)
  when month = 9 then convert(datetime,dbo.fn_displayDate(Receipt_Date_9,@Company_Profile_Code,'PAYROLL'),103)
  when month = 10 then convert(datetime,dbo.fn_displayDate(Receipt_Date_10,@Company_Profile_Code,'PAYROLL'),103)
  when month = 11 then convert(datetime,dbo.fn_displayDate(Receipt_Date_11,@Company_Profile_Code,'PAYROLL'),103)
  when month = 12 then convert(datetime,dbo.fn_displayDate(Receipt_Date_12,@Company_Profile_Code,'PAYROLL'),103)
  else '' end
  From Payroll_PCB2    
  Where Payroll_PCB2.Company_Profile_Code = @Company_Profile_Code     
  And Payroll_PCB2.year = @Year
  And Option_Location = @Location 
else
  Update #pr_rptpcb2     
  Set recp = case when month = 1 then Receipt_No_1
  when month = 2 then RECEIPT_NO_2
  when month = 3 then RECEIPT_NO_3
  when month = 4 then RECEIPT_NO_4
  when month = 5 then RECEIPT_NO_5
  when month = 6 then RECEIPT_NO_6
  when month = 7 then RECEIPT_NO_7
  when month = 8 then RECEIPT_NO_8
  when month = 9 then RECEIPT_NO_9
  when month = 10 then RECEIPT_NO_10
  when month = 11 then RECEIPT_NO_11
  when month = 12 then RECEIPT_NO_12
  else '' end,
  dt = case when month = 1 then convert(datetime,dbo.fn_displayDate(Receipt_Date_1,@Company_Profile_Code,'PAYROLL'),101)
  when month = 2 then convert(datetime,dbo.fn_displayDate(Receipt_Date_2,@Company_Profile_Code,'PAYROLL'),101)
  when month = 3 then convert(datetime,dbo.fn_displayDate(Receipt_Date_3,@Company_Profile_Code,'PAYROLL'),101)
  when month = 4 then convert(datetime,dbo.fn_displayDate(Receipt_Date_4,@Company_Profile_Code,'PAYROLL'),101)
  when month = 5 then convert(datetime,dbo.fn_displayDate(Receipt_Date_5,@Company_Profile_Code,'PAYROLL'),101)
  when month = 6 then convert(datetime,dbo.fn_displayDate(Receipt_Date_6,@Company_Profile_Code,'PAYROLL'),101)
  when month = 7 then convert(datetime,dbo.fn_displayDate(Receipt_Date_7,@Company_Profile_Code,'PAYROLL'),101)
  when month = 8 then convert(datetime,dbo.fn_displayDate(Receipt_Date_8,@Company_Profile_Code,'PAYROLL'),101)
  when month = 9 then convert(datetime,dbo.fn_displayDate(Receipt_Date_9,@Company_Profile_Code,'PAYROLL'),101)
  when month = 10 then convert(datetime,dbo.fn_displayDate(Receipt_Date_10,@Company_Profile_Code,'PAYROLL'),101)
  when month = 11 then convert(datetime,dbo.fn_displayDate(Receipt_Date_11,@Company_Profile_Code,'PAYROLL'),101)
  when month = 12 then convert(datetime,dbo.fn_displayDate(Receipt_Date_12,@Company_Profile_Code,'PAYROLL'),101)
  else '' end
  From Payroll_PCB2    
  Where Payroll_PCB2.Company_Profile_Code = @Company_Profile_Code     
  And Payroll_PCB2.year = @Year
  And Option_Location = @Location    
                  
  Update #pr_rptpcb2    
  Set orgpic = Employee_CodeName_Vw.Name,    
      orgpos = Organisation_Code_Profile_Vw.name     
  From Employee_CodeName_Vw, Organisation_Code_Profile_Vw, Employee_Status
  where Employee_CodeName_Vw.Code = @PIC
  and Employee_Status.Employee_Profile_ID = Employee_CodeName_Vw.Employee_Profile_ID
  and Employee_Status.OCP_ID_Job_Title = Organisation_Code_Profile_Vw.ID
      
  Update #pr_rptpcb2    
  Set orgpcbno = Company_Statutory.Value,     
      orgtel = Phone_No,
	  orgName = Company_profile.Name    
  From Company_profile, Company_Statutory    
  Where Code = @Company_Profile_Code    
  And Code = Company_Statutory.Company_Profile_Code
  and Option_Location = @Location
  and Company_Statutory.Option_Type = 'PCB_NO'

  Update #pr_rptpcb2    
  Set orgpcbno = DIVISION_STATUTORY.Value
  From Company_profile, DIVISION_STATUTORY    
  Where Code = @Company_Profile_Code    
  And Code = DIVISION_STATUTORY.Company_Profile_Code
  and Option_Location = @Location
  and DIVISION_STATUTORY.STATUTORY_Type = 'PCB_NO'
  and #pr_rptpcb2.OCP_ID = DIVISION_STATUTORY.OCP_ID

  Update #pr_rptpcb2    
  Set orgName = DIVISION_STATUTORY.Value
  From Company_profile, DIVISION_STATUTORY    
  Where Code = @Company_Profile_Code    
  And Code = DIVISION_STATUTORY.Company_Profile_Code
  and Option_Location = @Location
  and DIVISION_STATUTORY.STATUTORY_Type = 'COMPANY_NAME'
  and #pr_rptpcb2.OCP_ID = DIVISION_STATUTORY.OCP_ID
    
  update #pr_rptpcb2 set govnm1 = @Caw

  update #pr_rptpcb2
  set govaddr1 = Lot + ' '+ Street,
  govaddr2 = Postal +' ' + Address_City_Code + ' ' + Address_State_Code 
  from Company_Address
  where company_profile_code = @Company_Profile_Code

 update #pr_rptpcb2
 set PreAllowance = b.OCP_ID_SALARY,
 PreMonth = 12,
 PreYear = b.YEAR ,
 CP39A = d.Employee_Credit +d.Company_Credit,
 PreReceipt = b.RECEIPT_NO,
 PreDate = convert(datetime,isnull(dbo.fn_displayDate(b.RECEIPT_DATE ,@Company_Profile_Code,'PAYROLL'),''),103)
 from
 commision_last_year b, Employee_Benefit c,employee_payroll_details d
 where #pr_rptpcb2.employee_profile_id = b.employee_profile_id
 and b.Employee_Profile_ID = c.Employee_Profile_ID
 and b.PAY_ON between c.Date_From and c.Date_To
 and b.OCP_ID_SALARY = c.OCP_ID_Salary
 and dbo.fn_DatePart('year',b.pay_on) = @Year
 and b.EMPLOYEE_PROFILE_ID = d.Employee_Profile_ID
 and d.Year = @Year
 and d.OCP_ID_Salary in (
 select id from Organisation_Code_Profile_Vw
 where Company_Profile_Code = @Company_Profile_Code
 and CODE like 'CP39A%')
 and d.Employee_Credit +d.Company_Credit > 0
 
  update #pr_rptpcb2 set Employee_Profile_ID = isnull(dbo.fn_GetEmpCode(Employee_Profile_ID),'')
  update #pr_rptpcb2 set PreAllowance = isnull(dbo.fn_GetOCPNameByID (PreAllowance),'')
 
Select Employee_Profile_ID,empnm,nric,emppcbno,year,month,
PCBamt,CP38amt,recp,
case when dt = '01/01/1900' then 
	''
else
convert(varchar(10),dt,103)
end 'dt',
orgpic,orgpos,orgpcbno,orgtel,govnm1,govnm2,govaddr1,govaddr2,govpostcd,govcity,govstate,govpic,orgname,
dbo.fn_displaydate(dbo.fn_GetCurrentDatetime(getdate()),@Company_Profile_Code,'PAYROLL')
as 'PrintDate',
PreAllowance,PreMonth,PreYear,CP39A,PreReceipt,
case when PreDate = '01/01/1900' then 
	''
else
convert(varchar(10),PreDate,103)
end 'PreDate' From #pr_rptpcb2 Order by empnm 
delete from report_control_temp where Report_ID = @SpID and User_Profile_Code = @User_Profile_Code
drop table #empgroup   

go

insert into report_control_temp
select 'LBG','132','60','1333_hr'
union
select 'LBG','72','60','1333_hr'
union
select 'LBG','137','60','1333_hr'

exec sp_pr_RptEPCB2 'LBG','2015',60,'PENINSULA','ADD','1333_hr'

--select * from employee_payroll_details where year = '2015' and OCP_ID_Salary = 8