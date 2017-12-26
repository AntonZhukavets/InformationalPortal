USE [InfPortal]
GO

/****** Object:  StoredProcedure [dbo].[AddHeading]    Script Date: 12/25/2017 21:51:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddHeading]
@name varchar(100),
@desc varchar(500)
AS
BEGIN
	insert into Headings(name,description,deleted)
	values(@name,@desc,0)		
END


GO

