USE [GreenGarden]
GO
/****** Object:  Table [dbo].[Activities]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activities](
	[activity_id] [int] IDENTITY(1,1) NOT NULL,
	[activity_name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK__Activiti__482FBD636E0705D9] PRIMARY KEY CLUSTERED 
(
	[activity_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Amenities]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Amenities](
	[amenity_id] [int] IDENTITY(1,1) NOT NULL,
	[amenity_name] [varchar](100) NOT NULL,
	[Description] [varchar](255) NULL,
	[price] [decimal](10, 2) NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[amenity_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampingCategories]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampingCategories](
	[gear_category_id] [int] IDENTITY(1,1) NOT NULL,
	[gear_category_name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__CampingC__F8C87C83BE4F9F5A] PRIMARY KEY CLUSTERED 
(
	[gear_category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampingGear]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampingGear](
	[gear_id] [int] IDENTITY(1,1) NOT NULL,
	[gear_name] [nvarchar](100) NOT NULL,
	[quantityAvailable] [int] NOT NULL,
	[rentalPrice] [decimal](10, 2) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[created_at] [datetime] NULL,
	[gear_category_id] [int] NULL,
	[img_url] [varchar](255) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK__CampingG__82E64D3D5FA9D30D] PRIMARY KEY CLUSTERED 
(
	[gear_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComboCampingGearDetails]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComboCampingGearDetails](
	[combo_id] [int] NOT NULL,
	[gear_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[combo_id] ASC,
	[gear_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComboFootDetails]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComboFootDetails](
	[combo_id] [int] NOT NULL,
	[item_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[combo_id] ASC,
	[item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Combos]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Combos](
	[combo_id] [int] IDENTITY(1,1) NOT NULL,
	[combo_name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[price] [decimal](10, 2) NOT NULL,
	[created_at] [datetime] NULL,
	[img_url] [varchar](255) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK__Combos__18F74AA38F758C5A] PRIMARY KEY CLUSTERED 
(
	[combo_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComboTicketDetails]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComboTicketDetails](
	[combo_id] [int] NOT NULL,
	[ticket_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[combo_id] ASC,
	[ticket_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[event_id] [int] IDENTITY(1,1) NOT NULL,
	[event_name] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NULL,
	[event_date] [date] NOT NULL,
	[start_time] [time](7) NOT NULL,
	[end_time] [time](7) NULL,
	[location] [nvarchar](255) NULL,
	[picture_url] [varchar](255) NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
	[create_by] [int] NULL,
 CONSTRAINT [PK__Events__2370F727CF87F188] PRIMARY KEY CLUSTERED 
(
	[event_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodAndDrinkCategories]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodAndDrinkCategories](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__FoodAndD__D54EE9B48C3EC2FF] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodAndDrinks]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodAndDrinks](
	[item_id] [int] IDENTITY(1,1) NOT NULL,
	[item_name] [nvarchar](100) NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[quantityAvailable] [int] NOT NULL,
	[Description] [nvarchar](255) NULL,
	[created_at] [datetime] NULL,
	[category_id] [int] NULL,
	[img_url] [varchar](255) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK__FoodAndD__52020FDD3C1D54D9] PRIMARY KEY CLUSTERED 
(
	[item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodCombos]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodCombos](
	[combo_id] [int] IDENTITY(1,1) NOT NULL,
	[combo_name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[price] [decimal](10, 2) NOT NULL,
	[created_at] [datetime] NULL,
	[img_url] [varchar](255) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK__FoodComb__18F74AA3B610D622] PRIMARY KEY CLUSTERED 
(
	[combo_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FootComboItems]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FootComboItems](
	[item_id] [int] NOT NULL,
	[combo_id] [int] NOT NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[item_id] ASC,
	[combo_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderCampingGearDetails]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderCampingGearDetails](
	[gear_id] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[gear_id] ASC,
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderComboDetails]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderComboDetails](
	[combo_id] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[combo_id] ASC,
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderFoodComboDetails]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderFoodComboDetails](
	[combo_id] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[combo_id] ASC,
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderFoodDetails]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderFoodDetails](
	[item_id] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[item_id] ASC,
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NULL,
	[employee_id] [int] NULL,
	[customer_name] [nvarchar](100) NULL,
	[order_date] [datetime] NULL,
	[order_usage_date] [datetime] NULL,
	[deposit] [decimal](10, 2) NOT NULL,
	[total_amount] [decimal](10, 2) NOT NULL,
	[amount_payable] [decimal](10, 2) NOT NULL,
	[status_order] [bit] NULL,
	[activity_id] [int] NULL,
	[phone_customer] [varchar](15) NULL,
	[order_checkout_date] [datetime] NULL,
 CONSTRAINT [PK__Orders__4659622907057969] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderTicketDetails]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderTicketDetails](
	[ticket_id] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ticket_id] ASC,
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [varchar](50) NOT NULL,
	[description] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketCategories]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketCategories](
	[ticket_category_id] [int] IDENTITY(1,1) NOT NULL,
	[ticket_category_name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__TicketCa__3FC8DEA2CEB02082] PRIMARY KEY CLUSTERED 
(
	[ticket_category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[ticket_id] [int] IDENTITY(1,1) NOT NULL,
	[ticket_name] [nvarchar](100) NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[created_at] [datetime] NULL,
	[ticket_category_id] [int] NULL,
	[img_url] [varchar](255) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK__Tickets__D596F96B08919655] PRIMARY KEY CLUSTERED 
(
	[ticket_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/6/2024 6:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](50) NOT NULL,
	[last_name] [nvarchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[phone_number] [varchar](15) NULL,
	[address] [nvarchar](300) NULL,
	[date_of_birth] [date] NULL,
	[gender] [char](6) NULL,
	[profile_picture_url] [varchar](255) NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
	[role_id] [int] NULL,
 CONSTRAINT [PK__Users__B9BE370FC75333E6] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Activities] ON 
GO
INSERT [dbo].[Activities] ([activity_id], [activity_name], [Description]) VALUES (1, N'Đợi sử dụng', NULL)
GO
INSERT [dbo].[Activities] ([activity_id], [activity_name], [Description]) VALUES (2, N'Đang sử dụng', NULL)
GO
INSERT [dbo].[Activities] ([activity_id], [activity_name], [Description]) VALUES (3, N'Đã thanh toán', NULL)
GO
INSERT [dbo].[Activities] ([activity_id], [activity_name], [Description]) VALUES (1002, N'Đã hủy', NULL)
GO
INSERT [dbo].[Activities] ([activity_id], [activity_name], [Description]) VALUES (1003, N'Thanh toán', NULL)
GO
SET IDENTITY_INSERT [dbo].[Activities] OFF
GO
SET IDENTITY_INSERT [dbo].[CampingCategories] ON 
GO
INSERT [dbo].[CampingCategories] ([gear_category_id], [gear_category_name], [Description], [created_at]) VALUES (1, N'Lều cắm trại', N'Lều dành cho 4-6 người, dễ dàng lắp đặt và sử dụng.', CAST(N'2024-10-07T09:59:36.457' AS DateTime))
GO
INSERT [dbo].[CampingCategories] ([gear_category_id], [gear_category_name], [Description], [created_at]) VALUES (2, N'Dụng cụ nấu ăn', N'Dụng cụ nấu ăn', CAST(N'2024-10-07T09:59:36.457' AS DateTime))
GO
INSERT [dbo].[CampingCategories] ([gear_category_id], [gear_category_name], [Description], [created_at]) VALUES (3, N'Dụng cụ tiện ích khác', N'Dụng cụ tiện ích', CAST(N'2024-10-07T09:59:36.457' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[CampingCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[CampingGear] ON 
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (1, N'Bộ bàn ghế Camp (6 người)', 10, CAST(100000.00 AS Decimal(10, 2)), N'Bộ bàn ghế dành cho 6 người, dễ dàng lắp đặt.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732717803/Gear/Gear/Screenshot%202024-11-21%20172803.png', 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (2, N'Chảo nướng (6 người)', 15, CAST(150000.00 AS Decimal(10, 2)), N'Chảo nướng dành cho 6 người, phù hợp cho các bữa tiệc ngoài trời.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732718971/Gear/Gear/Screenshot%202024-11-21%20173110.png', 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (3, N'Bếp nướng đá (10-20 người)', 5, CAST(250000.00 AS Decimal(10, 2)), N'Bếp nướng đá cho 10-20 người, lý tưởng cho cắm trại lớn.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732718985/Gear/Gear/Screenshot%202024-11-21%20173334.png', 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (4, N'Lều grand', 8, CAST(200000.00 AS Decimal(10, 2)), N'Lều lớn dành cho 4-6 người, thoải mái và bền.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 1, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732719000/Gear/Gear/Screenshot%202024-11-21%20173510.png', 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (5, N'Bếp từ', 12, CAST(150000.00 AS Decimal(10, 2)), N'Bếp từ tiện lợi, dễ sử dụng và an toàn.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732719011/Gear/Gear/Screenshot%202024-11-21%20173947.png', 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (6, N'Quạt', 20, CAST(60000.00 AS Decimal(10, 2)), N'Quạt điện, giúp không khí mát mẻ trong suốt chuyến đi.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732719097/Gear/Gear/Screenshot%202024-11-21%20174033.png', 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (7, N'Loa kéo', 10, CAST(250000.00 AS Decimal(10, 2)), N'Loa kéo, âm thanh lớn, thích hợp cho các bữa tiệc ngoài trời.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732719109/Gear/Gear/Screenshot%202024-11-21%20174442.png', 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (8, N'Tăng bạt', 5, CAST(100000.00 AS Decimal(10, 2)), N'Tăng bạt để che nắng, gió cho khu vực cắm trại.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732719123/Gear/Gear/Screenshot%202024-11-21%20174614.png', 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (9, N'Thảm', 15, CAST(50000.00 AS Decimal(10, 2)), N'Thảm trải đất, tạo không gian thoải mái khi ngồi.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732719148/Gear/Gear/Screenshot%202024-11-21%20174715.png', 1)
GO
SET IDENTITY_INSERT [dbo].[CampingGear] OFF
GO
INSERT [dbo].[ComboCampingGearDetails] ([combo_id], [gear_id], [quantity], [Description]) VALUES (1, 1, 1, NULL)
GO
INSERT [dbo].[ComboCampingGearDetails] ([combo_id], [gear_id], [quantity], [Description]) VALUES (1, 3, 1, NULL)
GO
INSERT [dbo].[ComboCampingGearDetails] ([combo_id], [gear_id], [quantity], [Description]) VALUES (1, 4, 1, NULL)
GO
INSERT [dbo].[ComboCampingGearDetails] ([combo_id], [gear_id], [quantity], [Description]) VALUES (1, 8, 1, NULL)
GO
INSERT [dbo].[ComboCampingGearDetails] ([combo_id], [gear_id], [quantity], [Description]) VALUES (2, 1, 1, NULL)
GO
INSERT [dbo].[ComboCampingGearDetails] ([combo_id], [gear_id], [quantity], [Description]) VALUES (2, 3, 1, NULL)
GO
INSERT [dbo].[ComboCampingGearDetails] ([combo_id], [gear_id], [quantity], [Description]) VALUES (2, 4, 2, NULL)
GO
INSERT [dbo].[ComboCampingGearDetails] ([combo_id], [gear_id], [quantity], [Description]) VALUES (2, 8, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (1, 43, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (1, 44, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (1, 45, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (1, 46, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (1, 47, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (1, 48, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (2, 43, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (2, 44, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (2, 45, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (2, 46, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (2, 47, 1, NULL)
GO
INSERT [dbo].[ComboFootDetails] ([combo_id], [item_id], [quantity], [Description]) VALUES (2, 48, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Combos] ON 
GO
INSERT [dbo].[Combos] ([combo_id], [combo_name], [Description], [price], [created_at], [img_url], [status]) VALUES (1, N'Combo và BBQ từ 5-10 người', N'Tính theo đầu người.', CAST(300000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T13:55:06.890' AS DateTime), N'combo510.jpg', 1)
GO
INSERT [dbo].[Combos] ([combo_id], [combo_name], [Description], [price], [created_at], [img_url], [status]) VALUES (2, N'Combo và BBQ từ 15-20 người', N'Tính theo đầu người.', CAST(250000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T13:55:06.890' AS DateTime), N'combo1520.jpg', 1)
GO
SET IDENTITY_INSERT [dbo].[Combos] OFF
GO
SET IDENTITY_INSERT [dbo].[Events] ON 
GO
INSERT [dbo].[Events] ([event_id], [event_name], [description], [event_date], [start_time], [end_time], [location], [picture_url], [is_active], [created_at], [create_by]) VALUES (1005, N'Lễ Khai Mạc Mùa Cắm Trại GreenGarden', N'Lễ khai mạc mùa cắm trại GreenGarden sẽ chính thức mở cửa chào đón khách tham quan và những người yêu thích thiên nhiên, cắm trại. Đây là dịp để các gia đình, nhóm bạn và những ai đam mê hoạt động ngoài trời tham gia vào một ngày hội tràn đầy năng lượng. Buổi sáng sẽ bắt đầu với một buổi lễ khai mạc ấn tượng, theo sau là các hoạt động vui chơi như trò chơi teambuilding, đốt lửa trại, nướng marshmallow và ca hát bên bếp lửa.

Đặc điểm nổi bật:

Lễ khai mạc với nghi thức cắt băng, đón khách đặc biệt.
Các trò chơi ngoài trời như đua thuyền, leo núi, và đua xe địa hình.
Thưởng thức những món ăn đặc sản của địa phương.
Đối tượng tham gia:

Các gia đình, nhóm bạn và những người yêu thích không gian cắm trại.
Các tổ chức muốn tổ chức sự kiện ngoài trời.', CAST(N'2024-12-01' AS Date), CAST(N'01:23:00' AS Time), CAST(N'09:28:00' AS Time), N'Gần đình chùa Quán Tính,Giang Biên,Long Biên,Hà Nội', N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732760622/Event/Event/447736781_930120299125668_2391234246024360015_n.jpg', 1, CAST(N'2024-11-28T09:23:42.113' AS DateTime), 2)
GO
INSERT [dbo].[Events] ([event_id], [event_name], [description], [event_date], [start_time], [end_time], [location], [picture_url], [is_active], [created_at], [create_by]) VALUES (1006, N' Trại Cắm Trại Dã Ngoại Kết Nối Gia Đình', N'Trại cắm trại dã ngoại này được thiết kế đặc biệt dành cho các gia đình muốn có những kỷ niệm tuyệt vời bên nhau. Các hoạt động trong suốt ngày sẽ bao gồm leo núi, bắt cá, nấu ăn ngoài trời và các trò chơi đội nhóm. Những người tham gia sẽ được hướng dẫn cách dựng lều, chuẩn bị bữa ăn ngoài trời và học các kỹ năng sinh tồn cơ bản. Đây là cơ hội tuyệt vời để gia đình gắn kết và tận hưởng không gian xanh mát, trong lành.

Đặc điểm nổi bật:

Các trò chơi kết nối gia đình như giải đố thiên nhiên, tìm kiếm kho báu.
Tham gia vào các buổi học kỹ năng cắm trại và sinh tồn.
Buổi tối sẽ có tiệc nướng BBQ, giao lưu và chơi nhạc.
Đối tượng tham gia:

Các gia đình muốn tận hưởng kỳ nghỉ cuối tuần.
Những người mới bắt đầu tham gia các hoạt động cắm trại.', CAST(N'2024-12-01' AS Date), CAST(N'10:26:00' AS Time), CAST(N'14:26:00' AS Time), N'Gần đình chùa Quán Tính,Giang Biên,Long Biên,Hà Nội', N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732760824/Event/Event/436235073_917834577020907_1422479122397827308_n.jpg', 1, CAST(N'2024-11-28T09:27:03.867' AS DateTime), 2)
GO
INSERT [dbo].[Events] ([event_id], [event_name], [description], [event_date], [start_time], [end_time], [location], [picture_url], [is_active], [created_at], [create_by]) VALUES (1007, N'Festival Hoa & Lửa Trại Mùa Đông', N'Festival Hoa & Lửa Trại Mùa Đông tại GreenGarden sẽ mang đến không khí lễ hội đầy màu sắc và ấm áp. Buổi tối sẽ được chiếu sáng bằng hàng ngàn ngọn đèn hoa và đèn lồng, tạo nên một không gian lung linh. Các hoạt động đặc biệt bao gồm trưng bày các loài hoa mùa đông, triển lãm sản phẩm thủ công mỹ nghệ và các hoạt động giải trí như múa lửa, kể chuyện bên lửa trại và hát hò giao lưu.

Đặc điểm nổi bật:

Triển lãm hoa và cây cảnh, đặc biệt là hoa mùa đông.
Lửa trại, âm nhạc và các trò chơi dân gian.
Thưởng thức món ăn truyền thống bên lửa trại.
Đối tượng tham gia:

Các bạn trẻ, gia đình và nhóm bạn muốn khám phá không gian mùa đông.
Các cặp đôi yêu thích không gian lãng mạn.', CAST(N'2024-12-01' AS Date), CAST(N'10:29:00' AS Time), CAST(N'12:28:00' AS Time), N'Gần đình chùa Quán Tính,Giang Biên,Long Biên,Hà Nội', N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732760934/Event/Event/contact.jpg', 1, CAST(N'2024-11-28T09:28:54.153' AS DateTime), 2)
GO
INSERT [dbo].[Events] ([event_id], [event_name], [description], [event_date], [start_time], [end_time], [location], [picture_url], [is_active], [created_at], [create_by]) VALUES (1008, N' Cuộc Thi Lướt Thuyền & Kéo Co Trên Sông', N'Đây là một cuộc thi thể thao đặc biệt với các môn thi lướt thuyền và kéo co trên sông. Các đội sẽ thi đấu để giành chiến thắng, với phần thưởng hấp dẫn cho đội chiến thắng. Mọi người có thể tham gia thi thuyền kayak, hoặc tham gia kéo co, một môn thể thao vui nhộn nhưng đầy thử thách. Cũng sẽ có các lớp huấn luyện trước để người tham gia làm quen với các môn thể thao nước.

Đặc điểm nổi bật:

Thi đấu thể thao dưới nước như kayak và đua thuyền.
Trò chơi kéo co đầy sức mạnh và vui nhộn.
Hoạt động thư giãn như tắm nắng, ngồi thuyền thưởng ngoạn cảnh đẹp.
Đối tượng tham gia:

Những ai yêu thích thể thao và thử thách.
Các nhóm bạn, tổ chức, công ty muốn xây dựng tinh thần đồng đội.', CAST(N'2024-11-29' AS Date), CAST(N'09:41:00' AS Time), CAST(N'11:37:00' AS Time), N'Gần đình chùa Quán Tính,Giang Biên,Long Biên,Hà Nội', N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761276/Event/Event/Screenshot%202024-11-28%20093414.png', 1, CAST(N'2024-11-28T09:35:54.563' AS DateTime), 2)
GO
INSERT [dbo].[Events] ([event_id], [event_name], [description], [event_date], [start_time], [end_time], [location], [picture_url], [is_active], [created_at], [create_by]) VALUES (1009, N' Dạ Tiệc Chào Năm Mới Tại Khu Cắm Trại GreenGarden', N'Dạ tiệc chào đón năm mới sẽ là sự kiện hoành tráng nhất trong năm tại khu cắm trại GreenGarden. Với không gian ngoài trời, bầu không khí trong lành và mát mẻ, bạn sẽ được thưởng thức những món ăn đặc sắc, tham gia các trò chơi vui nhộn và thưởng thức âm nhạc, nhảy múa. Lúc 00:00, tất cả sẽ cùng nhau đếm ngược để đón chào một năm mới với hy vọng và hạnh phúc.

Đặc điểm nổi bật:

Bữa tối thịnh soạn với món ăn đặc sản địa phương.
Pháo hoa chào đón năm mới.
Chương trình văn nghệ, nhảy múa, hát karaoke.
Đối tượng tham gia:

Các gia đình, nhóm bạn và các cặp đôi.
Mọi người yêu thích không khí tiệc tùng ngoài trời và lễ hội năm mới.', CAST(N'2024-11-30' AS Date), CAST(N'02:37:00' AS Time), CAST(N'15:37:00' AS Time), N'Gần đình chùa Quán Tính,Giang Biên,Long Biên,Hà Nội', N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761773/Event/Event/Screenshot%202024-11-28%20094240.png', 1, CAST(N'2024-11-28T09:42:53.153' AS DateTime), 2)
GO
INSERT [dbo].[Events] ([event_id], [event_name], [description], [event_date], [start_time], [end_time], [location], [picture_url], [is_active], [created_at], [create_by]) VALUES (1010, N'Ngày Hội Thử Thách Sinh Tồn', N'Ngày hội thử thách sinh tồn là cơ hội để những ai yêu thích thiên nhiên và khám phá khả năng của bản thân tham gia vào những thử thách sinh tồn thực tế trong rừng. Người tham gia sẽ được học các kỹ năng sinh tồn cơ bản như dựng lều, bắt cá, nấu ăn bằng lửa trại, tìm kiếm nguồn nước và đọc bản đồ. Các đội tham gia sẽ phải vượt qua những thử thách và thực hiện các nhiệm vụ được giao để giành chiến thắng.

Đặc điểm nổi bật:

Thử thách sinh tồn thực tế trong thiên nhiên.
Học các kỹ năng sống sót và kỹ thuật di chuyển an toàn.
Cuộc thi theo đội với phần thưởng hấp dẫn cho đội chiến thắng.
Đối tượng tham gia:

Những ai đam mê thể thao mạo hiểm và sinh tồn.
Các nhóm bạn hoặc gia đình muốn thử thách bản thân trong thiên nhiên.', CAST(N'2024-11-30' AS Date), CAST(N'02:46:00' AS Time), CAST(N'12:46:00' AS Time), N'Gần đình chùa Quán Tính,Giang Biên,Long Biên,Hà Nội', N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762038/Event/Event/Screenshot%202024-11-28%20094707.png', 1, CAST(N'2024-11-28T09:47:17.783' AS DateTime), 2)
GO
SET IDENTITY_INSERT [dbo].[Events] OFF
GO
SET IDENTITY_INSERT [dbo].[FoodAndDrinkCategories] ON 
GO
INSERT [dbo].[FoodAndDrinkCategories] ([category_id], [category_name], [Description], [created_at]) VALUES (1, N'Món Cá', N'.', CAST(N'2024-10-07T10:11:25.880' AS DateTime))
GO
INSERT [dbo].[FoodAndDrinkCategories] ([category_id], [category_name], [Description], [created_at]) VALUES (2, N'Món Gà-Vịt-Ngan', N'.', CAST(N'2024-10-07T10:11:25.880' AS DateTime))
GO
INSERT [dbo].[FoodAndDrinkCategories] ([category_id], [category_name], [Description], [created_at]) VALUES (3, N'Món Bò-Trâu', N'.', CAST(N'2024-10-07T10:11:25.880' AS DateTime))
GO
INSERT [dbo].[FoodAndDrinkCategories] ([category_id], [category_name], [Description], [created_at]) VALUES (4, N'Món Phụ', N'.', CAST(N'2024-10-07T10:11:25.880' AS DateTime))
GO
INSERT [dbo].[FoodAndDrinkCategories] ([category_id], [category_name], [Description], [created_at]) VALUES (5, N'Đồ uống', N'.', CAST(N'2024-10-07T10:11:25.880' AS DateTime))
GO
INSERT [dbo].[FoodAndDrinkCategories] ([category_id], [category_name], [Description], [created_at]) VALUES (6, N'Món BBQ', N'.', CAST(N'2024-10-07T10:11:25.880' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[FoodAndDrinkCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[FoodAndDrinks] ON 
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (1, N'Cá trắm đen nướng 1kg', CAST(300000.00 AS Decimal(10, 2)), 50, N'Cá trắm đen nướng thơm ngon, phục vụ với nước chấm chua ngọt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732760487/Food/Food/catramdennuong.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (2, N'Cá trắm trắng nướng', CAST(600000.00 AS Decimal(10, 2)), 45, N'Cá trắm trắng nướng vàng ươm, ăn kèm rau sống.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732553572/Food/Food/catramtrangnuong.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (3, N'Cá trắm trắng hấp bia', CAST(1000000.00 AS Decimal(10, 2)), 30, N'Cá trắm trắng hấp bia ngọt thịt, mát lành.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732556815/Food/Food/cachephapbiasa.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (4, N'Cá chép nướng', CAST(400000.00 AS Decimal(10, 2)), 20, N'Cá chép nướng than, thịt mềm và đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, N'cachepnuong.jpg', 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (5, N'Cá chép nướng mỡ hành', CAST(600000.00 AS Decimal(10, 2)), 40, N'Cá chép nướng mỡ hành, giòn tan và thơm phức.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, N'catramtrangnuong.jpg', 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (6, N'Lẩu cá trắm trắng', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu cá trắm trắng nóng hổi, tươi ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732760801/Food/Food/laucachepomdua.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (7, N'Lẩu cá trắm trắng om dưa', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu cá trắm trắng kết hợp với nấm, thơm ngọt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, N'laucachepomdua.jpg', 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (8, N'Lẩu cá chép (Om dưa)', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu cá chép om dưa, chua chua ngọt ngọt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, N'laucachepomdua.jpg', 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (9, N'Lẩu cá chép (Om dưa) với rau sống', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu cá chép om dưa, thêm rau sống tươi ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, N'laucachepomdua.jpg', 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (10, N'Cá chép/Trắm trắng hấp bia sả', CAST(400000.00 AS Decimal(10, 2)), 25, N'Cá chép/trắm trắng hấp bia và sả, thơm lừng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (11, N'Cá chép/Trắm trắng hấp bia sả (tùy chọn)', CAST(1000000.00 AS Decimal(10, 2)), 25, N'Cá hấp với bia và sả, lựa chọn hảo hạng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (12, N'Gà nướng', CAST(400000.00 AS Decimal(10, 2)), 25, N'Gà nướng mềm mại, ướp gia vị đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732760971/Food/Food/ganuong.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (13, N'Gà nướng mật ong', CAST(600000.00 AS Decimal(10, 2)), 25, N'Gà nướng mật ong ngọt ngào, thơm phức.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732760992/Food/Food/garang.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (14, N'Gà luộc', CAST(400000.00 AS Decimal(10, 2)), 25, N'Gà luộc mềm, ăn kèm nước chấm gừng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761026/Food/Food/galuoc.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (15, N'Gà luộc với rau củ', CAST(600000.00 AS Decimal(10, 2)), 25, N'Gà luộc ngon miệng, phục vụ với rau củ tươi.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761026/Food/Food/galuoc.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (16, N'Lẩu gà', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu gà nóng hổi, ngọt nước, thích hợp cho gia đình.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761203/Food/Food/Screenshot%202024-11-28%20093310.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (17, N'Lẩu gà cay', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu gà cay, cho những ai thích ăn cay.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761317/Food/Food/Screenshot%202024-11-28%20093508.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (18, N'Gà rang muối', CAST(400000.00 AS Decimal(10, 2)), 25, N'Gà rang muối giòn tan, hương vị đặc biệt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732760992/Food/Food/garang.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (19, N'Gà rang muối và tiêu', CAST(600000.00 AS Decimal(10, 2)), 25, N'Gà rang muối tiêu thơm phức, giòn rụm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761422/Food/Food/Screenshot%202024-11-28%20093649.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (20, N'Vịt/Ngan luộc', CAST(300000.00 AS Decimal(10, 2)), 25, N'Vịt/nan luộc, ngọt nước, ăn kèm với nước chấm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761501/Food/Food/Screenshot%202024-11-28%20093811.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (21, N'Vịt/Ngan luộc với gia vị', CAST(500000.00 AS Decimal(10, 2)), 25, N'Vịt/nan luộc, thịt mềm và ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761733/Food/Food/Screenshot%202024-11-28%20094202.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (22, N'Vịt/Ngan om măng', CAST(300000.00 AS Decimal(10, 2)), 25, N'Vịt/nan om măng chua chua, thanh mát.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761562/Food/Food/Screenshot%202024-11-28%20093913.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (23, N'Vịt/Ngan om măng với nấm', CAST(500000.00 AS Decimal(10, 2)), 25, N'Vịt/nan om măng và nấm, hương vị tuyệt vời.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761614/Food/Food/Screenshot%202024-11-28%20094004.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (24, N'Vịt/Ngan lẩu', CAST(300000.00 AS Decimal(10, 2)), 25, N'Vịt/nan nấu lẩu với nước dùng đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761653/Food/Food/Screenshot%202024-11-28%20094043.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (25, N'Vịt/Ngan lẩu hải sản', CAST(500000.00 AS Decimal(10, 2)), 25, N'Lẩu vịt/nan kết hợp với hải sản tươi.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (26, N'Bò xào cần tỏi', CAST(150000.00 AS Decimal(10, 2)), 25, N'Bò xào cần tỏi giòn giòn, thơm ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761828/Food/Food/Screenshot%202024-11-28%20094337.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (27, N'Trâu xào rau muống', CAST(150000.00 AS Decimal(10, 2)), 25, N'Trâu xào rau muống tươi, thanh mát.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761871/Food/Food/Screenshot%202024-11-28%20094422.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (28, N'Trâu xào lá lốt', CAST(200000.00 AS Decimal(10, 2)), 25, N'Trâu xào lá lốt, hương vị đậm đà, hấp dẫn.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761946/Food/Food/Screenshot%202024-11-28%20094531.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (29, N'Bò xào lá lốt', CAST(200000.00 AS Decimal(10, 2)), 25, N'Bò xào lá lốt, thơm phức và ngọt thịt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732761999/Food/Food/Screenshot%202024-11-28%20094629.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (30, N'Lẩu riêu cua bắp bò', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu riêu cua với bắp bò, thơm ngon, đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762068/Food/Food/Screenshot%202024-11-28%20094738.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (31, N'Lẩu riêu cua bắp bò', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu riêu cua bắp bò với nấm, ngon miệng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (32, N'Lẩu hải sản', CAST(500000.00 AS Decimal(10, 2)), 25, N'Lẩu hải sản tươi ngon, đa dạng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762122/Food/Food/Screenshot%202024-11-28%20094833.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (33, N'Lẩu hải sản với rau sống', CAST(800000.00 AS Decimal(10, 2)), 25, N'Lẩu hải sản thơm ngon, ăn kèm rau sống.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (34, N'Lẩu đuôi bò', CAST(500000.00 AS Decimal(10, 2)), 15, N'Lẩu đuôi bò đậm đà, hấp dẫn.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762212/Food/Food/Screenshot%202024-11-28%20095001.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (35, N'Lẩu đuôi bò với gia vị đặc biệt', CAST(800000.00 AS Decimal(10, 2)), 15, N'Lẩu đuôi bò với gia vị đậm đà, hương vị phong phú.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (36, N'Rau theo mùa xào', CAST(50000.00 AS Decimal(10, 2)), 25, N'Rau theo mùa xào, tươi ngon và dinh dưỡng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762277/Food/Food/Screenshot%202024-11-28%20095052.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (37, N'Ngô khoai chiên', CAST(50000.00 AS Decimal(10, 2)), 25, N'Ngô khoai chiên giòn, thơm lừng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762351/Food/Food/NgoChien.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (38, N'Khoai lang kén', CAST(50000.00 AS Decimal(10, 2)), 25, N'Khoai lang kén vàng giòn, ngọt ngào.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762316/Food/Food/khoailangken.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (39, N'Đậu xốt cà chua             ', CAST(50000.00 AS Decimal(10, 2)), 25, N'Đậu rán giòn rụm, ăn kèm với nước chấm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732557065/Food/Food/dauxotcachua.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (40, N'Đậu xốt cà chua', CAST(50000.00 AS Decimal(10, 2)), 25, N'Đậu xốt cà chua, đậm đà hương vị.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL, 0)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (41, N'Đậu tẩm hành', CAST(50000.00 AS Decimal(10, 2)), 25, N'Đậu tẩm hành chiên giòn, thơm ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762433/Food/Food/dautamhanh.jpg', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (42, N'Trứng rán', CAST(50000.00 AS Decimal(10, 2)), 25, N'Trứng rán giòn, ăn kèm với cơm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762500/Food/Food/Screenshot%202024-11-28%20095436.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (43, N'Ba chỉ bò mỹ BBQ', CAST(50000.00 AS Decimal(10, 2)), 25, N'Ba chỉ bò mỹ BBQ thơm ngon, hấp dẫn.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762544/Food/Food/Screenshot%202024-11-28%20095528.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (44, N'Ba chỉ heo BBQ', CAST(50000.00 AS Decimal(10, 2)), 25, N'Ba chỉ heo BBQ giòn tan, thơm phức.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762580/Food/Food/Screenshot%202024-11-28%20095611.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (45, N'Cánh gà BBQ', CAST(50000.00 AS Decimal(10, 2)), 25, N'Cánh gà BBQ, món ăn không thể thiếu trong tiệc nướng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762638/Food/Food/Screenshot%202024-11-28%20095709.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (46, N'Sườn heo BBQ', CAST(20000.00 AS Decimal(10, 2)), 25, N'Sườn heo BBQ, ngọt thịt, đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762684/Food/Food/Screenshot%202024-11-28%20095753.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (47, N'Xúc xích BBQ', CAST(20000.00 AS Decimal(10, 2)), 25, N'Xúc xích BBQ, thơm ngon, đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762721/Food/Food/Screenshot%202024-11-28%20095829.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (48, N'Rau củ BBQ', CAST(20000.00 AS Decimal(10, 2)), 25, N'Rau củ BBQ, ăn kèm với các món nướng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762786/Food/Food/Screenshot%202024-11-28%20095935.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (49, N'Bia Hà Nội', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762827/Food/Food/Screenshot%202024-11-28%20100018.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (50, N'Coca cola', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762909/Food/Food/Screenshot%202024-11-28%20100139.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (51, N'Sting', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762942/Food/Food/Screenshot%202024-11-28%20100210.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (52, N'Seven up', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762990/Food/Food/Screenshot%202024-11-28%20100300.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (53, N'Nước cam', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732763074/Food/Food/Screenshot%202024-11-28%20100408.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (54, N'Rượi 1 Chai', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732763163/Food/Food/Screenshot%202024-11-28%20100525.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (55, N'Bia Sài Gòn', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732762862/Food/Food/Screenshot%202024-11-28%20100051.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (56, N'Nước khoáng', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732763169/Food/Food/Screenshot%202024-11-28%20100554.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (57, N'Bia Hà Nội (Thùng)', CAST(250000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732763027/Food/Food/Screenshot%202024-11-28%20100336.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (58, N'Coca cola (Thùng)', CAST(250000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732763107/Food/Food/Screenshot%202024-11-28%20100455.png', 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (1006, N'string', CAST(10.00 AS Decimal(10, 2)), 0, N'string', CAST(N'2024-11-13T14:31:17.510' AS DateTime), 2, N'string', 0)
GO
SET IDENTITY_INSERT [dbo].[FoodAndDrinks] OFF
GO
SET IDENTITY_INSERT [dbo].[FoodCombos] ON 
GO
INSERT [dbo].[FoodCombos] ([combo_id], [combo_name], [Description], [price], [created_at], [img_url], [status]) VALUES (1, N'Combo BBQ', N'.', CAST(150000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T13:46:27.720' AS DateTime), N'combofood.jpg', 1)
GO
SET IDENTITY_INSERT [dbo].[FoodCombos] OFF
GO
INSERT [dbo].[FootComboItems] ([item_id], [combo_id], [quantity]) VALUES (43, 1, 1)
GO
INSERT [dbo].[FootComboItems] ([item_id], [combo_id], [quantity]) VALUES (44, 1, 1)
GO
INSERT [dbo].[FootComboItems] ([item_id], [combo_id], [quantity]) VALUES (45, 1, 1)
GO
INSERT [dbo].[FootComboItems] ([item_id], [combo_id], [quantity]) VALUES (46, 1, 1)
GO
INSERT [dbo].[FootComboItems] ([item_id], [combo_id], [quantity]) VALUES (47, 1, 1)
GO
INSERT [dbo].[FootComboItems] ([item_id], [combo_id], [quantity]) VALUES (48, 1, 1)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2111, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2114, 6, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2115, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2122, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2127, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2128, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2129, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2136, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2147, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2154, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2165, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2166, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2168, 8, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2169, 10, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2170, 7, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2171, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2173, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2174, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2175, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2176, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2194, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2196, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2197, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2198, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2200, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2205, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2206, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2209, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2217, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2222, 4, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2224, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2227, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2236, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2239, 7, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2242, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2243, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2256, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2107, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2109, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2111, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2114, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2115, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2154, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2158, 8, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2167, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2169, 15, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2170, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2171, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2172, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2173, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2174, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2175, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2176, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2194, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2196, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2200, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2205, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2206, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2207, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2209, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2217, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2222, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2224, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2235, 5, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2236, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2238, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2239, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2241, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2242, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2243, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2256, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2107, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2114, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2172, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2200, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2205, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2236, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2238, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2242, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2243, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (4, 2236, 1, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2111, 7, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2122, 2, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2127, 14, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2128, 2, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2161, 5, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2177, 7, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2208, 2, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2239, 6, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2240, 1, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2245, 3, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2252, 6, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2256, 2, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2258, 3, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (2, 2122, 2, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (2, 2128, 1, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (2, 2208, 2, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (2, 2258, 2, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2125, 2, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2158, 3, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2159, 6, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2172, 2, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2173, 2, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2209, 1, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2224, 1, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2231, 1, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2235, 1, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2237, 2, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2244, 2, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2245, 4, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2256, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2107, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2109, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2111, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2114, 4, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2115, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2125, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2127, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2129, 3, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2136, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2137, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2138, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2139, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2158, 3, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2159, 3, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2172, 3, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2173, 3, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2176, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2209, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2215, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2224, 3, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2235, 3, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2239, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2256, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2107, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2109, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2111, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2115, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2136, 1, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2137, 6, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2138, 1, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2139, 1, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2147, 1, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2159, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2172, 3, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2173, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2176, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2209, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2215, 1, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2224, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2239, 4, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2256, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2115, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2139, 1, N'Cá tr?m tr?ng h?p bia')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2147, 1, N'Cá tr?m tr?ng h?p bia')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2159, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2107, NULL, NULL, N'Nguyen Dang Hoang', CAST(N'2024-10-26T11:35:24.893' AS DateTime), CAST(N'2024-10-25T11:34:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1710000.00 AS Decimal(10, 2)), CAST(1710000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2109, NULL, NULL, N'Nguyen Dang Hoang', CAST(N'2024-10-26T15:35:42.843' AS DateTime), CAST(N'2024-10-25T15:35:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1760000.00 AS Decimal(10, 2)), CAST(1560000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2111, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T15:45:45.223' AS DateTime), CAST(N'2024-10-30T15:45:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(4500000.00 AS Decimal(10, 2)), CAST(4300000.00 AS Decimal(10, 2)), 1, 3, NULL, NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2114, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T21:29:20.930' AS DateTime), CAST(N'2024-10-26T21:26:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(2610000.00 AS Decimal(10, 2)), CAST(2410000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2115, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T21:30:21.517' AS DateTime), CAST(N'2024-10-26T21:29:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(3460000.00 AS Decimal(10, 2)), CAST(3260000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2121, 1003, NULL, N'Test Customer', CAST(N'2024-10-27T17:24:52.187' AS DateTime), CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(130000.00 AS Decimal(10, 2)), CAST(130000.00 AS Decimal(10, 2)), 0, 3, N'1234567890', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2122, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-27T22:52:21.480' AS DateTime), CAST(N'2024-10-27T22:51:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1300000.00 AS Decimal(10, 2)), CAST(1100000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-20T19:03:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2125, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-28T22:07:49.113' AS DateTime), CAST(N'2024-10-28T22:06:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1320000.00 AS Decimal(10, 2)), CAST(1120000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2127, NULL, 2, N'Nguyen Dang Hoang1', CAST(N'2024-10-28T22:09:20.793' AS DateTime), CAST(N'2024-10-28T22:08:00.000' AS DateTime), CAST(2000000.00 AS Decimal(10, 2)), CAST(4600000.00 AS Decimal(10, 2)), CAST(2600000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2128, NULL, 2, N'Nguyen Dang Hoang1', CAST(N'2024-10-28T22:11:32.323' AS DateTime), CAST(N'2024-10-28T22:11:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(950000.00 AS Decimal(10, 2)), CAST(750000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2129, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-28T22:55:47.767' AS DateTime), CAST(N'2024-10-28T22:55:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1440000.00 AS Decimal(10, 2)), CAST(1240000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2131, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-29T09:49:00.140' AS DateTime), CAST(N'2024-10-29T09:48:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), CAST(40000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2135, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-29T13:03:03.810' AS DateTime), CAST(N'2024-10-29T18:02:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(640000.00 AS Decimal(10, 2)), CAST(440000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2136, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-10-29T20:46:46.603' AS DateTime), CAST(N'2024-10-31T00:00:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1330000.00 AS Decimal(10, 2)), CAST(1130000.00 AS Decimal(10, 2)), 1, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2137, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-10-29T21:00:00.373' AS DateTime), CAST(N'2024-10-30T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(4030000.00 AS Decimal(10, 2)), CAST(4030000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2138, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-10-29T21:01:03.650' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(900000.00 AS Decimal(10, 2)), CAST(900000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2139, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-10-29T21:05:08.577' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(2030000.00 AS Decimal(10, 2)), CAST(2030000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2147, 1004, NULL, N'Hoang Phi', CAST(N'2024-10-30T08:43:16.027' AS DateTime), CAST(N'2024-11-01T09:42:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1830000.00 AS Decimal(10, 2)), CAST(1830000.00 AS Decimal(10, 2)), 0, 1002, N'0986169300', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2149, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T13:56:24.863' AS DateTime), CAST(N'2024-11-01T13:55:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(210000.00 AS Decimal(10, 2)), CAST(10000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2150, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T13:56:48.180' AS DateTime), CAST(N'2024-11-01T13:56:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2152, NULL, NULL, N'Nguyen Dang Anh', CAST(N'2024-11-01T15:39:54.397' AS DateTime), CAST(N'2024-11-01T15:39:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(260000.00 AS Decimal(10, 2)), CAST(60000.00 AS Decimal(10, 2)), 1, 1002, N'0981694289', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2154, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T15:46:59.410' AS DateTime), CAST(N'2024-11-01T15:46:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1680000.00 AS Decimal(10, 2)), CAST(1680000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', CAST(N'2024-11-03T12:27:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2155, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T18:23:01.950' AS DateTime), CAST(N'2024-11-01T18:22:00.000' AS DateTime), CAST(400000.00 AS Decimal(10, 2)), CAST(800000.00 AS Decimal(10, 2)), CAST(400000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2156, NULL, NULL, N'Nguyen Dang Hoang', CAST(N'2024-11-01T18:31:29.457' AS DateTime), CAST(N'2024-11-01T18:30:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(800000.00 AS Decimal(10, 2)), CAST(600000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2157, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T18:32:09.693' AS DateTime), CAST(N'2024-11-02T18:31:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(800000.00 AS Decimal(10, 2)), CAST(600000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2158, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T18:36:32.340' AS DateTime), CAST(N'2024-11-01T18:35:00.000' AS DateTime), CAST(800000.00 AS Decimal(10, 2)), CAST(3200000.00 AS Decimal(10, 2)), CAST(2400000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2159, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T18:37:20.587' AS DateTime), CAST(N'2024-11-03T10:22:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(4470000.00 AS Decimal(10, 2)), CAST(4470000.00 AS Decimal(10, 2)), 0, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2160, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T18:39:12.133' AS DateTime), CAST(N'2024-11-01T22:39:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(400000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2161, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T19:35:57.177' AS DateTime), CAST(N'2024-11-01T19:35:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(1500000.00 AS Decimal(10, 2)), CAST(1200000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2162, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-11-01T21:55:09.287' AS DateTime), CAST(N'2024-11-03T01:23:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2165, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-03T09:44:58.270' AS DateTime), CAST(N'2024-11-02T09:43:00.000' AS DateTime), CAST(76000.00 AS Decimal(10, 2)), CAST(560000.00 AS Decimal(10, 2)), CAST(484000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-03T10:07:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2166, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-03T10:25:08.720' AS DateTime), CAST(N'2024-11-03T22:25:00.000' AS DateTime), CAST(92000.00 AS Decimal(10, 2)), CAST(460000.00 AS Decimal(10, 2)), CAST(368000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-03T19:16:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2167, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-03T10:26:29.940' AS DateTime), CAST(N'2024-11-03T10:26:00.000' AS DateTime), CAST(112000.00 AS Decimal(10, 2)), CAST(560000.00 AS Decimal(10, 2)), CAST(448000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2168, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-03T10:26:58.993' AS DateTime), CAST(N'2024-11-03T17:35:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1140000.00 AS Decimal(10, 2)), CAST(1140000.00 AS Decimal(10, 2)), 0, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2169, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-03T10:28:53.393' AS DateTime), CAST(N'2024-11-03T19:38:00.000' AS DateTime), CAST(226000.00 AS Decimal(10, 2)), CAST(3380000.00 AS Decimal(10, 2)), CAST(3154000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-03T18:09:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2170, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-03T10:30:30.810' AS DateTime), CAST(N'2024-11-02T22:00:00.000' AS DateTime), CAST(172000.00 AS Decimal(10, 2)), CAST(1320000.00 AS Decimal(10, 2)), CAST(1148000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-03T19:15:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2171, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-03T12:31:46.050' AS DateTime), CAST(N'2024-11-01T19:16:00.000' AS DateTime), CAST(132000.00 AS Decimal(10, 2)), CAST(660000.00 AS Decimal(10, 2)), CAST(528000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2172, NULL, NULL, N'Nguyen Dang Hoang', CAST(N'2024-11-03T13:16:27.810' AS DateTime), CAST(N'2024-11-03T13:16:00.000' AS DateTime), CAST(800000.00 AS Decimal(10, 2)), CAST(4390000.00 AS Decimal(10, 2)), CAST(3590000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2173, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-03T13:17:03.340' AS DateTime), CAST(N'2024-11-03T22:37:00.000' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(3390000.00 AS Decimal(10, 2)), CAST(2390000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2174, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-03T18:09:40.117' AS DateTime), CAST(N'2024-11-02T23:40:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(1280000.00 AS Decimal(10, 2)), CAST(980000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-03T22:44:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2175, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-03T19:10:25.783' AS DateTime), CAST(N'2024-11-03T19:13:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(810000.00 AS Decimal(10, 2)), CAST(510000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2176, NULL, NULL, N'Nguyen Dang Hoang', CAST(N'2024-11-03T22:46:23.813' AS DateTime), CAST(N'2024-11-20T22:46:00.000' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(2740000.00 AS Decimal(10, 2)), CAST(1740000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-04T15:48:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2177, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-11-03T23:51:23.830' AS DateTime), CAST(N'2024-11-14T14:51:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(2100000.00 AS Decimal(10, 2)), CAST(2100000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2178, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T15:20:06.530' AS DateTime), CAST(N'2024-11-05T15:09:00.000' AS DateTime), CAST(60000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(280000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-04T16:27:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2179, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T15:20:49.110' AS DateTime), CAST(N'2024-11-02T18:22:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(140000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2180, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T15:27:45.353' AS DateTime), CAST(N'2024-11-04T15:27:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(390000.00 AS Decimal(10, 2)), CAST(290000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2181, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T15:28:09.477' AS DateTime), CAST(N'2024-11-05T15:27:00.000' AS DateTime), CAST(80000.00 AS Decimal(10, 2)), CAST(210000.00 AS Decimal(10, 2)), CAST(130000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2182, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T15:28:44.537' AS DateTime), CAST(N'2024-11-05T15:28:00.000' AS DateTime), CAST(40000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), CAST(200000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2183, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T15:37:12.137' AS DateTime), CAST(N'2024-11-06T15:36:00.000' AS DateTime), CAST(40000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), CAST(200000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2184, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T15:43:54.490' AS DateTime), CAST(N'2024-11-06T15:43:00.000' AS DateTime), CAST(50000.00 AS Decimal(10, 2)), CAST(260000.00 AS Decimal(10, 2)), CAST(210000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2185, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T15:48:24.327' AS DateTime), CAST(N'2024-11-06T15:48:00.000' AS DateTime), CAST(40000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), CAST(200000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2186, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T15:50:15.990' AS DateTime), CAST(N'2024-11-04T15:49:00.000' AS DateTime), CAST(60000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(280000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2187, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T15:53:05.053' AS DateTime), CAST(N'2024-11-04T15:52:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2188, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T16:04:22.587' AS DateTime), CAST(N'2024-11-04T16:01:00.000' AS DateTime), CAST(80000.00 AS Decimal(10, 2)), CAST(420000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2189, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T16:05:43.133' AS DateTime), CAST(N'2024-11-04T16:04:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2190, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T16:08:31.983' AS DateTime), CAST(N'2024-11-04T16:08:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2191, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T16:13:07.710' AS DateTime), CAST(N'2024-11-04T17:31:00.000' AS DateTime), CAST(30000.00 AS Decimal(10, 2)), CAST(160000.00 AS Decimal(10, 2)), CAST(130000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-04T16:32:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2192, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T16:38:04.517' AS DateTime), CAST(N'2024-11-04T16:51:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2193, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T16:52:07.180' AS DateTime), CAST(N'2024-11-04T20:41:00.000' AS DateTime), CAST(80000.00 AS Decimal(10, 2)), CAST(2040000.00 AS Decimal(10, 2)), CAST(1960000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-09T11:10:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2194, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T17:08:55.450' AS DateTime), CAST(N'2024-11-04T17:09:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(840000.00 AS Decimal(10, 2)), CAST(740000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2195, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T17:14:39.623' AS DateTime), CAST(N'2024-11-04T17:14:00.000' AS DateTime), CAST(40000.00 AS Decimal(10, 2)), CAST(210000.00 AS Decimal(10, 2)), CAST(170000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2196, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-05T11:03:26.497' AS DateTime), CAST(N'2024-11-05T11:03:00.000' AS DateTime), CAST(40000.00 AS Decimal(10, 2)), CAST(740000.00 AS Decimal(10, 2)), CAST(700000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2197, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-05T11:05:20.560' AS DateTime), CAST(N'2024-11-06T11:05:00.000' AS DateTime), CAST(40000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2198, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-05T11:25:03.633' AS DateTime), CAST(N'2024-11-05T11:24:00.000' AS DateTime), CAST(30000.00 AS Decimal(10, 2)), CAST(1380000.00 AS Decimal(10, 2)), CAST(1350000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-09T11:07:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2199, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-08T15:42:43.077' AS DateTime), CAST(N'2024-11-08T15:42:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(390000.00 AS Decimal(10, 2)), CAST(290000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2200, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-08T15:45:45.717' AS DateTime), CAST(N'2024-11-09T11:04:00.000' AS DateTime), CAST(90000.00 AS Decimal(10, 2)), CAST(890000.00 AS Decimal(10, 2)), CAST(800000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-09T11:11:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2201, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-08T16:00:45.563' AS DateTime), CAST(N'2024-11-10T16:01:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(290000.00 AS Decimal(10, 2)), CAST(90000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2202, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-08T21:11:25.900' AS DateTime), CAST(N'2024-11-08T21:11:00.000' AS DateTime), CAST(60000.00 AS Decimal(10, 2)), CAST(320000.00 AS Decimal(10, 2)), CAST(260000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2203, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-09T11:18:01.030' AS DateTime), CAST(N'2024-11-09T11:11:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(390000.00 AS Decimal(10, 2)), CAST(390000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', CAST(N'2024-11-11T14:14:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2205, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-11T15:07:37.860' AS DateTime), CAST(N'2024-11-11T15:06:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(5200000.00 AS Decimal(10, 2)), CAST(5100000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-20T19:04:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2206, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-11T17:26:37.607' AS DateTime), CAST(N'2024-11-12T17:26:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(510000.00 AS Decimal(10, 2)), CAST(510000.00 AS Decimal(10, 2)), 0, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2207, NULL, 2, N'danghoang', CAST(N'2024-11-12T19:30:26.147' AS DateTime), CAST(N'2024-11-12T19:30:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(410000.00 AS Decimal(10, 2)), CAST(110000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2208, NULL, 2, N'danghoang', CAST(N'2024-11-12T19:32:59.507' AS DateTime), CAST(N'2024-11-12T19:32:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(1100000.00 AS Decimal(10, 2)), CAST(1000000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-20T20:31:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2209, NULL, 2, N'danghoang', CAST(N'2024-11-12T19:45:45.393' AS DateTime), CAST(N'2024-11-12T19:45:00.000' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(1430000.00 AS Decimal(10, 2)), CAST(430000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2210, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-13T13:19:00.137' AS DateTime), CAST(N'2024-11-13T13:18:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), CAST(140000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2211, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-11-13T13:34:12.520' AS DateTime), CAST(N'2024-11-12T13:35:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2212, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-11-13T13:38:10.937' AS DateTime), CAST(N'2024-11-12T13:39:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2213, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-11-13T13:43:51.900' AS DateTime), CAST(N'2024-11-10T14:43:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2214, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-13T13:46:07.057' AS DateTime), CAST(N'2024-11-06T13:46:00.000' AS DateTime), CAST(50000.00 AS Decimal(10, 2)), CAST(260000.00 AS Decimal(10, 2)), CAST(210000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2215, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-11-13T13:50:29.307' AS DateTime), CAST(N'2024-11-14T13:51:00.000' AS DateTime), CAST(20000.00 AS Decimal(10, 2)), CAST(980000.00 AS Decimal(10, 2)), CAST(960000.00 AS Decimal(10, 2)), 1, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2216, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-11-13T21:37:54.133' AS DateTime), CAST(N'2024-11-13T21:46:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2217, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-14T08:42:09.953' AS DateTime), CAST(N'2024-11-14T08:38:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(380000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2218, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-14T08:42:48.843' AS DateTime), CAST(N'2024-11-16T08:42:00.000' AS DateTime), CAST(60000.00 AS Decimal(10, 2)), CAST(160000.00 AS Decimal(10, 2)), CAST(100000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2219, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-14T08:43:30.090' AS DateTime), CAST(N'2024-11-16T08:43:00.000' AS DateTime), CAST(70000.00 AS Decimal(10, 2)), CAST(130000.00 AS Decimal(10, 2)), CAST(60000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2220, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-14T10:04:23.113' AS DateTime), CAST(N'2024-11-14T10:04:00.000' AS DateTime), CAST(10000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(70000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2221, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-14T10:05:04.140' AS DateTime), CAST(N'2024-11-14T10:04:00.000' AS DateTime), CAST(10000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(70000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2222, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-17T17:23:15.840' AS DateTime), CAST(N'2024-11-17T17:13:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1110000.00 AS Decimal(10, 2)), CAST(1110000.00 AS Decimal(10, 2)), 0, 1002, N'1234567345', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2223, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-18T12:07:32.557' AS DateTime), CAST(N'2024-11-18T12:07:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1200000.00 AS Decimal(10, 2)), CAST(1200000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', CAST(N'2024-11-20T20:54:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2224, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-18T12:28:42.620' AS DateTime), CAST(N'2024-11-18T12:28:00.000' AS DateTime), CAST(2000000.00 AS Decimal(10, 2)), CAST(2960000.00 AS Decimal(10, 2)), CAST(960000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2225, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-18T12:30:30.170' AS DateTime), CAST(N'2024-11-21T12:29:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(320000.00 AS Decimal(10, 2)), CAST(320000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', CAST(N'2024-11-20T20:55:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2226, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-18T14:11:36.713' AS DateTime), CAST(N'2024-11-18T14:11:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(160000.00 AS Decimal(10, 2)), CAST(60000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2227, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-18T18:20:55.563' AS DateTime), CAST(N'2024-11-18T18:20:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(330000.00 AS Decimal(10, 2)), CAST(230000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2228, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-19T11:16:23.843' AS DateTime), CAST(N'2024-11-19T11:15:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(500000.00 AS Decimal(10, 2)), CAST(200000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2229, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-19T12:00:31.270' AS DateTime), CAST(N'2024-11-21T12:00:00.000' AS DateTime), CAST(90000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), CAST(150000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2230, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-20T14:50:54.380' AS DateTime), CAST(N'2024-11-20T14:50:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', CAST(N'2024-11-20T20:54:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2231, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-20T18:40:15.870' AS DateTime), CAST(N'2024-11-20T18:38:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(310000.00 AS Decimal(10, 2)), CAST(210000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2232, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-20T20:56:13.287' AS DateTime), CAST(N'2024-11-20T20:53:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(480000.00 AS Decimal(10, 2)), CAST(180000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2235, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-21T11:40:58.053' AS DateTime), CAST(N'2024-11-21T11:40:00.000' AS DateTime), CAST(2000000.00 AS Decimal(10, 2)), CAST(1880000.00 AS Decimal(10, 2)), CAST(-120000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-21T12:02:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2236, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-21T11:59:55.650' AS DateTime), CAST(N'2024-11-21T11:59:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(2640000.00 AS Decimal(10, 2)), CAST(2440000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-23T00:04:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2237, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-21T12:01:27.790' AS DateTime), CAST(N'2024-11-21T12:01:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(1500000.00 AS Decimal(10, 2)), CAST(1400000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-23T00:03:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2238, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-21T12:17:02.113' AS DateTime), CAST(N'2024-11-24T23:56:00.000' AS DateTime), CAST(500000.00 AS Decimal(10, 2)), CAST(710000.00 AS Decimal(10, 2)), CAST(210000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2239, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-21T12:17:42.103' AS DateTime), CAST(N'2024-11-23T14:23:00.000' AS DateTime), CAST(2000000.00 AS Decimal(10, 2)), CAST(5950000.00 AS Decimal(10, 2)), CAST(3950000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-24T20:17:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2240, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-21T12:18:40.353' AS DateTime), CAST(N'2024-11-21T12:18:00.000' AS DateTime), CAST(700000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)), CAST(-400000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-27T21:37:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2241, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-21T12:56:34.543' AS DateTime), CAST(N'2024-11-23T14:24:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(660000.00 AS Decimal(10, 2)), CAST(660000.00 AS Decimal(10, 2)), 0, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2242, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-21T13:00:50.780' AS DateTime), CAST(N'2024-11-23T13:00:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1080000.00 AS Decimal(10, 2)), CAST(1080000.00 AS Decimal(10, 2)), 0, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2243, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-21T13:01:21.907' AS DateTime), CAST(N'2024-11-23T13:00:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1180000.00 AS Decimal(10, 2)), CAST(1180000.00 AS Decimal(10, 2)), 0, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2244, NULL, 3, N'Nguyen Dang Hoang', CAST(N'2024-11-21T18:15:53.770' AS DateTime), CAST(N'2024-11-21T18:09:00.000' AS DateTime), CAST(2000000.00 AS Decimal(10, 2)), CAST(1320000.00 AS Decimal(10, 2)), CAST(-680000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-23T00:04:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2245, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-21T20:22:55.620' AS DateTime), CAST(N'2024-11-21T20:22:00.000' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(1500000.00 AS Decimal(10, 2)), CAST(500000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2246, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-24T20:17:47.700' AS DateTime), CAST(N'2024-11-24T20:17:00.000' AS DateTime), CAST(600000.00 AS Decimal(10, 2)), CAST(800000.00 AS Decimal(10, 2)), CAST(200000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', CAST(N'2024-11-24T20:18:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2247, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-24T20:21:26.630' AS DateTime), CAST(N'2024-11-24T20:21:00.000' AS DateTime), CAST(60000.00 AS Decimal(10, 2)), CAST(320000.00 AS Decimal(10, 2)), CAST(260000.00 AS Decimal(10, 2)), 1, 3, N'1234567345', CAST(N'2024-11-27T21:53:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2248, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-24T20:37:57.170' AS DateTime), CAST(N'2024-11-25T20:51:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(150000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)), 1, 1002, N'1234567890', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2249, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-24T20:39:09.197' AS DateTime), CAST(N'2024-11-24T20:38:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)), CAST(100000.00 AS Decimal(10, 2)), 1, 1002, N'1234567823', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2250, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-24T20:52:51.397' AS DateTime), CAST(N'2024-11-24T20:50:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(2000000.00 AS Decimal(10, 2)), CAST(2000000.00 AS Decimal(10, 2)), 0, 3, N'1234444444', CAST(N'2024-11-28T10:12:00.000' AS DateTime))
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2251, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-24T22:56:43.727' AS DateTime), CAST(N'2024-11-02T22:56:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(480000.00 AS Decimal(10, 2)), CAST(180000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2252, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-24T22:57:06.060' AS DateTime), CAST(N'2024-11-24T22:56:00.000' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(1800000.00 AS Decimal(10, 2)), CAST(800000.00 AS Decimal(10, 2)), 1, 1002, N'1234567890', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2253, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-27T21:24:41.803' AS DateTime), CAST(N'2024-11-26T21:24:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(320000.00 AS Decimal(10, 2)), CAST(120000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2254, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-27T21:24:55.207' AS DateTime), CAST(N'2024-11-26T21:24:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(260000.00 AS Decimal(10, 2)), CAST(60000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2255, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-27T21:37:20.237' AS DateTime), CAST(N'2024-11-27T21:30:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(160000.00 AS Decimal(10, 2)), CAST(160000.00 AS Decimal(10, 2)), 0, 2, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2256, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-27T22:12:14.030' AS DateTime), CAST(N'2024-11-27T22:11:00.000' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(2450000.00 AS Decimal(10, 2)), CAST(1450000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2257, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-28T10:12:27.580' AS DateTime), CAST(N'2024-11-28T10:11:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(320000.00 AS Decimal(10, 2)), CAST(320000.00 AS Decimal(10, 2)), 0, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2258, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-28T17:19:03.007' AS DateTime), CAST(N'2024-11-28T17:13:00.000' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(1400000.00 AS Decimal(10, 2)), CAST(400000.00 AS Decimal(10, 2)), 1, 2, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (3257, NULL, 2, N'Nguyen Van An', CAST(N'2024-12-01T08:41:47.450' AS DateTime), CAST(N'2024-12-01T08:41:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), CAST(140000.00 AS Decimal(10, 2)), 1, 1, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (3258, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-12-01T08:45:21.267' AS DateTime), CAST(N'2024-12-01T08:45:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), CAST(140000.00 AS Decimal(10, 2)), 1, 1, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (3259, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-12-01T08:47:07.323' AS DateTime), CAST(N'2024-12-01T08:46:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(260000.00 AS Decimal(10, 2)), CAST(260000.00 AS Decimal(10, 2)), 0, 2, N'0981694286', NULL)
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2107, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2109, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2114, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2115, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2121, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2125, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2129, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2131, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2135, 8, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2136, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2137, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2139, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2147, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2149, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2150, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2152, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2154, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2155, 10, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2156, 10, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2157, 10, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2158, 5, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2159, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2160, 5, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2162, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2165, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2166, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2167, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2168, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2169, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2170, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2171, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2172, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2173, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2174, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2175, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2176, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2178, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2179, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2180, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2181, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2182, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2183, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2184, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2185, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2186, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2187, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2188, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2189, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2190, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2191, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2192, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2193, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2194, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2195, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2196, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2197, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2198, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2199, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2200, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2201, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2202, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2203, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2205, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2206, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2207, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2209, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2210, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2211, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2212, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2213, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2214, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2215, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2216, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2217, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2218, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2219, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2220, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2221, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2222, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2223, 5, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2224, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2225, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2226, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2227, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2228, 5, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2229, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2230, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2231, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2232, 6, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2235, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2236, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2237, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2238, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2241, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2242, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2243, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2244, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2246, 10, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2247, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2250, 5, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2251, 6, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2253, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2254, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2255, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2257, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 3257, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 3258, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 3259, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2107, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2109, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2114, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2115, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2121, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2125, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2136, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2137, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2139, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2147, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2149, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2150, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2152, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2154, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2158, 5, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2159, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2165, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2166, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2167, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2168, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2169, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2171, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2172, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2173, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2174, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2175, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2176, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2178, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2179, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2180, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2181, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2184, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2186, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2187, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2188, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2189, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2190, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2192, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2193, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2194, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2195, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2199, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2201, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2203, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2205, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2206, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2207, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2209, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2214, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2217, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2219, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2222, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2224, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2227, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2228, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2236, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2241, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2244, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2248, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2249, 6, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2254, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 3259, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2139, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2199, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2203, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2206, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2209, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2228, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2236, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([role_id], [role_name], [description]) VALUES (1, N'Owner', NULL)
GO
INSERT [dbo].[Roles] ([role_id], [role_name], [description]) VALUES (2, N'Employee', NULL)
GO
INSERT [dbo].[Roles] ([role_id], [role_name], [description]) VALUES (3, N'Customer', NULL)
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[TicketCategories] ON 
GO
INSERT [dbo].[TicketCategories] ([ticket_category_id], [ticket_category_name], [Description], [created_at]) VALUES (1, N'Vé đi theo nhóm', N'đi theo nhóm', CAST(N'2024-10-07T09:40:29.253' AS DateTime))
GO
INSERT [dbo].[TicketCategories] ([ticket_category_id], [ticket_category_name], [Description], [created_at]) VALUES (2, N'Vé đơn', N'đi đơn', CAST(N'2024-10-07T09:40:29.253' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[TicketCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Tickets] ON 
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url], [status]) VALUES (1, N'Người lớn', CAST(80000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732717757/Ticket/Ticket/Screenshot%202024-11-21%20180016.png', 1)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url], [status]) VALUES (2, N'Trẻ em (3T- <1,4m)', CAST(50000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732717738/Ticket/Ticket/Screenshot%202024-11-21%20175802.png', 1)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url], [status]) VALUES (3, N'Trẻ em < 3T', CAST(0.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 2, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732717770/Ticket/Ticket/Screenshot%202024-11-21%20175916.png', 1)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url], [status]) VALUES (4, N'Nhóm 20-50 khách', CAST(50000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 1, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732763393/Ticket/Ticket/Screenshot%202024-11-28%20100938.png', 1)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url], [status]) VALUES (5, N'Nhóm trên 50 khách', CAST(40000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 1, N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732763476/Ticket/Ticket/Screenshot%202024-11-28%20101034.png', 1)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url], [status]) VALUES (6, N'string', CAST(10.00 AS Decimal(10, 2)), CAST(N'2024-11-13T21:30:55.017' AS DateTime), 1, N'string', 0)
GO
SET IDENTITY_INSERT [dbo].[Tickets] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (2, N'Đăng', N'Hoàng', N'hoangndhe164015@fpt.edu.vn', N'1234567', N'0981694286', N'Sầm sơn, Thanh Hóa', CAST(N'2002-06-21' AS Date), N'male  ', N'https://res.cloudinary.com/dxpsghdhb/image/upload/v1732718213/Avatar/Avatar/hoang3.jpg', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 1)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (3, N'Tạ Thị', N'Bích Thủy', N'hocausongque@gmail.com', N'1234567', N'0987654321', N'456 Elm St, Riverside', CAST(N'1990-03-22' AS Date), N'male  ', N'Screenshot 2024-10-29 104719.png', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 1)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (4, N'Nguyen Tuan ', N'Dung', N'dung@gmail.com', N'1234567', N'0987654321', N'789 Oak St, Mountain View', CAST(N'1995-07-30' AS Date), N'other ', N'409828268_814603424010690_193134261700068709_n.jpg', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 3)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (1003, N'Phung Con', N'Tu', N'tupc2002@gmail.com', N'566020', N'0987654321', NULL, NULL, NULL, NULL, 0, CAST(N'2024-10-08T22:44:07.097' AS DateTime), 3)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (1004, N'Hoang', N'Phi', N'hoangphihung.hn2002@gmail.com', N'123', N'0986169300', NULL, NULL, NULL, NULL, 1, CAST(N'2024-10-29T22:01:52.703' AS DateTime), 3)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (1005, N'Doi', N'Tan', N'doituan12345@gmail.com', N'10052002tan', N'0344780136', NULL, NULL, NULL, NULL, 1, CAST(N'2024-10-29T22:42:28.397' AS DateTime), 3)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (1007, N'Đăng', N'Hoàng', N'dh694286@gmail.com', N'12345678', N'0981694286', NULL, NULL, NULL, NULL, 1, CAST(N'2024-11-14T14:54:44.233' AS DateTime), 3)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__783254B16A44ECCD]    Script Date: 12/6/2024 6:09:56 PM ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[role_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__AB6E61644EC97341]    Script Date: 12/6/2024 6:09:56 PM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ__Users__AB6E61644EC97341] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Amenities] ADD  DEFAULT ((0.00)) FOR [price]
GO
ALTER TABLE [dbo].[Amenities] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[CampingCategories] ADD  CONSTRAINT [DF__CampingCa__creat__3E52440B]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[CampingGear] ADD  CONSTRAINT [DF__CampingGe__creat__412EB0B6]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[ComboCampingGearDetails] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[ComboFootDetails] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[Combos] ADD  CONSTRAINT [DF__Combos__created___7C4F7684]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[ComboTicketDetails] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF__Events__is_activ__2F10007B]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF__Events__created___300424B4]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[FoodAndDrinkCategories] ADD  CONSTRAINT [DF__FoodAndDr__creat__44FF419A]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[FoodAndDrinks] ADD  CONSTRAINT [DF__FoodAndDr__creat__47DBAE45]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[FoodCombos] ADD  CONSTRAINT [DF__FoodCombo__creat__4BAC3F29]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[FootComboItems] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[OrderCampingGearDetails] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[OrderComboDetails] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[OrderFoodComboDetails] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[OrderFoodDetails] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__order_da__628FA481]  DEFAULT (getdate()) FOR [order_date]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__status_o__6383C8BA]  DEFAULT ((0)) FOR [status_order]
GO
ALTER TABLE [dbo].[OrderTicketDetails] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[TicketCategories] ADD  CONSTRAINT [DF__TicketCat__creat__33D4B598]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Tickets] ADD  CONSTRAINT [DF__Tickets__created__36B12243]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__is_active__2A4B4B5E]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__created_a__2B3F6F97]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[CampingGear]  WITH CHECK ADD  CONSTRAINT [FK__CampingGe__gear___4222D4EF] FOREIGN KEY([gear_category_id])
REFERENCES [dbo].[CampingCategories] ([gear_category_id])
GO
ALTER TABLE [dbo].[CampingGear] CHECK CONSTRAINT [FK__CampingGe__gear___4222D4EF]
GO
ALTER TABLE [dbo].[ComboCampingGearDetails]  WITH CHECK ADD  CONSTRAINT [FK__ComboCamp__combo__09A971A2] FOREIGN KEY([combo_id])
REFERENCES [dbo].[Combos] ([combo_id])
GO
ALTER TABLE [dbo].[ComboCampingGearDetails] CHECK CONSTRAINT [FK__ComboCamp__combo__09A971A2]
GO
ALTER TABLE [dbo].[ComboCampingGearDetails]  WITH CHECK ADD  CONSTRAINT [FK__ComboCamp__gear___0A9D95DB] FOREIGN KEY([gear_id])
REFERENCES [dbo].[CampingGear] ([gear_id])
GO
ALTER TABLE [dbo].[ComboCampingGearDetails] CHECK CONSTRAINT [FK__ComboCamp__gear___0A9D95DB]
GO
ALTER TABLE [dbo].[ComboFootDetails]  WITH CHECK ADD  CONSTRAINT [FK__ComboFoot__combo__04E4BC85] FOREIGN KEY([combo_id])
REFERENCES [dbo].[Combos] ([combo_id])
GO
ALTER TABLE [dbo].[ComboFootDetails] CHECK CONSTRAINT [FK__ComboFoot__combo__04E4BC85]
GO
ALTER TABLE [dbo].[ComboFootDetails]  WITH CHECK ADD  CONSTRAINT [FK__ComboFoot__item___05D8E0BE] FOREIGN KEY([item_id])
REFERENCES [dbo].[FoodAndDrinks] ([item_id])
GO
ALTER TABLE [dbo].[ComboFootDetails] CHECK CONSTRAINT [FK__ComboFoot__item___05D8E0BE]
GO
ALTER TABLE [dbo].[ComboTicketDetails]  WITH CHECK ADD  CONSTRAINT [FK__ComboTick__combo__0E6E26BF] FOREIGN KEY([combo_id])
REFERENCES [dbo].[Combos] ([combo_id])
GO
ALTER TABLE [dbo].[ComboTicketDetails] CHECK CONSTRAINT [FK__ComboTick__combo__0E6E26BF]
GO
ALTER TABLE [dbo].[ComboTicketDetails]  WITH CHECK ADD  CONSTRAINT [FK__ComboTick__ticke__0F624AF8] FOREIGN KEY([ticket_id])
REFERENCES [dbo].[Tickets] ([ticket_id])
GO
ALTER TABLE [dbo].[ComboTicketDetails] CHECK CONSTRAINT [FK__ComboTick__ticke__0F624AF8]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK__Events__create_b__30F848ED] FOREIGN KEY([create_by])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK__Events__create_b__30F848ED]
GO
ALTER TABLE [dbo].[FoodAndDrinks]  WITH CHECK ADD  CONSTRAINT [FK__FoodAndDr__categ__48CFD27E] FOREIGN KEY([category_id])
REFERENCES [dbo].[FoodAndDrinkCategories] ([category_id])
GO
ALTER TABLE [dbo].[FoodAndDrinks] CHECK CONSTRAINT [FK__FoodAndDr__categ__48CFD27E]
GO
ALTER TABLE [dbo].[FootComboItems]  WITH CHECK ADD  CONSTRAINT [FK__FootCombo__combo__5070F446] FOREIGN KEY([combo_id])
REFERENCES [dbo].[FoodCombos] ([combo_id])
GO
ALTER TABLE [dbo].[FootComboItems] CHECK CONSTRAINT [FK__FootCombo__combo__5070F446]
GO
ALTER TABLE [dbo].[FootComboItems]  WITH CHECK ADD  CONSTRAINT [FK__FootCombo__item___4F7CD00D] FOREIGN KEY([item_id])
REFERENCES [dbo].[FoodAndDrinks] ([item_id])
GO
ALTER TABLE [dbo].[FootComboItems] CHECK CONSTRAINT [FK__FootCombo__item___4F7CD00D]
GO
ALTER TABLE [dbo].[OrderCampingGearDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderCamp__gear___73BA3083] FOREIGN KEY([gear_id])
REFERENCES [dbo].[CampingGear] ([gear_id])
GO
ALTER TABLE [dbo].[OrderCampingGearDetails] CHECK CONSTRAINT [FK__OrderCamp__gear___73BA3083]
GO
ALTER TABLE [dbo].[OrderCampingGearDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderCamp__order__74AE54BC] FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[OrderCampingGearDetails] CHECK CONSTRAINT [FK__OrderCamp__order__74AE54BC]
GO
ALTER TABLE [dbo].[OrderComboDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderComb__combo__00200768] FOREIGN KEY([combo_id])
REFERENCES [dbo].[Combos] ([combo_id])
GO
ALTER TABLE [dbo].[OrderComboDetails] CHECK CONSTRAINT [FK__OrderComb__combo__00200768]
GO
ALTER TABLE [dbo].[OrderComboDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderComb__order__01142BA1] FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[OrderComboDetails] CHECK CONSTRAINT [FK__OrderComb__order__01142BA1]
GO
ALTER TABLE [dbo].[OrderFoodComboDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderFood__combo__787EE5A0] FOREIGN KEY([combo_id])
REFERENCES [dbo].[FoodCombos] ([combo_id])
GO
ALTER TABLE [dbo].[OrderFoodComboDetails] CHECK CONSTRAINT [FK__OrderFood__combo__787EE5A0]
GO
ALTER TABLE [dbo].[OrderFoodComboDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderFood__order__797309D9] FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[OrderFoodComboDetails] CHECK CONSTRAINT [FK__OrderFood__order__797309D9]
GO
ALTER TABLE [dbo].[OrderFoodDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderFood__item___6A30C649] FOREIGN KEY([item_id])
REFERENCES [dbo].[FoodAndDrinks] ([item_id])
GO
ALTER TABLE [dbo].[OrderFoodDetails] CHECK CONSTRAINT [FK__OrderFood__item___6A30C649]
GO
ALTER TABLE [dbo].[OrderFoodDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderFood__order__6B24EA82] FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[OrderFoodDetails] CHECK CONSTRAINT [FK__OrderFood__order__6B24EA82]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__activity__66603565] FOREIGN KEY([activity_id])
REFERENCES [dbo].[Activities] ([activity_id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__activity__66603565]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__customer__6477ECF3] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__customer__6477ECF3]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__employee__656C112C] FOREIGN KEY([employee_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__employee__656C112C]
GO
ALTER TABLE [dbo].[OrderTicketDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderTick__order__6FE99F9F] FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[OrderTicketDetails] CHECK CONSTRAINT [FK__OrderTick__order__6FE99F9F]
GO
ALTER TABLE [dbo].[OrderTicketDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderTick__ticke__6EF57B66] FOREIGN KEY([ticket_id])
REFERENCES [dbo].[Tickets] ([ticket_id])
GO
ALTER TABLE [dbo].[OrderTicketDetails] CHECK CONSTRAINT [FK__OrderTick__ticke__6EF57B66]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK__Tickets__ticket___37A5467C] FOREIGN KEY([ticket_category_id])
REFERENCES [dbo].[TicketCategories] ([ticket_category_id])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK__Tickets__ticket___37A5467C]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__role_id__2C3393D0] FOREIGN KEY([role_id])
REFERENCES [dbo].[Roles] ([role_id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__role_id__2C3393D0]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK__Users__gender__29572725] CHECK  (([gender]='other' OR [gender]='female' OR [gender]='male'))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK__Users__gender__29572725]
GO
