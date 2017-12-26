USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 12/25/2017 22:17:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Login]
@login nvarchar(200),
@password nvarchar(200)	
AS
BEGIN
	select isExist=COUNT(*) from Users 
	where login=@login and password=@password and isBlocked=0	 	
END

GO

