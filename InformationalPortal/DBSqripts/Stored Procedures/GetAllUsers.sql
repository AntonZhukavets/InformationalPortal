USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 12/25/2017 21:58:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
	select Users.userId,Users.firstName, Users.lastName, Users.email,
	Users.login, Users.password,Users.isBlocked, Roles.roleId, Roles.roleName, Roles.roleDesc
	from Roles
	INNER JOIN UsersRoles on Roles.roleId=UsersRoles.roleId
	INNER JOIN Users on UsersRoles.userId=Users.userId			 	
END

GO

