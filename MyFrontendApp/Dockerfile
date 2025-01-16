# Use uma imagem base do Node.js
FROM node:18 AS build

# Defina o diretório de trabalho
WORKDIR /app

# Copie os arquivos do projeto
COPY . .

# Instale as dependências do React
RUN npm install

# Construa a aplicação
RUN npm run build

# Use uma imagem base do Nginx para servir os arquivos estáticos
FROM nginx:alpine

# Copie os arquivos construídos para o Nginx
COPY --from=build /app/build /usr/share/nginx/html

# Exponha a porta onde o frontend vai rodar
EXPOSE 80

# Comando para rodar o Nginx
CMD ["nginx", "-g", "daemon off;"]
