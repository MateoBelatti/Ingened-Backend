# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos csproj y restaurar dependencias
COPY ["Core/Core.csproj", "Core/"]
COPY ["Ingened/Api.csproj", "Ingened/"]
RUN dotnet restore "Ingened/Api.csproj"

# Copiar el resto del código fuente
COPY . .

# Publicar la aplicación
RUN dotnet publish "Ingened/Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto que usará Railway (suele inyectar la variable PORT)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Api.dll"]