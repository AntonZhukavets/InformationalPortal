USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[AddLanguage]    Script Date: 12/25/2017 21:52:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddLanguage]
@languageName nvarchar(100)
AS
BEGIN
	IF NOT EXISTS( select * from Languages where Languages.languageName=@languageName)
		BEGIN
			INSERT INTO Languages(languageName, deleted)
			values(@languageName, 0)
			SELECT 1 AS RESULT
		END
	ELSE
		BEGIN
			SELECT 0 AS RESULT
		END		
END


GO

