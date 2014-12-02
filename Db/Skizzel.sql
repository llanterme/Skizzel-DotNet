USE [master]
GO
/****** Object:  Database [Skizzel]    Script Date: 11/8/2014 12:57:17 PM ******/
CREATE DATABASE [Skizzel]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Skizzel', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Skizzel.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Skizzel_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Skizzel_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Skizzel] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Skizzel].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Skizzel] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Skizzel] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Skizzel] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Skizzel] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Skizzel] SET ARITHABORT OFF 
GO
ALTER DATABASE [Skizzel] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Skizzel] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Skizzel] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Skizzel] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Skizzel] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Skizzel] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Skizzel] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Skizzel] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Skizzel] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Skizzel] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Skizzel] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Skizzel] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Skizzel] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Skizzel] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Skizzel] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Skizzel] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Skizzel] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Skizzel] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Skizzel] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Skizzel] SET  MULTI_USER 
GO
ALTER DATABASE [Skizzel] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Skizzel] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Skizzel] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Skizzel] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Skizzel]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/8/2014 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Image]    Script Date: 11/8/2014 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Image](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptId] [int] NOT NULL,
	[ImageUrl] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Millage]    Script Date: 11/8/2014 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Millage](
	[MillageId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Alias] [varchar](100) NOT NULL,
	[DateCreated] [varchar](100) NOT NULL,
	[StartLat] [varchar](50) NOT NULL,
	[StartLong] [varchar](50) NOT NULL,
	[StopLat] [varchar](50) NOT NULL,
	[StopLong] [varchar](50) NOT NULL,
	[Total] [varchar](50) NOT NULL,
	[FilterDate] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Millage] PRIMARY KEY CLUSTERED 
(
	[MillageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 11/8/2014 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Receipt](
	[ReceiptId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[DateCreated] [varchar](50) NOT NULL,
	[Alias] [varchar](50) NOT NULL,
	[FilterDate] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Receipt] PRIMARY KEY CLUSTERED 
(
	[ReceiptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/8/2014 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_User]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_Receipt] FOREIGN KEY([ReceiptId])
REFERENCES [dbo].[Receipt] ([ReceiptId])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_Receipt]
GO
ALTER TABLE [dbo].[Millage]  WITH CHECK ADD  CONSTRAINT [FK_Millage_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Millage] CHECK CONSTRAINT [FK_Millage_Category]
GO
ALTER TABLE [dbo].[Millage]  WITH CHECK ADD  CONSTRAINT [FK_Millage_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Millage] CHECK CONSTRAINT [FK_Millage_User]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_Category]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_User]
GO
USE [master]
GO
ALTER DATABASE [Skizzel] SET  READ_WRITE 
GO
