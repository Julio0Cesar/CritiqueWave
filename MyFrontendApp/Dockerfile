# Imagem base do Node.js
FROM node:18 AS build

# Diretório de trabalho
WORKDIR /app

# Copia os arquivos do projeto
COPY . .

# Instala o React
RUN npm install

# Construi a aplicação
RUN npm run build

# NGINX é um servidor web, apine é uma versão mais leve
FROM nginx:alpine

# Copia os arquivos construídos para o Nginx
COPY --from=build /app/build /usr/share/nginx/html

# porta do frontend
EXPOSE 3000

# Comando para rodar o Nginx em primeiro plano
CMD ["nginx", "-g", "daemon off;"]
