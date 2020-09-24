
if not exists (select * from sys.objects where name = 'EA_FORM_MANUAL_2015')
select * into EA_FORM_MANUAL_2015 from EA_FORM_MANUAL

if not exists (select * from sys.objects where name = 'Payroll_EAReport_Field_2015')
select * into Payroll_EAReport_Field_2015 from Payroll_EAReport_Field


if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'Category')
alter table Payroll_E8DFORM add Category nvarchar(20)
if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'noofchild')
alter table Payroll_E8DFORM add  noofchild int
if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'totalChild')
 alter table Payroll_E8DFORM add totalChild int
 if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'BIK')
 alter table Payroll_E8DFORM add BIK decimal(15,2)
 if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'vola')
 alter table Payroll_E8DFORM add vola decimal(15,2)
 if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'ESOS')
 alter table Payroll_E8DFORM add ESOS decimal(15,2)
 if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'EPF')
  alter table Payroll_E8DFORM add EPF decimal(15,2)
  if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'SOCSO')
 alter table Payroll_E8DFORM add SOCSO decimal(15,2)
 if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'Zakat')
 alter table Payroll_E8DFORM add Zakat decimal(15,2)
 if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'TP1')
  alter table Payroll_E8DFORM add TP1 decimal(15,2)
  
  if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'TP1Zakat')
begin
  alter table Payroll_E8DFORM drop column TaxPay
  alter table Payroll_E8DFORM add TP1Zakat decimal(15,2)
end
  if not exists(select * from sys.columns where object_id in (
select object_id from sys.objects where name = 'Payroll_E8DFORM')
and name = 'TaxPay')
 alter table Payroll_E8DFORM add TaxPay int
 

 drop table REPORT_FORMAT
 go
 Create table REPORT_FORMAT(
 Table_Profile_Code nvarchar(50),
 From_Year nvarchar(4),
 To_Year nvarchar(4),
 ReportCode nvarchar(50),
 ReportName nvarchar(50))

 insert into REPORT_FORMAT
 select 'REPORT_E','2000','2015','E-FORM','pr_GovEForm_2015'
 insert into REPORT_FORMAT
 select 'REPORT_E','2016','3000','E-FORM','pr_GovEForm'
 insert into REPORT_FORMAT
 select 'REPORT_E','2000','2015','CP8D','pr_GovCP8D_2015'
 insert into REPORT_FORMAT
 select 'REPORT_E','2016','3000','CP8D','pr_GovCP8D'
  insert into REPORT_FORMAT
 select 'REPORT_E','2000','2015','E-CP8D','sp_pr_rptE8D_2015'
 insert into REPORT_FORMAT
 select 'REPORT_E','2016','3000','E-CP8D','sp_pr_rptE8D'
 insert into REPORT_FORMAT
 select 'REPORT_EA','2000','2015','EA-Form','pr_GovEA_2015'
 insert into REPORT_FORMAT
 select 'REPORT_EA','2016','3000','EA-Form','pr_GovEA'
  insert into REPORT_FORMAT
 select 'REPORT_EA','2000','2015','EA-SP','sp_pr_InsDelTempEAForm_2015'
 insert into REPORT_FORMAT
 select 'REPORT_EA','2016','3000','EA-SP','sp_pr_InsDelTempEAForm'

 -- new for E-EA
  insert into REPORT_FORMAT
 select 'REPORT_E_EA','2000','2015','EA-SP','sp_pr_InsDelTempEAForm_2015'
 insert into REPORT_FORMAT
 select 'REPORT_E_EA','2016','3000','EA-SP','sp_pr_InsDelTempEAForm'
  insert into REPORT_FORMAT
 select 'REPORT_E_EA','2000','2015','EA-Form','pr_GovEEA_2015'
 insert into REPORT_FORMAT
 select 'REPORT_E_EA','2016','3000','EA-Form','pr_GovEEA'
 insert into REPORT_FORMAT
 select 'REPORT_E_EA','2000','2015','EPCBII','pr_GOVEPCBII'
 insert into REPORT_FORMAT
 select 'REPORT_E_EA','2016','3000','EPCBII','pr_GOVEPCBII'


