# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore neurozen.API/neurozen.API.csproj
RUN dotnet publish neurozen.API/neurozen.API.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 10000
ENTRYPOINT ["sh","-c","ASPNETCORE_URLS=http://0.0.0.0:${PORT:-10000} dotnet neurozen.API.dll"]