USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetFullArticleById]    Script Date: 12/25/2017 22:12:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetFullArticleById]
@articleId int	
AS
BEGIN
	select Articles.id,Articles.name,Articles.pictureLink,Info.date,
	Languages.languageId, Languages.languageName, Info.text,Info.videoLink,
	Users.userId,Users.login 
	from Articles
	INNER JOIN Info on Articles.id=Info.id 
	INNER JOIN Languages on Info.languageId=Languages.languageId
	INNER JOIN Users on Articles.userId=Users.userId	
	where Articles.id=@articleId	 	
END


GO

