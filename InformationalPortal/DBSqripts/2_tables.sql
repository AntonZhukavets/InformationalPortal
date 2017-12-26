USE [InfPortal]
GO

/****** Object:  Table [dbo].[Articles]    Script Date: 12/25/2017 21:37:15 ******/
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

/****** Object:  Table [dbo].[ArticlesHeadings]    Script Date: 12/25/2017 21:37:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ArticlesHeadings](
	[ArticleId] [int] NOT NULL,
	[HeadingId] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ArticlesHeadings] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC,
	[HeadingId] ASC,
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [InfPortal]
GO

/****** Object:  Table [dbo].[Headings]    Script Date: 12/25/2017 21:37:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Headings](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[description] [nvarchar](500) NULL,
	[deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Headings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [InfPortal]
GO

/****** Object:  Table [dbo].[Info]    Script Date: 12/25/2017 21:37:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Info](
	[id] [int] NOT NULL,
	[text] [nvarchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
	[videoLink] [nvarchar](max) NULL,
	[languageId] [int] NOT NULL,
 CONSTRAINT [PK_Info] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [InfPortal]
GO

/****** Object:  Table [dbo].[Languages]    Script Date: 12/25/2017 21:37:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Languages](
	[languageId] [int] IDENTITY(1,1) NOT NULL,
	[languageName] [nvarchar](100) NOT NULL,
	[deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[languageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [InfPortal]
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 12/25/2017 21:37:15 ******/
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

/****** Object:  Table [dbo].[Users]    Script Date: 12/25/2017 21:37:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](100) NOT NULL,
	[lastName] [nvarchar](100) NOT NULL,
	[login] [nvarchar](100) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[isBlocked] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [InfPortal]
GO

/****** Object:  Table [dbo].[UsersRoles]    Script Date: 12/25/2017 21:37:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsersRoles](
	[userId] [int] NOT NULL,
	[roleId] [int] NOT NULL,
 CONSTRAINT [PK_UsersRoles] PRIMARY KEY CLUSTERED 
(
	[userId] ASC,
	[roleId] ASC
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

ALTER TABLE [dbo].[Info]  WITH CHECK ADD  CONSTRAINT [FK_Info_Languages] FOREIGN KEY([languageId])
REFERENCES [dbo].[Languages] ([languageId])
GO

ALTER TABLE [dbo].[Info] CHECK CONSTRAINT [FK_Info_Languages]
GO

ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Roles] FOREIGN KEY([roleId])
REFERENCES [dbo].[Roles] ([roleId])
GO

ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Roles]
GO

ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO

ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Users]
GO

