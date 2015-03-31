USE [asp_sumegi]
GO

/****** Object:  Table [dbo].[db-user_role]    Script Date: 2012.07.20. 14:48:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[db-user_role](
	[userid] [int] NOT NULL,
	[roleid] [int] NOT NULL,
 CONSTRAINT [IX_db-user_role] UNIQUE NONCLUSTERED 
(
	[userid] ASC,
	[roleid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


