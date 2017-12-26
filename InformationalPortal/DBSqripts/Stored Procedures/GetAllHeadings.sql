USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetAllHeadings]    Script Date: 12/25/2017 21:57:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllHeadings]
AS
BEGIN	
	select * from Headings where Headings.deleted=0
END


GO

