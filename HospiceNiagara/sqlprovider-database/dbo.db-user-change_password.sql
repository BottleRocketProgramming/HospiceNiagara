USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-user-change_password]    Script Date: 2012.07.12. 13:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[db-user-change_password]  (
	@applicationName	nvarchar(50) = NULL,
	@username			nvarchar(255),
	@newPassword		nvarchar(50) = NULL,
	@newQuestion		nvarchar(255) = NULL,
	@newAnswer			nvarchar(255) = NULL,
	@passwordFormat		int = 0,
	@isEmailAsUserName	bit = 0
)
AS
BEGIN
	declare @rowcnt int
	
	SELECT	@rowcnt=0

	if (@newPassword is not null)
	begin
		update [dbo].[db-user] set 
			pw=@newPassword, 
			lastPasswordChangedDate=GETDATE(),
			passwordFormat=@passwordFormat 
		where		(username = (case when @isEmailAsUserName=1 then username else @username end))
				and	(email = (case when @isEmailAsUserName=0 then email else @username end)) 
				and isnull(applicationName,'')=isnull(@applicationName,'')
		
		SELECT	@rowcnt=@@ROWCOUNT
	end

	if ((@newQuestion is not null) and (@newAnswer is not null))
	begin
		update [dbo].[db-user] set 
			passwordQuestion=@newQuestion, 
			passwordAnswer=@newAnswer, 
			lastPasswordChangedDate=GETDATE(),
			passwordFormat=@passwordFormat 
		where		(username = (case when @isEmailAsUserName=1 then username else @username end))
				and	(email = (case when @isEmailAsUserName=0 then email else @username end)) 
				and isnull(applicationName,'')=isnull(@applicationName,'')

		SELECT	@rowcnt=@rowcnt + @@ROWCOUNT
	end

	SELECT	@rowcnt AS rowcnt
END

GO