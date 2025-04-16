CREATE DATABASE NHOM7_LTUD
USE NHOM7_LTUD;
DROP DATABASE NHOM7_LTUD
--Bảng user--
CREATE TABLE Admin (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100),
    Role NVARCHAR(10) NOT NULL default N'admin');


CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100),
    Role NVARCHAR(10) NOT NULL default N'user');

ALTER TABLE Users
ADD MaSV NVARCHAR(20) NULL;

ALTER TABLE Users
ADD CONSTRAINT FK_Users_SinhVien
FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV);
drop table Users

--Bảng sinh viên--
CREATE TABLE SinhVien (
    MaSV NVARCHAR(20) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Lop NVARCHAR(10),
    Khoa NVARCHAR(10),
	foreign key (Lop) references Lop(MALOP),
	foreign key (Khoa) references KhoaVien(MAKHOA),
);
drop table SinhVien
--Bảng lớp--
create table Lop(
MALOP nvarchar(10) not null primary key,
TENLOP nvarchar(30),
MAKHOA nvarchar(10) not null,
CVHT char(30), GHICHU char(100),
foreign key (MAKHOA) references KhoaVien(MAKHOA));
drop table Lop

--Bảng khoa/viện--
create table KhoaVien(
MAKHOA nvarchar(10) not null primary key,
TENKHOA nvarchar(50) not null,
DIACHI char(50), SODT char(12));

drop table KhoaVien

--Bảng tín chỉ--
create table TinChi(
MATIN nvarchar(10) not null primary key,
MALOP nvarchar(10) not null,
SOTIEN1TIN int,
foreign key (MALOP) references Lop(MALOP)
)

drop table TinChi
--Bảng kì học--
CREATE TABLE KiHoc (
    KiHocID NVARCHAR(10) PRIMARY KEY,
    TenKiHoc NVARCHAR(50) NOT NULL,  -- Ví dụ: "HK1 2025", "HK2 2025"
    NamHoc INT NOT NULL  ,            -- Ví dụ: 2025
	TGBatDau Date,
	TGKetThuc Date
);
Drop Table KiHoc 
--Bảng học phí--
CREATE TABLE HocPhi (
    HocPhiID nvarchar(20) PRIMARY KEY,
    MaSV NVARCHAR(20) NOT NULL,
    KiHocID NVARCHAR(10) NOT NULL,
    SoTien DECIMAL(18, 2) NOT NULL,
    HanDong DATE,
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Chưa đóng',
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (KiHocID) REFERENCES KiHoc(KiHocID)
);
DROP TABLE HocPhi;
--Bảng thanh toán--
CREATE TABLE ThanhToan (
    ThanhToanID INT IDENTITY(1,1) PRIMARY KEY,
    HocPhiID NVARCHAR(20) NOT NULL,
    NgayThanhToan DATE DEFAULT GETDATE(),
    SoTienDaDong DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (HocPhiID) REFERENCES HocPhi(HocPhiID)
);
--Bảng hóa đơn--
CREATE TABLE HoaDon (
    HoaDonID INT IDENTITY(1,1) PRIMARY KEY,
    ThanhToanID INT NOT NULL,
    NgayLap DATE DEFAULT GETDATE(),
    SoTien DECIMAL(18, 2) NOT NULL,
	NganHang NVARCHAR(100)
    FOREIGN KEY (ThanhToanID) REFERENCES ThanhToan(ThanhToanID)
);
--Bảng thông báo nợ--
CREATE TABLE ThongBaoNo (
    TBID INT IDENTITY(1,1) PRIMARY KEY,
    MaSV NVARCHAR(20) NOT NULL,
    KiHocID NVARCHAR(10) NOT NULL,
    SoTienNo DECIMAL(18, 2) NOT NULL,
    NgayThongBao DATE DEFAULT GETDATE(),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (KiHocID) REFERENCES KiHoc(KiHocID)
);

-- Thủ tục thêm, sửa, xóa cho các bảng --
--Bảng users
-- Thêm
alter PROCEDURE sp_ThemUsers
    @Username NVARCHAR(50),
    @Password NVARCHAR(100),
	@FullName NVARCHAR(100),
	@MaSV NVARCHAR(20)
AS
BEGIN
    INSERT INTO Users(Username, Password, FullName, Role, MaSV)
    VALUES (@Username, @Password, @FullName, 'user', @MaSV);
END;
GO
select * from Users

-- Sửa
CREATE PROCEDURE sp_SuaUsers
    @UserID INT,
    @Username NVARCHAR(50),
    @Password NVARCHAR(100),
    @FullName NVARCHAR(100)
AS
BEGIN
    UPDATE Users
    SET Username = @Username,
        Password = @Password,
        FullName = @FullName,
        Role = @Role
    WHERE UserID = @UserID;
END;
GO

select* from Users

-- Xóa
alter PROCEDURE sp_XoaUsers
    @UserID int,
	@MaSV NVARCHAR(20)
AS
BEGIN
    DELETE FROM Users WHERE UserID = @UserID and MaSV = @MaSV;
END;
GO
--Ví dụ cách thêm, sửa, xóa
/*-- Gọi thủ tục thêm
EXEC sp_ThemUsers 'username', 'password', 'Tên đầy đủ', 'admin';

-- Gọi thủ tục sửa
EXEC sp_SuaUsers 1, 'new_user', 'new_pass', 'New Name', 'user';

-- Gọi thủ tục xóa
EXEC sp_XoaUsers 1;*/

--Bảng sinh viên
CREATE PROCEDURE sp_ThemSinhVien
    @MaSV NVARCHAR(20),
    @FullName NVARCHAR(100),
    @Lop NVARCHAR(20),
    @Khoa NVARCHAR(50)
AS
BEGIN
    INSERT INTO SinhVien(MaSV, FullName, Lop, Khoa)
    VALUES (@MaSV, @FullName, @Lop, @Khoa);
END;
GO

CREATE PROCEDURE sp_SuaSinhVien
    @MaSV NVARCHAR(20),
    @FullName NVARCHAR(100),
    @Lop NVARCHAR(20),
    @Khoa NVARCHAR(50)
AS
BEGIN
    UPDATE SinhVien
    SET FullName = @FullName,
        Lop = @Lop,
        Khoa = @Khoa
    WHERE MaSV = @MaSV;
END;
GO

CREATE PROCEDURE sp_XoaSinhVien
    @MaSV NVARCHAR(20)
AS
BEGIN
    DELETE FROM SinhVien WHERE MaSV = @MaSV;
END;
GO

--Bảng kì học
CREATE PROCEDURE sp_ThemKiHoc
	@KiHocID NVARCHAR(10),
    @TenKiHoc NVARCHAR(50),
    @NamHoc INT,
	@TGBatDau Date,
	@TGKetThuc Date
AS
BEGIN
    INSERT INTO KiHoc (KiHocID,TenKiHoc, NamHoc,TGBatDau,TGKetThuc)
    VALUES (@KiHocID,@TenKiHoc, @NamHoc,@TGBatDau,@TGKetThuc);
END;
GO

CREATE PROCEDURE sp_SuaKiHoc
	@KiHocID NVARCHAR(10),
	@TGBatDau Date,
	@TGKetThuc Date
AS
BEGIN
    UPDATE KiHoc
    SET TGBatDau = @TGBatDau,
		TGKetThuc = @TGKetThuc
    WHERE KiHocID = @KiHocID;
END;
GO

CREATE PROCEDURE sp_XoaKiHoc
    @KiHocID NVARCHAR(10)
AS
BEGIN
    DELETE FROM KiHoc WHERE KiHocID = @KiHocID;
END;
GO

--Bảng học phí
CREATE PROCEDURE sp_ThemHocPhi
	@HocPhiID nvarchar(20),
    @MaSV NVARCHAR(20),
    @KiHocID NVARCHAR(10),
    @SoTien DECIMAL(18, 2),
    @HanDong DATE
AS
BEGIN
    INSERT INTO HocPhi(HocPhiID,MaSV, KiHocID, SoTien, HanDong)
    VALUES (@HocPhiID,@MaSV, @KiHocID, @SoTien, @HanDong);
END;
GO

CREATE PROCEDURE sp_SuaHocPhi
	@HocPhiID nvarchar(20),
    @SoTien DECIMAL(18, 2),
    @HanDong DATE
AS
BEGIN
    UPDATE HocPhi
    SET SoTien = @SoTien,
        HanDong = @HanDong
    WHERE HocPhiID = @HocPhiID;
END;
GO

CREATE PROCEDURE sp_XoaHocPhi
    @HocPhiID NVARCHAR(20)
AS
BEGIN
    DELETE FROM HocPhi WHERE HocPhiID = @HocPhiID;
END;
GO

CREATE PROC sp_XoaHocPhiTheoMSV
	@MaSV NVARCHAR(10)
as
begin
	DELETE FROM HocPhi WHERE MaSV = @MaSV;
end
go

--Bảng thanh toán 
CREATE PROCEDURE sp_ThemThanhToan
    @HocPhiID NVARCHAR(20),
    @NgayThanhToan DATE,
    @SoTienDaDong DECIMAL(18,2)
AS
BEGIN
    INSERT INTO ThanhToan(HocPhiID, NgayThanhToan, SoTienDaDong)
    VALUES (@HocPhiID, @NgayThanhToan, @SoTienDaDong);
END;
GO

CREATE PROCEDURE sp_SuaThanhToan
    @ThanhToanID INT,
    @HocPhiID nvarchar(20),
    @NgayThanhToan DATE,
    @SoTienDaDong DECIMAL(18,2)
AS
BEGIN
    UPDATE ThanhToan
    SET HocPhiID = @HocPhiID,
        NgayThanhToan = @NgayThanhToan,
        SoTienDaDong = @SoTienDaDong
    WHERE ThanhToanID = @ThanhToanID;
END;
GO

CREATE PROCEDURE sp_XoaThanhToan
    @ThanhToanID INT
AS
BEGIN
    DELETE FROM ThanhToan WHERE ThanhToanID = @ThanhToanID;
END;
GO


--Bảng hóa đơn 
CREATE PROCEDURE sp_ThemHoaDon
    @ThanhToanID INT,
    @NgayLap DATE,
    @SoTien DECIMAL(18,2)
AS
BEGIN
    INSERT INTO HoaDon(ThanhToanID, NgayLap, SoTien)
    VALUES (@ThanhToanID, @NgayLap, @SoTien);
END;
GO

CREATE PROCEDURE sp_SuaHoaDon
    @HoaDonID INT,
    @ThanhToanID INT,
    @NgayLap DATE,
    @SoTien DECIMAL(18,2)
AS
BEGIN
    UPDATE HoaDon
    SET ThanhToanID = @ThanhToanID,
        NgayLap = @NgayLap,
        SoTien = @SoTien
    WHERE HoaDonID = @HoaDonID;
END;
GO

CREATE PROCEDURE sp_XoaHoaDon
    @HoaDonID INT
AS
BEGIN
    DELETE FROM HoaDon WHERE HoaDonID = @HoaDonID;
END;
GO

--Bảng thông báo nợ
CREATE PROCEDURE sp_ThemThongBaoNo
    @MaSV NVARCHAR(20),
    @KiHocID INT,
    @SoTienNo DECIMAL(18,2),
    @NgayThongBao DATE
AS
BEGIN
    INSERT INTO ThongBaoNo(MaSV, KiHocID, SoTienNo, NgayThongBao)
    VALUES (@MaSV, @KiHocID, @SoTienNo, @NgayThongBao);
END;
GO

CREATE PROCEDURE sp_SuaThongBaoNo
    @TBID INT,
    @MaSV NVARCHAR(20),
    @KiHocID INT,
    @SoTienNo DECIMAL(18,2),
    @NgayThongBao DATE
AS
BEGIN
    UPDATE ThongBaoNo
    SET MaSV = @MaSV,
        KiHocID = @KiHocID,
        SoTienNo = @SoTienNo,
        NgayThongBao = @NgayThongBao
    WHERE TBID = @TBID;
END;
GO

CREATE PROCEDURE sp_XoaThongBaoNo
    @TBID INT
AS
BEGIN
    DELETE FROM ThongBaoNo WHERE TBID = @TBID;
END;
GO


CREATE OR ALTER PROCEDURE sp_LayHocPhiTheoMaSV
    @MaSV NVARCHAR(20)
AS
BEGIN
    SELECT 
        kh.TenKiHoc,
        kh.NamHoc,
        hp.SoTien AS TongTien,
        ISNULL(hp.SoTien - (
            SELECT SUM(SoTienDaDong)
            FROM ThanhToan
            WHERE HocPhiID = hp.HocPhiID
        ), hp.SoTien) AS SoTienConNo,
        hp.HanDong,
        hp.TrangThai
    FROM HocPhi hp
    INNER JOIN KiHoc kh ON hp.KiHocID = kh.KiHocID
    WHERE hp.MaSV = @MaSV
    ORDER BY kh.NamHoc DESC, kh.TenKiHoc;
END;

exec sp_LayHocPhiTheoMaSV '11220011'
CREATE PROCEDURE sp_LocHocPhi
    @MaSV NVARCHAR(20),
    @TenKiHoc NVARCHAR(50) = NULL,
    @TrangThai NVARCHAR(20) = NULL
AS
BEGIN
    SELECT 
        kh.TenKiHoc,
        kh.NamHoc,
        hp.SoTien,
        hp.HanDong,
        hp.TrangThai
    FROM HocPhi hp
    INNER JOIN KiHoc kh ON hp.KiHocID = kh.KiHocID
    WHERE hp.MaSV = @MaSV
        AND (@TenKiHoc IS NULL OR kh.TenKiHoc = @TenKiHoc)
        AND (@TrangThai IS NULL OR hp.TrangThai = @TrangThai)
    ORDER BY kh.NamHoc DESC, kh.TenKiHoc;
END;

EXEC sp_LocHocPhi 
    @MaSV = '11220001', 
    @TenKiHoc = NULL, 
    @TrangThai = NULL;







INSERT INTO Admin (Username, Password, FullName)
VALUES
('admin1', 'pass1', 'Nguyen Van Admin'),
('admin2', 'pass2', 'Nguyen Thi Admin')

select * from Admin

INSERT INTO Users (Username, Password, FullName, Role, MaSV)
VALUES
('user1', 'pass1', 'Nguyen Van A', 'user','11220001'),
('user2', 'pass2', 'Le Thi B', 'user','11220002'),
('user3', 'pass3', 'Tran Van C', 'user','11220003'),
('user4', 'pass4', 'Pham Thi D', 'user','11220004'),
('user5', 'pass5', 'Hoang Van E', 'user','11220005'),
('user6', 'pass6', 'Do Thi F', 'user','11220006'),
('user7', 'pass7', 'Ngo Van G', 'user','11220007'),
('user8', 'pass8', 'Dang Thi G', 'user','11220008'),
('user9', 'pass9', 'Bui Van I', 'user','11220009'),
('user10', 'pass10', 'Mai Thi K', 'user','11220010');
select * from Users
INSERT INTO SinhVien (MaSV, FullName, Lop, Khoa)
VALUES
('11220001', 'Nguyen Van A', 'CNTT1', 'CNTT'),
('11220002', 'Le Thi B', 'CNTT1', 'CNTT'),
('11220003', 'Tran Van C', 'CNTT2', 'CNTT'),
('11220004', 'Pham Thi D', 'CNTT2', 'CNTT'),
('11220005', 'Hoang Van E', 'KTPM1', 'KTPM'),
('11220006', 'Do Thi F', 'KTPM1', 'KTPM'),
('11220007', 'Ngo Van G', 'HTTT1', 'HTTT'),
('11220008', 'Dang Thi H', 'HTTT1', 'HTTT'),
('11220009', 'Bui Van I', 'HTTT2', 'HTTT'),
('11220010', 'Mai Thi K', 'HTTT2', 'HTTT');

INSERT INTO Lop(MALOP, TENLOP, MAKHOA, CVHT)
VALUES
('CNTT1',N'Công Nghệ Thông Tin 1','CNTT',N'Nguyễn Văn A'),
('CNTT2',N'Công Nghệ Thông Tin 2','CNTT',N'Nguyễn Văn B'),
('KTPM1',N'Kỹ Thuật Phần Mềm 1','KTPM',N'Nguyễn Văn C'),
('HTTT1',N'Hệ Thống Thông Tin 1','HTTT',N'Nguyễn Văn D'),
('HTTT2',N'Hệ Thống Thông Tin 2','HTTT',N'Nguyễn Văn E');

INSERT INTO KhoaVien(MAKHOA, TENKHOA, DIACHI, SODT)
VALUES
('CNTT',N'Khoa Công Nghệ Thông Tin',N'Tòa A2','0123456789'),
('KTPM',N'Viện Kỹ Thuật',N'Tòa A2','0123456789'),
('HTTT',N'Khoa Kinh Tế',N'Tòa A2','0123456789');
select* from Users, Admin

UPDATE Users SET MaSV = '11220001' WHERE Username = 'user1';
UPDATE Users SET MaSV = '11220002' WHERE Username = 'user2';
UPDATE Users SET MaSV = '11220003' WHERE Username = 'user3';
UPDATE Users SET MaSV = '11220004' WHERE Username = 'user4';
UPDATE Users SET MaSV = '11220005' WHERE Username = 'user5';
UPDATE Users SET MaSV = '11220006' WHERE Username = 'user6';
UPDATE Users SET MaSV = '11220007' WHERE Username = 'user7';
UPDATE Users SET MaSV = '11220008' WHERE Username = 'user8';
UPDATE Users SET MaSV = '11220009' WHERE Username = 'user9';
UPDATE Users SET MaSV = '11220010' WHERE Username = 'user10';

INSERT INTO KiHoc (KiHocID, TenKiHoc, NamHoc, TGBatDau, TGKetThuc)
VALUES
('1-2024','HK1 2024', 2024, '2023-09-06', '2024-02-06'),
('2-2024','HK2 2024', 2024, '2024-03-01', '2024-06-20'),
('1-2025','HK1 2025', 2025, '2024-09-06', '2025-02-06'),
('2-2025','HK2 2025', 2025, '2025-03-01', '2025-06-20'),
('1-2023','HK1 2023', 2023, '2022-09-06', '2022-02-06'),
('2-2023','HK2 2023', 2023, '2023-03-01', '2023-06-20'),
('1-2022','HK1 2022', 2022, '2021-09-06', '2022-02-06'),
('2-2022','HK2 2022', 2022, '2022-03-01', '2022-06-20'),
('1-2021','HK1 2021', 2021, '2020-09-06', '2021-02-06'),
('2-2021','HK2 2021', 2021, '2021-03-01', '2021-06-20');

Select * from KiHoc

INSERT INTO TinChi 
values
('CNTT1-450', 'CNTT1',450000),
('CNTT2-450', 'CNTT2',450000),
('KTPM1-415', 'KTPM1',4150000),
('HTTT1-500', 'HTTT1',500000),
('HTTT2-500', 'HTTT2',500000)

drop table TinChi

select SOTIEN1TIN from TinChi where MALOP = 'CNTT1'
delete from HocPhi
INSERT INTO HocPhi (HocPhiID, MaSV, KiHocID, SoTien, HanDong)
VALUES
('11220001-1-2022','11220001', '1-2022', 5000000, '2023-01-31'),
('11220002-1-2022','11220002', '1-2022', 5200000, '2023-01-31'),
('11220003-1-2022','11220003', '1-2022', 4800000, '2023-01-31'),
('11220004-1-2022','11220004', '1-2022', 5500000, '2023-01-31'),
('11220005-1-2022','11220005', '1-2022', 6000000, '2023-01-31'),
('11220006-1-2022','11220006', '1-2022', 6100000, '2023-01-31'),
('11220007-1-2022','11220007', '1-2022', 6200000, '2023-01-31'),
('11220008-1-2022','11220008', '1-2022', 6300000, '2023-01-31'),
('11220009-1-2022','11220009', '1-2022', 5400000, '2023-01-31'),
('11220010-1-2022','11220010', '1-2022', 5600000, '2023-01-31'),

-- 2-2023 = KiHocID 6
('11220001-2-2022','11220001', '2-2022', 5000000, '2022-05-30'),
('11220002-2-2022','11220002', '2-2022', 5100000, '2022-05-30'),
('11220003-2-2022','11220003', '2-2022', 5200000, '2022-05-30'),
('11220004-2-2022','11220004', '2-2022', 5300000, '2022-05-30'),
('11220005-2-2022','11220005', '2-2022', 5400000, '2022-05-30'),
('11220006-2-2022','11220006', '2-2022', 5500000, '2022-05-30'),
('11220007-2-2022','11220007', '2-2022', 5600000, '2022-05-30'),
('11220008-2-2022','11220008', '2-2022', 5700000, '2022-05-30'),
('11220009-2-2022','11220009', '2-2022', 5800000, '2022-05-30'),
('11220010-2-2022','11220010', '2-2022', 5900000, '2022-05-30'),

-- 1-2022 = KiHocID 7
('11220001-1-2023','11220001', '1-2023', 5050000, '2023-01-31'),
('11220002-1-2023','11220002', '1-2023', 5150000, '2023-01-31'),
('11220003-1-2023','11220003', '1-2023', 5250000, '2023-01-31'),
('11220004-1-2023','11220004', '1-2023', 5350000, '2023-01-31'),
('11220005-1-2023','11220005', '1-2023', 5450000, '2023-01-31'),
('11220006-1-2023','11220006', '1-2023', 5550000, '2023-01-31'),
('11220007-1-2023','11220007', '1-2023', 5650000, '2023-01-31'),
('11220008-1-2023','11220008', '1-2023', 5750000, '2023-01-31'),
('11220009-1-2023','11220009', '1-2023', 5850000, '2023-01-31'),
('11220010-1-2023','11220010', '1-2023', 5950000, '2023-01-31'),

-- 2-2022 = KiHocID 8
('11220001-2-2023','11220001', '2-2023', 5100000, '2023-05-30'),
('11220002-2-2023','11220002', '2-2023', 5200000, '2023-05-30'),
('11220003-2-2023','11220003', '2-2023', 5300000, '2023-05-30'),
('11220004-2-2023','11220004', '2-2023', 5400000, '2023-05-30'),
('11220005-2-2023','11220005', '2-2023', 5500000, '2023-05-30'),
('11220006-2-2023','11220006', '2-2023', 5600000, '2023-05-30'),
('11220007-2-2023','11220007', '2-2023', 5700000, '2023-05-30'),
('11220008-2-2023','11220008', '2-2023', 5800000, '2023-05-30'),
('11220009-2-2023','11220009', '2-2023', 5900000, '2023-05-30'),
('11220010-2-2023','11220010', '2-2023', 6000000, '2023-05-30'),

-- 1-2021 = KiHocID 9
('11220001-1-2024','11220001', '1-2024', 5150000, '2024-01-31'),
('11220002-1-2024','11220002', '1-2024', 5250000, '2024-01-31'),
('11220003-1-2024','11220003', '1-2024', 5350000, '2024-01-31'),
('11220004-1-2024','11220004', '1-2024', 5450000, '2024-01-31'),
('11220005-1-2024','11220005', '1-2024', 5550000, '2024-01-31'),
('11220006-1-2024','11220006', '1-2024', 5650000, '2024-01-31'),
('11220007-1-2024','11220007', '1-2024', 5750000, '2024-01-31'),
('11220008-1-2024','11220008', '1-2024', 5850000, '2024-01-31'),
('11220009-1-2024','11220009', '1-2024', 5950000, '2024-01-31'),
('11220010-1-2024','11220010', '1-2024', 6050000, '2024-01-31'),

-- 2-2021 = KiHocID 10
('11220001-2-2024','11220001', '2-2024', 5200000, '2024-05-30'),
('11220002-2-2024','11220002', '2-2024', 5300000, '2024-05-30'),
('11220003-2-2024','11220003', '2-2024', 5400000, '2024-05-30'),
('11220004-2-2024','11220004', '2-2024', 5500000, '2024-05-30'),
('11220005-2-2024','11220005', '2-2024', 5600000, '2024-05-30'),
('11220006-2-2024','11220006', '2-2024', 5700000, '2024-05-30'),
('11220007-2-2024','11220007', '2-2024', 5800000, '2024-05-30'),
('11220008-2-2024','11220008', '2-2024', 5900000, '2024-05-30'),
('11220009-2-2024','11220009', '2-2024', 6000000, '2024-05-30'),
('11220010-2-2024','11220010', '2-2024', 6100000, '2024-05-30');

INSERT INTO ThanhToan (HocPhiID, NgayThanhToan, SoTienDaDong)
VALUES
(1, '2024-01-05', 5000000),
(2, '2024-01-06', 5200000),
(3, '2024-06-01', 4800000),
(4, '2024-06-02', 5500000),
(5, '2025-01-10', 6000000),
(6, '2025-01-11', 6100000),
(7, '2025-06-05', 6200000),
(8, '2025-06-06', 6300000),
(9, '2023-01-03', 5400000),
(10, '2023-01-04', 5600000);

INSERT INTO HoaDon (ThanhToanID, NgayLap, SoTien)
VALUES
(1, '2024-01-05', 5000000),
(2, '2024-01-06', 5200000),
(3, '2024-06-01', 4800000),
(4, '2024-06-02', 5500000),
(5, '2025-01-10', 6000000),
(6, '2025-01-11', 6100000),
(7, '2025-06-05', 6200000),
(8, '2025-06-06', 6300000),
(9, '2023-01-03', 5400000),
(10, '2023-01-04', 5600000);

INSERT INTO ThongBaoNo (MaSV, KiHocID, SoTienNo, NgayThongBao)
VALUES
('11220001', 6, 3000000, '2023-07-01'),
('11220002', 6, 2000000, '2023-07-01'),
('11220003', 7, 1000000, '2022-01-01'),
('11220004', 7, 500000,  '2022-01-01'),
('11220005', 8, 4500000, '2022-06-01'),
('11220006', 8, 2500000, '2022-06-01'),
('11220007', 9, 1800000, '2021-01-01'),
('11220008', 9, 1200000, '2021-01-01'),
('11220009', 10, 1000000, '2021-06-01'),
('11220010', 10, 900000,  '2021-06-01');

SELECT * FROM Users ;
SELECT * FROM SinhVien;
SELECT * FROM KiHoc;
SELECT * FROM HocPhi;
SELECT * FROM ThanhToan;
SELECT * FROM HoaDon;
SELECT * FROM ThongBaoNo;
--xoa cac ban ghi 
DELETE FROM HoaDon;
DELETE FROM ThanhToan;
DELETE FROM ThongBaoNo;
DELETE FROM HocPhi;
DELETE FROM KiHoc;
DELETE FROM SinhVien;
DELETE FROM Users;

-- Bảng con trước
DROP TABLE IF EXISTS HoaDon;
DROP TABLE IF EXISTS ThanhToan;
DROP TABLE IF EXISTS ThongBaoNo;
DROP TABLE IF EXISTS HocPhi;

-- Bảng trung gian
DROP TABLE IF EXISTS KiHoc;

-- Bảng chính
DROP TABLE IF EXISTS SinhVien;
DROP TABLE IF EXISTS Users;

SELECT TOP 1 MaSV FROM SinhVien
ORDER BY MaSV DESC;



CREATE OR ALTER VIEW v_HoaDon AS
SELECT 
    hp.HocPhiID, -- 🟢 Thêm dòng này
    sv.MaSV,
    sv.FullName AS HoTen,
    sv.Lop,
    kh.TenKiHoc,
    hp.SoTien AS HocPhi,
    ISNULL(SUM(tt.SoTienDaDong), 0) AS TongDaDong,
    (hp.SoTien - ISNULL(SUM(tt.SoTienDaDong), 0)) AS SoTienConNo,
    MAX(hd.NgayLap) AS NgayThanhToan,
    MAX(hd.NganHang) AS NganHang
FROM 
    SinhVien sv
JOIN HocPhi hp ON sv.MaSV = hp.MaSV
JOIN KiHoc kh ON kh.KiHocID = hp.KiHocID
LEFT JOIN ThanhToan tt ON hp.HocPhiID = tt.HocPhiID
LEFT JOIN HoaDon hd ON hd.ThanhToanID = tt.ThanhToanID
GROUP BY 
    hp.HocPhiID, -- 🟢 Thêm vào đây nữa
    sv.MaSV, sv.FullName, sv.Lop, kh.TenKiHoc, hp.SoTien


SELECT* FROM v_HoaDon 

CREATE OR ALTER VIEW v_HoaDon_ChiTiet AS
SELECT 
    sv.MaSV,
    sv.FullName AS HoTen,
    sv.Lop,
    kh.TenKiHoc,
    hp.HocPhiID,
    hp.SoTien AS HocPhi,
    tt.SoTienDaDong AS SoTienVuaDong,   
    hd.NganHang,
    hd.NgayLap AS NgayThanhToan
FROM HocPhi hp
JOIN SinhVien sv ON sv.MaSV = hp.MaSV
JOIN KiHoc kh ON kh.KiHocID = hp.KiHocID
JOIN (
    SELECT tt1.*
    FROM ThanhToan tt1
    JOIN (
        SELECT HocPhiID, MAX(NgayThanhToan) AS MaxNgay
        FROM ThanhToan
        GROUP BY HocPhiID
    ) latest ON tt1.HocPhiID = latest.HocPhiID AND tt1.NgayThanhToan = latest.MaxNgay
) tt ON tt.HocPhiID = hp.HocPhiID
JOIN HoaDon hd ON hd.ThanhToanID = tt.ThanhToanID

select * from v_HoaDon_ChiTiet where v_HoaDon_ChiTiet.HocPhiID = '11220001-2-2024'


EXEC sp_LayHocPhiTheoMaSV 'SV001'














