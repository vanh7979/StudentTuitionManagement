CREATE DATABASE NHOM7_LTUD;
GO
USE NHOM7_LTUD;
GO
CREATE TABLE KhoaVien (
    MAKHOA NVARCHAR(10) PRIMARY KEY,
    TENKHOA NVARCHAR(50) NOT NULL,
    DIACHI CHAR(50),
    SODT CHAR(12)
);
CREATE TABLE Lop (
    MALOP NVARCHAR(10) PRIMARY KEY,
    TENLOP NVARCHAR(30),
    MAKHOA NVARCHAR(10) NOT NULL,
    CVHT CHAR(30),
    GHICHU CHAR(100),
    FOREIGN KEY (MAKHOA) REFERENCES KhoaVien(MAKHOA)
);
CREATE TABLE SinhVien (
    MaSV NVARCHAR(20) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Lop NVARCHAR(10),
    Khoa NVARCHAR(10),
    FOREIGN KEY (Lop) REFERENCES Lop(MALOP),
    FOREIGN KEY (Khoa) REFERENCES KhoaVien(MAKHOA)
);
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100),
    Role NVARCHAR(10) NOT NULL CHECK (Role IN ('admin', 'user')),
    MaSV NVARCHAR(20),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV) ON DELETE CASCADE
);
CREATE TABLE TinChi (
    MATIN NVARCHAR(10) PRIMARY KEY,
    MALOP NVARCHAR(10) NOT NULL,
    SOTIEN1TIN INT,
    FOREIGN KEY (MALOP) REFERENCES Lop(MALOP)
);
CREATE TABLE KiHoc (
    KiHocID NVARCHAR(10) PRIMARY KEY,
    TenKiHoc NVARCHAR(50) NOT NULL,
    NamHoc INT NOT NULL,
    TGBatDau DATE,
    TGKetThuc DATE
);
CREATE TABLE HocPhi (
    HocPhiID NVARCHAR(20) PRIMARY KEY,
    MaSV NVARCHAR(20) NOT NULL,
    KiHocID NVARCHAR(10) NOT NULL,
    SoTien DECIMAL(18, 2) NOT NULL,
    HanDong DATE,
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Chưa đóng',
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV) ON DELETE CASCADE,
    FOREIGN KEY (KiHocID) REFERENCES KiHoc(KiHocID) ON DELETE CASCADE
);
CREATE TABLE ThanhToan (
    ThanhToanID INT IDENTITY(1,1) PRIMARY KEY,
    HocPhiID NVARCHAR(20) NOT NULL,
    NgayThanhToan DATE DEFAULT GETDATE(),
    SoTienDaDong DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (HocPhiID) REFERENCES HocPhi(HocPhiID) ON DELETE CASCADE
);
CREATE TABLE HoaDon (
    HoaDonID INT IDENTITY(1,1) PRIMARY KEY,
    ThanhToanID INT NOT NULL,
    NgayLap DATE DEFAULT GETDATE(),
    SoTien DECIMAL(18, 2) NOT NULL,
    NganHang NVARCHAR(100),
    FOREIGN KEY (ThanhToanID) REFERENCES ThanhToan(ThanhToanID) ON DELETE CASCADE
);
CREATE TABLE ThongBaoNo (
    TBID INT IDENTITY(1,1) PRIMARY KEY,
    MaSV NVARCHAR(20) NOT NULL,
    KiHocID NVARCHAR(10) NOT NULL,
    SoTienNo DECIMAL(18, 2) NOT NULL,
    NgayThongBao DATE DEFAULT GETDATE(),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV) ON DELETE CASCADE,
    FOREIGN KEY (KiHocID) REFERENCES KiHoc(KiHocID) ON DELETE CASCADE
);

-- View tổng hợp hóa đơn
CREATE OR ALTER VIEW v_HoaDon AS
SELECT 
    hp.HocPhiID,
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
    hp.HocPhiID, 
    sv.MaSV, sv.FullName, sv.Lop, kh.TenKiHoc, hp.SoTien;

-- View chi tiết hóa đơn mới nhất
CREATE OR ALTER VIEW v_HoaDon_ChiTiet AS
WITH LatestThanhToan AS (
    SELECT tt1.*
    FROM ThanhToan tt1
    JOIN (
        SELECT HocPhiID, MAX(ThanhToanID) AS MaxTTID
        FROM ThanhToan
        GROUP BY HocPhiID
    ) latest ON tt1.HocPhiID = latest.HocPhiID AND tt1.ThanhToanID = latest.MaxTTID
)
SELECT 
    sv.MaSV,
    sv.FullName AS HoTen,
    sv.Lop,
    kh.TenKiHoc,
    CONVERT(NVARCHAR(20), hp.HocPhiID) AS HocPhiID,
    hp.SoTien AS HocPhi,
    tt.SoTienDaDong AS SoTienVuaDong,   
    hd.NganHang,
    hd.NgayLap AS NgayThanhToan
FROM HocPhi hp
JOIN SinhVien sv ON sv.MaSV = hp.MaSV
JOIN KiHoc kh ON kh.KiHocID = hp.KiHocID
JOIN LatestThanhToan tt ON tt.HocPhiID = hp.HocPhiID
JOIN HoaDon hd ON hd.ThanhToanID = tt.ThanhToanID;
-- Thống kê sinh viên đầy đủ
CREATE OR ALTER VIEW v_ThongKeSinhVienDayDu AS
SELECT 
    sv.MaSV,
    sv.FullName,
    sv.Lop,
    sv.Khoa,
    kv.TENKHOA
FROM SinhVien sv
JOIN KhoaVien kv ON sv.Khoa = kv.MAKHOA;

-- Báo cáo học phí theo kỳ
CREATE OR ALTER VIEW v_BaoCaoHocPhi AS
SELECT 
    sv.MaSV,
    sv.FullName,
    sv.Lop,
    kh.TenKiHoc,
    hp.SoTien AS TongTien,
    ISNULL(SUM(tt.SoTienDaDong), 0) AS DaDong,
    hp.SoTien - ISNULL(SUM(tt.SoTienDaDong), 0) AS ConNo,
    hp.TrangThai,
    hp.HanDong
FROM HocPhi hp
JOIN SinhVien sv ON sv.MaSV = hp.MaSV
JOIN KiHoc kh ON kh.KiHocID = hp.KiHocID
LEFT JOIN ThanhToan tt ON hp.HocPhiID = tt.HocPhiID
GROUP BY sv.MaSV, sv.FullName, sv.Lop, kh.TenKiHoc, hp.SoTien, hp.TrangThai, hp.HanDong;

-- Thống kê tổng quát học phí
CREATE OR ALTER VIEW v_ThongKeHocPhiTongQuat AS
SELECT 
    COUNT(DISTINCT sv.MaSV) AS TongSoSinhVien,
    SUM(hp.SoTien) AS TongTien,
    SUM(ISNULL(tt.SoTienDaDong, 0)) AS DaDong,
    SUM(hp.SoTien - ISNULL(tt.SoTienDaDong, 0)) AS ConNo
FROM 
    SinhVien sv
JOIN HocPhi hp ON sv.MaSV = hp.MaSV
LEFT JOIN ThanhToan tt ON tt.HocPhiID = hp.HocPhiID;
-- Trigger xóa toàn bộ dữ liệu liên quan khi xóa sinh viên
CREATE OR ALTER TRIGGER trg_DeleteSinhVien
ON SinhVien
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Xóa hóa đơn
    DELETE hd
    FROM HoaDon hd
    JOIN ThanhToan tt ON hd.ThanhToanID = tt.ThanhToanID
    JOIN HocPhi hp ON tt.HocPhiID = hp.HocPhiID
    JOIN deleted d ON hp.MaSV = d.MaSV;

    -- Xóa thanh toán
    DELETE tt
    FROM ThanhToan tt
    JOIN HocPhi hp ON tt.HocPhiID = hp.HocPhiID
    JOIN deleted d ON hp.MaSV = d.MaSV;

    -- Xóa thông báo nợ
    DELETE tb
    FROM ThongBaoNo tb
    JOIN deleted d ON tb.MaSV = d.MaSV;

    -- Xóa học phí
    DELETE hp
    FROM HocPhi hp
    JOIN deleted d ON hp.MaSV = d.MaSV;

    -- Xóa tài khoản người dùng
    DELETE u
    FROM Users u
    JOIN deleted d ON u.MaSV = d.MaSV;
END;
CREATE OR ALTER PROCEDURE sp_ThanhToanHocPhi
    @MaSV NVARCHAR(20),
    @TenKiHoc NVARCHAR(50),
    @SoTien DECIMAL(18,2),
    @NganHang NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @HocPhiID NVARCHAR(20), @KiHocID NVARCHAR(50), @TongTien DECIMAL(18,2);

    -- Lấy học phí
    SELECT TOP 1
        @HocPhiID = hp.HocPhiID,
        @TongTien = hp.SoTien,
        @KiHocID = CONVERT(NVARCHAR(50), kh.KiHocID)
    FROM HocPhi hp
    JOIN KiHoc kh ON hp.KiHocID = kh.KiHocID
    WHERE hp.MaSV = @MaSV AND kh.TenKiHoc = @TenKiHoc;

    IF @HocPhiID IS NULL
    BEGIN
        RAISERROR(N'Không tìm thấy học phí cho sinh viên này trong kỳ học này.', 16, 1);
        RETURN;
    END

    DECLARE @DaDongTruoc DECIMAL(18,2) = 0;
    SELECT @DaDongTruoc = ISNULL(SUM(SoTienDaDong), 0)
    FROM ThanhToan
    WHERE HocPhiID = @HocPhiID;

    DECLARE @TongDaDong DECIMAL(18,2) = @DaDongTruoc + @SoTien;
    DECLARE @ConLai DECIMAL(18,2) = @TongTien - @TongDaDong;

    DECLARE @TrangThai NVARCHAR(20) =
        CASE
            WHEN @TongDaDong = 0 THEN N'Chưa đóng'
            WHEN @ConLai <= 0 THEN N'Đã đóng'
            ELSE N'Còn nợ'
        END;

    -- Ghi nhận thanh toán
    INSERT INTO ThanhToan (HocPhiID, NgayThanhToan, SoTienDaDong)
    VALUES (@HocPhiID, GETDATE(), @SoTien);

    DECLARE @ThanhToanID INT = SCOPE_IDENTITY();

    -- Ghi hóa đơn
    INSERT INTO HoaDon (ThanhToanID, NgayLap, SoTien, NganHang)
    VALUES (@ThanhToanID, GETDATE(), @SoTien, @NganHang);

    -- Cập nhật trạng thái
    UPDATE HocPhi
    SET TrangThai = @TrangThai
    WHERE HocPhiID = @HocPhiID;

    -- Ghi nhận còn nợ nếu có
    IF @ConLai > 0
    BEGIN
        INSERT INTO ThongBaoNo (MaSV, KiHocID, SoTienNo, NgayThongBao)
        VALUES (@MaSV, @KiHocID, @ConLai, GETDATE());
    END
END;
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
CREATE OR ALTER PROCEDURE sp_LocHocPhi
    @MaSV NVARCHAR(20),
    @TenKiHoc NVARCHAR(50) = NULL,
    @TrangThai NVARCHAR(20) = NULL
AS
BEGIN
    SELECT 
        kh.TenKiHoc,
        kh.NamHoc,
        hp.SoTien,
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
        AND (@TenKiHoc IS NULL OR kh.TenKiHoc = @TenKiHoc)
        AND (@TrangThai IS NULL OR hp.TrangThai = @TrangThai)
    ORDER BY kh.NamHoc DESC, kh.TenKiHoc;
END;

CREATE OR ALTER VIEW v_ThongTinSinhVien AS
SELECT 
    sv.MaSV,
    sv.FullName AS HoTen,
    sv.Lop AS MaLop,
    l.TENLOP,
    sv.Khoa AS MaKhoa,
    kv.TENKHOA,
    l.CVHT AS CoVanHocTap
FROM 
    SinhVien sv
JOIN Lop l ON sv.Lop = l.MALOP
JOIN KhoaVien kv ON sv.Khoa = kv.MAKHOA;



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
DROP TABLE IF EXISTS TinChi;
DROP TABLE IF EXISTS Lop;
DROP TABLE IF EXISTS KhoaVien;

-- Khoa/Viện
INSERT INTO KhoaVien(MAKHOA, TENKHOA, DIACHI, SODT) VALUES
('CNTT',N'Khoa Công Nghệ Thông Tin',N'Tòa A2','0123456789'),
('KTPM',N'Viện Kỹ Thuật',N'Tòa A2','0123456789'),
('HTTT',N'Khoa Kinh Tế',N'Tòa A2','0123456789');

-- Lớp
INSERT INTO Lop(MALOP, TENLOP, MAKHOA, CVHT) VALUES
('CNTT1',N'Công Nghệ Thông Tin 1','CNTT',N'Nguyễn Văn A'),
('CNTT2',N'Công Nghệ Thông Tin 2','CNTT',N'Nguyễn Văn B'),
('KTPM1',N'Kỹ Thuật Phần Mềm 1','KTPM',N'Nguyễn Văn C'),
('HTTT1',N'Hệ Thống Thông Tin 1','HTTT',N'Nguyễn Văn D'),
('HTTT2',N'Hệ Thống Thông Tin 2','HTTT',N'Nguyễn Văn E');

-- Sinh viên
INSERT INTO SinhVien (MaSV, FullName, Lop, Khoa) VALUES
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

-- Users
INSERT INTO Users (Username, Password, FullName, Role, MaSV) VALUES
('admin1', 'admin1', 'Nguyen Van Admin', 'admin', NULL),
('admin2', 'admin2', 'Tran Thi Admin', 'admin', NULL),
('user1',  'pass1',  'Nguyen Van A',    'user',  '11220001'),
('user2',  'pass2',  'Le Thi B',        'user',  '11220002'),
('user3',  'pass3',  'Tran Van C',      'user',  '11220003'),
('user4',  'pass4',  'Pham Thi D',      'user',  '11220004'),
('user5',  'pass5',  'Hoang Van E',     'user',  '11220005'),
('user6',  'pass6',  'Do Thi F',        'user',  '11220006'),
('user7',  'pass7',  'Ngo Van G',       'user',  '11220007'),
('user8',  'pass8',  'Dang Thi G',      'user',  '11220008'),
('user9',  'pass9',  'Bui Van I',       'user',  '11220009'),
('user10', 'pass10', 'Mai Thi K',       'user',  '11220010');


-- Kỳ học
INSERT INTO KiHoc (KiHocID, TenKiHoc, NamHoc, TGBatDau, TGKetThuc) VALUES
('1-2024','HK1 2024', 2024, '2023-09-06', '2024-02-06'),
('2-2024','HK2 2024', 2024, '2024-03-01', '2024-06-20');

-- Tín chỉ
INSERT INTO TinChi VALUES
('CNTT1-450', 'CNTT1', 450000),
('CNTT2-450', 'CNTT2', 450000),
('KTPM1-415', 'KTPM1', 4150000),
('HTTT1-500', 'HTTT1', 500000),
('HTTT2-500', 'HTTT2', 500000);

-- Học phí 
INSERT INTO HocPhi (HocPhiID, MaSV, KiHocID, SoTien, HanDong)
VALUES
('11220001-1-2022', '11220001', '1-2022', 5100000, '2023-01-31'),
('11220001-2-2022', '11220001', '2-2022', 5200000, '2023-05-30'),
('11220001-1-2023', '11220001', '1-2023', 5300000, '2023-09-30'),
('11220001-2-2023', '11220001', '2-2023', 5400000, '2024-01-31'),
('11220001-1-2024', '11220001', '1-2024', 5500000, '2024-05-30'),
('11220001-2-2024', '11220001', '2-2024', 5600000, '2024-09-30'),

('11220002-1-2022', '11220002', '1-2022', 5200000, '2023-01-31'),
('11220002-2-2022', '11220002', '2-2022', 5300000, '2023-05-30'),
('11220002-1-2023', '11220002', '1-2023', 5400000, '2023-09-30'),
('11220002-2-2023', '11220002', '2-2023', 5500000, '2024-01-31'),
('11220002-1-2024', '11220002', '1-2024', 5600000, '2024-05-30'),
('11220002-2-2024', '11220002', '2-2024', 5700000, '2024-09-30'),

('11220003-1-2022', '11220003', '1-2022', 5300000, '2023-01-31'),
('11220003-2-2022', '11220003', '2-2022', 5400000, '2023-05-30'),
('11220003-1-2023', '11220003', '1-2023', 5500000, '2023-09-30'),
('11220003-2-2023', '11220003', '2-2023', 5600000, '2024-01-31'),
('11220003-1-2024', '11220003', '1-2024', 5700000, '2024-05-30'),
('11220003-2-2024', '11220003', '2-2024', 5800000, '2024-09-30'),

('11220004-1-2022', '11220004', '1-2022', 5400000, '2023-01-31'),
('11220004-2-2022', '11220004', '2-2022', 5500000, '2023-05-30'),
('11220004-1-2023', '11220004', '1-2023', 5600000, '2023-09-30'),
('11220004-2-2023', '11220004', '2-2023', 5700000, '2024-01-31'),
('11220004-1-2024', '11220004', '1-2024', 5800000, '2024-05-30'),
('11220004-2-2024', '11220004', '2-2024', 5900000, '2024-09-30'),

('11220005-1-2022', '11220005', '1-2022', 5500000, '2023-01-31'),
('11220005-2-2022', '11220005', '2-2022', 5600000, '2023-05-30'),
('11220005-1-2023', '11220005', '1-2023', 5700000, '2023-09-30'),
('11220005-2-2023', '11220005', '2-2023', 5800000, '2024-01-31'),
('11220005-1-2024', '11220005', '1-2024', 5900000, '2024-05-30'),
('11220005-2-2024', '11220005', '2-2024', 6000000, '2024-09-30'),

('11220006-1-2022', '11220006', '1-2022', 5600000, '2023-01-31'),
('11220006-2-2022', '11220006', '2-2022', 5700000, '2023-05-30'),
('11220006-1-2023', '11220006', '1-2023', 5800000, '2023-09-30'),
('11220006-2-2023', '11220006', '2-2023', 5900000, '2024-01-31'),
('11220006-1-2024', '11220006', '1-2024', 6000000, '2024-05-30'),
('11220006-2-2024', '11220006', '2-2024', 6100000, '2024-09-30'),

('11220007-1-2022', '11220007', '1-2022', 5700000, '2023-01-31'),
('11220007-2-2022', '11220007', '2-2022', 5800000, '2023-05-30'),
('11220007-1-2023', '11220007', '1-2023', 5900000, '2023-09-30'),
('11220007-2-2023', '11220007', '2-2023', 6000000, '2024-01-31'),
('11220007-1-2024', '11220007', '1-2024', 6100000, '2024-05-30'),
('11220007-2-2024', '11220007', '2-2024', 6200000, '2024-09-30'),

('11220008-1-2022', '11220008', '1-2022', 5800000, '2023-01-31'),
('11220008-2-2022', '11220008', '2-2022', 5900000, '2023-05-30'),
('11220008-1-2023', '11220008', '1-2023', 6000000, '2023-09-30'),
('11220008-2-2023', '11220008', '2-2023', 6100000, '2024-01-31'),
('11220008-1-2024', '11220008', '1-2024', 6200000, '2024-05-30'),
('11220008-2-2024', '11220008', '2-2024', 6300000, '2024-09-30'),

('11220009-1-2022', '11220009', '1-2022', 5900000, '2023-01-31'),
('11220009-2-2022', '11220009', '2-2022', 6000000, '2023-05-30'),
('11220009-1-2023', '11220009', '1-2023', 6100000, '2023-09-30'),
('11220009-2-2023', '11220009', '2-2023', 6200000, '2024-01-31'),
('11220009-1-2024', '11220009', '1-2024', 6300000, '2024-05-30'),
('11220009-2-2024', '11220009', '2-2024', 6400000, '2024-09-30'),

('11220010-1-2022', '11220010', '1-2022', 6000000, '2023-01-31'),
('11220010-2-2022', '11220010', '2-2022', 6100000, '2023-05-30'),
('11220010-1-2023', '11220010', '1-2023', 6200000, '2023-09-30'),
('11220010-2-2023', '11220010', '2-2023', 6300000, '2024-01-31'),
('11220010-1-2024', '11220010', '1-2024', 6400000, '2024-05-30'),
('11220010-2-2024', '11220010', '2-2024', 6500000, '2024-09-30');

-- Thông tin cấu trúc bảng
sp_help KhoaVien;
sp_help Lop;
sp_help SinhVien;
sp_help Users;
sp_help TinChi;
sp_help KiHoc;
sp_help HocPhi;
sp_help ThanhToan;
sp_help HoaDon;
sp_help ThongBaoNo;

-- Thủ tục nghiệp vụ
EXEC sp_ThanhToanHocPhi N'11220001', N'HK1 2024', 5000000, N'Vietcombank';
EXEC sp_LayHocPhiTheoMaSV N'11220001';
EXEC sp_LocHocPhi N'11220001', NULL, NULL;

-- Xem dữ liệu các view
SELECT * FROM v_HoaDon;
SELECT * FROM v_HoaDon_ChiTiet;
SELECT * FROM v_ThongKeSinhVienDayDu;
SELECT * FROM v_BaoCaoHocPhi;
SELECT * FROM v_ThongKeHocPhiTongQuat;
SELECT * FROM v_ThongTinSinhVien;


SELECT * FROM KhoaVien;
SELECT * FROM Lop;
SELECT * FROM SinhVien;
SELECT * FROM Users;
SELECT * FROM TinChi;
SELECT * FROM KiHoc;
SELECT * FROM HocPhi;
SELECT * FROM ThanhToan;
SELECT * FROM HoaDon;
SELECT * FROM ThongBaoNo;

