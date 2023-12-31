USE [master]
GO
/****** Object:  Database [DB_DiDong]    Script Date: 23/10/2023 9:21:21 PM ******/
CREATE DATABASE [DB_DiDong]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_DiDong', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.ADUY\MSSQL\DATA\DB_DiDong.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DB_DiDong_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.ADUY\MSSQL\DATA\DB_DiDong_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DB_DiDong] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_DiDong].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_DiDong] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_DiDong] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_DiDong] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_DiDong] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_DiDong] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_DiDong] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_DiDong] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_DiDong] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_DiDong] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_DiDong] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_DiDong] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_DiDong] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_DiDong] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_DiDong] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_DiDong] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB_DiDong] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_DiDong] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_DiDong] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_DiDong] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_DiDong] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_DiDong] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_DiDong] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_DiDong] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DB_DiDong] SET  MULTI_USER 
GO
ALTER DATABASE [DB_DiDong] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_DiDong] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_DiDong] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_DiDong] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DB_DiDong] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DB_DiDong] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DB_DiDong] SET QUERY_STORE = ON
GO
ALTER DATABASE [DB_DiDong] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DB_DiDong]
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 23/10/2023 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[MaCTHD] [int] IDENTITY(1,1) NOT NULL,
	[MaHD] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
	[SoLuong] [int] NULL,
	[DonGia] [decimal](18, 0) NULL,
	[ThanhTien] [decimal](18, 0) NULL,
 CONSTRAINT [PK_ChiTietHoaDon] PRIMARY KEY CLUSTERED 
(
	[MaCTHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 23/10/2023 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHD] [int] IDENTITY(1,1) NOT NULL,
	[NgayLap] [datetime] NULL,
	[Tong] [decimal](18, 0) NULL,
	[MaKH] [int] NOT NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 23/10/2023 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[TenKH] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[SDT] [varchar](50) NULL,
	[NgaySinh] [datetime] NULL,
	[TaiKhoan] [nvarchar](50) NULL,
	[MatKhau] [nvarchar](50) NULL,
	[Salt] [nchar](6) NULL,
	[TrangThai] [bit] NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kho]    Script Date: 23/10/2023 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kho](
	[MaKho] [int] NOT NULL,
	[TenSP] [nvarchar](50) NULL,
	[SoLuong] [int] NULL,
 CONSTRAINT [PK_Kho] PRIMARY KEY CLUSTERED 
(
	[MaKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loai]    Script Date: 23/10/2023 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loai](
	[MaLoai] [int] NOT NULL,
	[TenLoai] [nvarchar](50) NULL,
 CONSTRAINT [PK_Loai] PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhaSanXuat]    Script Date: 23/10/2023 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaSanXuat](
	[MaNSX] [int] NOT NULL,
	[TenNSX] [nvarchar](50) NULL,
 CONSTRAINT [PK_NhaSanXuat] PRIMARY KEY CLUSTERED 
(
	[MaNSX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 23/10/2023 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [int] NOT NULL,
	[TenSP] [nvarchar](50) NULL,
	[SoLuong] [int] NULL,
	[Gia] [decimal](18, 0) NULL,
	[Hinh] [nvarchar](max) NULL,
	[MaLoai] [int] NULL,
	[MaNSX] [int] NULL,
	[MaKho] [int] NULL,
	[MoTa] [nvarchar](max) NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 23/10/2023 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[MaTaiKhoan] [int] IDENTITY(1,1) NOT NULL,
	[SDT] [varchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[MatKhau] [nvarchar](50) NULL,
	[MaVaiTro] [int] NOT NULL,
	[Salt] [nchar](6) NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[MaTaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaiTro]    Script Date: 23/10/2023 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaiTro](
	[MaVaiTro] [int] NOT NULL,
	[VaiTro] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_VaiTro] PRIMARY KEY CLUSTERED 
(
	[MaVaiTro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ChiTietHoaDon] ON 

INSERT [dbo].[ChiTietHoaDon] ([MaCTHD], [MaHD], [MaSP], [SoLuong], [DonGia], [ThanhTien]) VALUES (1, 5, 12, 1, CAST(8490000 AS Decimal(18, 0)), CAST(8490000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCTHD], [MaHD], [MaSP], [SoLuong], [DonGia], [ThanhTien]) VALUES (2, 6, 12, 1, CAST(8490000 AS Decimal(18, 0)), CAST(8490000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCTHD], [MaHD], [MaSP], [SoLuong], [DonGia], [ThanhTien]) VALUES (3, 7, 8, 1, CAST(14490000 AS Decimal(18, 0)), CAST(14490000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCTHD], [MaHD], [MaSP], [SoLuong], [DonGia], [ThanhTien]) VALUES (4, 8, 3, 1, CAST(27990000 AS Decimal(18, 0)), CAST(27990000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[ChiTietHoaDon] OFF
GO
SET IDENTITY_INSERT [dbo].[HoaDon] ON 

INSERT [dbo].[HoaDon] ([MaHD], [NgayLap], [Tong], [MaKH]) VALUES (5, CAST(N'2023-10-18T21:47:24.687' AS DateTime), CAST(8490000 AS Decimal(18, 0)), 2)
INSERT [dbo].[HoaDon] ([MaHD], [NgayLap], [Tong], [MaKH]) VALUES (6, CAST(N'2023-10-18T21:52:38.240' AS DateTime), CAST(8490000 AS Decimal(18, 0)), 2)
INSERT [dbo].[HoaDon] ([MaHD], [NgayLap], [Tong], [MaKH]) VALUES (7, CAST(N'2023-10-19T10:55:14.000' AS DateTime), CAST(14490000 AS Decimal(18, 0)), 2)
INSERT [dbo].[HoaDon] ([MaHD], [NgayLap], [Tong], [MaKH]) VALUES (8, CAST(N'2023-10-20T12:46:53.877' AS DateTime), CAST(27990000 AS Decimal(18, 0)), 2)
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [SDT], [NgaySinh], [TaiKhoan], [MatKhau], [Salt], [TrangThai], [Email]) VALUES (2, N'anh duy', N'hcm', N'0395372462', NULL, NULL, N'd95e90e3dc518e55a2f632d26f57eb2c', N')nf]~ ', NULL, N'anhduy@gmail.com')
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
GO
INSERT [dbo].[Kho] ([MaKho], [TenSP], [SoLuong]) VALUES (1, N'A', 100)
INSERT [dbo].[Kho] ([MaKho], [TenSP], [SoLuong]) VALUES (2, N'B', 200)
GO
INSERT [dbo].[Loai] ([MaLoai], [TenLoai]) VALUES (1, N'Cao Cấp')
INSERT [dbo].[Loai] ([MaLoai], [TenLoai]) VALUES (2, N'Phổ Thông')
GO
INSERT [dbo].[NhaSanXuat] ([MaNSX], [TenNSX]) VALUES (1, N'IPHONE')
INSERT [dbo].[NhaSanXuat] ([MaNSX], [TenNSX]) VALUES (2, N'SAMSUNG')
INSERT [dbo].[NhaSanXuat] ([MaNSX], [TenNSX]) VALUES (3, N'OPPO')
GO
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (1, N'Điện Thoại IPhone 15 Pro Max 256GB', 10, CAST(33990000 AS Decimal(18, 0)), N'dien-thoai-iphone-15-pro-max-256gb.jpg', 1, 1, 1, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (2, N'Điện Thoại IPhone 15 Pro 256GB', 10, CAST(30990000 AS Decimal(18, 0)), N'dien-thoai-iphone-15-pro-256gb.jpg', 1, 1, 1, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (3, N'Điện Thoại IPhone 15 Plus 256GB', 10, CAST(27990000 AS Decimal(18, 0)), N'dien-thoai-iphone-15-plus-256gb.jpg', 1, 1, 1, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (4, N'Điện Thoại IPhone 15 256GB', 10, CAST(24990000 AS Decimal(18, 0)), N'dien-thoai-iphone-15-256gb.jpg', 1, 1, 1, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (5, N'Điện Thoại Samsung Galaxy Z Fold5 5G 256GB', 10, CAST(36990000 AS Decimal(18, 0)), N'dien-thoai-samsung-galaxy-z-fold5-5g-256gb.jpg', 1, 2, 1, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (6, N'Điện Thoại Samsung Galaxy S23 Ultra 5G 256GB', 10, CAST(23990000 AS Decimal(18, 0)), N'dien-thoai-samsung-galaxy-s23-ultra-5g-256gb.jpg', 1, 2, 1, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (7, N'Điện Thoại Samsung Galaxy Z Flip4 5G 128GB', 10, CAST(12990000 AS Decimal(18, 0)), N'dien-thoai-samsung-galaxy-z-flip4-5g-128gb.jpg', 2, 2, 2, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (8, N'Điện Thoại Samsung Galaxy S23 FE 5G', 10, CAST(14490000 AS Decimal(18, 0)), N'dien-thoai-samsung-galaxy-s23-fe-5g.jpg', 2, 2, 2, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (9, N'Điện Thoại OPPO Reno10 Pro 5G', 10, CAST(13990000 AS Decimal(18, 0)), N'dien-thoai-oppo-reno10-pro-5g.jpg', 2, 3, 2, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (10, N'Điện Thoại OPPO Reno8 Pro 5G', 10, CAST(13990000 AS Decimal(18, 0)), N'dien-thoai-oppo-reno8-pro-5g.jpg', 2, 3, 2, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (11, N'Điện Thoại OPPO Reno10 5G 128GB', 10, CAST(9490000 AS Decimal(18, 0)), N'dien-thoai-oppo-reno10-5g-128gb.jpg', 2, 3, 2, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [SoLuong], [Gia], [Hinh], [MaLoai], [MaNSX], [MaKho], [MoTa]) VALUES (12, N'Điện Thoại OPPO Reno8 T 5G 256GB ', 10, CAST(8490000 AS Decimal(18, 0)), N'dien-thoai-oppo-reno8-t-5g-256gb.jpg', 2, 3, 2, N'Mới, đầy đủ phụ kiện từ nhà sản xuất')
GO
SET IDENTITY_INSERT [dbo].[TaiKhoan] ON 

INSERT [dbo].[TaiKhoan] ([MaTaiKhoan], [SDT], [Email], [MatKhau], [MaVaiTro], [Salt]) VALUES (3, N'0395372462', N'admin@gmail.com', N'123123', 1, NULL)
SET IDENTITY_INSERT [dbo].[TaiKhoan] OFF
GO
INSERT [dbo].[VaiTro] ([MaVaiTro], [VaiTro], [Description]) VALUES (1, N'Admin', N'Quản lý hệ thống')
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_HoaDon] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HoaDon] ([MaHD])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_HoaDon]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_SanPham]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_Kho] FOREIGN KEY([MaKho])
REFERENCES [dbo].[Kho] ([MaKho])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_Kho]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_Loai] FOREIGN KEY([MaLoai])
REFERENCES [dbo].[Loai] ([MaLoai])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_Loai]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_NhaSanXuat] FOREIGN KEY([MaNSX])
REFERENCES [dbo].[NhaSanXuat] ([MaNSX])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_NhaSanXuat]
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD  CONSTRAINT [FK_TaiKhoan_VaiTro] FOREIGN KEY([MaVaiTro])
REFERENCES [dbo].[VaiTro] ([MaVaiTro])
GO
ALTER TABLE [dbo].[TaiKhoan] CHECK CONSTRAINT [FK_TaiKhoan_VaiTro]
GO
USE [master]
GO
ALTER DATABASE [DB_DiDong] SET  READ_WRITE 
GO
