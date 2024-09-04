FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["nuget.config", "."]
COPY ["Email.Service/Email.Service.csproj", "Email.Service/"]
COPY ["Email.Service.BLL/Email.Service.BLL.csproj", "Email.Service.BLL/"]
COPY ["Email.Service.DAL/Email.Service.DAL.csproj", "Email.Service.DAL/"]
COPY ["Email.Service.Shared/Email.Service.Shared.csproj", "Email.Service.Shared/"]
RUN dotnet restore "./Email.Service/Email.Service.csproj"
COPY . .
WORKDIR "/src/Email.Service"
RUN dotnet build "./Email.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Email.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Email.Service.dll"]