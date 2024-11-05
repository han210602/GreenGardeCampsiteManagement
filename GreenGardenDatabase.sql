USE [GreenGarden]
GO
/****** Object:  Table [dbo].[Activities]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[Amenities]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[CampingCategories]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[CampingGear]    Script Date: 11/5/2024 11:35:01 AM ******/
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
 CONSTRAINT [PK__CampingG__82E64D3D5FA9D30D] PRIMARY KEY CLUSTERED 
(
	[gear_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComboCampingGearDetails]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[ComboFootDetails]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[Combos]    Script Date: 11/5/2024 11:35:01 AM ******/
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
 CONSTRAINT [PK__Combos__18F74AA38F758C5A] PRIMARY KEY CLUSTERED 
(
	[combo_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComboTicketDetails]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[Events]    Script Date: 11/5/2024 11:35:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[event_id] [int] IDENTITY(1,1) NOT NULL,
	[event_name] [varchar](100) NOT NULL,
	[description] [text] NULL,
	[event_date] [date] NOT NULL,
	[start_time] [time](7) NOT NULL,
	[end_time] [time](7) NULL,
	[location] [varchar](255) NULL,
	[picture_url] [varchar](255) NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
	[create_by] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[event_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodAndDrinkCategories]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[FoodAndDrinks]    Script Date: 11/5/2024 11:35:01 AM ******/
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
 CONSTRAINT [PK__FoodAndD__52020FDD3C1D54D9] PRIMARY KEY CLUSTERED 
(
	[item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodCombos]    Script Date: 11/5/2024 11:35:01 AM ******/
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
 CONSTRAINT [PK__FoodComb__18F74AA3B610D622] PRIMARY KEY CLUSTERED 
(
	[combo_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FootComboItems]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[OrderCampingGearDetails]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[OrderComboDetails]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[OrderFoodComboDetails]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[OrderFoodDetails]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 11/5/2024 11:35:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NULL,
	[employee_id] [int] NULL,
	[customer_name] [varchar](100) NULL,
	[order_date] [datetime] NULL,
	[order_usage_date] [datetime] NULL,
	[deposit] [decimal](10, 2) NOT NULL,
	[total_amount] [decimal](10, 2) NOT NULL,
	[amount_payable] [decimal](10, 2) NOT NULL,
	[status_order] [bit] NULL,
	[activity_id] [int] NULL,
	[phone_customer] [varchar](15) NULL,
	[order_checkout_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderTicketDetails]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[TicketCategories]    Script Date: 11/5/2024 11:35:01 AM ******/
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
/****** Object:  Table [dbo].[Tickets]    Script Date: 11/5/2024 11:35:01 AM ******/
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
 CONSTRAINT [PK__Tickets__D596F96B08919655] PRIMARY KEY CLUSTERED 
(
	[ticket_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/5/2024 11:35:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[phone_number] [varchar](15) NULL,
	[address] [text] NULL,
	[date_of_birth] [date] NULL,
	[gender] [char](6) NULL,
	[profile_picture_url] [varchar](255) NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
	[role_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url]) VALUES (1, N'Bộ bàn ghế Camp (6 người)', 10, CAST(100000.00 AS Decimal(10, 2)), N'Bộ bàn ghế dành cho 6 người, dễ dàng lắp đặt.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, NULL)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url]) VALUES (2, N'Chảo nướng (6 người)', 15, CAST(150000.00 AS Decimal(10, 2)), N'Chảo nướng dành cho 6 người, phù hợp cho các bữa tiệc ngoài trời.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 2, NULL)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url]) VALUES (3, N'Bếp nướng đá (10-20 người)', 5, CAST(250000.00 AS Decimal(10, 2)), N'Bếp nướng đá cho 10-20 người, lý tưởng cho cắm trại lớn.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 2, NULL)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url]) VALUES (4, N'Lều grand', 8, CAST(200000.00 AS Decimal(10, 2)), N'Lều lớn dành cho 4-6 người, thoải mái và bền.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 1, NULL)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url]) VALUES (5, N'Bếp từ', 12, CAST(150000.00 AS Decimal(10, 2)), N'Bếp từ tiện lợi, dễ sử dụng và an toàn.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 2, NULL)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url]) VALUES (6, N'Quạt', 20, CAST(60000.00 AS Decimal(10, 2)), N'Quạt điện, giúp không khí mát mẻ trong suốt chuyến đi.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, NULL)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url]) VALUES (7, N'Loa kéo', 10, CAST(250000.00 AS Decimal(10, 2)), N'Loa kéo, âm thanh lớn, thích hợp cho các bữa tiệc ngoài trời.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, NULL)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url]) VALUES (8, N'Tăng bạt', 5, CAST(100000.00 AS Decimal(10, 2)), N'Tăng bạt để che nắng, gió cho khu vực cắm trại.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, NULL)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url]) VALUES (9, N'Thảm', 15, CAST(50000.00 AS Decimal(10, 2)), N'Thảm trải đất, tạo không gian thoải mái khi ngồi.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, NULL)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url]) VALUES (12, N'dhdhdhdhd', 33, CAST(30000.00 AS Decimal(10, 2)), N'string', CAST(N'2024-10-16T20:00:44.740' AS DateTime), 2, N'string')
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
INSERT [dbo].[Combos] ([combo_id], [combo_name], [Description], [price], [created_at], [img_url]) VALUES (1, N'Combo và BBQ từ 5-10 người', N'Tính theo đầu người.', CAST(300000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T13:55:06.890' AS DateTime), NULL)
GO
INSERT [dbo].[Combos] ([combo_id], [combo_name], [Description], [price], [created_at], [img_url]) VALUES (2, N'Combo và BBQ từ 15-20 người', N'Tính theo đầu người.', CAST(250000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T13:55:06.890' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Combos] OFF
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
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (1, N'Cá trắm đen nướng 1kg', CAST(300000.00 AS Decimal(10, 2)), 50, N'Cá trắm đen nướng thơm ngon, phục vụ với nước chấm chua ngọt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (2, N'Cá trắm trắng nướng', CAST(600000.00 AS Decimal(10, 2)), 45, N'Cá trắm trắng nướng vàng ươm, ăn kèm rau sống.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (3, N'Cá trắm trắng hấp bia', CAST(1000000.00 AS Decimal(10, 2)), 30, N'Cá trắm trắng hấp bia ngọt thịt, mát lành.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (4, N'Cá chép nướng', CAST(400000.00 AS Decimal(10, 2)), 20, N'Cá chép nướng than, thịt mềm và đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (5, N'Cá chép nướng mỡ hành', CAST(600000.00 AS Decimal(10, 2)), 40, N'Cá chép nướng mỡ hành, giòn tan và thơm phức.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (6, N'Lẩu cá trắm trắng', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu cá trắm trắng nóng hổi, tươi ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (7, N'Lẩu cá trắm trắng với nấm', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu cá trắm trắng kết hợp với nấm, thơm ngọt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (8, N'Lẩu cá chép (Om dưa)', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu cá chép om dưa, chua chua ngọt ngọt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (9, N'Lẩu cá chép (Om dưa) với rau sống', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu cá chép om dưa, thêm rau sống tươi ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (10, N'Cá chép/Trắm trắng hấp bia sả', CAST(400000.00 AS Decimal(10, 2)), 25, N'Cá chép/trắm trắng hấp bia và sả, thơm lừng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (11, N'Cá chép/Trắm trắng hấp bia sả (tùy chọn)', CAST(1000000.00 AS Decimal(10, 2)), 25, N'Cá hấp với bia và sả, lựa chọn hảo hạng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (12, N'Gà nướng', CAST(400000.00 AS Decimal(10, 2)), 25, N'Gà nướng mềm mại, ướp gia vị đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (13, N'Gà nướng mật ong', CAST(600000.00 AS Decimal(10, 2)), 25, N'Gà nướng mật ong ngọt ngào, thơm phức.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (14, N'Gà luộc', CAST(400000.00 AS Decimal(10, 2)), 25, N'Gà luộc mềm, ăn kèm nước chấm gừng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (15, N'Gà luộc với rau củ', CAST(600000.00 AS Decimal(10, 2)), 25, N'Gà luộc ngon miệng, phục vụ với rau củ tươi.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (16, N'Lẩu gà', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu gà nóng hổi, ngọt nước, thích hợp cho gia đình.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (17, N'Lẩu gà cay', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu gà cay, cho những ai thích ăn cay.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (18, N'Gà rang muối', CAST(400000.00 AS Decimal(10, 2)), 25, N'Gà rang muối giòn tan, hương vị đặc biệt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (19, N'Gà rang muối và tiêu', CAST(600000.00 AS Decimal(10, 2)), 25, N'Gà rang muối tiêu thơm phức, giòn rụm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (20, N'Vịt/Ngan luộc', CAST(300000.00 AS Decimal(10, 2)), 25, N'Vịt/nan luộc, ngọt nước, ăn kèm với nước chấm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (21, N'Vịt/Ngan luộc với gia vị', CAST(500000.00 AS Decimal(10, 2)), 25, N'Vịt/nan luộc, thịt mềm và ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (22, N'Vịt/Ngan om măng', CAST(300000.00 AS Decimal(10, 2)), 25, N'Vịt/nan om măng chua chua, thanh mát.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (23, N'Vịt/Ngan om măng với nấm', CAST(500000.00 AS Decimal(10, 2)), 25, N'Vịt/nan om măng và nấm, hương vị tuyệt vời.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (24, N'Vịt/Ngan lẩu', CAST(300000.00 AS Decimal(10, 2)), 25, N'Vịt/nan nấu lẩu với nước dùng đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (25, N'Vịt/Ngan lẩu hải sản', CAST(500000.00 AS Decimal(10, 2)), 25, N'Lẩu vịt/nan kết hợp với hải sản tươi.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (26, N'Bò xào cần tỏi', CAST(150000.00 AS Decimal(10, 2)), 25, N'Bò xào cần tỏi giòn giòn, thơm ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (27, N'Trâu xào rau muống', CAST(150000.00 AS Decimal(10, 2)), 25, N'Trâu xào rau muống tươi, thanh mát.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (28, N'Trâu xào lá lốt', CAST(200000.00 AS Decimal(10, 2)), 25, N'Trâu xào lá lốt, hương vị đậm đà, hấp dẫn.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (29, N'Bò xào lá lốt', CAST(200000.00 AS Decimal(10, 2)), 25, N'Bò xào lá lốt, thơm phức và ngọt thịt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (30, N'Lẩu riêu cua bắp bò', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu riêu cua với bắp bò, thơm ngon, đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (31, N'Lẩu riêu cua bắp bò', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu riêu cua bắp bò với nấm, ngon miệng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (32, N'Lẩu hải sản', CAST(500000.00 AS Decimal(10, 2)), 25, N'Lẩu hải sản tươi ngon, đa dạng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (33, N'Lẩu hải sản với rau sống', CAST(800000.00 AS Decimal(10, 2)), 25, N'Lẩu hải sản thơm ngon, ăn kèm rau sống.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (34, N'Lẩu đuôi bò', CAST(500000.00 AS Decimal(10, 2)), 15, N'Lẩu đuôi bò đậm đà, hấp dẫn.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (35, N'Lẩu đuôi bò với gia vị đặc biệt', CAST(800000.00 AS Decimal(10, 2)), 15, N'Lẩu đuôi bò với gia vị đậm đà, hương vị phong phú.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (36, N'Rau theo mùa xào', CAST(50000.00 AS Decimal(10, 2)), 25, N'Rau theo mùa xào, tươi ngon và dinh dưỡng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (37, N'Ngô khoai chiên', CAST(50000.00 AS Decimal(10, 2)), 25, N'Ngô khoai chiên giòn, thơm lừng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (38, N'Khoai lang kén', CAST(50000.00 AS Decimal(10, 2)), 25, N'Khoai lang kén vàng giòn, ngọt ngào.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (39, N'Đậu rán', CAST(50000.00 AS Decimal(10, 2)), 25, N'Đậu rán giòn rụm, ăn kèm với nước chấm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (40, N'Đậu xốt cà chua', CAST(50000.00 AS Decimal(10, 2)), 25, N'Đậu xốt cà chua, đậm đà hương vị.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (41, N'Đậu tẩm hành', CAST(50000.00 AS Decimal(10, 2)), 25, N'Đậu tẩm hành chiên giòn, thơm ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (42, N'Trứng rán', CAST(50000.00 AS Decimal(10, 2)), 25, N'Trứng rán giòn, ăn kèm với cơm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (43, N'Ba chỉ bò mỹ BBQ', CAST(50000.00 AS Decimal(10, 2)), 25, N'Ba chỉ bò mỹ BBQ thơm ngon, hấp dẫn.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (44, N'Ba chỉ heo BBQ', CAST(50000.00 AS Decimal(10, 2)), 25, N'Ba chỉ heo BBQ giòn tan, thơm phức.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (45, N'Cánh gà BBQ', CAST(50000.00 AS Decimal(10, 2)), 25, N'Cánh gà BBQ, món ăn không thể thiếu trong tiệc nướng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (46, N'Sườn heo BBQ', CAST(20000.00 AS Decimal(10, 2)), 25, N'Sườn heo BBQ, ngọt thịt, đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (47, N'Xúc xích BBQ', CAST(20000.00 AS Decimal(10, 2)), 25, N'Xúc xích BBQ, thơm ngon, đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (48, N'Rau củ BBQ', CAST(20000.00 AS Decimal(10, 2)), 25, N'Rau củ BBQ, ăn kèm với các món nướng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (49, N'Bia Hà Nội', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (50, N'Coca cola', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (51, N'Sting', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (52, N'Seven up', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (53, N'Nước cam', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (54, N'Rượi 1 Chai', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (55, N'Bia Sài Gòn', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (56, N'Nước khoáng', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (57, N'Bia Hà Nội (Thùng)', CAST(250000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url]) VALUES (58, N'Coca cola (Thùng)', CAST(250000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL)
GO
SET IDENTITY_INSERT [dbo].[FoodAndDrinks] OFF
GO
SET IDENTITY_INSERT [dbo].[FoodCombos] ON 
GO
INSERT [dbo].[FoodCombos] ([combo_id], [combo_name], [Description], [price], [created_at], [img_url]) VALUES (1, N'Combo BBQ', N'.', CAST(150000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T13:46:27.720' AS DateTime), NULL)
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
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2098, 10, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2100, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2102, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2103, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2106, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2110, 5, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2111, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2113, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2114, 6, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2115, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2117, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2122, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2124, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2126, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2127, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2128, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2129, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2130, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2136, 3, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2140, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2144, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2145, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2146, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2147, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2148, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2153, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2154, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2163, 7, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 2164, 12, NULL)
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
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2102, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2103, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2106, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2107, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2109, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2110, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2111, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2113, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2114, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2115, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2117, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2124, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2126, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2140, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2146, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2148, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2153, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2154, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2158, 8, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 2163, 5, NULL)
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
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2103, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2106, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2107, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2114, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2145, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2146, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (3, 2172, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (4, 2102, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (4, 2103, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (4, 2113, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (5, 2113, 1, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (7, 2103, 3, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2102, 1, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2103, 5, NULL)
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
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (2, 2102, 1, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (2, 2122, 2, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (2, 2128, 1, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2098, 1, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2105, 3, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2124, 1, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2125, 2, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2126, 1, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2140, 5, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2148, 6, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2158, 3, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2159, 6, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2172, 2, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 2173, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2098, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2099, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2102, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2103, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2104, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2106, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2107, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2109, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2110, 4, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2111, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2113, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2114, 4, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2115, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2116, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2119, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2123, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2124, 3, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2125, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2127, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2129, 3, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2130, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2136, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2137, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2138, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2139, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2140, 3, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2142, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2143, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2145, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2146, 1, N'Cá tr?m den nu?ng 1kg')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 2148, 1, NULL)
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
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2099, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2106, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2107, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2109, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2110, 4, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2111, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2113, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2115, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2119, 1, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2123, 1, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2136, 1, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2137, 6, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2138, 1, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2139, 1, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2144, 3, N'Cá tr?m tr?ng nu?ng')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 2146, 1, N'Cá tr?m tr?ng nu?ng')
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
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2106, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2115, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2119, 1, N'Cá tr?m tr?ng h?p bia')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2139, 1, N'Cá tr?m tr?ng h?p bia')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2143, 1, N'Cá tr?m tr?ng h?p bia')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2145, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2147, 1, N'Cá tr?m tr?ng h?p bia')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (3, 2159, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (10, 2103, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (10, 2104, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (11, 2103, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2098, NULL, 2, N'Nguyen Van An', CAST(N'2024-10-22T16:24:32.753' AS DateTime), CAST(N'2024-11-01T16:23:00.000' AS DateTime), CAST(2000.00 AS Decimal(10, 2)), CAST(2300000.00 AS Decimal(10, 2)), CAST(2298000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2099, 2, 2, N'Nguyen Van An', CAST(N'2024-10-22T19:30:41.920' AS DateTime), CAST(N'2024-10-20T19:22:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(-16940000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2100, 2, 2, N'Nguyen Van A', CAST(N'2024-10-22T21:38:02.870' AS DateTime), CAST(N'2024-10-30T21:37:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(330000.00 AS Decimal(10, 2)), CAST(130000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2102, 2, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-23T09:26:47.267' AS DateTime), CAST(N'2024-10-23T09:22:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1300000.00 AS Decimal(10, 2)), CAST(1300000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2103, 3, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-23T09:40:14.053' AS DateTime), CAST(N'2024-10-31T09:22:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(4900000.00 AS Decimal(10, 2)), CAST(4900000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2104, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-24T13:43:19.387' AS DateTime), CAST(N'2024-10-26T13:42:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1160000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2105, NULL, 2, N'Nguyen Van An', CAST(N'2024-10-25T11:38:31.500' AS DateTime), CAST(N'2024-10-26T11:37:00.000' AS DateTime), CAST(2000.00 AS Decimal(10, 2)), CAST(450000.00 AS Decimal(10, 2)), CAST(448000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2106, NULL, 2, N'Nguyen Van An', CAST(N'2024-10-25T21:55:53.067' AS DateTime), CAST(N'2024-10-26T21:55:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(4560000.00 AS Decimal(10, 2)), CAST(4560000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2107, NULL, NULL, N'Nguyen Dang Hoang', CAST(N'2024-10-26T11:35:24.893' AS DateTime), CAST(N'2024-10-25T11:34:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1710000.00 AS Decimal(10, 2)), CAST(1710000.00 AS Decimal(10, 2)), 0, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2109, NULL, NULL, N'Nguyen Dang Hoang', CAST(N'2024-10-26T15:35:42.843' AS DateTime), CAST(N'2024-10-25T15:35:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1760000.00 AS Decimal(10, 2)), CAST(1560000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2110, NULL, NULL, N'Nguyen Dang Hoang', CAST(N'2024-10-26T15:40:43.347' AS DateTime), CAST(N'2024-10-30T15:40:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(4030000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2111, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T15:45:45.223' AS DateTime), CAST(N'2024-10-30T15:45:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(4500000.00 AS Decimal(10, 2)), CAST(4300000.00 AS Decimal(10, 2)), 1, 1002, NULL, NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2112, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T15:50:45.530' AS DateTime), CAST(N'2024-11-01T15:50:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2113, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T20:10:28.087' AS DateTime), CAST(N'2024-10-27T20:08:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(2160000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2114, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T21:29:20.930' AS DateTime), CAST(N'2024-10-26T21:26:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(2610000.00 AS Decimal(10, 2)), CAST(2410000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2115, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T21:30:21.517' AS DateTime), CAST(N'2024-10-26T21:29:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(3460000.00 AS Decimal(10, 2)), CAST(3260000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2116, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T21:37:57.170' AS DateTime), CAST(N'2024-10-30T21:36:00.000' AS DateTime), CAST(20000.00 AS Decimal(10, 2)), CAST(860000.00 AS Decimal(10, 2)), CAST(840000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2117, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T21:47:29.340' AS DateTime), CAST(N'2024-10-26T22:46:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(760000.00 AS Decimal(10, 2)), CAST(760000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2118, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T21:48:17.917' AS DateTime), CAST(N'2024-10-26T22:47:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(140000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2119, 1003, NULL, N'Test Customer', CAST(N'2024-10-27T10:12:43.577' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(2110000.00 AS Decimal(10, 2)), CAST(1810000.00 AS Decimal(10, 2)), 1, 3, N'1234567890', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2120, 1003, NULL, N'Test Customer', CAST(N'2024-10-27T16:20:00.073' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 0, 3, N'1234567890', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2121, 1003, NULL, N'Test Customer', CAST(N'2024-10-27T17:24:52.187' AS DateTime), CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(130000.00 AS Decimal(10, 2)), CAST(130000.00 AS Decimal(10, 2)), 0, 1002, N'1234567890', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2122, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-27T22:52:21.480' AS DateTime), CAST(N'2024-10-27T22:51:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1300000.00 AS Decimal(10, 2)), CAST(1100000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2123, 1003, NULL, N'Test Customer', CAST(N'2024-10-28T01:07:46.920' AS DateTime), CAST(N'2024-10-30T00:00:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1230000.00 AS Decimal(10, 2)), CAST(1030000.00 AS Decimal(10, 2)), 1, 3, N'1234567890', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2124, NULL, 2, N'danghoang', CAST(N'2024-10-28T20:23:35.710' AS DateTime), CAST(N'2024-10-28T20:23:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1570000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2125, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-28T22:07:49.113' AS DateTime), CAST(N'2024-10-28T22:06:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1320000.00 AS Decimal(10, 2)), CAST(1120000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2126, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-28T22:08:20.050' AS DateTime), CAST(N'2024-10-30T22:07:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1040000.00 AS Decimal(10, 2)), CAST(1040000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2127, NULL, 2, N'Nguyen Dang Hoang1', CAST(N'2024-10-28T22:09:20.793' AS DateTime), CAST(N'2024-10-28T22:08:00.000' AS DateTime), CAST(2000000.00 AS Decimal(10, 2)), CAST(4600000.00 AS Decimal(10, 2)), CAST(2600000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2128, NULL, 2, N'Nguyen Dang Hoang1', CAST(N'2024-10-28T22:11:32.323' AS DateTime), CAST(N'2024-10-28T22:11:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(950000.00 AS Decimal(10, 2)), CAST(750000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2129, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-28T22:55:47.767' AS DateTime), CAST(N'2024-10-28T22:55:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1440000.00 AS Decimal(10, 2)), CAST(1240000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2130, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-28T22:57:35.933' AS DateTime), CAST(N'2024-10-28T22:57:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1090000.00 AS Decimal(10, 2)), CAST(1090000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2131, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-29T09:49:00.140' AS DateTime), CAST(N'2024-10-29T09:48:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), CAST(40000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2132, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-29T09:53:11.970' AS DateTime), CAST(N'2024-10-30T09:52:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(550000.00 AS Decimal(10, 2)), CAST(550000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2133, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-29T09:59:14.953' AS DateTime), CAST(N'2024-10-29T16:58:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(720000.00 AS Decimal(10, 2)), CAST(420000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2134, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-29T10:41:44.940' AS DateTime), CAST(N'2024-10-29T17:40:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(440000.00 AS Decimal(10, 2)), CAST(140000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
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
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2140, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-29T21:08:12.933' AS DateTime), CAST(N'2024-10-29T23:07:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(2410000.00 AS Decimal(10, 2)), CAST(200000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2141, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-29T21:11:59.627' AS DateTime), CAST(N'2024-10-29T22:11:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(390000.00 AS Decimal(10, 2)), CAST(390000.00 AS Decimal(10, 2)), 0, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2142, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-10-29T21:46:13.570' AS DateTime), CAST(N'2024-10-29T22:44:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(380000.00 AS Decimal(10, 2)), CAST(380000.00 AS Decimal(10, 2)), 0, 3, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2143, 1004, NULL, N'Hoang Phi', CAST(N'2024-10-29T22:15:22.083' AS DateTime), CAST(N'2024-10-30T22:17:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1460000.00 AS Decimal(10, 2)), CAST(1460000.00 AS Decimal(10, 2)), 0, 3, N'0986169300', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2144, 1005, NULL, N'Doi Tan', CAST(N'2024-10-29T22:44:34.850' AS DateTime), CAST(N'2024-10-29T22:42:58.823' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(2080000.00 AS Decimal(10, 2)), CAST(2080000.00 AS Decimal(10, 2)), 0, 1002, N'0344780136', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2145, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-10-30T08:38:03.847' AS DateTime), CAST(N'2024-10-30T11:35:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1860000.00 AS Decimal(10, 2)), CAST(200000.00 AS Decimal(10, 2)), 1, 3, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2146, 1005, NULL, N'Doi Tan', CAST(N'2024-10-30T08:41:59.040' AS DateTime), CAST(N'2024-10-31T11:41:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(2289959.00 AS Decimal(10, 2)), CAST(2089959.00 AS Decimal(10, 2)), 1, 3, N'0344780136', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2147, 1004, NULL, N'Hoang Phi', CAST(N'2024-10-30T08:43:16.027' AS DateTime), CAST(N'2024-11-01T09:42:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1830000.00 AS Decimal(10, 2)), CAST(1830000.00 AS Decimal(10, 2)), 0, 1002, N'0986169300', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2148, NULL, 2, N'Anh Tien', CAST(N'2024-10-30T09:25:57.323' AS DateTime), CAST(N'2024-11-20T09:07:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(13790000.00 AS Decimal(10, 2)), CAST(13590000.00 AS Decimal(10, 2)), 1, 3, N'0979329948', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2149, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T13:56:24.863' AS DateTime), CAST(N'2024-11-01T13:55:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(210000.00 AS Decimal(10, 2)), CAST(10000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2150, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T13:56:48.180' AS DateTime), CAST(N'2024-11-01T13:56:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2151, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-01T13:57:13.330' AS DateTime), CAST(N'2024-11-01T16:56:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(140000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2152, NULL, NULL, N'Nguyen Dang Anh', CAST(N'2024-11-01T15:39:54.397' AS DateTime), CAST(N'2024-11-01T15:39:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(260000.00 AS Decimal(10, 2)), CAST(60000.00 AS Decimal(10, 2)), 1, 1002, N'0981694289', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2153, NULL, 2, N'Nguyen Dang Anh', CAST(N'2024-11-01T15:41:02.563' AS DateTime), CAST(N'2024-11-02T15:40:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(490000.00 AS Decimal(10, 2)), CAST(290000.00 AS Decimal(10, 2)), 1, 3, N'0981694289', NULL)
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
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2163, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-02T14:16:10.443' AS DateTime), CAST(N'2024-11-20T14:15:00.000' AS DateTime), CAST(748000.00 AS Decimal(10, 2)), CAST(1870000.00 AS Decimal(10, 2)), CAST(1122000.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2164, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-02T14:16:50.013' AS DateTime), CAST(N'2024-11-03T02:55:09.450' AS DateTime), CAST(154000.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(10, 2)), CAST(-153900.00 AS Decimal(10, 2)), 1, 3, N'0981694286', NULL)
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
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2193, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T16:52:07.180' AS DateTime), CAST(N'2024-11-04T20:41:00.000' AS DateTime), CAST(80000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(260000.00 AS Decimal(10, 2)), 1, 2, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2194, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T17:08:55.450' AS DateTime), CAST(N'2024-11-04T17:09:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(840000.00 AS Decimal(10, 2)), CAST(740000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2195, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-04T17:14:39.623' AS DateTime), CAST(N'2024-11-04T17:14:00.000' AS DateTime), CAST(40000.00 AS Decimal(10, 2)), CAST(210000.00 AS Decimal(10, 2)), CAST(170000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2196, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-05T11:03:26.497' AS DateTime), CAST(N'2024-11-05T11:03:00.000' AS DateTime), CAST(40000.00 AS Decimal(10, 2)), CAST(740000.00 AS Decimal(10, 2)), CAST(700000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2197, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-05T11:05:20.560' AS DateTime), CAST(N'2024-11-06T11:05:00.000' AS DateTime), CAST(40000.00 AS Decimal(10, 2)), CAST(340000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)), 1, 1, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2198, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-05T11:25:03.633' AS DateTime), CAST(N'2024-11-05T11:24:00.000' AS DateTime), CAST(30000.00 AS Decimal(10, 2)), CAST(160000.00 AS Decimal(10, 2)), CAST(130000.00 AS Decimal(10, 2)), 1, 2, N'0981694286', NULL)
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2098, 100966, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2099, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2100, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2104, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2106, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2107, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2109, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2110, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2112, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2113, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2114, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2115, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2116, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2117, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2118, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2119, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2120, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2121, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2123, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2124, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2125, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2126, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2129, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2130, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2131, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2132, 5, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2133, 9, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2134, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2135, 8, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2136, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2137, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2139, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2140, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2141, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2142, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2143, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2144, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2145, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2146, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2147, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2148, 9, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2149, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2150, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2151, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2152, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2153, 3, NULL)
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
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2163, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 2164, 4, NULL)
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
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2098, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2099, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2100, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2104, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2106, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2107, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2109, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2110, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2112, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2113, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2114, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2115, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2116, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2117, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2118, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2119, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2121, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2123, 5, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2124, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2125, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2126, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2130, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2132, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2134, 4, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2136, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2137, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2139, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2140, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2141, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2145, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2146, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2147, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2149, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2150, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2151, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2152, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2154, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2158, 5, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2159, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2163, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 2164, 1, NULL)
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
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2098, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2099, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2119, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2139, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2141, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 2146, 1, NULL)
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
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url]) VALUES (1, N'Người lớn', CAST(80000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 2, NULL)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url]) VALUES (2, N'Trẻ em (3T- <1,4m)', CAST(50000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 2, NULL)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url]) VALUES (3, N'Trẻ em < 3T', CAST(0.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 2, NULL)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url]) VALUES (4, N'Nhóm 20-50 khách', CAST(50000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 1, NULL)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url]) VALUES (5, N'Nhóm trên 50 khách', CAST(40000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Tickets] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (2, N'Nguyen Dang', N'Hoang', N'hoangndhe164015@fpt.edu.vn', N'123456', N'0981694286', N'123 Main St, Springfield', CAST(N'1985-01-15' AS Date), N'male  ', N'/images/owner.png', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 1)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (3, N'Phung Cong ', N'Tu', N'tu@gmail.com', N'123456', N'0987654321', N'456 Elm St, Riverside', CAST(N'1990-03-22' AS Date), N'male  ', N'http://example.com/profile/jane.jpg', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 2)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (4, N'Nguyen Tuan ', N'Dung', N'dung@gmail.com', N'123456', N'1122334455', N'789 Oak St, Mountain View', CAST(N'1995-07-30' AS Date), N'other ', N'http://example.com/profile/sam.jpg', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 3)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (1003, N'Phung Con', N'Tu', N'tupc2002@gmail.com', N'566020', N'012345678', NULL, NULL, NULL, NULL, 1, CAST(N'2024-10-08T22:44:07.097' AS DateTime), 3)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (1004, N'Hoang', N'Phi', N'hoangphihung.hn2002@gmail.com', N'123', N'0986169300', NULL, NULL, NULL, NULL, 1, CAST(N'2024-10-29T22:01:52.703' AS DateTime), 3)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (1005, N'Doi', N'Tan', N'doituan12345@gmail.com', N'10052002tan', N'0344780136', NULL, NULL, NULL, NULL, 1, CAST(N'2024-10-29T22:42:28.397' AS DateTime), 3)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__783254B16A44ECCD]    Script Date: 11/5/2024 11:35:01 AM ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[role_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A1936A6B0CB566FE]    Script Date: 11/5/2024 11:35:01 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[phone_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__AB6E61644EC97341]    Script Date: 11/5/2024 11:35:01 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
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
ALTER TABLE [dbo].[Events] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Events] ADD  DEFAULT (getdate()) FOR [created_at]
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
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [order_date]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [status_order]
GO
ALTER TABLE [dbo].[OrderTicketDetails] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[TicketCategories] ADD  CONSTRAINT [DF__TicketCat__creat__33D4B598]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Tickets] ADD  CONSTRAINT [DF__Tickets__created__36B12243]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [created_at]
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
ALTER TABLE [dbo].[Events]  WITH CHECK ADD FOREIGN KEY([create_by])
REFERENCES [dbo].[Users] ([user_id])
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
ALTER TABLE [dbo].[OrderCampingGearDetails]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[OrderComboDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderComb__combo__00200768] FOREIGN KEY([combo_id])
REFERENCES [dbo].[Combos] ([combo_id])
GO
ALTER TABLE [dbo].[OrderComboDetails] CHECK CONSTRAINT [FK__OrderComb__combo__00200768]
GO
ALTER TABLE [dbo].[OrderComboDetails]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[OrderFoodComboDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderFood__combo__787EE5A0] FOREIGN KEY([combo_id])
REFERENCES [dbo].[FoodCombos] ([combo_id])
GO
ALTER TABLE [dbo].[OrderFoodComboDetails] CHECK CONSTRAINT [FK__OrderFood__combo__787EE5A0]
GO
ALTER TABLE [dbo].[OrderFoodComboDetails]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[OrderFoodDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderFood__item___6A30C649] FOREIGN KEY([item_id])
REFERENCES [dbo].[FoodAndDrinks] ([item_id])
GO
ALTER TABLE [dbo].[OrderFoodDetails] CHECK CONSTRAINT [FK__OrderFood__item___6A30C649]
GO
ALTER TABLE [dbo].[OrderFoodDetails]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__activity__66603565] FOREIGN KEY([activity_id])
REFERENCES [dbo].[Activities] ([activity_id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__activity__66603565]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([customer_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([employee_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[OrderTicketDetails]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
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
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[Roles] ([role_id])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([gender]='other' OR [gender]='female' OR [gender]='male'))
GO
