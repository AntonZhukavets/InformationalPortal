USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[HeadingDescriptionById]    Script Date: 12/25/2017 22:16:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[HeadingDescriptionById]
@headingId int	
AS
BEGIN
	select description 
	from Headings	
	where Headings.id=@headingId
	 	
END


GO

