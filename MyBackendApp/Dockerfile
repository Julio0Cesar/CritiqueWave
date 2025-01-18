# Use a imagem base do .NET SDK para construir o projeto
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Defina o diretório de trabalho
WORKDIR /app

# Copie os arquivos do projeto
COPY . .

# Publique a aplicação
RUN dotnet publish -c Release -o out

# Imagem base para execução da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Defina o diretório de trabalho
WORKDIR /app

# Copie os arquivos compilados da imagem de build
COPY --from=build /app/out .

# Exponha a porta que o seu backend vai rodar
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "SeuProjeto.dll"]
