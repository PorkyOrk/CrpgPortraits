FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CrpgP.WebApplication/CrpgP.WebApplication.csproj", "CrpgP.WebApplication/"]
RUN dotnet restore "CrpgP.WebApplication/CrpgP.WebApplication.csproj"
COPY . .
WORKDIR "/src/CrpgP.WebApplication"
RUN dotnet build "CrpgP.WebApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CrpgP.WebApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrpgP.WebApplication.dll"]
