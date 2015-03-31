USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-user-unlock_user]    Script Date: 2012.07.15. 0:04:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[db-user-unlock_user](
	@applicationName	nvarchar(50) = NULL,
	@username			nvarchar(100) = NULL,
	@isEmailAsUserName	bit = 0
) AS
BEGIN
	update [dbo].[db-user] set [status]=1, isLockedOut=0 
	where		(username = (case when @isEmailAsUserName=1 then username else @username end))
			and	(email = (case when @isEmailAsUserName=0 then email else @username end))
			and ([status]=2)
			and	(isLockedOut=1)
			and isnull(applicationName,'')=isnull(@applicationName,'')
		
	SELECT	@@ROWCOUNT AS rowcnt
END

GO
