USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-user-delete_user]    Script Date: 2012.07.15. 0:04:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[db-user-delete_user](
	@guid					uniqueidentifier = NULL,
	@username				nvarchar(100) = NULL,
	@deleteAllRelatedData	bit = 0,
	@applicationName		nvarchar(50) = NULL,
	@isEmailAsUserName		bit = 0
) AS
BEGIN
	declare @rowcnt int
	declare @userid int

	UPDATE [dbo].[db-user] SET [status]=3 
	WHERE		([guid] = (case when @guid is null then [guid] else @guid end))
			AND ([status]!=3)
			AND	(username = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=1) then username else @username end))
			AND	(email =  (case when (nullif(@username, '') is null) or (@isEmailAsUserName=0) then email else @username end))
			AND	(isnull(applicationName,'')=isnull(@applicationName,''))

	SELECT	@rowcnt=@@ROWCOUNT

	if (@rowcnt > 0)
	begin
		SELECT @userid=id FROM [dbo].[db-user] 
		WHERE		([guid] = (case when @guid is null then [guid] else @guid end))
				AND	(username = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=1) then username else @username end))
			AND	(email =  (case when (nullif(@username, '') is null) or (@isEmailAsUserName=0) then email else @username end))
				AND	(isnull(applicationName,'')=isnull(@applicationName,''))

		DELETE [dbo].[db-user_role] WHERE (userid = @userid)
		
		if (@deleteAllRelatedData > 0)
		begin
			DELETE [dbo].[db-user_role] WHERE (userid = @userid)
		end
	end

	SELECT	@rowcnt AS rowcnt
END

GO
