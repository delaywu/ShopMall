USE [master]
GO
/****** Object:  Database [ShopMall]    Script Date: 2020-11-08 20:02:20 ******/
CREATE DATABASE [ShopMall]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShopMall', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ShopMall.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ShopMall_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ShopMall_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ShopMall] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShopMall].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShopMall] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShopMall] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShopMall] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShopMall] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShopMall] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShopMall] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShopMall] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShopMall] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShopMall] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShopMall] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShopMall] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShopMall] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShopMall] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShopMall] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShopMall] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShopMall] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShopMall] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShopMall] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShopMall] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShopMall] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShopMall] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShopMall] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShopMall] SET RECOVERY FULL 
GO
ALTER DATABASE [ShopMall] SET  MULTI_USER 
GO
ALTER DATABASE [ShopMall] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShopMall] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShopMall] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShopMall] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ShopMall] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ShopMall', N'ON'
GO
USE [ShopMall]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 2020-11-08 20:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Contacts] [nvarchar](50) NOT NULL,
	[Mobile] [nvarchar](50) NOT NULL,
	[Village] [nvarchar](500) NOT NULL,
	[Room] [nvarchar](50) NOT NULL,
	[Longitude] [float] NOT NULL,
	[Latitude] [float] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 2020-11-08 20:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Sort] [int] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Merchant]    Script Date: 2020-11-08 20:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Merchant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Mobile] [nvarchar](50) NOT NULL,
	[Longitude] [float] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Score] [int] NOT NULL,
 CONSTRAINT [PK_Merchant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MerchantProduct]    Script Date: 2020-11-08 20:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MerchantProduct](
	[Id] [uniqueidentifier] NOT NULL,
	[MerchantId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Price] [decimal](10, 2) NULL,
	[Stock] [int] NOT NULL,
	[Sales] [int] NOT NULL,
 CONSTRAINT [PK_MerchantProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 2020-11-08 20:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[ImgUrl] [nvarchar](1000) NOT NULL,
	[Recommend] [int] NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 2020-11-08 20:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mobile] [varchar](50) NOT NULL,
	[Portrait] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[V_MerchantProduct]    Script Date: 2020-11-08 20:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[V_MerchantProduct]
AS
SELECT A.*,B.Name AS ProductName,B.ImgUrl,B.CategoryId FROM MerchantProduct A INNER JOIN Product B ON A.ProductId=B.Id


GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (1, N'饮料/水', 1)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (2, N'休闲零食', 2)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (3, N'酒类', 3)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (4, N'水果', 4)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (5, N'香烟', 5)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (6, N'方便速食', 6)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (7, N'牛奶乳品', 7)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (8, N'粮油米面', 8)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (9, N'蔬菜', 9)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (10, N'肉类禽蛋', 10)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (11, N'日常生活', 11)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (12, N'个人洗护', 12)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (13, N'男士护理', 13)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (14, N'女性用品', 14)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (15, N'文体用品', 15)
INSERT [dbo].[Category] ([Id], [Name], [Sort]) VALUES (16, N'3C配件', 16)
SET IDENTITY_INSERT [dbo].[Category] OFF
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'945b16a4-0db0-11b8-a4c2-0d6f220693e5', 1, 3, CAST(188.00 AS Decimal(10, 2)), 99999, 108)
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'7cd3a1d7-c1da-aa67-cf4e-1cbc0b5e3cf3', 1, 12, CAST(13.00 AS Decimal(10, 2)), 99999, 108)
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'df66f82c-523c-cf90-10ec-2df219af0f1a', 1, 7, CAST(188.00 AS Decimal(10, 2)), 99999, 108)
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'cb272f30-95b8-2975-e01b-4c33d1a38abc', 1, 13, CAST(56.00 AS Decimal(10, 2)), 99999, 108)
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'2ea49361-c159-cfc9-4947-5e8eef828b28', 1, 11, CAST(15.00 AS Decimal(10, 2)), 99999, 108)
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'fce7cd6e-f50b-0412-ed0f-846abe7475b1', 1, 4, CAST(188.00 AS Decimal(10, 2)), 99999, 108)
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'2743c142-3ca9-dc0b-7255-91e4af0f1dd3', 1, 2, CAST(188.00 AS Decimal(10, 2)), 99999, 108)
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'd481571a-9e56-c954-6641-c304d3152814', 1, 5, CAST(188.00 AS Decimal(10, 2)), 99999, 108)
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'e53b8427-e7be-e180-de23-c7926d9fe867', 1, 1, CAST(188.00 AS Decimal(10, 2)), 1, 1)
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'77ead53a-2623-26eb-87fb-ca0e682001a2', 1, 10, CAST(188.00 AS Decimal(10, 2)), 99999, 108)
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'77ead5da-2623-26eb-87fb-ca0e682001a2', 1, 9, CAST(188.00 AS Decimal(10, 2)), 99999, 108)
INSERT [dbo].[MerchantProduct] ([Id], [MerchantId], [ProductId], [Price], [Stock], [Sales]) VALUES (N'77ead5da-2623-26eb-87fb-ca0e682091a1', 1, 6, CAST(188.00 AS Decimal(10, 2)), 99999, 108)
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (1, 1, N'可口可乐', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604840059948&di=065b907133ab671d174a64dd2690e8e1&imgtype=0&src=http%3A%2F%2F4888152.s21i-4.faidns.com%2F2%2FABUIABACGAAgzvv_pQUog7udqwQwrAI4rAI.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (2, 1, N'雪碧', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604845135049&di=1dc3365dd34a44481f79501684234541&imgtype=0&src=http%3A%2F%2Fimg2.99114.com%2Fgroup10%2FM00%2F6A%2FD4%2FrBADs1oLBpyAVCGnAABUIX1U0SM632.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (3, 1, N'可口可乐', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604840059948&di=065b907133ab671d174a64dd2690e8e1&imgtype=0&src=http%3A%2F%2F4888152.s21i-4.faidns.com%2F2%2FABUIABACGAAgzvv_pQUog7udqwQwrAI4rAI.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (4, 1, N'可口可乐', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604840059948&di=065b907133ab671d174a64dd2690e8e1&imgtype=0&src=http%3A%2F%2F4888152.s21i-4.faidns.com%2F2%2FABUIABACGAAgzvv_pQUog7udqwQwrAI4rAI.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (5, 1, N'可口可乐', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604840059948&di=065b907133ab671d174a64dd2690e8e1&imgtype=0&src=http%3A%2F%2F4888152.s21i-4.faidns.com%2F2%2FABUIABACGAAgzvv_pQUog7udqwQwrAI4rAI.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (6, 3, N'金皖', CAST(1.00 AS Decimal(10, 2)), N'https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=3916743920,910113425&fm=26&gp=0.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (7, 1, N'可口可乐', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604840059948&di=065b907133ab671d174a64dd2690e8e1&imgtype=0&src=http%3A%2F%2F4888152.s21i-4.faidns.com%2F2%2FABUIABACGAAgzvv_pQUog7udqwQwrAI4rAI.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (8, 3, N'金皖', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604845209694&di=ceff654e62036585e7212706ff22f653&imgtype=0&src=http%3A%2F%2Fimgsrc.baidu.com%2Fforum%2Fw%3D580%2Fsign%3D83783c8a22a446237ecaa56aa8237246%2F6d7fc20828381f30077ecb59ab014c086f06f0a9.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (9, 3, N'金皖', CAST(1.00 AS Decimal(10, 2)), N'https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=3916743920,910113425&fm=26&gp=0.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (10, 3, N'金皖', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604845209694&di=ceff654e62036585e7212706ff22f653&imgtype=0&src=http%3A%2F%2Fimgsrc.baidu.com%2Fforum%2Fw%3D580%2Fsign%3D83783c8a22a446237ecaa56aa8237246%2F6d7fc20828381f30077ecb59ab014c086f06f0a9.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (11, 3, N'金皖', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604845209694&di=ceff654e62036585e7212706ff22f653&imgtype=0&src=http%3A%2F%2Fimgsrc.baidu.com%2Fforum%2Fw%3D580%2Fsign%3D83783c8a22a446237ecaa56aa8237246%2F6d7fc20828381f30077ecb59ab014c086f06f0a9.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (12, 3, N'金皖', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604845209694&di=ceff654e62036585e7212706ff22f653&imgtype=0&src=http%3A%2F%2Fimgsrc.baidu.com%2Fforum%2Fw%3D580%2Fsign%3D83783c8a22a446237ecaa56aa8237246%2F6d7fc20828381f30077ecb59ab014c086f06f0a9.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (13, 3, N'金皖', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604845209694&di=ceff654e62036585e7212706ff22f653&imgtype=0&src=http%3A%2F%2Fimgsrc.baidu.com%2Fforum%2Fw%3D580%2Fsign%3D83783c8a22a446237ecaa56aa8237246%2F6d7fc20828381f30077ecb59ab014c086f06f0a9.jpg', 1, N'1')
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [ImgUrl], [Recommend], [Description]) VALUES (14, 3, N'金皖', CAST(1.00 AS Decimal(10, 2)), N'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604845209694&di=ceff654e62036585e7212706ff22f653&imgtype=0&src=http%3A%2F%2Fimgsrc.baidu.com%2Fforum%2Fw%3D580%2Fsign%3D83783c8a22a446237ecaa56aa8237246%2F6d7fc20828381f30077ecb59ab014c086f06f0a9.jpg', 1, N'1')
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Mobile], [Portrait]) VALUES (1, N'18949336330', N'123')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_Sort]  DEFAULT ((0)) FOR [Sort]
GO
ALTER TABLE [dbo].[Merchant] ADD  CONSTRAINT [DF_Merchant_Score]  DEFAULT ((5)) FOR [Score]
GO
ALTER TABLE [dbo].[MerchantProduct] ADD  CONSTRAINT [DF_MerchantProduct_Stock]  DEFAULT ((99999)) FOR [Stock]
GO
ALTER TABLE [dbo].[MerchantProduct] ADD  CONSTRAINT [DF_MerchantProduct_Sales]  DEFAULT ((108)) FOR [Sales]
GO
/****** Object:  StoredProcedure [dbo].[p_MerchantProduct]    Script Date: 2020-11-08 20:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_MerchantProduct]
@MerchantId INT =0
AS
SELECT a.*,b.Name AS ProductName FROM MerchantProduct  a INNER JOIN Product b ON a.ProductId = b.Id WHERE
a.MerchantId=@MerchantId


GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'Contacts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'小区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'Village'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'房间号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'Room'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'经度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'Longitude'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纬度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Address', @level2type=N'COLUMN',@level2name=N'Latitude'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Merchant', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Merchant', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'经度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Merchant', @level2type=N'COLUMN',@level2name=N'Longitude'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纬度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Merchant', @level2type=N'COLUMN',@level2name=N'Latitude'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺评分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Merchant', @level2type=N'COLUMN',@level2name=N'Score'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MerchantProduct', @level2type=N'COLUMN',@level2name=N'Stock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'月售' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MerchantProduct', @level2type=N'COLUMN',@level2name=N'Sales'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ImgUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推荐指数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Recommend'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Description'
GO
USE [master]
GO
ALTER DATABASE [ShopMall] SET  READ_WRITE 
GO
