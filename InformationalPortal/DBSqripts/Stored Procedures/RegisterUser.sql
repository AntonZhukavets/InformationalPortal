USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 12/25/2017 22:18:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[RegisterUser]
@firstName nvarchar(100),
@lastName nvarchar(100),
@email nvarchar(100),
@login nvarchar(100),
@password nvarchar(100)
AS
BEGIN
declare @lastId int
If NOT EXISTS(select * from Users where login=@login)
begin
	insert into Users(firstName,lastName,email,login,password,isBlocked)
	values(@firstName,@lastName,@email,@login,@password,0)
	set @lastId=SCOPE_IDENTITY()
	insert into UsersRoles(userId, roleId)
	values(@lastId,2)
select 1 as result
end
else
select 0 as result	
	 	
END


GO

