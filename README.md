# WebBanSachMVC.net
Bookstore Website Project
1. Giới thiệu
Dự án xây dựng website bán sách trực tuyến sử dụng ASP.NET Core. Website cung cấp đầy đủ các tính năng cho người dùng và quản trị viên, bao gồm:

Người dùng: Đăng ký, đăng nhập, tìm kiếm sách, thêm vào giỏ hàng, thanh toán và theo dõi đơn hàng.
Quản trị viên: Quản lý sách, đơn hàng, và phân quyền người dùng.
2. Công nghệ sử dụng
ASP.NET Core 8: Phát triển ứng dụng web hiệu năng cao.
Entity Framework Core: Quản lý cơ sở dữ liệu và ORM.
Identity: Quản lý xác thực và phân quyền người dùng.
MailKit: Gửi email xác nhận đơn hàng và khôi phục mật khẩu.
SQL Server: Lưu trữ dữ liệu.
3. Cách cài đặt
Yêu cầu hệ thống
Visual Studio 2022 hoặc mới hơn.
.NET SDK tương thích với dự án.
SQL Server và SQL Server Management Studio.
Hướng dẫn cài đặt
Tải công cụ:

Visual Studio 2022, .NET SDK, SQL Server từ trang chủ chính thức.
Cài đặt Visual Studio:

Chọn workload "ASP.NET and web development".
Cấu hình SQL Server:

Đảm bảo SQL Server hoạt động với cổng mặc định (1433).
Cấu hình project:

Mở file appsettings.json trong project và sửa chuỗi kết nối cơ sở dữ liệu.
Khởi tạo cơ sở dữ liệu:

Chạy các lệnh:
dotnet ef migrations add InitialCreate  
dotnet ef database update  
Chạy project:

Nhấn F5 trong Visual Studio.
Truy cập ứng dụng tại https://localhost:7246/.
4. Chức năng chính
Người dùng:
Đăng ký, đăng nhập.
Tìm kiếm sách, thêm vào giỏ hàng.
Thanh toán và theo dõi đơn hàng.
Quản trị viên:
Quản lý sách và đơn hàng.
Phân quyền người dùng.
