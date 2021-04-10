#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["FileStorage.Host/FileStorage.Host.csproj", "FileStorage.Host/"]
COPY ["FileStorage.Application/FileStorage.Application.csproj", "FileStorage.Application/"]
COPY ["FileStorage.Application.Contracts/FileStorage.Application.Contracts.csproj", "FileStorage.Application.Contracts/"]
COPY ["FileStorage.Domain/FileStorage.Domain.csproj", "FileStorage.Domain/"]
COPY ["FileStorage.EntityFrameworkCore/FileStorage.EntityFrameworkCore.csproj", "FileStorage.EntityFrameworkCore/"]
RUN dotnet restore "FileStorage.Host/FileStorage.Host.csproj"
COPY . .
WORKDIR "/src/FileStorage.Host"
RUN dotnet build "FileStorage.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileStorage.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileStorage.Host.dll"]