USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetArticlesPreViewByHeadingIdAndDatePeriod]    Script Date: 12/25/2017 22:10:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetArticlesPreViewByHeadingIdAndDatePeriod]
@headingId int,
@dateFrom datetime,
@dateTo datetime	
AS
BEGIN

	select Articles.Id, Articles.name, Articles.pictureLink, Users.userId, Users.login
	from Headings 
	INNER JOIN ArticlesHeadings on Headings.id=ArticlesHeadings.HeadingId 
	INNER JOIN Articles on ArticlesHeadings.ArticleId=Articles.id
	INNER JOIN Info on Articles.id=Info.id 
	INNER JOIN Users on Articles.userId=Users.userId
	where Headings.id=@HeadingId and Info.date BETWEEN @dateFrom and @dateTo	
END


GO

