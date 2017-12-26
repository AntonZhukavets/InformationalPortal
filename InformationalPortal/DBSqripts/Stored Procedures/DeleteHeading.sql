USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[DeleteHeading]    Script Date: 12/25/2017 21:55:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteHeading]
@id int
AS
BEGIN
	Update Headings
	set deleted=1	
	where Headings.id=@id	 	
END


GO

