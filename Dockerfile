FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["Ingened/Api.csproj", "Ingened/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "Ingened/Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Ingened"
RUN dotnet build "Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Exponer el puerto que usará Railway (suele inyectar la variable PORT)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Api.dll"]
