USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[MakeAdmin]    Script Date: 12/25/2017 22:17:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[MakeAdmin]
@userId int	
AS
BEGIN
	Update UsersRoles
	set roleId=1
	where userId=@userId	 	
END


GO

