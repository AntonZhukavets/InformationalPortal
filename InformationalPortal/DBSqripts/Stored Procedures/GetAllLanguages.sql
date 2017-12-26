USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[GetAllLanguages]    Script Date: 12/25/2017 21:58:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllLanguages]
AS
BEGIN
	select Languages.languageId, Languages.languageName
	from Languages
	where Languages.deleted=0	
END


GO

