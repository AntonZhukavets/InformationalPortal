USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[DeleteArticle]    Script Date: 12/25/2017 21:53:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteArticle]
@articleId int
AS
BEGIN
	delete from Articles
	where Articles.id=@articleId
	 	
END


GO

