USE [asp_sumegi]
GO

/****** Object:  StoredProcedure [dbo].[db-user-get_onlinenumber]    Script Date: 2012.07.15. 0:04:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[db-user-get_onlinenumber](
	@compareTime		datetime,
	@applicationName	nvarchar(50) = NULL
) AS
BEGIN
	declare @rowcnt int

	select @rowcnt=count(u.id) from (
		SELECT id 
		FROM [dbo].[db-user] with (nolock) 
		WHERE		(isOnLine=1) 
				AND ((last_login > @compareTime) or (lastActivityDate > @compareTime))
				AND	(isnull(applicationName,'')=isnull(@applicationName,''))
	) u

	SELECT	@rowcnt AS rowcnt
END

GO
