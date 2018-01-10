USE [InfPortal]
GO
/****** Object:  StoredProcedure [dbo].[AddHeading]    Script Date: 01/09/2018 22:12:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[AddHeading]
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


