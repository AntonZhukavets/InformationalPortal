USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[UpdateHeading]    Script Date: 12/25/2017 22:22:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateHeading]
@id int,
@name nvarchar(100),
@desc nvarchar(100)
AS
BEGIN
	Update Headings
	set name=@name,
	description=@desc	
	where Headings.id=@id	 	
END


GO

