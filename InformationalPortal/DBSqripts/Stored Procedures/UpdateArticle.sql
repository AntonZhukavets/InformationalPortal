USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[UpdateArticle]    Script Date: 12/25/2017 22:20:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateArticle]
@id int,
@name nvarchar(100),
@pictureLink nvarchar(100),
@text nvarchar(MAX),
@languageId int,
@date datetime,
@videoLink nvarchar(MAX)
	
AS
BEGIN
	Update Articles
	set name=@name,
	pictureLink=@pictureLink	
	where Articles.id=@id
	
	Update Info
	set text=@text,
	languageId=@languageId,
	date=@date,
	videoLink=@videoLink	
	where Info.id=@id
	 	
END


GO

