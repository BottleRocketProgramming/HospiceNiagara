USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-user-set_user]    Script Date: 2012.07.12. 13:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[db-user-set_user]  (
	@applicationName			nvarchar(50) = NULL,
	@username					nvarchar(255),
	@email						nvarchar(100),
	@comment					nvarchar(255),
	@isApproved					bit,
	@firstName					nvarchar(50),
	@lastName					nvarchar(50),
	@birthDate					datetime,
	@status						int,

	@isEmailAsUserName			bit = 0,
	@isNewUser					bit = 0,

	@guid						uniqueidentifier = NULL,
	@password					nvarchar(50) = NULL,
	@passwordQuestion			nvarchar(255) = NULL,
	@passwordAnswer				nvarchar(255) = NULL,
	@passwordFormat				int = 0,
	@isNewUserLoggedIn			bit = 0

)
AS
BEGIN
	declare @now datetime
	declare @user nvarchar(255)
	
	select @now=getdate()

	if (@isNewUser = 0)
	begin
		if (@isEmailAsUserName = 1)
		begin
			update [dbo].[db-user] set	
				[comment]=@comment,
				[first_name]=@firstName,
				[last_name]=@lastName,
				[birthdate]=@birthDate,
				[status]=@status,
				[updated]=@now
			where email=@email and (isnull(applicationName,'')=isnull(@applicationName,''))
		end
		else
		begin
			update [dbo].[db-user] set 
				[comment]=@comment,
				[first_name]=@firstName,
				[last_name]=@lastName,
				[birthdate]=@birthDate,
				[status]=@status,
				[updated]=@now
			where username=@username and (isnull(applicationName,'')=isnull(@applicationName,''))
		end
	end
	else
	begin
		if (@isEmailAsUserName = 1)
		begin
			select @user=@email
		end

		INSERT INTO [dbo].[db-user] (
				[guid], 
				[status], 
				[username], 
				[applicationName], 
				[email], 
				[pw], 
				[passwordQuestion], 
				[passwordAnswer], 
				[passwordFormat],
				[first_name], 
				[last_name], 
				[birthdate], 
				[activated], 
				[last_login], 
				[lastActivityDate], 
				[lastPasswordChangedDate], 
				[isOnLine], 
				[isLockedOut], 
				[lastLockedOutDate], 
				[failedPasswordAttemptCount], 
				[failedPasswordAttemptWindowStart], 
				[failedPasswordAnswerAttemptCount], 
				[failedPasswordAnswerAttemptWindowStart], 
				[entered], 
				[entered_by], 
				[updated], 
				[updated_by], 
				[comment]) 
			VALUES (
				@guid, 
				@status, 
				@user, 
				@applicationName, 
				@email, 
				@password, 
				@passwordQuestion, 
				@passwordAnswer, 
				@passwordFormat,
				@firstName, 
				@lastName, 
				@birthDate, 
				(case when (@status = 1) then @now else NULL end), 
				(case when (@isNewUserLoggedIn = 1) then @now else NULL end), 
				@now, 
				@now, 
				0, 
				0, 
				@now, 
				0, 
				@now, 
				0, 
				@now,
				@now, 
				NULL, 
				NULL, 
				NULL, 
				@comment)
	end

	SELECT	@@ROWCOUNT as rowcnt
END

GO