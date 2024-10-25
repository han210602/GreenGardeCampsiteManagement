USE [GreenGarden]
GO
/****** Object:  Table [dbo].[Activities]    Script Date: 10/16/2024 9:52:23 PM ******/
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
/****** Object:  Table [dbo].[Amenities]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[CampingCategories]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[CampingGear]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[ComboCampingGearDetails]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[ComboFootDetails]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[Combos]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[ComboTicketDetails]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[Events]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[FoodAndDrinkCategories]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[FoodAndDrinks]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[FoodCombos]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[FootComboItems]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[OrderCampingGearDetails]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[OrderComboDetails]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[OrderFoodComboDetails]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[OrderFoodDetails]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 10/16/2024 9:52:24 PM ******/
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
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderTicketDetails]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[TicketCategories]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[Tickets]    Script Date: 10/16/2024 9:52:24 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 10/16/2024 9:52:24 PM ******/
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
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (1, 1086, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 1083, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 1084, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 1085, 2, NULL)
GO
INSERT [dbo].[OrderCampingGearDetails] ([gear_id], [order_id], [quantity], [Description]) VALUES (2, 1087, 2, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 1087, 30, NULL)
GO
INSERT [dbo].[OrderFoodComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (1, 1086, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 1008, 2, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (1, 1086, 2, N'string')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 1008, 1, NULL)
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 1084, 2, N'string')
GO
INSERT [dbo].[OrderFoodDetails] ([item_id], [order_id], [quantity], [Description]) VALUES (2, 1085, 2, N'string')
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1008, 3, NULL, N'', CAST(N'2024-10-08T17:39:15.970' AS DateTime), CAST(N'2024-10-08T00:00:00.000' AS DateTime), CAST(5.00 AS Decimal(10, 2)), CAST(4000000.00 AS Decimal(10, 2)), CAST(3999995.00 AS Decimal(10, 2)), 1, 3, N'0989463688')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1021, NULL, NULL, N'Nguyen Tuan Dung', CAST(N'2024-10-13T21:35:30.357' AS DateTime), CAST(N'2024-10-08T00:00:00.000' AS DateTime), CAST(4.00 AS Decimal(10, 2)), CAST(18000.00 AS Decimal(10, 2)), CAST(17996.00 AS Decimal(10, 2)), 1, 1, NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1022, NULL, NULL, N'Nguyen Dang ANh', CAST(N'2024-10-13T21:56:42.227' AS DateTime), CAST(N'2024-02-02T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(20000.00 AS Decimal(10, 2)), CAST(20000.00 AS Decimal(10, 2)), 0, 3, NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1034, NULL, NULL, N'33333', CAST(N'2024-10-16T16:55:44.397' AS DateTime), NULL, CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1002, NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1043, NULL, NULL, N'hoang', CAST(N'2024-10-16T19:26:09.490' AS DateTime), NULL, CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 3, NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1069, 3, 2, N'Hoang nguyen Dang', CAST(N'2024-10-16T20:04:21.750' AS DateTime), CAST(N'2024-10-16T00:00:00.000' AS DateTime), CAST(20.00 AS Decimal(10, 2)), CAST(30.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(10, 2)), 1, 1002, NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1070, 3, 2, N'string', CAST(N'2024-10-16T20:08:37.483' AS DateTime), CAST(N'2024-10-16T13:08:22.960' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1071, 3, 2, N'string', CAST(N'2024-10-16T20:09:28.040' AS DateTime), CAST(N'2024-10-16T13:08:22.960' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1072, 3, 2, N'string', CAST(N'2024-10-16T20:10:02.620' AS DateTime), CAST(N'2024-10-16T13:08:22.960' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1074, 3, 2, N'string', CAST(N'2024-10-16T20:13:58.137' AS DateTime), CAST(N'2024-10-16T13:13:23.077' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1075, 3, 2, N'string', CAST(N'2024-10-16T20:14:12.743' AS DateTime), CAST(N'2024-10-16T13:13:23.077' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1076, 2, 3, N'string', CAST(N'2024-10-16T20:16:21.847' AS DateTime), CAST(N'2024-10-16T13:15:54.277' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1077, 2, 3, N'string', CAST(N'2024-10-16T20:18:00.717' AS DateTime), CAST(N'2024-10-16T13:17:44.353' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1080, 3, 3, N'string', CAST(N'2024-10-16T20:22:50.450' AS DateTime), CAST(N'2024-10-16T13:20:56.563' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1081, 2, 3, N'string', CAST(N'2024-10-16T20:24:05.193' AS DateTime), CAST(N'2024-10-16T13:23:56.887' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1082, 2, 3, N'string', CAST(N'2024-10-16T20:25:23.077' AS DateTime), CAST(N'2024-10-16T13:23:56.887' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1083, 2, 3, N'string', CAST(N'2024-10-16T20:30:11.590' AS DateTime), CAST(N'2024-10-16T13:29:38.833' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1084, 3, 2, N'string', CAST(N'2024-10-16T20:33:03.000' AS DateTime), CAST(N'2024-10-16T13:32:44.733' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1085, 2, 3, N'string', CAST(N'2024-10-16T20:33:28.673' AS DateTime), CAST(N'2024-10-16T13:32:44.733' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0, 1, N'string')
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1086, NULL, 2, N'string', CAST(N'2024-10-16T20:37:39.117' AS DateTime), CAST(N'2024-10-16T13:37:12.050' AS DateTime), CAST(200.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(-200.00 AS Decimal(10, 2)), 1, 1002, NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer]) VALUES (1087, NULL, 2, N'Nguyen Tuan Dung', CAST(N'2024-10-16T21:48:32.480' AS DateTime), CAST(N'2024-10-16T14:47:26.003' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(2500000.00 AS Decimal(10, 2)), CAST(2300000.00 AS Decimal(10, 2)), 1, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 1008, 3, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 1080, 1, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 1083, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 1084, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 1085, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (1, 1086, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (2, 1008, 2, NULL)
GO
INSERT [dbo].[OrderTicketDetails] ([ticket_id], [order_id], [quantity], [Description]) VALUES (3, 1008, 4, NULL)
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
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (2, N'admin', N'123', N'hoangndhe164015@fpt.edu.vn', N'123', N'0981694286', N'123 Main St, Springfield', CAST(N'1985-01-15' AS Date), N'Female', N'http://example.com/profile/john.jpg', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 1)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (3, N'Ngyen Dang', N'Hoang', N'jane.smith@example.com', N'hashed_password_2', N'0987654321', N'456 Elm St, Riverside', CAST(N'1990-03-22' AS Date), N'male  ', N'http://example.com/profile/jane.jpg', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 2)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (4, N'Phung Cong', N'Tu', N'sam.wilson@example.com', N'hashed_password_3', N'1122334455', N'789 Oak St, Mountain View', CAST(N'1995-07-30' AS Date), N'other ', N'http://example.com/profile/sam.jpg', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 3)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (1003, N'Phung Con', N'Tu', N'tupc2002@gmail.com', N'566020', N'012345678', NULL, NULL, NULL, NULL, 1, CAST(N'2024-10-08T22:44:07.097' AS DateTime), 3)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__783254B16A44ECCD]    Script Date: 10/16/2024 9:52:24 PM ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[role_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A1936A6B0CB566FE]    Script Date: 10/16/2024 9:52:24 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[phone_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__AB6E61644EC97341]    Script Date: 10/16/2024 9:52:24 PM ******/
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
