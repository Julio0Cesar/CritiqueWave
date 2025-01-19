# Imagem base do .NET SDK para construir o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Diretório de trabalho
WORKDIR /app

# Copia dos arquivos
COPY . .

# versão otimizada do app e publica no diretorio out
# especificando a pasta do projeto para não incluir o MyBackendAppTest
RUN dotnet publish MyBackendApp.csproj -c Release -o out

# Imagem base para execução da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Diretório de trabalho
WORKDIR /app

# Copia dos arquivos compilados da imagem de build
COPY --from=build /app/out .

# porta do back
EXPOSE 5218

# Comando para rodar o app
ENTRYPOINT ["dotnet", "MyBackendApp.dll"]
