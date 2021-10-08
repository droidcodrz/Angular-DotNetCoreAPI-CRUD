# AngularNet
Angular and Dotnet Core
Execute below script in ProductDb Database

USE [ProductDb]
GO

/****** Object:  Table [dbo].[ProductTable]    Script Date: 10/8/2021 3:34:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductTable](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](50) NOT NULL,
	[UnitPrice] [decimal](10, 2) NULL,
	[ProductModel] [varchar](50) NULL,
	[ProductInStock] [bit] NULL,
	[CreatedDate] [date] NULL,
 CONSTRAINT [PK_ProductTable] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



