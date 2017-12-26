USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[AddArticlesHeadings]    Script Date: 12/25/2017 21:50:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddArticlesHeadings]
@articleId int,
@headingId int	
AS
BEGIN
    insert into ArticlesHeadings(ArticleId,HeadingId)
	values(@articleId,@headingId)		
END


GO

