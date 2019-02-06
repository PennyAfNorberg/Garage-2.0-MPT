select *
 from  ParkedVehicle t1

 select * 
 from Vehicles t1
 left join ParkedVehicle t2 on t1.Id=t2.VehicleId
 where
  ( t2.ParkOutDate is  null) and t2.id is not null

 select *
  from Members t3

   insert into Members
   values('Test','Testsson','Testgatan 123','12345','Testville','Noone@nowhere.no',0x12)

    insert into ParkedVehicle
	values(1,2,getdate(),null,null)