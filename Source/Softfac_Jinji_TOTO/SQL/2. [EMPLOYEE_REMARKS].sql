select user_Profile_code = 'RWEM' into #audit
delete from explorer_profile where code = 'EMPLOYEE_REMARKS'
delete from page_profile where code = 'EMPLOYEE_REMARKS'
delete from table_profile where code = 'EMPLOYEE_REMARKS'
delete from table_profile where code = 'EMPLOYEE_REMARKS_CARD_VW'
delete from table_field where table_profile_code = 'EMPLOYEE_REMARKS'
delete from table_field where table_profile_code = 'EMPLOYEE_REMARKS_CARD_VW'
delete from user_define_function where table_profile_code = 'EMPLOYEE_REMARKS'
delete from Lookup_Profile where table_profile_code = 'EMPLOYEE_REMARKS'
delete from Security_Role_Details where table_profile_code = 'EMPLOYEE_REMARKS'

insert into explorer_profile select 'EMPLOYEE_REMARKS','EMPLOYEE REMARKS','3','2','CHILD','EMPLOYEE_REMARKS','INFORMATION_SYSTEM','NO','','employee_remarks.ico','','INFORMATION_SYSTEM','YES'
insert into page_profile select 'EMPLOYEE_REMARKS','EMPLOYEE_REMARKS','EMPLOYEE_REMARKS','YES','YES','YES','YES','YES','10'
insert into table_profile select 'EMPLOYEE_REMARKS','EMPLOYEE_REMARKS','EMPLOYEE_REMARKS','TABLE','NO',''
insert into table_profile select 'EMPLOYEE_REMARKS_CARD_VW','EMPLOYEE_REMARKS Card View','EMPLOYEE_REMARKS_CARD_VW','VIEW','NO',''
insert into table_field select 'EMPLOYEE_REMARKS','10','COMPANY_PROFILE_CODE','Company Profile Code','YES','COMPANY_PROFILE_CODE','LOOKUP','50','0','YES','YES','NO','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','140','DATETIME_CREATE','Datetime Create','NO','Datetime_Code_Create','DATETIME','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','160','DATETIME_MODIFY','Datetime Modify','NO','Datetime_Modify','DATETIME','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','20','EMPLOYEE_PROFILE_ID','Employee Profile ID','YES','EMPLOYEE_PROFILE_ID','LOOKUP','50','0','YES','YES','NO','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','30','REMARK1','Remark 1','NO','REMARK1','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','120','REMARK10','Remark 10','NO','REMARK10','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','40','REMARK2','Remark 2','NO','REMARK2','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','50','REMARK3','Remark 3','NO','REMARK3','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','60','REMARK4','Remark 4','NO','REMARK4','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','70','REMARK5','Remark 5','NO','REMARK5','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','80','REMARK6','Remark 6','NO','REMARK6','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','90','REMARK7','Remark 7','NO','REMARK7','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','100','REMARK8','Remark 8','NO','REMARK8','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','110','REMARK9','Remark 9','NO','REMARK9','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','130','USER_PROFILE_CODE_CREATE','User Profile Code Create','NO','User_Profile_Code_Create','LOOKUP','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EMPLOYEE_REMARKS','150','USER_PROFILE_CODE_MODIFY','User Profile Code Modify','NO','User_Profile_Code_Modify','LOOKUP','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','10','COMPANY_PROFILE_CODE','Company Profile Code','YES','COMPANY_PROFILE_CODE','LOOKUP','50','0','YES','YES','NO','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','140','DATETIME_CREATE','Datetime Create','NO','Datetime_Code_Create','DATETIME','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','160','DATETIME_MODIFY','Datetime Modify','NO','Datetime_Modify','DATETIME','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','20','EMPLOYEE_PROFILE_ID','Employee Profile ID','YES','EMPLOYEE_PROFILE_ID','LOOKUP','50','0','YES','YES','NO','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','30','REMARK1','Remark 1','NO','REMARK1','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','120','REMARK10','Remark 10','NO','REMARK10','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','40','REMARK2','Remark 2','NO','REMARK2','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','50','REMARK3','Remark 3','NO','REMARK3','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','60','REMARK4','Remark 4','NO','REMARK4','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','70','REMARK5','Remark 5','NO','REMARK5','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','80','REMARK6','Remark 6','NO','REMARK6','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','90','REMARK7','Remark 7','NO','REMARK7','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','100','REMARK8','Remark 8','NO','REMARK8','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','110','REMARK9','Remark 9','NO','REMARK9','CHARACTER','200','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','130','USER_PROFILE_CODE_CREATE','User Profile Code Create','NO','User_Profile_Code_Create','LOOKUP','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EMPLOYEE_REMARKS_CARD_VW','150','USER_PROFILE_CODE_MODIFY','User Profile Code Modify','NO','User_Profile_Code_Modify','LOOKUP','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into user_define_function select Code,'INFORMATION_SYSTEM','EMPLOYEE_REMARKS','COMPANY_PROFILE_CODE','DELETE','dbo.fn_ReturnCompanyProfileCode','','','','' from company_profile
insert into user_define_function select Code,'INFORMATION_SYSTEM','EMPLOYEE_REMARKS','COMPANY_PROFILE_CODE','INSERT','dbo.fn_ReturnCompanyProfileCode','','','','' from company_profile
insert into user_define_function select Code,'INFORMATION_SYSTEM','EMPLOYEE_REMARKS','COMPANY_PROFILE_CODE','SELECT','dbo.fn_GetCompanyProfileCodeName','','','','' from company_profile
insert into user_define_function select Code,'INFORMATION_SYSTEM','EMPLOYEE_REMARKS','COMPANY_PROFILE_CODE','UPDATE','dbo.fn_ReturnCompanyProfileCode','','','','' from company_profile
insert into user_define_function select Code,'INFORMATION_SYSTEM','EMPLOYEE_REMARKS','EMPLOYEE_PROFILE_ID','DELETE','dbo.fn_ReturnEmpIDByCodeName','','','','' from company_profile
insert into user_define_function select Code,'INFORMATION_SYSTEM','EMPLOYEE_REMARKS','EMPLOYEE_PROFILE_ID','INSERT','dbo.fn_ReturnEmpIDByCodeName','','','','' from company_profile
insert into user_define_function select Code,'INFORMATION_SYSTEM','EMPLOYEE_REMARKS','EMPLOYEE_PROFILE_ID','SELECT','dbo.fn_GetEmpCodeName','','','','' from company_profile
insert into user_define_function select Code,'INFORMATION_SYSTEM','EMPLOYEE_REMARKS','EMPLOYEE_PROFILE_ID','UPDATE','dbo.fn_ReturnEmpIDByCodeName','','','','' from company_profile
insert into Lookup_Profile select Code,'INFORMATION_SYSTEM','EMPLOYEE_REMARKS','COMPANY_PROFILE_CODE','COMPANY_PROFILE','' from company_profile
insert into Lookup_Profile select Code,'INFORMATION_SYSTEM','EMPLOYEE_REMARKS','EMPLOYEE_PROFILE_ID','Employee_CodeName_Vw','' from company_profile
insert into Security_Role_Details select Code,'ADM','ADMIN','EMPLOYEE_REMARKS','YES' from company_profile

drop table #audit

go

if not exists(select * from sys.objects where name = 'EMPLOYEE_REMARKS')
begin

CREATE TABLE [dbo].[EMPLOYEE_REMARKS](
	[COMPANY_PROFILE_CODE] [nvarchar](50) NOT NULL,
	[EMPLOYEE_PROFILE_ID] [nvarchar](50) NOT NULL,
	[REMARK1] [nvarchar](100) NOT NULL,
	[REMARK2] [nvarchar](100) NULL,
	[REMARK3] [nvarchar](100) NULL,
	[REMARK4] [nvarchar](100) NULL,
	[REMARK5] [nvarchar](100) NULL,
	[REMARK6] [nvarchar](100) NULL,
	[REMARK7] [nvarchar](100) NULL,
	[REMARK8] [nvarchar](100) NULL,
	[REMARK9] [nvarchar](100) NULL,
	[REMARK10] [nvarchar](100) NULL,
	[USER_PROFILE_CODE_CREATE] [nvarchar](50) NULL,
	[DATETIME_CREATE] [nvarchar](50) NULL,
	[USER_PROFILE_CODE_MODIFY] [nvarchar](50) NULL,
	[DATETIME_MODIFY] [nvarchar](50) NULL,
 CONSTRAINT [PK_EMPLOYEE_REMARKS] PRIMARY KEY CLUSTERED 
(
	[COMPANY_PROFILE_CODE] ASC,
	[EMPLOYEE_PROFILE_ID] ASC,
	[REMARK1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

end
if not exists(select * from sys.objects where name = 'EMPLOYEE_REMARKS_CARD_VW')
begin

Create View [dbo].[EMPLOYEE_REMARKS_CARD_VW] As SELECT dbo.fn_GetCompanyProfileCodeName(dbo.[EMPLOYEE_REMARKS].[COMPANY_PROFILE_CODE]) As [COMPANY_PROFILE_CODE],dbo.fn_GetEmpCodeNameByID(dbo.[Employee_CodeName_Vw].[Company_Profile_Code],dbo.[EMPLOYEE_REMARKS].[EMPLOYEE_PROFILE_ID]) As [EMPLOYEE_PROFILE_ID],dbo.[EMPLOYEE_REMARKS].[REMARK1],dbo.[EMPLOYEE_REMARKS].[REMARK2],dbo.[EMPLOYEE_REMARKS].[REMARK3],dbo.[EMPLOYEE_REMARKS].[REMARK4],dbo.[EMPLOYEE_REMARKS].[REMARK5],dbo.[EMPLOYEE_REMARKS].[REMARK6],dbo.[EMPLOYEE_REMARKS].[REMARK7],dbo.[EMPLOYEE_REMARKS].[REMARK8],dbo.[EMPLOYEE_REMARKS].[REMARK9],dbo.[EMPLOYEE_REMARKS].[REMARK10],[USER_PROFILE_CODE_CREATE],[DATETIME_CREATE],[USER_PROFILE_CODE_MODIFY],[DATETIME_MODIFY] From [dbo].[EMPLOYEE_REMARKS] Left Outer Join dbo.[Employee_CodeName_Vw] On dbo.[EMPLOYEE_REMARKS].Employee_Profile_ID=dbo.[Employee_CodeName_Vw].[Employee_Profile_ID]


end


