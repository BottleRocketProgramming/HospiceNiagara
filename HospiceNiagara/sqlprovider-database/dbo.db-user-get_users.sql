USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-user-get_users]    Script Date: 2012.07.15. 0:04:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[db-user-get_users](
	@guid					uniqueidentifier = NULL,
	@username				nvarchar(255) = NULL,
	@email					nvarchar(100) = NULL,
	@emailToMatch			nvarchar(100) = NULL,
	@usernameToMatch		nvarchar(255) = NULL,
	@userIsOnline			bit = 0,
	@isEmailAsUserName		bit = 0,
	@applicationName		nvarchar(50) = NULL
) AS
BEGIN
	declare @rowcnt int

	if (((@guid IS NOT NULL) OR (nullif(@username, '') IS NOT NULL)) AND (@userIsOnline > 0))
	begin
		UPDATE [dbo].[db-user]	
		SET isOnLine=1, lastActivityDate=getdate() 
		WHERE		(username = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=1) then username else @username end)) 
				AND	(email = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=0) then email else @username end))  
				AND	(email = (case when nullif(@email, '') is null then email else @email end))  
				AND	([guid]= (case when @guid is null then [guid] else @guid end))
				AND ([status]=1)
				AND (isLockedOut=0)
				AND	(isnull(applicationName,'')=isnull(@applicationName,''))
	end
		
	SELECT 
		[guid],
		(case when @isEmailAsUserName=1 then email else username end) as username,
		email,
		passwordQuestion,
		NULL as comment,
		cast((case when [status]=1 then 1 else 0 end) as bit) as isApproved,
		isLockedOut,
		entered as creationDate,
		last_login,
		lastActivityDate,
		lastPasswordChangedDate,
		lastLockedOutDate,
		id,
		[status],
		first_name,
		last_name,
		isOnLine,
		birthdate,
		passwordFormat
	FROM [dbo].[db-user]  with (nolock)
	WHERE		(username = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=1) then username else @username end)) 
			AND	(email = (case when (nullif(@username, '') is null) or (@isEmailAsUserName=0) then email else @username end))  
			AND	(email = (case when nullif(@email, '') is null then email else @email end))  
			AND	([guid]= (case when @guid is null then [guid] else @guid end))
			AND	(username LIKE RTRIM(isnull(@usernameToMatch, '')) + '%')
			AND	([email] LIKE RTRIM(isnull(@emailToMatch, '')) + '%')
			AND	(isnull(applicationName,'')=isnull(@applicationName,''))
END

GO
