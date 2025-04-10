CREATE DATABASE NHOM7_LTUD
USE NHOM7_LTUD;
--Bảng user--
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100),
    Role NVARCHAR(10) NOT NULL CHECK (Role IN ('admin', 'user')));

ALTER TABLE Users
ADD MaSV NVARCHAR(20) NULL;

ALTER TABLE Users
ADD CONSTRAINT FK_Users_SinhVien
FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV);


--Bảng sinh viên--
CREATE TABLE SinhVien (
    MaSV NVARCHAR(20) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Lop NVARCHAR(20),
    Khoa NVARCHAR(50)
);
--Bảng kì học--
CREATE TABLE KiHoc (
    KiHocID INT IDENTITY(1,1) PRIMARY KEY,
    TenKiHoc NVARCHAR(50) NOT NULL,  -- Ví dụ: "HK1 2025", "HK2 2025"
    NamHoc INT NOT NULL              -- Ví dụ: 2025
);
Drop Table KiHoc 
--Bảng học phí--
CREATE TABLE HocPhi (
    HocPhiID INT IDENTITY(1,1) PRIMARY KEY,
    MaSV NVARCHAR(20) NOT NULL,
    KiHocID INT NOT NULL,
    SoTien DECIMAL(18, 2) NOT NULL,
    HanDong DATE,
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Chưa đóng',
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (KiHocID) REFERENCES KiHoc(KiHocID)
);
--DROP TABLE HocPhi;
--Bảng thanh toán--
CREATE TABLE ThanhToan (
    ThanhToanID INT IDENTITY(1,1) PRIMARY KEY,
    HocPhiID INT NOT NULL,
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
    FOREIGN KEY (ThanhToanID) REFERENCES ThanhToan(ThanhToanID)
);
--Bảng thông báo nợ--
CREATE TABLE ThongBaoNo (
    TBID INT IDENTITY(1,1) PRIMARY KEY,
    MaSV NVARCHAR(20) NOT NULL,
    KiHocID INT NOT NULL,
    SoTienNo DECIMAL(18, 2) NOT NULL,
    NgayThongBao DATE DEFAULT GETDATE(),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (KiHocID) REFERENCES KiHoc(KiHocID)
);

-- Thủ tục thêm, sửa, xóa cho các bảng --
--Bảng users
-- Thêm
CREATE PROCEDURE sp_ThemUsers
    @Username NVARCHAR(50),
    @Password NVARCHAR(100),
    @FullName NVARCHAR(100),
    @Role NVARCHAR(10)
AS
BEGIN
    INSERT INTO Users(Username, Password, FullName, Role)
    VALUES (@Username, @Password, @FullName, @Role);
END;
GO

-- Sửa
CREATE PROCEDURE sp_SuaUsers
    @UserID INT,
    @Username NVARCHAR(50),
    @Password NVARCHAR(100),
    @FullName NVARCHAR(100),
    @Role NVARCHAR(10)
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

-- Xóa
CREATE PROCEDURE sp_XoaUsers
    @UserID INT
AS
BEGIN
    DELETE FROM Users WHERE UserID = @UserID;
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
    @TenKiHoc NVARCHAR(50),
    @NamHoc INT
AS
BEGIN
    INSERT INTO KiHoc(TenKiHoc, NamHoc)
    VALUES (@TenKiHoc, @NamHoc);
END;
GO

CREATE PROCEDURE sp_SuaKiHoc
    @KiHocID INT,
    @TenKiHoc NVARCHAR(50),
    @NamHoc INT
AS
BEGIN
    UPDATE KiHoc
    SET TenKiHoc = @TenKiHoc,
        NamHoc = @NamHoc
    WHERE KiHocID = @KiHocID;
END;
GO

CREATE PROCEDURE sp_XoaKiHoc
    @KiHocID INT
AS
BEGIN
    DELETE FROM KiHoc WHERE KiHocID = @KiHocID;
END;
GO

--Bảng học phí
CREATE PROCEDURE sp_ThemHocPhi
    @MaSV NVARCHAR(20),
    @KiHocID INT,
    @SoTien DECIMAL(18,2),
    @HanDong DATE
AS
BEGIN
    INSERT INTO HocPhi(MaSV, KiHocID, SoTien, HanDong)
    VALUES (@MaSV, @KiHocID, @SoTien, @HanDong);
END;
GO

CREATE PROCEDURE sp_SuaHocPhi
    @HocPhiID INT,
    @MaSV NVARCHAR(20),
    @KiHocID INT,
    @SoTien DECIMAL(18,2),
    @HanDong DATE
AS
BEGIN
    UPDATE HocPhi
    SET MaSV = @MaSV,
        KiHocID = @KiHocID,
        SoTien = @SoTien,
        HanDong = @HanDong
    WHERE HocPhiID = @HocPhiID;
END;
GO

CREATE PROCEDURE sp_XoaHocPhi
    @HocPhiID INT
AS
BEGIN
    DELETE FROM HocPhi WHERE HocPhiID = @HocPhiID;
END;
GO

--Bảng thanh toán 
CREATE PROCEDURE sp_ThemThanhToan
    @HocPhiID INT,
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
    @HocPhiID INT,
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
    @MaSV = 'SV001', 
    @TenKiHoc = NULL, 
    @TrangThai = NULL;









INSERT INTO Users (Username, Password, FullName, Role)
VALUES
('admin1', 'pass1', 'Nguyen Van A', 'admin'),
('admin2', 'pass2', 'Tran Thi B', 'admin'),
('user1', 'pass3', 'Le Van C', 'user'),
('user2', 'pass4', 'Pham Thi D', 'user'),
('user3', 'pass5', 'Hoang Van E', 'user'),
('user4', 'pass6', 'Ngo Thi F', 'user'),
('user5', 'pass7', 'Dang Van G', 'user'),
('user6', 'pass8', 'Bui Thi H', 'user'),
('user7', 'pass9', 'Mai Van I', 'user'),
('user8', 'pass10', 'Do Thi K', 'user');

INSERT INTO SinhVien (MaSV, FullName, Lop, Khoa)
VALUES
('SV001', 'Nguyen Van A', 'CNTT1', 'CNTT'),
('SV002', 'Le Thi B', 'CNTT1', 'CNTT'),
('SV003', 'Tran Van C', 'CNTT2', 'CNTT'),
('SV004', 'Pham Thi D', 'CNTT2', 'CNTT'),
('SV005', 'Hoang Van E', 'KTPM1', 'KTPM'),
('SV006', 'Do Thi F', 'KTPM1', 'KTPM'),
('SV007', 'Ngo Van G', 'HTTT1', 'HTTT'),
('SV008', 'Dang Thi H', 'HTTT1', 'HTTT'),
('SV009', 'Bui Van I', 'HTTT2', 'HTTT'),
('SV010', 'Mai Thi K', 'HTTT2', 'HTTT');

UPDATE Users SET MaSV = 'SV001' WHERE Username = 'user1';
UPDATE Users SET MaSV = 'SV002' WHERE Username = 'user2';
UPDATE Users SET MaSV = 'SV003' WHERE Username = 'user3';
UPDATE Users SET MaSV = 'SV004' WHERE Username = 'user4';
UPDATE Users SET MaSV = 'SV005' WHERE Username = 'user5';
UPDATE Users SET MaSV = 'SV006' WHERE Username = 'user6';
UPDATE Users SET MaSV = 'SV007' WHERE Username = 'user7';
UPDATE Users SET MaSV = 'SV008' WHERE Username = 'user8';

INSERT INTO KiHoc (TenKiHoc, NamHoc)
VALUES
('HK1 2024', 2024),
('HK2 2024', 2024),
('HK1 2025', 2025),
('HK2 2025', 2025),
('HK1 2023', 2023),
('HK2 2023', 2023),
('HK1 2022', 2022),
('HK2 2022', 2022),
('HK1 2021', 2021),
('HK2 2021', 2021);

INSERT INTO HocPhi (MaSV, KiHocID, SoTien, HanDong)
VALUES
('SV001', 1, 5000000, '2024-01-31'),
('SV002', 1, 5200000, '2024-01-31'),
('SV003', 2, 4800000, '2024-06-30'),
('SV004', 2, 5500000, '2024-06-30'),
('SV005', 3, 6000000, '2025-01-31'),
('SV006', 3, 6100000, '2025-01-31'),
('SV007', 4, 6200000, '2025-06-30'),
('SV008', 4, 6300000, '2025-06-30'),
('SV009', 5, 5400000, '2023-01-31'),
('SV010', 5, 5600000, '2023-01-31');

INSERT INTO HocPhi (MaSV, KiHocID, SoTien, HanDong)
VALUES
-- HK2 2023 = KiHocID 6
('SV001', 6, 5000000, '2023-06-30'),
('SV002', 6, 5100000, '2023-06-30'),
('SV003', 6, 5200000, '2023-06-30'),
('SV004', 6, 5300000, '2023-06-30'),
('SV005', 6, 5400000, '2023-06-30'),
('SV006', 6, 5500000, '2023-06-30'),
('SV007', 6, 5600000, '2023-06-30'),
('SV008', 6, 5700000, '2023-06-30'),
('SV009', 6, 5800000, '2023-06-30'),
('SV010', 6, 5900000, '2023-06-30'),

-- HK1 2022 = KiHocID 7
('SV001', 7, 5050000, '2022-01-31'),
('SV002', 7, 5150000, '2022-01-31'),
('SV003', 7, 5250000, '2022-01-31'),
('SV004', 7, 5350000, '2022-01-31'),
('SV005', 7, 5450000, '2022-01-31'),
('SV006', 7, 5550000, '2022-01-31'),
('SV007', 7, 5650000, '2022-01-31'),
('SV008', 7, 5750000, '2022-01-31'),
('SV009', 7, 5850000, '2022-01-31'),
('SV010', 7, 5950000, '2022-01-31'),

-- HK2 2022 = KiHocID 8
('SV001', 8, 5100000, '2022-06-30'),
('SV002', 8, 5200000, '2022-06-30'),
('SV003', 8, 5300000, '2022-06-30'),
('SV004', 8, 5400000, '2022-06-30'),
('SV005', 8, 5500000, '2022-06-30'),
('SV006', 8, 5600000, '2022-06-30'),
('SV007', 8, 5700000, '2022-06-30'),
('SV008', 8, 5800000, '2022-06-30'),
('SV009', 8, 5900000, '2022-06-30'),
('SV010', 8, 6000000, '2022-06-30'),

-- HK1 2021 = KiHocID 9
('SV001', 9, 5150000, '2021-01-31'),
('SV002', 9, 5250000, '2021-01-31'),
('SV003', 9, 5350000, '2021-01-31'),
('SV004', 9, 5450000, '2021-01-31'),
('SV005', 9, 5550000, '2021-01-31'),
('SV006', 9, 5650000, '2021-01-31'),
('SV007', 9, 5750000, '2021-01-31'),
('SV008', 9, 5850000, '2021-01-31'),
('SV009', 9, 5950000, '2021-01-31'),
('SV010', 9, 6050000, '2021-01-31'),

-- HK2 2021 = KiHocID 10
('SV001', 10, 5200000, '2021-06-30'),
('SV002', 10, 5300000, '2021-06-30'),
('SV003', 10, 5400000, '2021-06-30'),
('SV004', 10, 5500000, '2021-06-30'),
('SV005', 10, 5600000, '2021-06-30'),
('SV006', 10, 5700000, '2021-06-30'),
('SV007', 10, 5800000, '2021-06-30'),
('SV008', 10, 5900000, '2021-06-30'),
('SV009', 10, 6000000, '2021-06-30'),
('SV010', 10, 6100000, '2021-06-30');

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
('SV001', 6, 3000000, '2023-07-01'),
('SV002', 6, 2000000, '2023-07-01'),
('SV003', 7, 1000000, '2022-01-01'),
('SV004', 7, 500000,  '2022-01-01'),
('SV005', 8, 4500000, '2022-06-01'),
('SV006', 8, 2500000, '2022-06-01'),
('SV007', 9, 1800000, '2021-01-01'),
('SV008', 9, 1200000, '2021-01-01'),
('SV009', 10, 1000000, '2021-06-01'),
('SV010', 10, 900000,  '2021-06-01');

SELECT * FROM Users;
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



CREATE VIEW v_HoaDon AS
SELECT 
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
    sv.MaSV, sv.FullName, sv.Lop, kh.TenKiHoc, hp.SoTien

ALTER TABLE HoaDon ADD NganHang NVARCHAR(100);

EXEC sp_LayHocPhiTheoMaSV 'SV001'














