FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CrpgP.Api/CrpgP.Api.csproj", "CrpgP.Api/"]
RUN dotnet restore "CrpgP.Api/CrpgP.Api.csproj"
COPY . .
WORKDIR "/src/CrpgP.Api"
RUN dotnet build "CrpgP.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrpgP.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrpgP.Api.dll"]
