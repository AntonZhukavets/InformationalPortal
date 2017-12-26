USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetHeadingsByArticleName]    Script Date: 12/25/2017 22:14:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetHeadingsByArticleName]
@articleName nvarchar(500)	
AS
BEGIN
	select Headings.id, Headings.name, Headings.description
	from Articles inner join ArticlesHeadings on Articles.id=ArticlesHeadings.ArticleId INNER JOIN Headings on
	ArticlesHeadings.HeadingId=Headings.id
	where Articles.name=@articleName and Headings.deleted=0
	 	
END


GO

