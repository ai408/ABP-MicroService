#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Business.Host/Business.Host.csproj", "Business.Host/"]
COPY ["Business.HttpApi/Business.HttpApi.csproj", "Business.HttpApi/"]
COPY ["Business.Application.Contracts/Business.Application.Contracts.csproj", "Business.Application.Contracts/"]
COPY ["Business.Application/Business.Application.csproj", "Business.Application/"]
COPY ["Business.Domain/Business.Domain.csproj", "Business.Domain/"]
COPY ["Business.Core/Business.Core.csproj", "Business.Core/"]
COPY ["Business.EntityFrameworkCore/Business.EntityFrameworkCore.csproj", "Business.EntityFrameworkCore/"]
RUN dotnet restore "Business.Host/Business.Host.csproj"
COPY . .
WORKDIR "/src/Business.Host"
RUN dotnet build "Business.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Business.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Business.Host.dll"]