# Stage  1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Coiar el proyecto y restaurar dependecias
COPY SentimentApi.csproj ./
RUN dotnet restore

# Copiar codigo fuente
COPY . ./
RUN dotnet publish -c Release -o out

# Stage  2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "SentimentApi.dll"]