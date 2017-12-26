/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [portal_user]    Script Date: 12/25/2017 22:34:51 ******/
CREATE LOGIN [portal_user] WITH PASSWORD=N'f±KúåÎZOIy<¿kIp¼Äèw"ÜSr_', DEFAULT_DATABASE=[InfPortal], DEFAULT_LANGUAGE=[русский], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

EXEC sys.sp_addsrvrolemember @loginame = N'portal_user', @rolename = N'sysadmin'
GO

ALTER LOGIN [portal_user] DISABLE
GO

