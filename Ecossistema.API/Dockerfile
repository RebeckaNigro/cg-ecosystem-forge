FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN mkdir Ecossistema.API
COPY "./Ecossistema.API/Ecossistema.API.csproj" "./Ecossistema.API/"
RUN dotnet restore "./Ecossistema.API/Ecossistema.API.csproj"
COPY . .
RUN dotnet build "./Ecossistema.API/Ecossistema.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ecossistema.API/Ecossistema.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/Ecossistema.API/appsettings.json /app/appsettings.json
ENTRYPOINT ["dotnet", "Ecossistema.API.dll"]
