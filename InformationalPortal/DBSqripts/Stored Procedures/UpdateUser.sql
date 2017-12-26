USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 12/25/2017 22:24:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateUser]
@firstName nvarchar(100),
@lastName nvarchar(100),
@email nvarchar(100),
@login nvarchar(100),
@password nvarchar(100),
@id int	
AS
BEGIN
	Update Users
	set firstName=@firstName,
	lastName=@lastName,
	email=@email,
	login=@login,
	password=@password	
	where userId=@id	 	
END


GO

