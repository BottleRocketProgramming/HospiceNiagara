USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-role-create_role]    Script Date: 2012.07.12. 13:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[db-role-create_role]  (
	@applicationName	nvarchar(50) = NULL,
	@rolename			nvarchar(50)
)
AS
BEGIN
	insert into [dbo].[db-role] (rolename,applicationName) values (@rolename,@applicationName)

	SELECT	@@ROWCOUNT as rowcnt
END

GO