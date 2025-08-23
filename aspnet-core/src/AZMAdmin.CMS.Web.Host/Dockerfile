FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/AZMAdmin.CMS.Web.Host/AZMAdmin.CMS.Web.Host.csproj", "src/AZMAdmin.CMS.Web.Host/"]
COPY ["src/AZMAdmin.CMS.Web.Core/AZMAdmin.CMS.Web.Core.csproj", "src/AZMAdmin.CMS.Web.Core/"]
COPY ["src/AZMAdmin.CMS.Application/AZMAdmin.CMS.Application.csproj", "src/AZMAdmin.CMS.Application/"]
COPY ["src/AZMAdmin.CMS.Core/AZMAdmin.CMS.Core.csproj", "src/AZMAdmin.CMS.Core/"]
COPY ["src/AZMAdmin.CMS.EntityFrameworkCore/AZMAdmin.CMS.EntityFrameworkCore.csproj", "src/AZMAdmin.CMS.EntityFrameworkCore/"]
WORKDIR "/src/src/AZMAdmin.CMS.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/AZMAdmin.CMS.Web.Host", "src/AZMAdmin.CMS.Web.Host"]
COPY ["src/AZMAdmin.CMS.Web.Core", "src/AZMAdmin.CMS.Web.Core"]
COPY ["src/AZMAdmin.CMS.Application", "src/AZMAdmin.CMS.Application"]
COPY ["src/AZMAdmin.CMS.Core", "src/AZMAdmin.CMS.Core"]
COPY ["src/AZMAdmin.CMS.EntityFrameworkCore", "src/AZMAdmin.CMS.EntityFrameworkCore"]
WORKDIR "/src/src/AZMAdmin.CMS.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "AZMAdmin.CMS.Web.Host.dll"]
