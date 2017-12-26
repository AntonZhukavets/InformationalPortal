USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetArticleNamesByInput]    Script Date: 12/25/2017 22:06:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetArticleNamesByInput]
@articleName nvarchar(100)	
AS
BEGIN
	select Articles.name
	from Articles 	
	where Articles.name like '%'+@articleName+'%' 
	 	
END


GO

