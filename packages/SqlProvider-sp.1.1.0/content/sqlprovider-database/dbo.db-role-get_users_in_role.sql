USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-role-get_users_in_role]    Script Date: 2012.07.12. 13:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[db-role-get_users_in_role]  (
	@rolename			nvarchar(100),
	@usernameToMatch	nvarchar(100) = NULL,
	@applicationName	nvarchar(50) = NULL,
	@isEmailAsUserName	bit = 0
)
AS
BEGIN
	select	(case when @isEmailAsUserName=1 then u.email else u.username end) as username 
	from [dbo].[db-user] u  with (nolock)
	INNER JOIN [dbo].[db-user_role] r with (nolock) ON (r.userid=u.id)
	INNER JOIN [dbo].[db-role] r1 with (nolock) ON (r1.id=r.roleid) AND (r1.rolename=@rolename) AND (isnull(r1.applicationName,'')=isnull(@applicationName,''))
	where		(isnull(u.applicationName,'')=isnull(@applicationName,''))
			and	(u.username LIKE (case when @isEmailAsUserName=1 then '' else RTRIM(isnull(@usernameToMatch,'')) end) + '%')
			and	(u.email LIKE (case when @isEmailAsUserName=0 then '' else RTRIM(isnull(@usernameToMatch,'')) end) + '%')
END

GO