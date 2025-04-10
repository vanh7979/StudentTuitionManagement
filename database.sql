CREATE DATABASE NHOM7_LTUD
USE NHOM7_LTUD;
--Bảng user--
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100),
    Role NVARCHAR(10) NOT NULL CHECK (Role IN ('admin', 'user'))
);
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
--Bảng học phí--
CREATE TABLE HocPhi (
    HocPhiID INT IDENTITY(1,1) PRIMARY KEY,
    MaSV NVARCHAR(20) NOT NULL,
    KiHocID INT NOT NULL,
    SoTien DECIMAL(18, 2) NOT NULL,
	HanDong DATE,
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








