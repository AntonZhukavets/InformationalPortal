USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetArticlesPreViewByArticleNameOrText]    Script Date: 12/25/2017 22:09:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetArticlesPreViewByArticleNameOrText]
@articleName nvarchar(100)	
AS
BEGIN
	select Articles.id,Articles.name,Articles.pictureLink,Users.userId,Users.login 
	from Articles 
	INNER JOIN Info on Articles.id=Info.id	
	INNER JOIN Users on Articles.userId=Users.userId	
	where Articles.name like '%'+@articleName+'%' or Info.text like '%'+@articleName+'%'
	 	
END


GO

