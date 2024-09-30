FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Rent.Service.API/Rent.Service.API.csproj", "Rent.Service.API/"]
COPY ["Rent.Service.Application/Rent.Service.Application.csproj", "Rent.Service.Application/"]
COPY ["Rent.Service.Domain/Rent.Service.Domain.csproj", "Rent.Service.Domain/"]
COPY ["Rent.Service.Infrastructure/Rent.Service.Infrastructure.csproj", "Rent.Service.Infrastructure/"]
RUN dotnet restore "./Rent.Service.API/Rent.Service.API.csproj"
COPY . .
WORKDIR "/src/Rent.Service.API"
RUN dotnet build "./Rent.Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Rent.Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rent.Service.API.dll"]
