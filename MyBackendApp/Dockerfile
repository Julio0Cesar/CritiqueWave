# Iniciando o projeto docker:
#
# docker build -t <Nome da imagem>:dev .
# docker run --name <Nome do container> -it --rm -p 5218:5218 -v "$(pwd):/app" <Nome da imagem> dotnet watch run
#
# OU
#
# docker build -t <Nome da imagem> .
# docker run --name <Nome do container> -p 5218:5218 <Nome da imagem>
#
#----------------------------------------
#
# Verificando os containers:
#
# docker ps -a
#
#----------------------------------------
#
# Acessando o container:
#
# docker exec -it <CONTAINER_ID ou NOME> sh
#
#----------------------------------------
#
# Reiniciar o container:
#
# docker stop <CONTAINER_ID ou NOME>
# docker start <CONTAINER_ID ou NOME>
#
#----------------------------------------
#
# Renomeando o container:
#
# docker rename nome_antigo novo_nome
#
#----------------------------------------
#
# Removendo containers:
# 
# docker rm <CONTAINER_ID ou NOME>
# docker container prune                (Remove todos parados de uma vez)
#
#----------------------------------------
#
# Removendo imagens
#
# docker rmi <IMAGE_ID>
# docker rmi -f <IMAGE_ID>              (Forçando a remoção)
#
#----------------------------------------
#
# Apagando tudo do docker
#
# docker rm -f $(docker ps -aq)         (remove todos os containers ativos e parados)
# docker system prune -a --volumes      (Apaga todas as imagens, redes, volumes parados)
#

# Imagem base do .NET SDK para construir o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Diretório de trabalho
WORKDIR /app

# Copia apenas o arquivo de projeto primeiro para aproveitar cache
#COPY MyBackendApp.csproj ./ (comando para dockerfile )
#COPY MyBackendApp/MyBackendApp.csproj ./ (comando para docker-compose)
COPY MyBackendApp/MyBackendApp.csproj ./
RUN dotnet restore

# Agora copia o restante do código
COPY . .

# Compila antes de rodar para evitar erros de código quebrado
#RUN dotnet build MyBackendApp.csproj -c Debug -o out (comando para dockerfile)
#RUN dotnet build MyBackendApp/MyBackendApp.csproj -c Debug -o out (comando para docker-compose)
RUN dotnet build MyBackendApp/MyBackendApp.csproj -c Debug -o out

# Expõe a porta
EXPOSE 5218

# Comando para rodar o backend em desenvolvimento
CMD ["dotnet", "watch", "run"]
