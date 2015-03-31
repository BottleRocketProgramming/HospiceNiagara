USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-user-set_last_login]    Script Date: 2012.07.12. 13:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[db-user-set_last_login]  (
	@applicationName	nvarchar(50) = NULL,
	@username			nvarchar(255),
	@isEmailAsUserName	bit = 0
)
AS
BEGIN
	declare @now datetime
	
	select @now = getdate()

	update [dbo].[db-user] set 
		last_login=@now, 
		lastActivityDate=@now,
		isOnLine=1,
		failedPasswordAnswerAttemptWindowStart=@now,
		failedPasswordAttemptWindowStart=@now
	where		(username = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=1) then username else @username end)) 
			and	(email = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=0) then email else @username end))  
			and isnull(applicationName,'')=isnull(@applicationName,'')
END

GO