create table CakeType
(
Id bigint primary key identity(1,1),
[Name] nvarchar(max)
)

create table Cake
(
Id bigint primary key identity(1,1),
[Name] nvarchar(max),
[Description] nvarchar(max),
Size nvarchar(max),
Price decimal (7,3), 
TypeId bigint,
foreign key(TypeId) references Cake(Id)
)

create table Customer
(
Id bigint primary key identity(1,1),
PhoneNumber nvarchar(9),
[Name] nvarchar(max),
Surname nvarchar(max),
Email nvarchar(max)
)


CREATE TABLE [dbo].[Order](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderHeaderId] [bigint] NULL,
	[OrderDetailId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 21.04.2022 18:44:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CakeId] [bigint] NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_PieQuantity] UNIQUE NONCLUSTERED 
(
	CakeId ASC,
	[Quantity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderHeader]    Script Date: 21.04.2022 18:44:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderHeader](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NULL,
	[OrderCreateDate] [datetime] NULL,
	[OrderDate] [datetime] NULL,
	[Remarks] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_OrderOrderDetail] FOREIGN KEY([OrderDetailId])
REFERENCES [dbo].[OrderDetail] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_OrderOrderDetail]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_OrderOrderHeader] FOREIGN KEY([OrderHeaderId])
REFERENCES [dbo].[OrderHeader] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_OrderOrderHeader]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetailPie] FOREIGN KEY(CakeId)
REFERENCES [dbo].Cake ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetailPie]
GO
ALTER TABLE [dbo].[OrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeaderCustomer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[OrderHeader] CHECK CONSTRAINT [FK_OrderHeaderCustomer]
GO

