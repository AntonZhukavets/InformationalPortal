USE [InfPortal]
GO

/****** Object:  Table [dbo].[Info]    Script Date: 12/25/2017 21:45:23 ******/
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

