FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OpenRemoteAPI/OpenRemoteAPI.csproj", "OpenRemoteAPI/"]
RUN dotnet restore "OpenRemoteAPI/OpenRemoteAPI.csproj"
COPY . .
WORKDIR "/src/OpenRemoteAPI"
RUN dotnet build "OpenRemoteAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OpenRemoteAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenRemoteAPI.dll"]
