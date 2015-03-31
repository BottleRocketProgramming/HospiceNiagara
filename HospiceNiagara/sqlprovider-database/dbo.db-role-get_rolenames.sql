USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-role-get_rolenames]    Script Date: 2012.07.12. 13:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[db-role-get_rolenames]  (
	@applicationName	nvarchar(50) = NULL
)
AS
BEGIN
	SELECT distinct	rolename from [dbo].[db-role] with (nolock) where isnull(applicationName,'')=isnull(@applicationName,'')
	UNION
	SELECT 'guest' as rolename where not exists (
		SELECT rolename from [dbo].[db-role] with (nolock) where isnull(applicationName,'')=isnull(@applicationName,'')
	)
END

GO