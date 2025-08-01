# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-noble AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Corrigé : suppression du double guillemet en trop au début, et chemin correct
COPY ["WebAppliNaissance.csproj", "./"]

# Comme on a copié le .csproj dans /src, on fait restore dessus ici
RUN dotnet restore "./WebAppliNaissance.csproj"

# Copier tout le reste du code source dans /src
COPY . .

WORKDIR "/src"
RUN dotnet build "./WebAppliNaissance.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebAppliNaissance.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppliNaissance.dll"]
