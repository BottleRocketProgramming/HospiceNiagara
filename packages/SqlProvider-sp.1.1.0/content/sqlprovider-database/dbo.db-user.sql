USE [asp_sumegi]
GO

/****** Object:  Table [dbo].[db-user]    Script Date: 2012.07.30. 22:27:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[db-user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[status] [int] NOT NULL,
	[username] [varchar](255) NULL,
	[applicationName] [varchar](255) NULL,
	[email] [varchar](100) NULL,
	[pw] [nvarchar](50) NOT NULL,
	[passwordQuestion] [nvarchar](255) NULL,
	[passwordAnswer] [nvarchar](255) NULL,
	[passwordFormat] [int] NOT NULL,
	[first_name] [nvarchar](50) NULL,
	[last_name] [nvarchar](50) NULL,
	[birthdate] [datetime] NULL,
	[activated] [datetime] NULL,
	[last_login] [datetime] NULL,
	[lastActivityDate] [datetime] NULL,
	[lastPasswordChangedDate] [datetime] NULL,
	[isOnLine] [bit] NULL,
	[isLockedOut] [bit] NULL,
	[lastLockedOutDate] [datetime] NULL,
	[failedPasswordAttemptCount] [int] NULL,
	[failedPasswordAttemptWindowStart] [datetime] NULL,
	[failedPasswordAnswerAttemptCount] [int] NULL,
	[failedPasswordAnswerAttemptWindowStart] [datetime] NULL,
	[entered] [datetime] NULL,
	[entered_by] [int] NULL,
	[updated] [datetime] NULL,
	[updated_by] [int] NULL,
	[comment] [nvarchar](255) NULL,
 CONSTRAINT [PK_db-user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY],
CONSTRAINT [IX_db-user] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[db-user] ADD  CONSTRAINT [DF_db-user_guid]  DEFAULT (newid()) FOR [guid]
GO

ALTER TABLE [dbo].[db-user] ADD  CONSTRAINT [DF_db-user_status]  DEFAULT ((0)) FOR [status]
GO

ALTER TABLE [dbo].[db-user] ADD  CONSTRAINT [DF_db-user_passwordFormat]  DEFAULT ((0)) FOR [passwordFormat]
GO

ALTER TABLE [dbo].[db-user] ADD  CONSTRAINT [DF_db-user_applicationName]  DEFAULT ('/') FOR [applicationName]
GO

ALTER TABLE [dbo].[db-user] ADD  CONSTRAINT [DF_db-user_entered]  DEFAULT (getdate()) FOR [entered]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=Regisztráció alatt, 1=Aktív, 2=Felfüggesztett, 3=Törölt
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'db-user', @level2type=N'COLUMN',@level2name=N'status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=Clear, 1=Hashed, 2=Encrypted
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'db-user', @level2type=N'COLUMN',@level2name=N'passwordFormat'
GO

