select user_profile_code = 'rwem' into #audit
delete from explorer_profile where code = 'EA_FORM_MANUAL_NEW'
delete from page_profile where code = 'EA_FORM_MANUAL_NEW'
delete from table_profile where code = 'EA_FORM_MANUAL_NEW'
delete from table_field where table_profile_code = 'EA_FORM_MANUAL_NEW'
delete from [option] where Table_Profile_Code = 'EA_FORM_MANUAL_NEW'
delete from user_define_function where table_profile_code = 'EA_FORM_MANUAL_NEW'
delete from Lookup_Profile where table_profile_code = 'EA_FORM_MANUAL_NEW'
delete from Security_Role_Details where table_profile_code = 'EA_FORM_MANUAL_NEW'

insert into explorer_profile select 'EA_FORM_MANUAL_NEW','EA Form New Manual Update','4','1','CHILD','EA_FORM_MANUAL_NEW','REPORTS_GOVERNMENT','NO','','EA_FORM_MANUAL_NEW.ico','','REPORTS','YES'
insert into page_profile select 'EA_FORM_MANUAL_NEW','EA_FORM_MANUAL_NEW','EA_FORM_MANUAL_NEW','YES','YES','YES','YES','YES','10'
insert into table_profile select 'EA_FORM_MANUAL_NEW','EA_FORM_MANUAL_NEW','EA_FORM_MANUAL_NEW','TABLE','NO',''
insert into table_field select 'EA_FORM_MANUAL_NEW','50','ALLOWANCE_1','B1.Gaji Kasar','NO','ALLOWANCE_1','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','140','ALLOWANCE_10','B5. Bayaran balik','NO','ALLOWANCE_10','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','150','ALLOWANCE_11','B6. Pampasan Kerana','NO','ALLOWANCE_11','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','160','ALLOWANCE_12','C1. Pencen','NO','ALLOWANCE_12','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','170','ALLOWANCE_13','C2. Anuiti Atau Bayaran','NO','ALLOWANCE_13','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','180','ALLOWANCE_14','E2. PERKESO','NO','ALLOWANCE_14','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','190','ALLOWANCE_15','D4.(a) TP1','NO','ALLOWANCE_15','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','200','ALLOWANCE_16','D4.(b) TP1 Zakat','NO','ALLOWANCE_16','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','210','ALLOWANCE_17','D5. Jumlah Pelepasan bagi anak','NO','ALLOWANCE_17','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','220','ALLOWANCE_18','ALLOWANCE_18','NO','ALLOWANCE_18','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','230','ALLOWANCE_19','ALLOWANCE_19','NO','ALLOWANCE_19','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','60','ALLOWANCE_2','B1.2 Bonus Amt','NO','ALLOWANCE_2','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','240','ALLOWANCE_20','ALLOWANCE_20','NO','ALLOWANCE_20','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','250','ALLOWANCE_21','ALLOWANCE_21','NO','ALLOWANCE_21','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','436','ALLOWANCE_22','F. Jumlah Elaun / Perkuisit','NO','ALLOWANCE_22','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','270','ALLOWANCE_23','ALLOWANCE_23','NO','ALLOWANCE_23','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','280','ALLOWANCE_24','ALLOWANCE_24','NO','ALLOWANCE_24','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','290','ALLOWANCE_25','ALLOWANCE_25','NO','ALLOWANCE_25','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','300','ALLOWANCE_26','ALLOWANCE_26','NO','ALLOWANCE_26','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','310','ALLOWANCE_27','ALLOWANCE_27','NO','ALLOWANCE_27','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','320','ALLOWANCE_28','ALLOWANCE_28','NO','ALLOWANCE_28','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','330','ALLOWANCE_29','ALLOWANCE_29','NO','ALLOWANCE_29','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','70','ALLOWANCE_3','B1.3 Tip Kasar','NO','ALLOWANCE_3','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','340','ALLOWANCE_30','ALLOWANCE_30','NO','ALLOWANCE_30','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','350','ALLOWANCE_31','ALLOWANCE_31','NO','ALLOWANCE_31','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','360','ALLOWANCE_32','ALLOWANCE_32','NO','ALLOWANCE_32','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','370','ALLOWANCE_33','ALLOWANCE_33','NO','ALLOWANCE_33','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','380','ALLOWANCE_34','ALLOWANCE_34','NO','ALLOWANCE_34','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','390','ALLOWANCE_35','ALLOWANCE_35','NO','ALLOWANCE_35','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','80','ALLOWANCE_4','B1.4 Cukai Majikan Bagi Pihat Perkerja','NO','ALLOWANCE_4','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','90','ALLOWANCE_5','B1.5 Manfaat Skim Opsyen Saham Pekerja.','NO','ALLOWANCE_5','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','100','ALLOWANCE_6','B1.6 Ganjaran Bagi','NO','ALLOWANCE_6','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','110','ALLOWANCE_7','B2. Butiran bayaran tunggakan','NO','ALLOWANCE_7','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','120','ALLOWANCE_8','B3. Maanfaat berupa barangan.','NO','ALLOWANCE_8','DECIMAL','50','0','YES','NO','NO','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','130','ALLOWANCE_9','B4. Nilai tempat kediaman','NO','ALLOWANCE_9','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'

insert into table_field select 'EA_FORM_MANUAL_NEW','75','BONUS_FROM','Bonus From Date','NO','BONUS_FROM','DATE','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','1.90001E+13','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','85','BONUS_TO','Bonus Date To','NO','BONUS_TO','DATE','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','1.90001E+13','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','105','CAR_DATE','Date for Car','NO','CAR_DATE','CHARACTER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','145','CAR_MODEL','Car Model','NO','CAR_MODEL','CHARACTER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','115','CAR_TYPE','Car Type','NO','CAR_TYPE','CHARACTER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','135','CAR_YEAR','Year For Car','NO','CAR_YEAR','CHARACTER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'

insert into table_field select 'EA_FORM_MANUAL_NEW','490','COMPANY_ADDR','Company_Addr','NO','COMPANY_ADDR','CHARACTER','100','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','480','COMPANY_NAME','Company_Name','NO','COMPANY_NAME','CHARACTER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','540','COMPANY_PCB_NO','Company_PCB_No','NO','COMPANY_PCB_NO','CHARACTER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','10','COMPANY_PROFILE_CODE','Company Profile Code','YES','COMPANY_PROFILE_CODE','LOOKUP','50','0','YES','YES','NO','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','420','CP38','D.2 CP38','NO','CP38','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','650','DATETIME_CREATE','Datetime Create','NO','Datetime_Code_Create','DATETIME','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','670','DATETIME_MODIFY','Datetime Modify','NO','Datetime_Modify','DATETIME','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','20','EMPLOYEE_PROFILE_ID','Employee Badge','YES','EMPLOYEE_PROFILE_ID','LOOKUP','50','0','YES','YES','NO','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','435','EPF_EMPLOYEE','E. EPF_Employee','NO','EPF_EMPLOYEE','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','155','HOUSE_ADDRESS','Address for House Benefit','NO','HOUSE_ADDRESS','CHARACTER','100','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'

insert into table_field select 'EA_FORM_MANUAL_NEW','510','HUSBAND_IC','Husband_IC','NO','HUSBAND_IC','CHARACTER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','500','HUSBAND_NAME','Husband_Name','NO','HUSBAND_NAME','CHARACTER','100','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','520','HUSBAND_PCB_NO','Husband_PCB_No','NO','HUSBAND_PCB_NO','CHARACTER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'

insert into table_field select 'EA_FORM_MANUAL_NEW','55','JOIN_DATE','Join_Date','NO','JOIN_DATE','DATE','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','19000101000000','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','530','NO_OF_CHILD','No_Of_Child','NO','NO_OF_CHILD','INTEGER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','630','OPTION_BLOCK','Block','NO','OPTION_BLOCK','OPTION','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','95','OTHER_SALARY','Perihal pembayaran','NO','OTHER_SALARY','CHARACTER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','410','PCB','D.1 (PCB)','NO','PCB','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','470','PREVIOUS_ADDR','Previous_Addr','NO','PREVIOUS_ADDR','CHARACTER','100','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','460','PREVIOUS_COMPANY','Previous_Company','NO','PREVIOUS_COMPANY','CHARACTER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','65','RESIGN_DATE','Resign_Date','NO','RESIGN_DATE','DATE','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','19000101000000','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','40','SERIAL_NO','Serial_No','NO','SERIAL_NO','CHARACTER','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','640','USER_PROFILE_CODE_CREATE','User Profile Code Create','NO','User_Profile_Code_Create','LOOKUP','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','660','USER_PROFILE_CODE_MODIFY','User Profile Code Modify','NO','User_Profile_Code_Modify','LOOKUP','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','NO','NO','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','30','YEAR','EA Year','YES','YEAR','CHARACTER','4','0','YES','YES','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into table_field select 'EA_FORM_MANUAL_NEW','430','ZAKAT','D.3 Zakat','NO','ZAKAT','DECIMAL','50','0','YES','NO','YES','NO','YES','0','0','0','0','NO','','','','','0','0.000000','0','0','YES','YES','','YES'
insert into [option] select 'EA_FORM_MANUAL_NEW','OPTION_BLOCK','NO','No','1','YES'
insert into [option] select 'EA_FORM_MANUAL_NEW','OPTION_BLOCK','YES','Yes','2','NO'
insert into user_define_function select Code,'REPORTS','EA_FORM_MANUAL_NEW','COMPANY_PROFILE_CODE','DELETE','dbo.fn_ReturnCompanyProfileCode','','','','' from company_profile
insert into user_define_function select Code,'REPORTS','EA_FORM_MANUAL_NEW','COMPANY_PROFILE_CODE','INSERT','dbo.fn_ReturnCompanyProfileCode','','','','' from company_profile
insert into user_define_function select Code,'REPORTS','EA_FORM_MANUAL_NEW','COMPANY_PROFILE_CODE','SELECT','dbo.fn_GetCompanyProfileCodeName','','','','' from company_profile
insert into user_define_function select Code,'REPORTS','EA_FORM_MANUAL_NEW','COMPANY_PROFILE_CODE','UPDATE','dbo.fn_ReturnCompanyProfileCode','','','','' from company_profile
insert into user_define_function select Code,'REPORTS','EA_FORM_MANUAL_NEW','EMPLOYEE_PROFILE_ID','DELETE','dbo.fn_ReturnEmpIDByCodeName','','','','' from company_profile
insert into user_define_function select Code,'REPORTS','EA_FORM_MANUAL_NEW','EMPLOYEE_PROFILE_ID','INSERT','dbo.fn_ReturnEmpIDByCodeName','','','','' from company_profile
insert into user_define_function select Code,'REPORTS','EA_FORM_MANUAL_NEW','EMPLOYEE_PROFILE_ID','SELECT','dbo.fn_GetEmpCodeName','','','','' from company_profile
insert into user_define_function select Code,'REPORTS','EA_FORM_MANUAL_NEW','EMPLOYEE_PROFILE_ID','UPDATE','dbo.fn_ReturnEmpIDByCodeName','','','','' from company_profile
insert into Lookup_Profile select Code,'REPORTS','EA_FORM_MANUAL_NEW','COMPANY_PROFILE_CODE','COMPANY_PROFILE','' from company_profile
insert into Lookup_Profile select Code,'REPORTS','EA_FORM_MANUAL_NEW','EMPLOYEE_PROFILE_ID','Employee_CodeName_Vw','' from company_profile
insert into Security_Role_Details select Code,'ADM','ADMIN','EA_FORM_MANUAL_NEW','YES' from company_profile
insert into Security_Role_Details select Code,'FIN','FIN','EA_FORM_MANUAL_NEW','YES' from company_profile
insert into Security_Role_Details select Code,'IT','IT','EA_FORM_MANUAL_NEW','YES' from company_profile
insert into Security_Role_Details select Code,'ITN','ITN','EA_FORM_MANUAL_NEW','YES' from company_profile

drop table #audit