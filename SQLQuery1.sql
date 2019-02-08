select * 
-- update t1 set [EmailConfirmed]=1
-- delete from t1
from GarageUser t1

select * 
-- update t1 set [NormalizedName]='ADMIN'
from AspNetRoles t1
select * 
from GarageUser t1

select *
 from AspNetUserRoles t1


  insert into  AspNetUserRoles
  values('11178022-4a98-4bea-a59f-01c81b024eb8','AECE0E4F-D854-4FAE-BE89-DA81A0D8BC56')
  ,('a040299c-5fc7-4b91-a663-34cb64f40c34','5561A8AB-60AE-4DD6-99F2-3B1D139F24D3')

  alter table AspNetUserRoles
  drop constraint FK_AspNetUserRoles_AspNetUsers_UserId