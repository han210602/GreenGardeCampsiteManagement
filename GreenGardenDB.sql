
USE [GreenGarden]
GO
/****** Object:  Table [dbo].[Activities]    Script Date: 11/13/2024 9:16:05 PM ******/
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
/****** Object:  Table [dbo].[Amenities]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[CampingCategories]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[CampingGear]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[ComboCampingGearDetails]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[ComboFootDetails]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[Combos]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[ComboTicketDetails]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[Events]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[FoodAndDrinkCategories]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[FoodAndDrinks]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[FoodCombos]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[FootComboItems]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[OrderCampingGearDetails]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[OrderComboDetails]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[OrderFoodComboDetails]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[OrderFoodDetails]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[OrderTicketDetails]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[TicketCategories]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[Tickets]    Script Date: 11/13/2024 9:16:06 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 11/13/2024 9:16:06 PM ******/
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
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (1, N'Bộ bàn ghế Camp (6 người)', 10, CAST(100000.00 AS Decimal(10, 2)), N'Bộ bàn ghế dành cho 6 người, dễ dàng lắp đặt.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (2, N'Chảo nướng (6 người)', 15, CAST(150000.00 AS Decimal(10, 2)), N'Chảo nướng dành cho 6 người, phù hợp cho các bữa tiệc ngoài trời.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (3, N'Bếp nướng đá (10-20 người)', 5, CAST(250000.00 AS Decimal(10, 2)), N'Bếp nướng đá cho 10-20 người, lý tưởng cho cắm trại lớn.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (4, N'Lều grand', 8, CAST(200000.00 AS Decimal(10, 2)), N'Lều lớn dành cho 4-6 người, thoải mái và bền.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (5, N'Bếp từ', 12, CAST(150000.00 AS Decimal(10, 2)), N'Bếp từ tiện lợi, dễ sử dụng và an toàn.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (6, N'Quạt', 20, CAST(60000.00 AS Decimal(10, 2)), N'Quạt điện, giúp không khí mát mẻ trong suốt chuyến đi.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (7, N'Loa kéo', 10, CAST(250000.00 AS Decimal(10, 2)), N'Loa kéo, âm thanh lớn, thích hợp cho các bữa tiệc ngoài trời.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (8, N'Tăng bạt', 5, CAST(100000.00 AS Decimal(10, 2)), N'Tăng bạt để che nắng, gió cho khu vực cắm trại.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (9, N'Thảm', 15, CAST(50000.00 AS Decimal(10, 2)), N'Thảm trải đất, tạo không gian thoải mái khi ngồi.', CAST(N'2024-10-07T10:04:06.603' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[CampingGear] ([gear_id], [gear_name], [quantityAvailable], [rentalPrice], [Description], [created_at], [gear_category_id], [img_url], [status]) VALUES (12, N'dhdhdhdhd', 33, CAST(30000.00 AS Decimal(10, 2)), N'string', CAST(N'2024-10-16T20:00:44.740' AS DateTime), 2, N'string', 1)
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
INSERT [dbo].[Combos] ([combo_id], [combo_name], [Description], [price], [created_at], [img_url], [status]) VALUES (1, N'Combo và BBQ từ 5-10 người', N'Tính theo đầu người.', CAST(300000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T13:55:06.890' AS DateTime), NULL, 1)
GO
INSERT [dbo].[Combos] ([combo_id], [combo_name], [Description], [price], [created_at], [img_url], [status]) VALUES (2, N'Combo và BBQ từ 15-20 người', N'Tính theo đầu người.', CAST(250000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T13:55:06.890' AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Combos] OFF
GO
SET IDENTITY_INSERT [dbo].[Events] ON 
GO
INSERT [dbo].[Events] ([event_id], [event_name], [description], [event_date], [start_time], [end_time], [location], [picture_url], [is_active], [created_at], [create_by]) VALUES (2, N'Xin loi thay', NULL, CAST(N'2024-11-13' AS Date), CAST(N'17:31:00' AS Time), CAST(N'18:28:00' AS Time), N'Sam son', NULL, 1, CAST(N'2024-11-11T17:28:18.083' AS DateTime), 2)
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
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (1, N'Cá trắm đen nướng 1kg', CAST(300000.00 AS Decimal(10, 2)), 50, N'Cá trắm đen nướng thơm ngon, phục vụ với nước chấm chua ngọt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (2, N'Cá trắm trắng nướng', CAST(600000.00 AS Decimal(10, 2)), 45, N'Cá trắm trắng nướng vàng ươm, ăn kèm rau sống.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (3, N'Cá trắm trắng hấp bia', CAST(1000000.00 AS Decimal(10, 2)), 30, N'Cá trắm trắng hấp bia ngọt thịt, mát lành.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (4, N'Cá chép nướng', CAST(400000.00 AS Decimal(10, 2)), 20, N'Cá chép nướng than, thịt mềm và đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (5, N'Cá chép nướng mỡ hành', CAST(600000.00 AS Decimal(10, 2)), 40, N'Cá chép nướng mỡ hành, giòn tan và thơm phức.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (6, N'Lẩu cá trắm trắng', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu cá trắm trắng nóng hổi, tươi ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (7, N'Lẩu cá trắm trắng với nấm', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu cá trắm trắng kết hợp với nấm, thơm ngọt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (8, N'Lẩu cá chép (Om dưa)', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu cá chép om dưa, chua chua ngọt ngọt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (9, N'Lẩu cá chép (Om dưa) với rau sống', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu cá chép om dưa, thêm rau sống tươi ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (10, N'Cá chép/Trắm trắng hấp bia sả', CAST(400000.00 AS Decimal(10, 2)), 25, N'Cá chép/trắm trắng hấp bia và sả, thơm lừng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (11, N'Cá chép/Trắm trắng hấp bia sả (tùy chọn)', CAST(1000000.00 AS Decimal(10, 2)), 25, N'Cá hấp với bia và sả, lựa chọn hảo hạng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (12, N'Gà nướng', CAST(400000.00 AS Decimal(10, 2)), 25, N'Gà nướng mềm mại, ướp gia vị đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (13, N'Gà nướng mật ong', CAST(600000.00 AS Decimal(10, 2)), 25, N'Gà nướng mật ong ngọt ngào, thơm phức.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (14, N'Gà luộc', CAST(400000.00 AS Decimal(10, 2)), 25, N'Gà luộc mềm, ăn kèm nước chấm gừng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (15, N'Gà luộc với rau củ', CAST(600000.00 AS Decimal(10, 2)), 25, N'Gà luộc ngon miệng, phục vụ với rau củ tươi.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (16, N'Lẩu gà', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu gà nóng hổi, ngọt nước, thích hợp cho gia đình.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (17, N'Lẩu gà cay', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu gà cay, cho những ai thích ăn cay.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (18, N'Gà rang muối', CAST(400000.00 AS Decimal(10, 2)), 25, N'Gà rang muối giòn tan, hương vị đặc biệt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (19, N'Gà rang muối và tiêu', CAST(600000.00 AS Decimal(10, 2)), 25, N'Gà rang muối tiêu thơm phức, giòn rụm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (20, N'Vịt/Ngan luộc', CAST(300000.00 AS Decimal(10, 2)), 25, N'Vịt/nan luộc, ngọt nước, ăn kèm với nước chấm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (21, N'Vịt/Ngan luộc với gia vị', CAST(500000.00 AS Decimal(10, 2)), 25, N'Vịt/nan luộc, thịt mềm và ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (22, N'Vịt/Ngan om măng', CAST(300000.00 AS Decimal(10, 2)), 25, N'Vịt/nan om măng chua chua, thanh mát.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (23, N'Vịt/Ngan om măng với nấm', CAST(500000.00 AS Decimal(10, 2)), 25, N'Vịt/nan om măng và nấm, hương vị tuyệt vời.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (24, N'Vịt/Ngan lẩu', CAST(300000.00 AS Decimal(10, 2)), 25, N'Vịt/nan nấu lẩu với nước dùng đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (25, N'Vịt/Ngan lẩu hải sản', CAST(500000.00 AS Decimal(10, 2)), 25, N'Lẩu vịt/nan kết hợp với hải sản tươi.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (26, N'Bò xào cần tỏi', CAST(150000.00 AS Decimal(10, 2)), 25, N'Bò xào cần tỏi giòn giòn, thơm ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (27, N'Trâu xào rau muống', CAST(150000.00 AS Decimal(10, 2)), 25, N'Trâu xào rau muống tươi, thanh mát.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (28, N'Trâu xào lá lốt', CAST(200000.00 AS Decimal(10, 2)), 25, N'Trâu xào lá lốt, hương vị đậm đà, hấp dẫn.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (29, N'Bò xào lá lốt', CAST(200000.00 AS Decimal(10, 2)), 25, N'Bò xào lá lốt, thơm phức và ngọt thịt.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (30, N'Lẩu riêu cua bắp bò', CAST(400000.00 AS Decimal(10, 2)), 25, N'Lẩu riêu cua với bắp bò, thơm ngon, đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (31, N'Lẩu riêu cua bắp bò', CAST(600000.00 AS Decimal(10, 2)), 25, N'Lẩu riêu cua bắp bò với nấm, ngon miệng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (32, N'Lẩu hải sản', CAST(500000.00 AS Decimal(10, 2)), 25, N'Lẩu hải sản tươi ngon, đa dạng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (33, N'Lẩu hải sản với rau sống', CAST(800000.00 AS Decimal(10, 2)), 25, N'Lẩu hải sản thơm ngon, ăn kèm rau sống.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (34, N'Lẩu đuôi bò', CAST(500000.00 AS Decimal(10, 2)), 15, N'Lẩu đuôi bò đậm đà, hấp dẫn.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (35, N'Lẩu đuôi bò với gia vị đặc biệt', CAST(800000.00 AS Decimal(10, 2)), 15, N'Lẩu đuôi bò với gia vị đậm đà, hương vị phong phú.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 3, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (36, N'Rau theo mùa xào', CAST(50000.00 AS Decimal(10, 2)), 25, N'Rau theo mùa xào, tươi ngon và dinh dưỡng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (37, N'Ngô khoai chiên', CAST(50000.00 AS Decimal(10, 2)), 25, N'Ngô khoai chiên giòn, thơm lừng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (38, N'Khoai lang kén', CAST(50000.00 AS Decimal(10, 2)), 25, N'Khoai lang kén vàng giòn, ngọt ngào.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (39, N'Đậu rán', CAST(50000.00 AS Decimal(10, 2)), 25, N'Đậu rán giòn rụm, ăn kèm với nước chấm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (40, N'Đậu xốt cà chua', CAST(50000.00 AS Decimal(10, 2)), 25, N'Đậu xốt cà chua, đậm đà hương vị.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (41, N'Đậu tẩm hành', CAST(50000.00 AS Decimal(10, 2)), 25, N'Đậu tẩm hành chiên giòn, thơm ngon.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (42, N'Trứng rán', CAST(50000.00 AS Decimal(10, 2)), 25, N'Trứng rán giòn, ăn kèm với cơm.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 4, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (43, N'Ba chỉ bò mỹ BBQ', CAST(50000.00 AS Decimal(10, 2)), 25, N'Ba chỉ bò mỹ BBQ thơm ngon, hấp dẫn.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (44, N'Ba chỉ heo BBQ', CAST(50000.00 AS Decimal(10, 2)), 25, N'Ba chỉ heo BBQ giòn tan, thơm phức.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (45, N'Cánh gà BBQ', CAST(50000.00 AS Decimal(10, 2)), 25, N'Cánh gà BBQ, món ăn không thể thiếu trong tiệc nướng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (46, N'Sườn heo BBQ', CAST(20000.00 AS Decimal(10, 2)), 25, N'Sườn heo BBQ, ngọt thịt, đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (47, N'Xúc xích BBQ', CAST(20000.00 AS Decimal(10, 2)), 25, N'Xúc xích BBQ, thơm ngon, đậm đà.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (48, N'Rau củ BBQ', CAST(20000.00 AS Decimal(10, 2)), 25, N'Rau củ BBQ, ăn kèm với các món nướng.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 6, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (49, N'Bia Hà Nội', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (50, N'Coca cola', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (51, N'Sting', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (52, N'Seven up', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (53, N'Nước cam', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (54, N'Rượi 1 Chai', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (55, N'Bia Sài Gòn', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (56, N'Nước khoáng', CAST(10000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (57, N'Bia Hà Nội (Thùng)', CAST(250000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL, 1)
GO
INSERT [dbo].[FoodAndDrinks] ([item_id], [item_name], [price], [quantityAvailable], [Description], [created_at], [category_id], [img_url], [status]) VALUES (58, N'Coca cola (Thùng)', CAST(250000.00 AS Decimal(10, 2)), 25, N'.', CAST(N'2024-10-07T13:43:59.463' AS DateTime), 5, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[FoodAndDrinks] OFF
GO
SET IDENTITY_INSERT [dbo].[FoodCombos] ON 
GO
INSERT [dbo].[FoodCombos] ([combo_id], [combo_name], [Description], [price], [created_at], [img_url], [status]) VALUES (1, N'Combo BBQ', N'.', CAST(150000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T13:46:27.720' AS DateTime), NULL, 1)
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
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (2, 2122, 2, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (2, 2128, 1, NULL)
GO
INSERT [dbo].[OrderComboDetails] ([combo_id], [order_id], [quantity], [Description]) VALUES (2, 2208, 2, NULL)
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
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2107, NULL, NULL, N'Nguyen Dang Hoang', CAST(N'2024-10-26T11:35:24.893' AS DateTime), CAST(N'2024-10-25T11:34:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(1710000.00 AS Decimal(10, 2)), CAST(1710000.00 AS Decimal(10, 2)), 0, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2109, NULL, NULL, N'Nguyen Dang Hoang', CAST(N'2024-10-26T15:35:42.843' AS DateTime), CAST(N'2024-10-25T15:35:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1760000.00 AS Decimal(10, 2)), CAST(1560000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2111, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T15:45:45.223' AS DateTime), CAST(N'2024-10-30T15:45:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(4500000.00 AS Decimal(10, 2)), CAST(4300000.00 AS Decimal(10, 2)), 1, 1002, NULL, NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2114, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T21:29:20.930' AS DateTime), CAST(N'2024-10-26T21:26:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(2610000.00 AS Decimal(10, 2)), CAST(2410000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2115, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-26T21:30:21.517' AS DateTime), CAST(N'2024-10-26T21:29:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(3460000.00 AS Decimal(10, 2)), CAST(3260000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2121, 1003, NULL, N'Test Customer', CAST(N'2024-10-27T17:24:52.187' AS DateTime), CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(130000.00 AS Decimal(10, 2)), CAST(130000.00 AS Decimal(10, 2)), 0, 1002, N'1234567890', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2122, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-10-27T22:52:21.480' AS DateTime), CAST(N'2024-10-27T22:51:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(1300000.00 AS Decimal(10, 2)), CAST(1100000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
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
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2205, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-11T15:07:37.860' AS DateTime), CAST(N'2024-11-11T15:06:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(2140000.00 AS Decimal(10, 2)), CAST(2040000.00 AS Decimal(10, 2)), 1, 2, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2206, NULL, 2, N'Nguyen Dang Hoang', CAST(N'2024-11-11T17:26:37.607' AS DateTime), CAST(N'2024-11-12T17:26:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(510000.00 AS Decimal(10, 2)), CAST(510000.00 AS Decimal(10, 2)), 0, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2207, NULL, 2, N'danghoang', CAST(N'2024-11-12T19:30:26.147' AS DateTime), CAST(N'2024-11-12T19:30:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)), CAST(410000.00 AS Decimal(10, 2)), CAST(110000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2208, NULL, 2, N'danghoang', CAST(N'2024-11-12T19:32:59.507' AS DateTime), CAST(N'2024-11-12T19:32:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(1100000.00 AS Decimal(10, 2)), CAST(1000000.00 AS Decimal(10, 2)), 1, 2, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2209, NULL, 2, N'danghoang', CAST(N'2024-11-12T19:45:45.393' AS DateTime), CAST(N'2024-11-12T19:45:00.000' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(1430000.00 AS Decimal(10, 2)), CAST(430000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2210, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-13T13:19:00.137' AS DateTime), CAST(N'2024-11-13T13:18:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(240000.00 AS Decimal(10, 2)), CAST(140000.00 AS Decimal(10, 2)), 1, 1, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2211, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-11-13T13:34:12.520' AS DateTime), CAST(N'2024-11-12T13:35:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2212, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-11-13T13:38:10.937' AS DateTime), CAST(N'2024-11-12T13:39:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2213, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-11-13T13:43:51.900' AS DateTime), CAST(N'2024-11-10T14:43:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)), 0, 1002, N'1122334455', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2214, NULL, 2, N'Nguyen Van An', CAST(N'2024-11-13T13:46:07.057' AS DateTime), CAST(N'2024-11-06T13:46:00.000' AS DateTime), CAST(50000.00 AS Decimal(10, 2)), CAST(260000.00 AS Decimal(10, 2)), CAST(210000.00 AS Decimal(10, 2)), 1, 1002, N'0981694286', NULL)
GO
INSERT [dbo].[Orders] ([order_id], [customer_id], [employee_id], [customer_name], [order_date], [order_usage_date], [deposit], [total_amount], [amount_payable], [status_order], [activity_id], [phone_customer], [order_checkout_date]) VALUES (2215, 4, NULL, N'Nguyen Tuan  Dung', CAST(N'2024-11-13T13:50:29.307' AS DateTime), CAST(N'2024-11-14T13:51:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), CAST(980000.00 AS Decimal(10, 2)), CAST(980000.00 AS Decimal(10, 2)), 0, 1, N'1122334455', NULL)
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
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url], [status]) VALUES (1, N'Người lớn', CAST(80000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url], [status]) VALUES (2, N'Trẻ em (3T- <1,4m)', CAST(50000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url], [status]) VALUES (3, N'Trẻ em < 3T', CAST(0.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 2, NULL, 1)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url], [status]) VALUES (4, N'Nhóm 20-50 khách', CAST(50000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 1, NULL, 1)
GO
INSERT [dbo].[Tickets] ([ticket_id], [ticket_name], [price], [created_at], [ticket_category_id], [img_url], [status]) VALUES (5, N'Nhóm trên 50 khách', CAST(40000.00 AS Decimal(10, 2)), CAST(N'2024-10-07T09:48:33.180' AS DateTime), 1, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Tickets] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (2, N'Dang', N'Hoang', N'hoangndhe164015@fpt.edu.vn', N'123456', N'0981694286', N'Sam Son Thanh Hoa', CAST(N'0006-06-21' AS Date), N'male  ', N'/images/owner.png', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 1)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (3, N'Phung Cong ', N'Tu', N'tu@gmail.com', N'123456', N'0987654321', N'456 Elm St, Riverside', CAST(N'1990-03-22' AS Date), N'male  ', N'http://example.com/profile/jane.jpg', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 2)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (4, N'Nguyen Tuan ', N'Dung', N'dung@gmail.com', N'123456', N'1122334455', N'789 Oak St, Mountain View', CAST(N'1995-07-30' AS Date), N'other ', N'http://example.com/profile/sam.jpg', 1, CAST(N'2024-10-07T09:17:50.273' AS DateTime), 3)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (1003, N'Phung Con', N'Tu', N'tupc2002@gmail.com', N'566020', N'012345678', NULL, NULL, NULL, NULL, 0, CAST(N'2024-10-08T22:44:07.097' AS DateTime), 3)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (1004, N'Hoang', N'Phi', N'hoangphihung.hn2002@gmail.com', N'123', N'0986169300', NULL, NULL, NULL, NULL, 1, CAST(N'2024-10-29T22:01:52.703' AS DateTime), 3)
GO
INSERT [dbo].[Users] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [address], [date_of_birth], [gender], [profile_picture_url], [is_active], [created_at], [role_id]) VALUES (1005, N'Doi', N'Tan', N'doituan12345@gmail.com', N'10052002tan', N'0344780136', NULL, NULL, NULL, NULL, 1, CAST(N'2024-10-29T22:42:28.397' AS DateTime), 3)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__783254B16A44ECCD]    Script Date: 11/13/2024 9:16:06 PM ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[role_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A1936A6B0CB566FE]    Script Date: 11/13/2024 9:16:06 PM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ__Users__A1936A6B0CB566FE] UNIQUE NONCLUSTERED 
(
	[phone_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__AB6E61644EC97341]    Script Date: 11/13/2024 9:16:06 PM ******/
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
