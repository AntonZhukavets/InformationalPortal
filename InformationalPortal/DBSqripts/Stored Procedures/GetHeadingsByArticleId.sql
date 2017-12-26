USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetHeadingsByArticleId]    Script Date: 12/25/2017 22:13:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetHeadingsByArticleId]
@articleId int	
AS
BEGIN
	select distinct Headings.id, Headings.name, Headings.description
	from Articles 
	INNER JOIN ArticlesHeadings on Articles.id=ArticlesHeadings.ArticleId 
	INNER JOIN Headings on ArticlesHeadings.HeadingId=Headings.id
	where Articles.id=@articleId and Headings.deleted=0	 	
END


GO

