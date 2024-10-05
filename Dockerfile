FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UsingFirebase.csproj", "."]
RUN dotnet restore "./UsingFirebase.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./UsingFirebase.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UsingFirebase.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# copy swaggerDark.css
COPY ["wwwroot/css/swaggerDark.css", "wwwroot/css/swaggerDark.css"]

# if .env exist's, will be copied
RUN if [ -f .env ]; then cp .env /app/.env; fi

ENTRYPOINT ["dotnet", "UsingFirebase.dll"]