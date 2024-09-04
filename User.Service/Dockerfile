FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["User.Service.API/User.Service.API.csproj", "User.Service.API/"]
COPY ["User.Service.BLL/User.Service.BLL.csproj", "User.Service.BLL/"]
COPY ["User.Service.DLL/User.Service.DLL.csproj", "User.Service.DLL/"]
COPY ["User.Service.Shared/User.Service.Shared.csproj", "User.Service.Shared/"]
RUN dotnet restore "./User.Service.API/User.Service.API.csproj"
COPY . .
WORKDIR "/src/User.Service.API"
RUN dotnet build "./User.Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./User.Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Service.API.dll"]
