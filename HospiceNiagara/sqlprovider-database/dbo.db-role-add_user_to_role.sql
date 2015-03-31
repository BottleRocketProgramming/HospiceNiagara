USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-role-add_user_to_role]    Script Date: 2012.07.12. 13:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[db-role-add_user_to_role]  (
	@applicationName	nvarchar(50) = NULL,
	@rolename			nvarchar(50),
	@username			nvarchar(255),
	@isEmailAsUserName	bit = 0
)
AS
BEGIN
	declare @rowcnt int
	declare @userid int
	declare @roleid int
	
	select @userid = u.id from [dbo].[db-user] u  with (nolock)
	where		(u.username = (case when @isEmailAsUserName=1 then u.username else @username end))
			and	(u.email = (case when @isEmailAsUserName=0 then u.email else @username end))
			and	(isnull(u.applicationName,'')=isnull(@applicationName,''))
	
	select @roleid = r.id from [dbo].[db-role] r  with (nolock)
	where	(r.rolename=@rolename) AND	(isnull(r.applicationName,'')=isnull(@applicationName,''))

	if ((@userid is not null) and (@roleid is not null))
	begin
		insert into [dbo].[db-user_role] (userid,roleid) values (@userid,@roleid)

		SELECT	@rowcnt=@@ROWCOUNT
	end
	else
	begin 
		SELECT	@rowcnt=0
	end

	SELECT	@rowcnt AS rowcnt
END

GO