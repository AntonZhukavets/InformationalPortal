USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetUserByLoginAndPassword]    Script Date: 12/25/2017 22:15:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetUserByLoginAndPassword]
@login nvarchar(200),
@password nvarchar(200)	
AS
BEGIN
	select Users.userId,Users.firstName, Users.lastName, Users.email,
	Users.login, Users.password,Users.isBlocked, Roles.roleId, Roles.roleName, Roles.roleDesc
	from Roles
	INNER JOIN UsersRoles on Roles.roleId=UsersRoles.roleId
	INNER JOIN Users on UsersRoles.userId=Users.userId
	where Users.login=@login and Users.password=@password
	 	
END

GO

