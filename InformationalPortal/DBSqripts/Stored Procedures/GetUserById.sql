USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 12/25/2017 22:15:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetUserById]
@userId nvarchar(200)	
AS
BEGIN
	select Users.userId,Users.firstName, Users.lastName, Users.email,
	Users.login, Users.password,Users.isBlocked, Roles.roleId, Roles.roleName, Roles.roleDesc
	from Roles
	INNER JOIN UsersRoles on Roles.roleId=UsersRoles.roleId
	INNER JOIN Users on UsersRoles.userId=Users.userId
	where Users.userId=@userId	 	
END

GO

