# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# Copy csproj and restore dependencies
COPY neurozen.API.sln .
COPY neurozen.API/*.csproj ./neurozen.API/
RUN dotnet restore

# Copy everything else and build
COPY neurozen.API/. ./neurozen.API/
WORKDIR /source/neurozen.API
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Set environment variable for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "neurozen.API.dll"]

