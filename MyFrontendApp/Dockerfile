# Iniciando o projeto docker:
#
# docker build -t <Nome da imagem>:dev .
# docker run --name <Nome do container> -it --rm -p 3000:3000 -v "$(pwd):/app" <Nome da imagem> npm start
#
# OU
#
# docker build -t <Nome da imagem> .
# docker run --name <Nome do container> -p 3000:3000 <Nome da imagem>
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
# Reinicie o container:
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
# docker container prune               (Remove todos parados de uma vez)
#
#----------------------------------------
#
# Removendo imagens
#
# docker rmi <IMAGE_ID>
# docker rmi -f <IMAGE_ID>            (Forçando a remoção)
#
#----------------------------------------
#
# Apagando tudo do docker
#
# docker rm -f $(docker ps -aq)         (remove todos os containers ativos e parados)
# docker system prune -a --volumes      (Apaga todas as imagens, redes, volumes parados)
#
# Imagem base do Node.js
FROM node:18 AS build

# Diretório de trabalho
WORKDIR /app

# Copia os arquivos do projeto

#COPY package*.json ./ (comando para dockerfile)
#COPY MyFrontendApp/package*.json ./ (comando para docker-compose)
COPY MyFrontendApp/package*.json ./

# Instala as dependências
RUN npm install

# Copia o restante dos arquivos
#COPY . . (comando para dockerfile)
#COPY MyFrontendApp/ . (comando para docker-compose)
COPY MyFrontendApp/ .

# Expõe a porta
EXPOSE 3000

# Comando para rodar o npm
CMD ["npm", "start"]