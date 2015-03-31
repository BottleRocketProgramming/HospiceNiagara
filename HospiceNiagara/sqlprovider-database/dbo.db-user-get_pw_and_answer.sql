USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-user-get_pw_and_answer]    Script Date: 2012.07.12. 13:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[db-user-get_pw_and_answer]  (
	@applicationName	nvarchar(50) = NULL,
	@username			nvarchar(255),
	@isEmailAsUserName	bit = 0
)
AS
BEGIN
	select 
		passwordAnswer, 
		isLockedOut, 
		pw,
		cast((case when [status]=1 then 1 else 0 end) as bit) as isApproved,
		passwordFormat
	from [dbo].[db-user] with (nolock)
	where		(username = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=1) then username else @username end)) 
			and	(email = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=0) then email else @username end))  
			and isnull(applicationName,'')=isnull(@applicationName,'')
END

GO