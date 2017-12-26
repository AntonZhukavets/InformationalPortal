USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetArticlesPreViewByHeadingId]    Script Date: 12/25/2017 22:10:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetArticlesPreViewByHeadingId]
@HeadingId nvarchar(50)	
AS
BEGIN

	select Articles.Id, Articles.name, Articles.pictureLink, Users.userId, Users.login
	from Headings Inner JOIN ArticlesHeadings on 
	Headings.id=ArticlesHeadings.HeadingId INNER JOIN Articles on
	ArticlesHeadings.ArticleId=Articles.id inner Join Users on Articles.userId=Users.userId
	where Headings.id=@HeadingId 	
END


GO

