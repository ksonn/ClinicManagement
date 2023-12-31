USE [Clinic11]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 11/13/2023 6:56:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[username] [varchar](150) NOT NULL,
	[password] [varchar](150) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonThuoc]    Script Date: 11/13/2023 6:56:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonThuoc](
	[DonThuocID] [int] NOT NULL,
	[pid] [int] NOT NULL,
	[ThuocID] [int] NOT NULL,
	[soluong] [int] NULL,
	[chandoan] [nvarchar](500) NULL,
 CONSTRAINT [PK_DonThuoc] PRIMARY KEY CLUSTERED 
(
	[DonThuocID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiThuoc]    Script Date: 11/13/2023 6:56:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiThuoc](
	[LoaiID] [int] NOT NULL,
	[TenLoai] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_LoaiThuoc] PRIMARY KEY CLUSTERED 
(
	[LoaiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 11/13/2023 6:56:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[pid] [int] NOT NULL,
	[pname] [nvarchar](50) NOT NULL,
	[gender] [bit] NOT NULL,
	[dob] [date] NOT NULL,
	[job] [nvarchar](50) NULL,
	[address] [nvarchar](50) NOT NULL,
	[phone] [varchar](50) NOT NULL,
	[cmnd] [nvarchar](50) NULL,
	[dayadd] [date] NOT NULL,
	[reason] [nvarchar](3000) NOT NULL,
	[photo] [varchar](200) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[pid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_Service]    Script Date: 11/13/2023 6:56:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_Service](
	[pid] [int] NOT NULL,
	[sid] [int] NOT NULL,
 CONSTRAINT [PK_Patient_Service] PRIMARY KEY CLUSTERED 
(
	[pid] ASC,
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 11/13/2023 6:56:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[sid] [int] NOT NULL,
	[sname] [nvarchar](250) NULL,
	[price] [float] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Treatment]    Script Date: 11/13/2023 6:56:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Treatment](
	[tid] [int] NOT NULL,
	[diagnose] [nvarchar](2500) NOT NULL,
	[solution] [nvarchar](2500) NOT NULL,
	[username] [varchar](150) NULL,
 CONSTRAINT [PK_Treatment] PRIMARY KEY CLUSTERED 
(
	[tid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TuThuoc]    Script Date: 11/13/2023 6:56:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TuThuoc](
	[ThuocID] [int] NOT NULL,
	[TenThuoc] [nvarchar](100) NOT NULL,
	[HangSanXuat] [nvarchar](100) NOT NULL,
	[NgaySX] [date] NOT NULL,
	[HanSX] [date] NOT NULL,
	[LoaiID] [int] NULL,
	[soluong] [int] NULL,
	[giatien] [money] NULL,
 CONSTRAINT [PK_TuThuoc] PRIMARY KEY CLUSTERED 
(
	[ThuocID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([username], [password], [FullName]) VALUES (N'son', N'1234', N'Khắc Sơn')
INSERT [dbo].[Account] ([username], [password], [FullName]) VALUES (N'xdang', N'1234680', N'Xuân Đăng')
GO
INSERT [dbo].[LoaiThuoc] ([LoaiID], [TenLoai]) VALUES (1, N'Test 1')
INSERT [dbo].[LoaiThuoc] ([LoaiID], [TenLoai]) VALUES (2, N'Test 2')
INSERT [dbo].[LoaiThuoc] ([LoaiID], [TenLoai]) VALUES (3, N'Đau Bụng')
GO
INSERT [dbo].[Patient] ([pid], [pname], [gender], [dob], [job], [address], [phone], [cmnd], [dayadd], [reason], [photo]) VALUES (1, N'hihhihi', 1, CAST(N'2000-01-01' AS Date), N'0', N'120 hsjhjhjh', N'09937874534', N'0', CAST(N'2023-09-01' AS Date), N'hihihihihihihi', NULL)
GO
INSERT [dbo].[Patient_Service] ([pid], [sid]) VALUES (1, 3)
INSERT [dbo].[Patient_Service] ([pid], [sid]) VALUES (1, 5)
GO
INSERT [dbo].[Service] ([sid], [sname], [price]) VALUES (2, N'Siêu Âm Ổ Bụng', 100000)
INSERT [dbo].[Service] ([sid], [sname], [price]) VALUES (3, N'Xét Nghiệm HP', 150000)
INSERT [dbo].[Service] ([sid], [sname], [price]) VALUES (4, N'Siêu Âm Tuyến Giáp', 100000)
INSERT [dbo].[Service] ([sid], [sname], [price]) VALUES (5, N'Siêu Âm Ngực', 100000)
GO
INSERT [dbo].[Treatment] ([tid], [diagnose], [solution], [username]) VALUES (1, N'ihihihihihih', N'ihihihihihih', NULL)
GO
INSERT [dbo].[TuThuoc] ([ThuocID], [TenThuoc], [HangSanXuat], [NgaySX], [HanSX], [LoaiID], [soluong], [giatien]) VALUES (1, N'Paracetamol', N'Paramo', CAST(N'2023-05-30' AS Date), CAST(N'2023-09-02' AS Date), 1, NULL, NULL)
INSERT [dbo].[TuThuoc] ([ThuocID], [TenThuoc], [HangSanXuat], [NgaySX], [HanSX], [LoaiID], [soluong], [giatien]) VALUES (2, N'Test 2', N'Test 2', CAST(N'2023-05-03' AS Date), CAST(N'2023-10-07' AS Date), 2, NULL, NULL)
INSERT [dbo].[TuThuoc] ([ThuocID], [TenThuoc], [HangSanXuat], [NgaySX], [HanSX], [LoaiID], [soluong], [giatien]) VALUES (3, N'Test Edit', N'Tester Bi beo', CAST(N'2023-06-25' AS Date), CAST(N'2023-08-05' AS Date), 1, NULL, NULL)
INSERT [dbo].[TuThuoc] ([ThuocID], [TenThuoc], [HangSanXuat], [NgaySX], [HanSX], [LoaiID], [soluong], [giatien]) VALUES (4, N'Test 4', N'Test 4', CAST(N'2023-05-03' AS Date), CAST(N'2023-10-07' AS Date), 2, NULL, NULL)
INSERT [dbo].[TuThuoc] ([ThuocID], [TenThuoc], [HangSanXuat], [NgaySX], [HanSX], [LoaiID], [soluong], [giatien]) VALUES (5, N'Thuốc Đau Bụng', N'Paramol', CAST(N'2023-06-29' AS Date), CAST(N'2023-07-28' AS Date), 3, NULL, NULL)
GO
ALTER TABLE [dbo].[DonThuoc]  WITH CHECK ADD  CONSTRAINT [FK_DonThuoc_Patient] FOREIGN KEY([pid])
REFERENCES [dbo].[Patient] ([pid])
GO
ALTER TABLE [dbo].[DonThuoc] CHECK CONSTRAINT [FK_DonThuoc_Patient]
GO
ALTER TABLE [dbo].[DonThuoc]  WITH CHECK ADD  CONSTRAINT [FK_DonThuoc_TuThuoc] FOREIGN KEY([ThuocID])
REFERENCES [dbo].[TuThuoc] ([ThuocID])
GO
ALTER TABLE [dbo].[DonThuoc] CHECK CONSTRAINT [FK_DonThuoc_TuThuoc]
GO
ALTER TABLE [dbo].[Patient_Service]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Service_Patient] FOREIGN KEY([pid])
REFERENCES [dbo].[Patient] ([pid])
GO
ALTER TABLE [dbo].[Patient_Service] CHECK CONSTRAINT [FK_Patient_Service_Patient]
GO
ALTER TABLE [dbo].[Patient_Service]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Service_Service] FOREIGN KEY([sid])
REFERENCES [dbo].[Service] ([sid])
GO
ALTER TABLE [dbo].[Patient_Service] CHECK CONSTRAINT [FK_Patient_Service_Service]
GO
ALTER TABLE [dbo].[Treatment]  WITH CHECK ADD  CONSTRAINT [FK_Treatment_Account] FOREIGN KEY([username])
REFERENCES [dbo].[Account] ([username])
GO
ALTER TABLE [dbo].[Treatment] CHECK CONSTRAINT [FK_Treatment_Account]
GO
ALTER TABLE [dbo].[Treatment]  WITH CHECK ADD  CONSTRAINT [FK_Treatment_Patient] FOREIGN KEY([tid])
REFERENCES [dbo].[Patient] ([pid])
GO
ALTER TABLE [dbo].[Treatment] CHECK CONSTRAINT [FK_Treatment_Patient]
GO
ALTER TABLE [dbo].[TuThuoc]  WITH CHECK ADD  CONSTRAINT [FK_TuThuoc_LoaiThuoc] FOREIGN KEY([LoaiID])
REFERENCES [dbo].[LoaiThuoc] ([LoaiID])
GO
ALTER TABLE [dbo].[TuThuoc] CHECK CONSTRAINT [FK_TuThuoc_LoaiThuoc]
GO
