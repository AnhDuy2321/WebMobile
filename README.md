# WebMobile
Project Web with ASP.Net
#Các bước để chạy project
Bước 1: Thực hiện add CSDL vào bài:
  -Sử dụng Restore Database trong SQL Server để add Database vào bằng file "DB_DiDong.bak".
  -Sau đó mở project vào phần appsettings.json chỗ ConnectionStrings đổi tên Data Source="tên server của máy". 
  -Tiếp theo bấm vào Tools -> NuGet Package Manager -> Package Manager Console.
  -Nhập lệnh "Scaffold-DbContext "Data Source=ADMIN\ADUY;Initial Catalog=DB_DiDong;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force (lưu ý thay đổi Data Source="tên server của máy"
  -Enter để chạy Model
Bước 2: Nếu Visual Studio 2022 chưa có tools thực hiện cài:
  -Bấm vào Tools -> NuGet Package Manager -> Manager NuGet Package Slution...
  -Cài những Tools sau:
  1.AspNetCoreHero.ToastNotification
  2.Microsoft.EntityFrameworkCore
  3.Microsoft.EntityFrameworkCore.Sqlite
  4.Microsoft.EntityFrameworkCore.SqlServer
  5.Microsoft.EntityFrameworkCore.Tools
  6.Microsoft.VisualStudio.Web.CodeGeneration.Design
  7.PagedList.Core.Mvc
  8.XAct.Core.PCL
