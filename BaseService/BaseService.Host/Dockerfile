#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BaseService.Host/BaseService.Host.csproj", "BaseService.Host/"]
COPY ["BaseService.EntityFrameworkCore/BaseService.EntityFrameworkCore.csproj", "BaseService.EntityFrameworkCore/"]
COPY ["BaseService.Domain/BaseService.Domain.csproj", "BaseService.Domain/"]
COPY ["BaseService.HttpApi/BaseService.HttpApi.csproj", "BaseService.HttpApi/"]
COPY ["BaseService.Application.Contracts/BaseService.Application.Contracts.csproj", "BaseService.Application.Contracts/"]
COPY ["BaseService.Application/BaseService.Application.csproj", "BaseService.Application/"]
RUN dotnet restore "BaseService.Host/BaseService.Host.csproj"
COPY . .
WORKDIR "/src/BaseService.Host"
RUN dotnet build "BaseService.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BaseService.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaseService.Host.dll"]