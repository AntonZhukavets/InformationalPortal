USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[DeleteLanguage]    Script Date: 12/25/2017 21:56:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteLanguage]
@languageId int
AS
BEGIN
	UPDATE Languages
	set deleted=1
	where Languages.languageId=@languageId	
END


GO

