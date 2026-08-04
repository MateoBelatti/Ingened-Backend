# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["Core/Core.csproj", "Core/"]
COPY ["Ingened/Api.csproj", "Ingened/"]
RUN dotnet restore "Ingened/Api.csproj"

# Copy the rest of the source code
COPY . .

# Publish the application
RUN dotnet publish "Ingened/Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port (Railway binds to PORT, ASP.NET 10.0 listens on 8080 by default)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Api.dll"]
