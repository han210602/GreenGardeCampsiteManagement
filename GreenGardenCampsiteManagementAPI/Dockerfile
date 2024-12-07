# Sử dụng image .NET 6 SDK chính thức để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
 
# Thiết lập thư mục làm việc trong container
WORKDIR /app
 
# Sao chép các file .csproj và khôi phục các dependency
COPY *.sln ./
COPY BusinessObject/*.csproj ./BusinessObject/
COPY DataAccess/*.csproj ./DataAccess/
COPY GreenGardenCampsiteManagementAPI/*.csproj ./GreenGardenCampsiteManagementAPI/
COPY Repositories/*.csproj ./Repositories/
 
RUN dotnet restore
 
# Sao chép phần còn lại của mã nguồn ứng dụng
COPY . .
 
# Build ứng dụng
RUN dotnet publish GreenGardenCampsiteManagementAPI -c Release -o out
 
# Sử dụng image .NET 6 runtime chính thức để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
 
# Thiết lập thư mục làm việc trong container
WORKDIR /app
 
# Sao chép các file build từ image build
COPY --from=build /app/out .
 
# Sao chép launchSettings.json vào vị trí phù hợp
COPY GreenGardenCampsiteManagementAPI/Properties/launchSettings.json ./Properties/launchSettings.json
 
# Thiết lập biến môi trường
ENV ASPNETCORE_URLS="http://+:80"
ENV ASPNETCORE_ENVIRONMENT="Development"
 
# Mở cổng mà ứng dụng sẽ chạy trên
EXPOSE 80
 
# Thiết lập điểm vào để chạy ứng dụng
ENTRYPOINT ["dotnet", "GreenGardenCampsiteManagementAPI.dll"]