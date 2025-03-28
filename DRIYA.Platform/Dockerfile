# Base image for running the application (used in production and debugging)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# Use a non-root user for security
USER $APP_UID
WORKDIR /app
# Expose ports for HTTP (8080) and HTTPS (8081)
EXPOSE 8080
EXPOSE 8081

# Build image for compiling the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Copy the project files to restore dependencies
COPY ["DRIYA.Platform/DRIYA.Platform.csproj", "DRIYA.Platform/"]
COPY ["DRIYA.Common/DRIYA.Common.csproj", "DRIYA.Common/"]
RUN dotnet restore "./DRIYA.Platform/DRIYA.Platform.csproj"

# Copy the full source code into the container
COPY . .
WORKDIR "/src/DRIYA.Platform"
# Build the project
RUN dotnet build "./DRIYA.Platform.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image for generating the application artifacts
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DRIYA.Platform.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image for running the published application
FROM base AS final
WORKDIR /app
# Copy the published output from the publish stage
COPY --from=publish /app/publish .

# Set environment variables for production
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Configure entrypoint for the application
ENTRYPOINT ["dotnet", "DRIYA.Platform.dll"]
