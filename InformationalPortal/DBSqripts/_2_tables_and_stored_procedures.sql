USE [InfPortal]
GO

/****** Object:  Table [dbo].[Articles]    Script Date: 12/12/2017 22:17:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Articles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[pictureLink] [nvarchar](max) NULL,
	[userId] [int] NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [InfPortal]
GO

/****** Object:  Table [dbo].[ArticlesHeadings]    Script Date: 12/12/2017 22:17:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ArticlesHeadings](
	[ArticleId] [int] NOT NULL,
	[HeadingId] [int] NOT NULL,
 CONSTRAINT [PK_ArticlesHeadings] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC,
	[HeadingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [InfPortal]
GO

/****** Object:  Table [dbo].[Headings]    Script Date: 12/12/2017 22:17:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Headings](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[description] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Headings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [InfPortal]
GO

/****** Object:  Table [dbo].[Info]    Script Date: 12/12/2017 22:17:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Info](
	[id] [int] NOT NULL,
	[text] [nvarchar](max) NOT NULL,
	[language] [nvarchar](50) NOT NULL,
	[date] [datetime] NOT NULL,
	[videoLink] [nvarchar](max) NULL,
 CONSTRAINT [PK_Info] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [InfPortal]
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 12/12/2017 22:17:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Roles](
	[roleId] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [nvarchar](50) NOT NULL,
	[roleDesc] [nvarchar](200) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [InfPortal]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 12/12/2017 22:17:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](100) NOT NULL,
	[lastName] [nvarchar](100) NOT NULL,
	[age] [int] NOT NULL,
	[login] [nvarchar](100) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[roleId] [int] NOT NULL,
	[email] [nvarchar](100) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO

ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Users]
GO

ALTER TABLE [dbo].[ArticlesHeadings]  WITH CHECK ADD  CONSTRAINT [FK_ArticlesHeadings_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([id])
GO

ALTER TABLE [dbo].[ArticlesHeadings] CHECK CONSTRAINT [FK_ArticlesHeadings_Articles]
GO

ALTER TABLE [dbo].[ArticlesHeadings]  WITH CHECK ADD  CONSTRAINT [FK_ArticlesHeadings_Headings] FOREIGN KEY([HeadingId])
REFERENCES [dbo].[Headings] ([id])
GO

ALTER TABLE [dbo].[ArticlesHeadings] CHECK CONSTRAINT [FK_ArticlesHeadings_Headings]
GO

ALTER TABLE [dbo].[Info]  WITH CHECK ADD  CONSTRAINT [FK_Info_Articles] FOREIGN KEY([id])
REFERENCES [dbo].[Articles] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Info] CHECK CONSTRAINT [FK_Info_Articles]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([roleId])
REFERENCES [dbo].[Roles] ([roleId])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO


USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[AddArticle]    Script Date: 12/12/2017 22:19:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddArticle]
@name nvarchar(100),
@pictureLink nvarchar(MAX),
@userId int,
@language nvarchar(100),
@date datetime,
@text nvarchar(MAX),
@videoLink nvarchar(MAX)
	
AS

BEGIN
    declare @lastId int
	insert into Articles(name,pictureLink,userId)
	values(@name,@pictureLink,@userId)	
	set @lastId=SCOPE_IDENTITY()
	insert into Info(id,language,date,text,videoLink)
	values(@lastId,@language,@date,@text,@videoLink)
	select 	@lastId as "lastid"
END

GO

/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 12/12/2017 22:19:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteUser]
@userId int	
AS
BEGIN
	delete from Users
	where userId=@userId	 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[GetAllHeadings]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllHeadings]
AS
BEGIN	
	select * from Headings 
END

GO

/****** Object:  StoredProcedure [dbo].[GetArticleById]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetArticleById]
@articleId int	
AS
BEGIN
	select Articles.id,Articles.name,Articles.pictureLink,Info.date,Info.language,Info.text,Info.videoLink,Users.userId,Users.login 
	from Articles inner join Info on Articles.id=Info.id  inner join Users on Articles.userId=Users.userId	
	where Articles.id=@articleId
	 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[GetArticleByName]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetArticleByName]
@articleName nvarchar(100)	
AS
BEGIN
	select Articles.id,Articles.name,Articles.pictureLink,Info.date,Info.language,Info.text,Info.videoLink,Users.userId,Users.login 
	from Articles inner join Info on Articles.id=Info.id  inner join Users on Articles.userId=Users.userId	
	where Articles.name like '%'+@articleName+'%'
	 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[GetArticleNamesByInput]    Script Date: 12/12/2017 22:19:47 ******/
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

/****** Object:  StoredProcedure [dbo].[GetArticles]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[GetArticles]
As
select Articles.id, Articles.name, Articles.pictureLink, Info.text, Info.language, Info.date, Info.videoLink, Users.userId, Users.login 
from Articles INNER JOIN Info on Articles.id=Info.id inner join Users on Articles.userId=Users.userId
GO

/****** Object:  StoredProcedure [dbo].[GetArticlesByHeadingId]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetArticlesByHeadingId]
@HeadingId nvarchar(50)	
AS
BEGIN

	select Articles.Id, Articles.name, Articles.pictureLink, Users.userId, Users.login
	from Headings Inner JOIN ArticlesHeadings on 
	Headings.id=ArticlesHeadings.HeadingId INNER JOIN Articles on
	ArticlesHeadings.ArticleId=Articles.id inner Join Users on Articles.userId=Users.userId
	where Headings.id=@HeadingId 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[GetArticlesByHeadingName]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[GetArticlesByHeadingName]
@HeadingName nvarchar(50)	
AS
BEGIN

	select Articles.Id, Articles.name, Articles.pictureLink
	from Headings Inner JOIN ArticlesHeadings on 
	Headings.id=ArticlesHeadings.HeadingId INNER JOIN Articles on
	ArticlesHeadings.ArticleId=Articles.id
	where Headings.name=@HeadingName 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[GetArticlesByUserId]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetArticlesByUserId]
@userId int	
AS
BEGIN

	select Articles.Id, Articles.name, Articles.pictureLink, Users.userId, Users.login
	from Articles INNER JOIN Users on Articles.userId=Users.userId
	where Users.userId=@userId 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[GetArticlesPreView]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetArticlesPreView]
AS
BEGIN
	select *  
	from Articles		
	 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[GetArticleWithInfoById]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetArticleWithInfoById]
@articleId int	
AS
BEGIN
	select * 
	from ArticlesWithInfo	
	where ArticlesWithInfo.id=@articleId
	 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[GetHeadingsByArticleId]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




Create PROCEDURE [dbo].[GetHeadingsByArticleId]
@articleId int	
AS
BEGIN
	select Headings.id, Headings.name, Headings.description
	from Articles inner join ArticlesHeadings on Articles.id=ArticlesHeadings.ArticleId INNER JOIN Headings on
	ArticlesHeadings.HeadingId=Headings.id
	where Articles.id=@articleId
	 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[GetHeadingsByArticleName]    Script Date: 12/12/2017 22:19:47 ******/
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
	where Articles.name=@articleName
	 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserById]
@userId nvarchar(200)	
AS
BEGIN
	select Users.userId,Users.firstName, Users.lastName, Users.email, Users.login, Users.password, Users.age, Roles.roleId, Roles.roleName, Roles.roleDesc
	from Users INNER JOIN Roles on Users.roleId=Roles.roleId	
	where Users.userId=@userId
	 	
ENd
GO

/****** Object:  StoredProcedure [dbo].[GetUserByLogin]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserByLogin]
@login nvarchar(200)	
AS
BEGIN
	select Users.userId,Users.firstName, Users.lastName, Users.email, Users.login, Users.password, Users.age, Roles.roleId, Roles.roleName, Roles.roleDesc
	from Users INNER JOIN Roles on Users.roleId=Roles.roleId	
	where Users.login=@login
	 	
ENd
GO

/****** Object:  StoredProcedure [dbo].[GetUserIdByLogin]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetUserIdByLogin]
@login nvarchar(200)	
AS
BEGIN
	select Users.userId	 from Users
	where Users.login=@login
	 	
ENd
GO

/****** Object:  StoredProcedure [dbo].[HeadingDescriptionById]    Script Date: 12/12/2017 22:19:47 ******/
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

/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Login]
@login nvarchar(200),
@password nvarchar(200)	
AS
BEGIN
	select isExist=COUNT(*) from Users where login=@login and password=@password	 	
END
GO

/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 12/12/2017 22:19:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegisterUser]
@firstName nvarchar(100),
@lastName nvarchar(100),
@email nvarchar(100),
@login nvarchar(100),
@password nvarchar(100),
@age int	
AS
BEGIN
	insert into Users(firstName,lastName,email,login,password,age,roleId)
	values(@firstName,@lastName,@email,@login,@password,@age,1)
	 	
ENd

GO

/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 12/12/2017 22:19:47 ******/
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
@age int,
@id int	
AS
BEGIN
	Update Users
	set firstName=@firstName,
	lastName=@lastName,
	email=@email,
	login=@login,
	password=@password,
	age=@age
	where userId=@id
	 	
ENd

GO

USE [InfPortal]
GO

/****** Object:  View [dbo].[ArticlesWithInfo]    Script Date: 12/12/2017 22:21:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[ArticlesWithInfo]
AS
	select Articles.id, Articles.name, Articles.pictureLink, Info.text, Info.language,Info.date, Info.videoLink 
	from Articles inner join Info on Articles.id=Info.id

GO

USE [InfPortal]
GO

/****** Object:  User [portal_user]    Script Date: 12/12/2017 22:27:32 ******/
GO

CREATE USER [portal_user] FOR LOGIN [portal_user] WITH DEFAULT_SCHEMA=[dbo]
GO

