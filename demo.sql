create database demo
go

use demo
go

create table NCC
(
	Id int identity(1,1) primary key,
	TenNCC nvarchar(max),
	DiaChi nvarchar(max),
	SDT nvarchar(20),
	Email nvarchar(200)
)
go



create table KhachHang
(
	Id nvarchar(128) primary key,
	TenKH nvarchar(max),
	GioiTinh nvarchar(10),
	SDT nvarchar(20),
	DiaChi nvarchar(max),
)
go

create table NhanVien
(
	Id nvarchar(128) primary key,
	TenNV nvarchar(max),
	GioiTinh nvarchar(10),
	NgaySinh DateTime,
	DiaChi nvarchar(max),
	SDT nvarchar(20),
	Email nvarchar(200),
	Luong float,
	Anh nvarchar(200)
)
go




create table LoaiSanPham
(
	Id int identity(1,1) primary key,
	TenLoaiSP nvarchar(max)
)
go

create table Size
(
	Id int identity(1,1) primary key not null,
	IdLSP int not null,
	KichThuoc nvarchar(20),

	foreign key (IdLSP) references LoaiSanPham(Id)
)
go

create table SanPham
(
	Id nvarchar(128) primary key,
	Anh nvarchar(max),
	TenSP nvarchar(max),
	IdLoaiSP int not null,
	GiaBan float default 0,
	GiamGia float default 0,
	GhiChu nvarchar(max)

	foreign key (IdLoaiSP) references LoaiSanPham(Id)
)
go

create table TonKho
(
	IdSP nvarchar(128),
	IdSize int,
	SLTon int

	primary key (IdSP, IdSize),
	foreign key (IdSP) references SanPham(Id),
	foreign key (IdSize) references Size(Id)
)
go

create table LoaiTK
(
	Id int identity(1,1) primary key,
	TenLoaiTK nvarchar(max),
)

create table TaiKhoan
(
	Id int identity(1,1) primary key,
	IdNV nvarchar(128) not null,
	Username nvarchar(100),
	Password nvarchar(max),
	IdLoaiTK int not null

	foreign key (IdNV) references NhanVien(Id),
	foreign key (IdLoaiTK) references LoaiTK(Id)
)
go

create table HDN
(
	Id nvarchar(128) primary key,
	NgayNhap datetime,
	TongTienHDN float
)
go

create table TTHDN
(
	Id nvarchar(128) not null primary key,
	IdSP nvarchar(128) not null,
	IdNCC int not null,
	IdSize int not null,
	IdHDN nvarchar(128) not null,
	GiaNhap float,
	SLNhap int,

	foreign key (IdSP) references SanPham(Id),
	foreign key (IdNCC) references NCC(Id),
	foreign key (IdHDN) references HDN(Id),
	foreign key (IdSize) references Size(Id)
)
go

create table HDB
(
	Id nvarchar(128) primary key,
	IdNV nvarchar(128) not null,
	IdKH nvarchar(128) not null,
	NgayBan datetime,
	TongTien float

	foreign key (IdNV) references NhanVien(Id),
	foreign key (IdKH) references KhachHang(Id)
)
go

create table TTHDB
(
	Id nvarchar(128) primary key,
	IdSP nvarchar(128) not null,
	IdSize int not null,
	IdHDB nvarchar(128) not null,
	SL int,

	foreign key (IdSP) references SanPham(Id),
	foreign key (IdHDB) references HDB(Id),
	foreign key (IdSize) references Size(Id)
)
go


