USE [master]
GO
/****** Object:  Database [Practica2025]    Script Date: 21/4/2025 20:11:08 ******/
USE [Practica2025]
GO
/****** Object:  User [practica]    Script Date: 21/4/2025 20:11:09 ******/
CREATE USER [practica] FOR LOGIN [practica] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [practica]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 21/4/2025 20:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[IdCustomer] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[IdCustomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Customer] ([IdCustomer], [Name]) VALUES (N'3f89c244-011f-f011-af4e-b81ea4d48094', N'Pepito')
GO
INSERT [dbo].[Customer] ([IdCustomer], [Name]) VALUES (N'385a8787-011f-f011-af4e-b81ea4d48094', N'Gabriel')
GO
INSERT [dbo].[Customer] ([IdCustomer], [Name]) VALUES (N'121ce893-011f-f011-af4e-b81ea4d48094', N'Jorgito')
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_IdCustomer]  DEFAULT (newsequentialid()) FOR [IdCustomer]
GO
USE [master]
GO
ALTER DATABASE [Practica2025] SET  READ_WRITE 
GO
