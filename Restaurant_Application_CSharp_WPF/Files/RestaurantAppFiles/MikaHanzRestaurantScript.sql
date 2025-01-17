USE [master]
GO
/****** Object:  Database [MikaHanzRestaurant]    Script Date: 5/8/2020 6:13:03 PM ******/
CREATE DATABASE [MikaHanzRestaurant]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MikaHanzRestaurant', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MikaHanzRestaurant.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MikaHanzRestaurant_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MikaHanzRestaurant_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MikaHanzRestaurant] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MikaHanzRestaurant].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MikaHanzRestaurant] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET ARITHABORT OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MikaHanzRestaurant] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MikaHanzRestaurant] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MikaHanzRestaurant] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MikaHanzRestaurant] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET RECOVERY FULL 
GO
ALTER DATABASE [MikaHanzRestaurant] SET  MULTI_USER 
GO
ALTER DATABASE [MikaHanzRestaurant] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MikaHanzRestaurant] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MikaHanzRestaurant] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MikaHanzRestaurant] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MikaHanzRestaurant] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MikaHanzRestaurant', N'ON'
GO
ALTER DATABASE [MikaHanzRestaurant] SET QUERY_STORE = OFF
GO
USE [MikaHanzRestaurant]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 5/8/2020 6:13:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmpID] [int] IDENTITY(100,1) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
	[EmployeType] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 5/8/2020 6:13:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailID] [int] IDENTITY(4001,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsReady] [bit] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderHeader]    Script Date: 5/8/2020 6:13:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderHeader](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[TableID] [int] NOT NULL,
	[EmpID] [int] NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[IsServing] [bit] NOT NULL,
	[DiningIn] [bit] NOT NULL,
 CONSTRAINT [PK_OrderHeader] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/8/2020 6:13:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ProductType] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Availability] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RestaurantTable]    Script Date: 5/8/2020 6:13:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestaurantTable](
	[TableID] [int] IDENTITY(701,1) NOT NULL,
	[Capacity] [smallint] NOT NULL,
	[Availability] [bit] NOT NULL,
 CONSTRAINT [PK_RestaurantTable] PRIMARY KEY CLUSTERED 
(
	[TableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_OrderHeader] FOREIGN KEY([OrderID])
REFERENCES [dbo].[OrderHeader] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_OrderHeader]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[OrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_Employee] FOREIGN KEY([EmpID])
REFERENCES [dbo].[Employee] ([EmpID])
GO
ALTER TABLE [dbo].[OrderHeader] CHECK CONSTRAINT [FK_OrderHeader_Employee]
GO
ALTER TABLE [dbo].[OrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_RestaurantTable] FOREIGN KEY([TableID])
REFERENCES [dbo].[RestaurantTable] ([TableID])
GO
ALTER TABLE [dbo].[OrderHeader] CHECK CONSTRAINT [FK_OrderHeader_RestaurantTable]
GO
USE [master]
GO
ALTER DATABASE [MikaHanzRestaurant] SET  READ_WRITE 
GO
