USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[AddArticle]    Script Date: 12/25/2017 21:39:40 ******/
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

/****** Object:  StoredProcedure [dbo].[AddArticlesHeadings]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddArticlesHeadings]
@articleId int,
@headingId int	
AS
BEGIN
    insert into ArticlesHeadings(ArticleId,HeadingId)
	values(@articleId,@headingId)		
END


GO

/****** Object:  StoredProcedure [dbo].[AddHeading]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddHeading]
@name varchar(100),
@desc varchar(500)
AS
BEGIN
IF EXISTS(select * from Headings where Headings.name=@name and Headings.deleted=1)
	BEGIN
		update Headings set deleted=0
		where name=@name
	END
	Else
	BEGIN	
		insert into Headings(name,description,deleted)
		values(@name,@desc,0)
	END		
END


GO

/****** Object:  StoredProcedure [dbo].[AddLanguage]    Script Date: 12/25/2017 21:39:41 ******/
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

/****** Object:  StoredProcedure [dbo].[DeleteArticle]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteArticle]
@articleId int
AS
BEGIN
	delete from Articles
	where Articles.id=@articleId
	 	
ENd


GO

/****** Object:  StoredProcedure [dbo].[DeleteArticlesHeadings]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteArticlesHeadings]
@articleId int
AS
BEGIN
    delete from ArticlesHeadings
	where ArticleId=@articleId	
END


GO

/****** Object:  StoredProcedure [dbo].[DeleteHeading]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteHeading]
@id int
AS
BEGIN
	Update Headings
	set deleted=1	
	where Headings.id=@id	 	
ENd


GO

/****** Object:  StoredProcedure [dbo].[DeleteLanguage]    Script Date: 12/25/2017 21:39:41 ******/
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

/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteUser]
@userId int	
AS
BEGIN
	Update Users
	set isBlocked=1
	where userId=@userId	 	
ENd


GO

/****** Object:  StoredProcedure [dbo].[GetAllHeadings]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllHeadings]
AS
BEGIN	
	select * from Headings where Headings.deleted=0
END


GO

/****** Object:  StoredProcedure [dbo].[GetAllLanguages]    Script Date: 12/25/2017 21:39:41 ******/
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

/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
	select Users.userId,Users.firstName, Users.lastName, Users.email,
	Users.login, Users.password,Users.isBlocked, Roles.roleId, Roles.roleName, Roles.roleDesc
	from Roles
	INNER JOIN UsersRoles on Roles.roleId=UsersRoles.roleId
	INNER JOIN Users on UsersRoles.userId=Users.userId			 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[GetArticleNamesByInput]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetArticleNamesByInput]
@articleName nvarchar(100)	
AS
BEGIN
	select Articles.name
	from Articles 	
	where Articles.name like '%'+@articleName+'%' 
	 	
ENd


GO

/****** Object:  StoredProcedure [dbo].[GetArticlesByHeadingName]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetArticlesByHeadingName]
@HeadingName nvarchar(50)	
AS
BEGIN

	select Articles.Id, Articles.name, Articles.pictureLink, Users.userId, Users.login
	from Headings 
	Inner JOIN ArticlesHeadings on 	Headings.id=ArticlesHeadings.HeadingId 
	INNER JOIN Articles on	ArticlesHeadings.ArticleId=Articles.id 
	INNER JOIN Users on Articles.userId=Users.userId 
	where Headings.name=@HeadingName 	
END


GO

/****** Object:  StoredProcedure [dbo].[GetArticlesLinksByHeadingId]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetArticlesLinksByHeadingId]
@headingId int
AS 
BEGIN
	select top 1 Articles.id, Articles.name
	from Headings
	INNER JOIN ArticlesHeadings on Headings.id=ArticlesHeadings.HeadingId
	INNER JOIN Articles on ArticlesHeadings.ArticleId=Articles.id 
	INNER JOIN Info on Articles.id=Info.id
	where Headings.id=@headingId
	order by Info.date DESC
			
END


GO

/****** Object:  StoredProcedure [dbo].[GetArticlesPreView]    Script Date: 12/25/2017 21:39:41 ******/
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

/****** Object:  StoredProcedure [dbo].[GetArticlesPreViewByArticleNameOrText]    Script Date: 12/25/2017 21:39:41 ******/
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

/****** Object:  StoredProcedure [dbo].[GetArticlesPreViewByHeadingId]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetArticlesPreViewByHeadingId]
@HeadingId nvarchar(50)	
AS
BEGIN

	select Articles.Id, Articles.name, Articles.pictureLink, Users.userId, Users.login
	from Headings Inner JOIN ArticlesHeadings on 
	Headings.id=ArticlesHeadings.HeadingId INNER JOIN Articles on
	ArticlesHeadings.ArticleId=Articles.id inner Join Users on Articles.userId=Users.userId
	where Headings.id=@HeadingId 	
END


GO

/****** Object:  StoredProcedure [dbo].[GetArticlesPreViewByHeadingIdAndDatePeriod]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetArticlesPreViewByHeadingIdAndDatePeriod]
@headingId int,
@dateFrom datetime,
@dateTo datetime	
AS
BEGIN

	select Articles.Id, Articles.name, Articles.pictureLink, Users.userId, Users.login
	from Headings 
	INNER JOIN ArticlesHeadings on Headings.id=ArticlesHeadings.HeadingId 
	INNER JOIN Articles on ArticlesHeadings.ArticleId=Articles.id
	INNER JOIN Info on Articles.id=Info.id 
	INNER JOIN Users on Articles.userId=Users.userId
	where Headings.id=@HeadingId and Info.date BETWEEN @dateFrom and @dateTo	
END


GO

/****** Object:  StoredProcedure [dbo].[GetArticlesPreViewByUserId]    Script Date: 12/25/2017 21:39:41 ******/
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

/****** Object:  StoredProcedure [dbo].[GetCountOfArticleByHeadingId]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetCountOfArticleByHeadingId]
@headingId int	
AS
BEGIN
declare @countOfArticles int
    select @countOfArticles = Count(Articles.Id)
	from Articles
	INNER JOIN ArticlesHeadings
	on Articles.id=ArticlesHeadings.ArticleId
	INNER JOIN Headings
	on ArticlesHeadings.HeadingId=Headings.id 
	where ArticlesHeadings.HeadingId=@headingId	
	select @countOfArticles
END


GO

/****** Object:  StoredProcedure [dbo].[GetFullArticleById]    Script Date: 12/25/2017 21:39:41 ******/
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

/****** Object:  StoredProcedure [dbo].[GetHeadingsByArticleId]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetHeadingsByArticleId]
@articleId int	
AS
BEGIN
	select distinct Headings.id, Headings.name, Headings.description
	from Articles 
	INNER JOIN ArticlesHeadings on Articles.id=ArticlesHeadings.ArticleId 
	INNER JOIN Headings on ArticlesHeadings.HeadingId=Headings.id
	where Articles.id=@articleId and Headings.deleted=0	 	
END


GO

/****** Object:  StoredProcedure [dbo].[GetHeadingsByArticleName]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetHeadingsByArticleName]
@articleName nvarchar(500)	
AS
BEGIN
	select Headings.id, Headings.name, Headings.description
	from Articles inner join ArticlesHeadings on Articles.id=ArticlesHeadings.ArticleId INNER JOIN Headings on
	ArticlesHeadings.HeadingId=Headings.id
	where Articles.name=@articleName and Headings.deleted=0
	 	
ENd


GO

/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetUserById]
@userId nvarchar(200)	
AS
BEGIN
	select Users.userId,Users.firstName, Users.lastName, Users.email,
	Users.login, Users.password,Users.isBlocked, Roles.roleId, Roles.roleName, Roles.roleDesc
	from Roles
	INNER JOIN UsersRoles on Roles.roleId=UsersRoles.roleId
	INNER JOIN Users on UsersRoles.userId=Users.userId
	where Users.userId=@userId	 	
END

GO

/****** Object:  StoredProcedure [dbo].[GetUserByLoginAndPassword]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetUserByLoginAndPassword]
@login nvarchar(200),
@password nvarchar(200)	
AS
BEGIN
	select Users.userId,Users.firstName, Users.lastName, Users.email,
	Users.login, Users.password,Users.isBlocked, Roles.roleId, Roles.roleName, Roles.roleDesc
	from Roles
	INNER JOIN UsersRoles on Roles.roleId=UsersRoles.roleId
	INNER JOIN Users on UsersRoles.userId=Users.userId
	where Users.login=@login and Users.password=@password
	 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[HeadingDescriptionById]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[HeadingDescriptionById]
@headingId int	
AS
BEGIN
	select description 
	from Headings	
	where Headings.id=@headingId
	 	
ENd


GO

/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Login]
@login nvarchar(200),
@password nvarchar(200)	
AS
BEGIN
	select isExist=COUNT(*) from Users 
	where login=@login and password=@password and isBlocked=0	 	
END

GO

/****** Object:  StoredProcedure [dbo].[MakeAdmin]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[MakeAdmin]
@userId int	
AS
BEGIN
	Update UsersRoles
	set roleId=1
	where userId=@userId	 	
ENd


GO

/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 12/25/2017 21:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[RegisterUser]
@firstName nvarchar(100),
@lastName nvarchar(100),
@email nvarchar(100),
@login nvarchar(100),
@password nvarchar(100)
AS
BEGIN
declare @lastId int
If NOT EXISTS(select * from Users where login=@login)
begin
	insert into Users(firstName,lastName,email,login,password,isBlocked)
	values(@firstName,@lastName,@email,@login,@password,0)
	set @lastId=SCOPE_IDENTITY()
	insert into UsersRoles(userId, roleId)
	values(@lastId,2)
select 1 as result
end
else
select 0 as result	
	 	
ENd


GO

/****** Object:  StoredProcedure [dbo].[RestoreLanguage]    Script Date: 12/25/2017 21:39:42 ******/
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

/****** Object:  StoredProcedure [dbo].[ResumeUser]    Script Date: 12/25/2017 21:39:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ResumeUser]
@userId int	
AS
BEGIN
	Update Users
	set isBlocked=0
	where userId=@userId	 	
ENd


GO

/****** Object:  StoredProcedure [dbo].[UpdateArticle]    Script Date: 12/25/2017 21:39:42 ******/
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
	 	
ENd


GO

/****** Object:  StoredProcedure [dbo].[UpdateArticlesHeadings]    Script Date: 12/25/2017 21:39:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateArticlesHeadings]
@articleId int,
@headingId int	
AS
BEGIN
declare @oldHeadingId int
set @oldHeadingId=@headingId
	Update ArticlesHeadings
	set HeadingId=@headingId		
	where ArticlesHeadings.ArticleId=@articleId and ArticlesHeadings.HeadingId=@oldHeadingId
ENd


GO

/****** Object:  StoredProcedure [dbo].[UpdateHeading]    Script Date: 12/25/2017 21:39:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateHeading]
@id int,
@name nvarchar(100),
@desc nvarchar(100)
AS
BEGIN
	Update Headings
	set name=@name,
	description=@desc	
	where Headings.id=@id	 	
ENd


GO

/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 12/25/2017 21:39:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateUser]
@firstName nvarchar(100),
@lastName nvarchar(100),
@email nvarchar(100),
@login nvarchar(100),
@password nvarchar(100),
@id int	
AS
BEGIN
	Update Users
	set firstName=@firstName,
	lastName=@lastName,
	email=@email,
	login=@login,
	password=@password	
	where userId=@id
	 	
ENd


GO

