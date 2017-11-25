
USE [InfPortal]
GO
/****** Object:  Table [dbo].[Articles]    Script Date: 11/25/2017 16:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[pictureLink] [nvarchar](max) NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Headings]    Script Date: 11/25/2017 16:27:37 ******/
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
/****** Object:  StoredProcedure [dbo].[HeadingDescriptionById]    Script Date: 11/25/2017 16:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE PROCEDURE GetAllHeadings
--AS
--BEGIN	
--	select * from Headings 
--END
--GO


--CREATE PROCEDURE GetArticlesByHeadingName
--@HeadingName nvarchar(50)	
--AS
--BEGIN

--	select Articles.Id, Articles.name, Articles.pictureLink
--	from Headings Inner JOIN ArticlesHeadings on 
--	Headings.id=ArticlesHeadings.HeadingId INNER JOIN Articles on
--	ArticlesHeadings.ArticleId=Articles.id
--	where Headings.name=@HeadingName 	
--ENd
--GO


--CREATE PROCEDURE GetHeadingsByArticleName
--@articleName nvarchar(500)	
--AS
--BEGIN
--	select Headings.id, Headings.name, Headings.description
--	from Articles inner join ArticlesHeadings on Articles.id=ArticlesHeadings.ArticleId INNER JOIN Headings on
--	ArticlesHeadings.HeadingId=Headings.id
--	where Articles.name=@articleName
	 	
--ENd
--GO
--CREATE PROCEDURE GetArticleWithInfoById
--@articleId int	
--AS
--BEGIN
--	select * 
--	from ArticlesWithInfo	
--	where ArticlesWithInfo.id=@articleId
	 	
--ENd
--GO


--CREATE View ArticlesWithInfo
--AS
--	select Articles.id, Articles.name, Articles.pictureLink, Info.text, Info.language,Info.date, Info.videoLink 
--	from Articles inner join Info on Articles.id=Info.id 	 	

--GO
CREATE PROCEDURE [dbo].[HeadingDescriptionById]
@headingId int	
AS
BEGIN
	select description 
	from Headings	
	where Headings.id=@headingId
	 	
ENd
GO
/****** Object:  Table [dbo].[ArticlesHeadings]    Script Date: 11/25/2017 16:27:36 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllHeadings]    Script Date: 11/25/2017 16:27:36 ******/
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
/****** Object:  Table [dbo].[Info]    Script Date: 11/25/2017 16:27:37 ******/
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
/****** Object:  StoredProcedure [dbo].[GetArticlesPreView]    Script Date: 11/25/2017 16:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE PROCEDURE GetAllHeadings
--AS
--BEGIN	
--	select * from Headings 
--END
--GO


--CREATE PROCEDURE GetArticlesByHeadingName
--@HeadingName nvarchar(50)	
--AS
--BEGIN

--	select Articles.Id, Articles.name, Articles.pictureLink
--	from Headings Inner JOIN ArticlesHeadings on 
--	Headings.id=ArticlesHeadings.HeadingId INNER JOIN Articles on
--	ArticlesHeadings.ArticleId=Articles.id
--	where Headings.name=@HeadingName 	
--ENd
--GO


--CREATE PROCEDURE GetHeadingsByArticleName
--@articleName nvarchar(500)	
--AS
--BEGIN
--	select Headings.id, Headings.name, Headings.description
--	from Articles inner join ArticlesHeadings on Articles.id=ArticlesHeadings.ArticleId INNER JOIN Headings on
--	ArticlesHeadings.HeadingId=Headings.id
--	where Articles.name=@articleName
	 	
--ENd
--GO
--CREATE PROCEDURE GetArticleWithInfoById
--@articleId int	
--AS
--BEGIN
--	select * 
--	from ArticlesWithInfo	
--	where ArticlesWithInfo.id=@articleId
	 	
--ENd
--GO


--CREATE View ArticlesWithInfo
--AS
--	select Articles.id, Articles.name, Articles.pictureLink, Info.text, Info.language,Info.date, Info.videoLink 
--	from Articles inner join Info on Articles.id=Info.id 	 	

--GO
CREATE PROCEDURE [dbo].[GetArticlesPreView]
AS
BEGIN
	select *  
	from Articles		
	 	
ENd
GO
/****** Object:  StoredProcedure [dbo].[GetArticlesByHeadingName]    Script Date: 11/25/2017 16:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE PROCEDURE GetAllHeadings
--AS
--BEGIN	
--	select * from Headings 
--END
--GO


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
/****** Object:  StoredProcedure [dbo].[GetArticleById]    Script Date: 11/25/2017 16:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE PROCEDURE GetAllHeadings
--AS
--BEGIN	
--	select * from Headings 
--END
--GO


--CREATE PROCEDURE GetArticlesByHeadingName
--@HeadingName nvarchar(50)	
--AS
--BEGIN

--	select Articles.Id, Articles.name, Articles.pictureLink
--	from Headings Inner JOIN ArticlesHeadings on 
--	Headings.id=ArticlesHeadings.HeadingId INNER JOIN Articles on
--	ArticlesHeadings.ArticleId=Articles.id
--	where Headings.name=@HeadingName 	
--ENd
--GO


--CREATE PROCEDURE GetHeadingsByArticleName
--@articleName nvarchar(500)	
--AS
--BEGIN
--	select Headings.id, Headings.name, Headings.description
--	from Articles inner join ArticlesHeadings on Articles.id=ArticlesHeadings.ArticleId INNER JOIN Headings on
--	ArticlesHeadings.HeadingId=Headings.id
--	where Articles.name=@articleName
	 	
--ENd
--GO
CREATE PROCEDURE [dbo].[GetArticleById]
@articleId int	
AS
BEGIN
	select * 
	from Articles inner join Info on Articles.id=Info.id 	
	where Articles.id=@articleId
	 	
ENd
GO
/****** Object:  View [dbo].[ArticlesWithInfo]    Script Date: 11/25/2017 16:27:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE PROCEDURE GetAllHeadings
--AS
--BEGIN	
--	select * from Headings 
--END
--GO


--CREATE PROCEDURE GetArticlesByHeadingName
--@HeadingName nvarchar(50)	
--AS
--BEGIN

--	select Articles.Id, Articles.name, Articles.pictureLink
--	from Headings Inner JOIN ArticlesHeadings on 
--	Headings.id=ArticlesHeadings.HeadingId INNER JOIN Articles on
--	ArticlesHeadings.ArticleId=Articles.id
--	where Headings.name=@HeadingName 	
--ENd
--GO


--CREATE PROCEDURE GetHeadingsByArticleName
--@articleName nvarchar(500)	
--AS
--BEGIN
--	select Headings.id, Headings.name, Headings.description
--	from Articles inner join ArticlesHeadings on Articles.id=ArticlesHeadings.ArticleId INNER JOIN Headings on
--	ArticlesHeadings.HeadingId=Headings.id
--	where Articles.name=@articleName
	 	
--ENd
--GO
--Alter PROCEDURE GetArticleWithInfoById
--@articleId int	
--AS
--BEGIN
--	select * 
--	from ArticlesWithInfo	
--	where ArticlesWithInfo.id=@articleName
	 	
--ENd
--GO


CREATE View [dbo].[ArticlesWithInfo]
AS
	select Articles.id, Articles.name, Articles.pictureLink, Info.text, Info.language,Info.date, Info.videoLink 
	from Articles inner join Info on Articles.id=Info.id
GO
/****** Object:  StoredProcedure [dbo].[GetHeadingsByArticleName]    Script Date: 11/25/2017 16:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE PROCEDURE GetAllHeadings
--AS
--BEGIN	
--	select * from Headings 
--END
--GO


--CREATE PROCEDURE GetArticlesByHeadingName
--@HeadingName nvarchar(50)	
--AS
--BEGIN

--	select Articles.Id, Articles.name, Articles.pictureLink
--	from Headings Inner JOIN ArticlesHeadings on 
--	Headings.id=ArticlesHeadings.HeadingId INNER JOIN Articles on
--	ArticlesHeadings.ArticleId=Articles.id
--	where Headings.name=@HeadingName 	
--ENd
--GO


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
/****** Object:  StoredProcedure [dbo].[GetArticleWithInfoById]    Script Date: 11/25/2017 16:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE PROCEDURE GetAllHeadings
--AS
--BEGIN	
--	select * from Headings 
--END
--GO


--CREATE PROCEDURE GetArticlesByHeadingName
--@HeadingName nvarchar(50)	
--AS
--BEGIN

--	select Articles.Id, Articles.name, Articles.pictureLink
--	from Headings Inner JOIN ArticlesHeadings on 
--	Headings.id=ArticlesHeadings.HeadingId INNER JOIN Articles on
--	ArticlesHeadings.ArticleId=Articles.id
--	where Headings.name=@HeadingName 	
--ENd
--GO


--CREATE PROCEDURE GetHeadingsByArticleName
--@articleName nvarchar(500)	
--AS
--BEGIN
--	select Headings.id, Headings.name, Headings.description
--	from Articles inner join ArticlesHeadings on Articles.id=ArticlesHeadings.ArticleId INNER JOIN Headings on
--	ArticlesHeadings.HeadingId=Headings.id
--	where Articles.name=@articleName
	 	
--ENd
--GO
CREATE PROCEDURE [dbo].[GetArticleWithInfoById]
@articleId int	
AS
BEGIN
	select * 
	from ArticlesWithInfo	
	where ArticlesWithInfo.id=@articleId
	 	
ENd
GO
/****** Object:  ForeignKey [FK_ArticlesHeadings_Articles]    Script Date: 11/25/2017 16:27:36 ******/
ALTER TABLE [dbo].[ArticlesHeadings]  WITH CHECK ADD  CONSTRAINT [FK_ArticlesHeadings_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([id])
GO
ALTER TABLE [dbo].[ArticlesHeadings] CHECK CONSTRAINT [FK_ArticlesHeadings_Articles]
GO
/****** Object:  ForeignKey [FK_ArticlesHeadings_Headings]    Script Date: 11/25/2017 16:27:36 ******/
ALTER TABLE [dbo].[ArticlesHeadings]  WITH CHECK ADD  CONSTRAINT [FK_ArticlesHeadings_Headings] FOREIGN KEY([HeadingId])
REFERENCES [dbo].[Headings] ([id])
GO
ALTER TABLE [dbo].[ArticlesHeadings] CHECK CONSTRAINT [FK_ArticlesHeadings_Headings]
GO
/****** Object:  ForeignKey [FK_Info_Articles]    Script Date: 11/25/2017 16:27:37 ******/
ALTER TABLE [dbo].[Info]  WITH CHECK ADD  CONSTRAINT [FK_Info_Articles] FOREIGN KEY([id])
REFERENCES [dbo].[Articles] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Info] CHECK CONSTRAINT [FK_Info_Articles]
GO
