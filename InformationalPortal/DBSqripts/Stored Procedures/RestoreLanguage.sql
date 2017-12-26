USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[RestoreLanguage]    Script Date: 12/25/2017 22:19:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[RestoreLanguage]
@languageId int
AS
BEGIN
	UPDATE Languages
	set deleted=0
	where Languages.languageId=@languageId	
END


GO

