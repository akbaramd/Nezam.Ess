# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage with .NET 9.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
COPY ["Nezam.EES.Service.Identity/Nezam.EES.Service.Identity.csproj", "Nezam.EES.Service.Identity/"]
COPY ["New/Payeh.SharedKernel/Payeh.SharedKernel.csproj", "New/Payeh.SharedKernel/"]
RUN dotnet restore "Nezam.EES.Service.Identity/Nezam.EES.Service.Identity.csproj"

# Copy source code and build the project
COPY . .
WORKDIR "/src/Nezam.EES.Service.Identity"
RUN dotnet build "Nezam.EES.Service.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Nezam.EES.Service.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nezam.EES.Service.Identity.dll"]
