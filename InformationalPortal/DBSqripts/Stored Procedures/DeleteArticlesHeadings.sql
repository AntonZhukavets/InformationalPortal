USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[DeleteArticlesHeadings]    Script Date: 12/25/2017 21:53:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteArticlesHeadings]
@articleId int
AS
BEGIN
    delete from ArticlesHeadings
	where ArticleId=@articleId	
END


GO

