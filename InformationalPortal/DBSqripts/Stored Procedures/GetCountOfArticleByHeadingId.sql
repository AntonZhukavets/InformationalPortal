USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetCountOfArticleByHeadingId]    Script Date: 12/25/2017 22:11:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetCountOfArticleByHeadingId]
@headingId int	
AS
BEGIN
declare @countOfArticles int
    select @countOfArticles = Count(Articles.Id)
	from Articles
	INNER JOIN ArticlesHeadings
	on Articles.id=ArticlesHeadings.ArticleId
	INNER JOIN Headings
	on ArticlesHeadings.HeadingId=Headings.id 
	where ArticlesHeadings.HeadingId=@headingId	
	select @countOfArticles
END


GO

