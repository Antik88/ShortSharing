FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ShortSharing.API/ShortSharing.API.csproj", "ShortSharing.API/"]
COPY ["ShortSharing.BLL/ShortSharing.BLL.csproj", "ShortSharing.BLL/"]
COPY ["ShortSharing.DAL/ShortSharing.DAL.csproj", "ShortSharing.DAL/"]
COPY ["ShortSharing.Shared/ShortSharing.Shared.csproj", "ShortSharing.Shared/"]
RUN dotnet restore "./ShortSharing.API/ShortSharing.API.csproj"
COPY . .
WORKDIR "/src/ShortSharing.API"
RUN dotnet build "./ShortSharing.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShortSharing.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShortSharing.API.dll"]