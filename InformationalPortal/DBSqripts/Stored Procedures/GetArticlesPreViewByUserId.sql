USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetArticlesPreViewByUserId]    Script Date: 12/25/2017 22:11:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetArticlesPreViewByUserId]
@userId int	
AS
BEGIN

	select Articles.Id, Articles.name, Articles.pictureLink, Users.userId, Users.login
	from Articles 
	INNER JOIN Users on Articles.userId=Users.userId
	where Users.userId=@userId 	
END


GO

