select user_profile_code = 'RWEM' into #audit
delete from [option] where table_profile_code = 'report_ea_set' and table_field_code = 'OPtion_field'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','1','1 -> 1(a) Gaji kasar, upah atau gaji cuti (termasuk gaji lebih masa) ',1,'YES'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','2','2 -> 1(b) Fi (termasuk fi pengarah), komisen atau bonus ',2,'NO'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','3','3 -> 1(c) Tip kasar, perkuisit, penerimaan sagu hati atau elaun-elaun lain',3,'NO'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','4','4 -> 1(d) Cukai Pendapatan yang dibayar oleh Majikan bagi pihak Pekerja',4,'NO'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','5','5 -> 1(e) Manfaat Skim Opsyen Saham Pekerja (ESOS)',5,'NO'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','6','6 -> 1(f) Ganjaran Bagi tempoh dari',6,'NO'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','7','7 -> 2) Butiran bayaran tunggakan dan lain-lain bagi tahun-tahun terdahulu dalam tahun semasa',7,'NO'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','8','8 -> 3) Manfaat berupa barangan',8,'NO'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','9','9 -> 4) Nilai tempat kediaman',9,'NO'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','10','10 -> 5) Bayaran balik daripada Kumpulan Wang Simpanan/Pencen yang tidak diluluskan',10,'NO'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','11','11 -> 6) Pampasan kerana Kehilangan pekerjaan',11,'NO'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','12','12 -> C.1) Pencen',12,'NO'
insert into [option]
select 'REPORT_EA_SET','OPTION_FIELD','13','13 -> C.2) Anuiti atau Bayaran Berkala yang lain',13,'NO'

update Payroll_EAReport_Field set OPTION_FIELD = 8 where OPTION_FIELD in (16,15,14,13,12,11,10,9,8,7,6,5)
update Payroll_EAReport_Field set OPTION_FIELD = 9 where OPTION_FIELD = 17
update Payroll_EAReport_Field set OPTION_FIELD = 10 where OPTION_FIELD = 18
update Payroll_EAReport_Field set OPTION_FIELD = 11 where OPTION_FIELD = 19
update Payroll_EAReport_Field set OPTION_FIELD = 12 where OPTION_FIELD = 20
update Payroll_EAReport_Field set OPTION_FIELD = 13 where OPTION_FIELD = 21 

 update a set ALLOWANCE_13 = b.ALLOWANCE_21,ALLOWANCE_12 = b.ALLOWANCE_20,ALLOWANCE_11 = b.ALLOWANCE_19,
 ALLOWANCE_10 = b.ALLOWANCE_18,ALLOWANCE_9 = b.ALLOWANCE_17,
 ALLOWANCE_8 = case when (isnull(b.ALLOWANCE_5,0)+isnull(b.ALLOWANCE_6,0)+
 isnull(b.ALLOWANCE_7,0)+
 isnull(b.ALLOWANCE_8,0)+isnull(b.ALLOWANCE_9,0)+
 isnull(b.ALLOWANCE_10,0)+isnull(b.ALLOWANCE_11,0)+
 isnull(b.ALLOWANCE_12,0)+isnull(b.ALLOWANCE_13,0)+
 isnull(b.ALLOWANCE_14,0)+isnull(b.ALLOWANCE_15,0)+
 isnull(b.ALLOWANCE_16,0)) = 0 then null else isnull(b.ALLOWANCE_5,0)+isnull(b.ALLOWANCE_6,0)+
 isnull(b.ALLOWANCE_7,0)+
 isnull(b.ALLOWANCE_8,0)+isnull(b.ALLOWANCE_9,0)+
 isnull(b.ALLOWANCE_10,0)+isnull(b.ALLOWANCE_11,0)+
 isnull(b.ALLOWANCE_12,0)+isnull(b.ALLOWANCE_13,0)+
 isnull(b.ALLOWANCE_14,0)+isnull(b.ALLOWANCE_15,0)+
 isnull(b.ALLOWANCE_16,0) end
 from EA_FORM_MANUAL a, EA_FORM_MANUAL_2015 b where a.year = 2016
 and a.YEAR = b.YEAR and a.EMPLOYEE_PROFILE_ID = b.EMPLOYEE_PROFILE_ID
 
 update EA_FORM_MANUAL set ALLOWANCE_21 = null where year = 2016 and ALLOWANCE_21 > 0
 update EA_FORM_MANUAL set ALLOWANCE_20 = null where year = 2016 and ALLOWANCE_20 > 0
 update EA_FORM_MANUAL set ALLOWANCE_19 = null where year = 2016 and ALLOWANCE_19 > 0
 update EA_FORM_MANUAL set ALLOWANCE_18 = null where year = 2016 and ALLOWANCE_18 > 0
 update EA_FORM_MANUAL set ALLOWANCE_17 = null where year = 2016 and ALLOWANCE_17 > 0
 update EA_FORM_MANUAL set ALLOWANCE_16 = null where year = 2016 and ALLOWANCE_16 > 0
 update EA_FORM_MANUAL set ALLOWANCE_15 = null where year = 2016 and ALLOWANCE_15 > 0
 update EA_FORM_MANUAL set ALLOWANCE_14 = null where year = 2016 and ALLOWANCE_14 > 0
 update EA_FORM_MANUAL set ALLOWANCE_7 = null where year = 2016 and ALLOWANCE_7 > 0
 update EA_FORM_MANUAL set ALLOWANCE_6 = null where year = 2016 and ALLOWANCE_6 > 0
 update EA_FORM_MANUAL set ALLOWANCE_5 = null where year = 2016 and ALLOWANCE_5 > 0

