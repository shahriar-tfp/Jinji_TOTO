select user_Profile_code = 'RWEM' into #audit
delete from explorer_profile where code = 'PUBLISH_EEA'
delete from page_profile where code = 'PUBLISH_EEA'
delete from Security_Role_Details where table_profile_code = 'PUBLISH_EEA'
insert into explorer_profile select 'PUBLISH_EEA','Publish E-EA-Form And E-PCB II','4','10','CHILD','PUBLISH_EEA','PAYROLL_PROCESS_MAIN','NO','','PUBLISH_EEA.ico','','PAYROLL','YES'
insert into page_profile select 'PUBLISH_EEA','Publish E-EA-Form And E-PCB II','PUBLISH_EEA','YES','YES','YES','YES','NO','10'
insert into Security_Role_Details select Code,'ADM','ADMIN','PUBLISH_EEA','YES' from company_profile
insert into Security_Role_Details select Code,'FIN','FIN','PUBLISH_EEA','YES' from company_profile
insert into Security_Role_Details select Code,'IT','IT','PUBLISH_EEA','YES' from company_profile
insert into Security_Role_Details select Code,'ITN','ITN','PUBLISH_EEA','YES' from company_profile

drop table #audit

if not exists(select * from sys.objects where name = 'Publish_EA')
begin
CREATE TABLE [dbo].[Publish_EA](
	[Employee_Profile_ID] [nvarchar](50) NULL,
	[Year] [int] NULL,
	[Option_Block] [nvarchar](50) NULL
) ON [PRIMARY]

end

go
Drop proc [dbo].[sp_pr_selPublishEEA]
go
/*
Alter By : SY Tey
Alter Date : 26/02/2016
Version : 1.0
Problem : publish EA.
*/
Create proc [dbo].[sp_pr_selPublishEEA]
@Company_Profile_Code nvarchar(100),
@Year nvarchar(100),
@Option_Block nvarchar(100)

as

Declare @PublishEA table(
Employee_profile_id nvarchar(200),
Year int,
Option_Block nvarchar(50))

insert into @PublishEA
select employee_profile_id,year,'NO'
from Payroll_EForm
where year = @Year
and Employee_profile_id in
(select Employee_Profile_ID 
from employee_codename_vw 
where company_profile_code = @Company_Profile_Code)

update B
set Option_Block = a.Option_Block
from Publish_EA a, @PublishEA B
where a.Employee_Profile_ID = B.Employee_profile_id
and a.Year = B.Year

select dbo.fn_getEmpCodeName(Employee_profile_id) as [Employee ID],Year,dbo.fn_GetOptionName('EMPLOYEE_PAYROLL_HEADER','OPTION_BLOCK',Option_Block) as Block
from @PublishEA
where Option_Block = @Option_Block
order by dbo.fn_getEmpCodeName(Employee_profile_id)

go

Drop proc [dbo].[sp_pr_UpdPublishEEA]
go
Create proc [dbo].[sp_pr_UpdPublishEEA]
@Employee_Profile_ID nvarchar(50),
@Year nvarchar(4),
@Option_Block nvarchar(50),
@Type nvarchar(50)

as

if @Type = 'ADD'
if exists( select * from Publish_EA where Employee_Profile_ID = @Employee_Profile_ID
and Year = @Year)
update Publish_EA
set Option_Block = @Option_Block
where Employee_Profile_ID = @Employee_Profile_ID
and Year = @Year
else
insert into Publish_EA
select @Employee_Profile_ID,@year, @Option_Block

if @Type = 'Publish Result'
select 'Publish E EA-Form Succesfull'

if @Type = 'UnPublish Result'
select 'UnPublish E EA-Form Succesfull'