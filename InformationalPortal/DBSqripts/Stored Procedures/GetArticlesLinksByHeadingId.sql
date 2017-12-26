USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetArticlesLinksByHeadingId]    Script Date: 12/25/2017 22:08:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetArticlesLinksByHeadingId]
@headingId int
AS 
BEGIN
	select top 1 Articles.id, Articles.name
	from Headings
	INNER JOIN ArticlesHeadings on Headings.id=ArticlesHeadings.HeadingId
	INNER JOIN Articles on ArticlesHeadings.ArticleId=Articles.id 
	INNER JOIN Info on Articles.id=Info.id
	where Headings.id=@headingId
	order by Info.date DESC
			
END


GO

