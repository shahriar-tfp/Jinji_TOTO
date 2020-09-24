
select user_profile_code = 'RWEM' into #audit
update Security_Role_Details set Option_Full_Access = 'NO'
where table_profile_code = 'REPORT_EA' 
and Security_Role_profile_code = 'EMP'

delete from explorer_profile where Code = 'REPORT_E_EA'
delete from page_profile where Code = 'REPORT_E_EA'
delete from table_profile where Code = 'REPORT_E_EA'
delete from table_field where table_profile_code = 'REPORT_E_EA'
delete from [option] where table_profile_code = 'REPORT_E_EA'
delete from Lookup_Profile where table_profile_code = 'REPORT_E_EA'
delete from Security_Role_Details where table_profile_code = 'REPORT_E_EA'

insert into explorer_profile select 'REPORT_E_EA','E-EA Form and E-PCB II','4','10','CHILD','REPORT_E_EA','REPORTS_GOVERNMENT','NO','','REPORT_E_EA.ico','','REPORTS','YES'
insert into page_profile select 'REPORT_E_EA','E-EA Form and E-PCB II','REPORT_E_EA','YES','YES','YES','YES','YES','20'
insert into table_profile select 'REPORT_E_EA','REPORT_E_EA','REPORT_E_EA','TABLE','NO',''
insert into table_field select 'REPORT_E_EA','3','BONUS1','Bonus Paid Period From','NO','BONUS1','DATE','50','0','YES','NO','NO','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'REPORT_E_EA','4','BONUS2','To','NO','BONUS2','DATE','50','0','YES','NO','NO','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'REPORT_E_EA','1','COMPANY_PROFILE_CODE','Company Code','YES','COMPANY_PROFILE_CODE','LOOKUP','50','0','YES','YES','NO','NO','YES','0','0','0','0','NO','Company','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'REPORT_E_EA','6','GROUP_CODE','Category Field','YES','GROUP_CODE','CHARACTER','50','0','YES','YES','NO','NO','YES','0','0','0','0','','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'REPORT_E_EA','5','OPTION_GROUP','By Category','YES','OPTION_GROUP','OPTION','50','0','YES','YES','NO','NO','YES','0','0','0','0','','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'REPORT_E_EA','2','YEAR_MONTH','Year','YES','YEAR_MONTH','DATE','50','0','YES','YES','NO','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into [option] select 'REPORT_E_EA','OPTION_GROUP','ADD','Employee','2','NO'
insert into Lookup_Profile select Code,'REPORTS','REPORT_E_EA','COMPANY_PROFILE_CODE','COMPANY_PROFILE','' from company_profile
insert into Lookup_Profile select Code,'REPORTS','REPORT_E_EA','EMPLOYEE_PROFILE_ID','EMPLOYEE_CODENAME_VW','' from company_profile
insert into Security_Role_Details select Code,'EMP','EMP','REPORT_E_EA','YES' from company_profile


drop table #audit
