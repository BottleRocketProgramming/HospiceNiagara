USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-role-get_roles_for_user]    Script Date: 2012.07.12. 13:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[db-role-get_roles_for_user]  (
	@username			nvarchar(100) = NULL,
	@applicationName	nvarchar(50) = NULL,
	@isEmailAsUserName	bit = 0
)
AS
BEGIN
	select	r1.rolename 
	from [dbo].[db-user] u with (nolock) 
	INNER JOIN [dbo].[db-user_role] r with (nolock) ON (r.userid=u.id)
	INNER JOIN [dbo].[db-role] r1 with (nolock) ON (r1.id=r.roleid) AND (isnull(r1.applicationName,'')=isnull(@applicationName,''))
	where		(u.username = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=1) then u.username else @username end)) 
			AND	(u.email = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=0) then u.email else @username end))  
			AND	(isnull(u.applicationName,'')=isnull(@applicationName,''))
	UNION
	select 'guest' as rolename where not exists (
		select	r1.rolename 
		from [dbo].[db-user] u with (nolock) 
		INNER JOIN [dbo].[db-user_role] r with (nolock) ON (r.userid=u.id)
		INNER JOIN [dbo].[db-role] r1 with (nolock) ON (r1.id=r.roleid) AND (isnull(r1.applicationName,'')=isnull(@applicationName,''))
		where		(u.username = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=1) then u.username else @username end)) 
				AND	(u.email = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=0) then u.email else @username end))  
				AND	(isnull(u.applicationName,'')=isnull(@applicationName,''))
	)
END

GO