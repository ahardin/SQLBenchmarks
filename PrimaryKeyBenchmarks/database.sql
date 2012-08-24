USE [master]
GO
/****** Object:  Database [PrimaryKeyBenchmarks]    Script Date: 08/23/2012 18:27:24 ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'PrimaryKeyBenchmarks')
BEGIN
	CREATE DATABASE [PrimaryKeyBenchmarks] 
END

GO
USE [PrimaryKeyBenchmarks]
GO
/****** Object:  Table [dbo].[Guid]    Script Date: 08/23/2012 18:27:26 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Guid_Key]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Guid] DROP CONSTRAINT [DF_Guid_Key]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Guid]') AND type in (N'U'))
DROP TABLE [dbo].[Guid]
GO
/****** Object:  Table [dbo].[Integer]    Script Date: 08/23/2012 18:27:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Integer]') AND type in (N'U'))
DROP TABLE [dbo].[Integer]
GO
/****** Object:  Table [dbo].[Integer]    Script Date: 08/23/2012 18:27:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Integer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Integer](
	[Key] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Integer] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Guid]    Script Date: 08/23/2012 18:27:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Guid]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Guid](
	[Key] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Guid_Key]  DEFAULT (newid()),
	[Value] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Guid] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
