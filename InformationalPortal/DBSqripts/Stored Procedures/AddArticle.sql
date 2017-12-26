USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[AddArticle]    Script Date: 12/25/2017 21:49:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddArticle]
@name nvarchar(100),
@pictureLink nvarchar(MAX),
@userId int,
@languageId int,
@date datetime,
@text nvarchar(MAX),
@videoLink nvarchar(MAX)
	
AS

BEGIN
    declare @lastId int
	insert into Articles(name,pictureLink,userId)
	values(@name,@pictureLink,@userId)	
	set @lastId=SCOPE_IDENTITY()
	insert into Info(id,languageId,date,text,videoLink)
	values(@lastId,@languageId,@date,@text,@videoLink)
	select 	@lastId as "lastid"
END


GO

