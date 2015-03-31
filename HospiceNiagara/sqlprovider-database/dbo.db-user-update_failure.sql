USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-user-update_failure]    Script Date: 2012.07.12. 13:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[db-user-update_failure]  (
	@applicationName			nvarchar(50) = NULL,
	@username					nvarchar(255),
	@failureType				nvarchar(50),
	@passwordAttemptWindow		int,
	@maxInvalidPasswordAttempts	int,
	@isEmailAsUserName			bit = 0,
	@lang						nvarchar(10)
)
AS
BEGIN
	declare @errorMessage nvarchar(255)
	declare @failureCount int
	declare @now DateTime
	declare @windowEnd DateTime
	declare @windowStart DateTime

	select @errorMessage=''

	if ((@failureType = 'password') or (@failureType = 'passwordAnswer'))
	begin
		select @now=getdate()

		select	
			@failureCount=
				case when (@failureType = 'password') 
				then failedPasswordAttemptCount 
				else failedPasswordAnswerAttemptCount end, 
			@windowEnd= 
				DateAdd(minute, @passwordAttemptWindow, 
					isnull(	case when (@failureType = 'password') 
							then failedPasswordAttemptWindowStart 
							else failedPasswordAnswerAttemptWindowStart end,
							@now))
		from [dbo].[db-user] with (nolock)
		where		(username = (case when @isEmailAsUserName=1 then username else @username end))
				and	(email = (case when @isEmailAsUserName=0 then email else @username end))
				and isnull(applicationName,'')=isnull(@applicationName,'')
		
		if ((@failureCount=0) or (@now > @windowEnd))
		begin
			if (@failureType = 'password')
			begin
				update [dbo].[db-user] 
					set failedPasswordAttemptCount=@failureCount + 1,
						failedPasswordAttemptWindowStart=@now
				where		(username = (case when @isEmailAsUserName=1 then username else @username end))
						and	(email = (case when @isEmailAsUserName=0 then email else @username end))
						and isnull(applicationName,'')=isnull(@applicationName,'')
			end
			else
			begin
				update [dbo].[db-user] 
					set failedPasswordAnswerAttemptCount=@failureCount + 1,
						failedPasswordAnswerAttemptWindowStart=@now
				where		(username = (case when @isEmailAsUserName=1 then username else @username end))
						and	(email = (case when @isEmailAsUserName=0 then email else @username end))
						and isnull(applicationName,'')=isnull(@applicationName,'')
			end

			if (@@ROWCOUNT < 1)
			begin
				SELECT	@errorMessage='Unable to update failure count and window start.'
			end
		end
		else
		begin
			if ((@failureCount + 1) >= @maxInvalidPasswordAttempts)
			begin
				update [dbo].[db-user] set isLockedOut=1, [status]=2
				where		(username = (case when @isEmailAsUserName=1 then username else @username end))
						and	(email = (case when @isEmailAsUserName=0 then email else @username end)) 
						and isnull(applicationName,'')=isnull(@applicationName,'')

				if (@@ROWCOUNT < 1)
				begin
					SELECT	@errorMessage='Unable to lock out user.'
				end
			end
			else
			begin
				if (@failureType = 'password')
				begin
					update [dbo].[db-user]  set failedPasswordAttemptCount=(@failureCount + 1)
					where		(username = (case when @isEmailAsUserName=1 then username else @username end))
							and	(email = (case when @isEmailAsUserName=0 then email else @username end)) 
							and isnull(applicationName,'')=isnull(@applicationName,'')
				end
				else
				begin
					update [dbo].[db-user]  set failedPasswordAnswerAttemptCount=(@failureCount + 1)
					where		(username = (case when @isEmailAsUserName=1 then username else @username end))
							and	(email = (case when @isEmailAsUserName=0 then email else @username end))
							and isnull(applicationName,'')=isnull(@applicationName,'')
				end

				if (@@ROWCOUNT < 1)
				begin
					SELECT	@errorMessage='Unable to update failure count.'
				end
			end
		end
	end
	else
	begin
		SELECT	@errorMessage='Invalid failuretype.'
	end

	SELECT	@errorMessage AS errorMessage
END

GO