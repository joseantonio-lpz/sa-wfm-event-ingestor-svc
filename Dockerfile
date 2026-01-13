# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development
ENV TZ=America/Mexico_City
EXPOSE 8080
ENTRYPOINT ["dotnet", "WFM.EventIngestor.API.dll"]

# 1. Crea un volumen nombrado
# docker build -t wfm-producer .

# 2. Crea un volumen nombrado (solo necesitas hacerlo una vez)
# docker volume create dataprotection-keys

# 3. Inicia tu contenedor usando el volumen
# Asegúrate de mapear al puerto 8080 del contenedor (ver siguiente sección)
# docker run -d -p 5000:8080 -v dataprotection-keys:/root/.aspnet/DataProtection-Keys --name wfm-producer-app wfm-producer