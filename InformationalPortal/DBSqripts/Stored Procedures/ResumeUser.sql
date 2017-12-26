USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[ResumeUser]    Script Date: 12/25/2017 22:20:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ResumeUser]
@userId int	
AS
BEGIN
	Update Users
	set isBlocked=0
	where userId=@userId	 	
ENd


GO

