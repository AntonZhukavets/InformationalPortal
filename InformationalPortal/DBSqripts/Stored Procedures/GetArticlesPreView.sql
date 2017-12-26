USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetArticlesPreView]    Script Date: 12/25/2017 22:08:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[GetArticlesPreView]
As
select Articles.id, Articles.name, Articles.pictureLink, Users.userId, Users.login 
from Articles 
INNER JOIN Info on Articles.id=Info.id 
INNER JOIN Users on Articles.userId=Users.userId

GO

