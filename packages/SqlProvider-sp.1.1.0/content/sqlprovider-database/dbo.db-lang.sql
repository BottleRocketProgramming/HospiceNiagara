USE [asp_sumegi]
GO

/****** Object:  Table [dbo].[db-lang]    Script Date: 2012.07.11. 13:46:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[db-lang](
	[lang] [nchar](10) NOT NULL,
	[imgurl] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ex-lang] PRIMARY KEY CLUSTERED 
(
	[lang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


