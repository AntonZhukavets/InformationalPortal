USE [InfPortal]
GO

/****** Object:  Table [dbo].[ArticlesHeadings]    Script Date: 12/25/2017 21:43:55 ******/
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

