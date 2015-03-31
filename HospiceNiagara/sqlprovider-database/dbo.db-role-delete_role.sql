USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-role-delete_role]    Script Date: 2012.07.12. 13:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[db-role-delete_role]  (
	@applicationName	nvarchar(50) = NULL,
	@rolename	nvarchar(50) = NULL,
	@throwOnPopulatedRole	bit = 1
)
AS
BEGIN
	declare @rowcnt int

	if ((@throwOnPopulatedRole = 1) and (@rolename = 'admin'))
	begin
		SELECT	@rowcnt=0
	end
	else
	begin
		DELETE from [dbo].[db-role] 
		where		isnull(applicationName,'')=isnull(@applicationName,'')
				and	rolename=@rolename

		SELECT	@rowcnt=@@ROWCOUNT
	end

	SELECT	@rowcnt AS rowcnt
END

GO