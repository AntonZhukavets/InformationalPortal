USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[UpdateArticlesHeadings]    Script Date: 12/25/2017 22:21:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateArticlesHeadings]
@articleId int,
@headingId int	
AS
BEGIN
declare @oldHeadingId int
set @oldHeadingId=@headingId
	Update ArticlesHeadings
	set HeadingId=@headingId		
	where ArticlesHeadings.ArticleId=@articleId and ArticlesHeadings.HeadingId=@oldHeadingId
END


GO

