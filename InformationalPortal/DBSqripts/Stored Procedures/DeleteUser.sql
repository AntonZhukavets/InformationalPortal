USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 12/25/2017 21:56:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteUser]
@userId int	
AS
BEGIN
	Update Users
	set isBlocked=1
	where userId=@userId	 	
END


GO

