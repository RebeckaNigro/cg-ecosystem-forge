# Base node image
FROM node:18-slim AS base

# Definir diretório de trabalho
WORKDIR /app

# Instalar dependências do sistema
RUN apt-get update && apt-get install -y curl sed

# Instalar pnpm e Azure Static Web Apps CLI globalmente
RUN npm install -g pnpm @azure/static-web-apps-cli

# Copiar arquivos de package para instalar dependências
COPY package.json pnpm-lock.yaml ./

# Instalar dependências do projeto
RUN pnpm install

# Copiar todo o código para o container
COPY . .

# Rodar comandos para gerar a constante e ajustar URLs
RUN echo "export const ID_SEM_INSTITUICAO=3" > ./src/utils/constantes.ts && \
    sed -i 's|https://hom-api\.ecossistemadeinovacaocg\.com\.br|'"$API_URL"'|g' src/utils/http/index.ts

# Build da aplicação
RUN pnpm build

# Copiar o arquivo de configuração para a pasta de build
RUN cp ./staticwebapp.config.json ./dist

# Comando padrão: deploy da app com token e URL
CMD ["sh", "-c", "npx @azure/static-web-apps-cli deploy ./dist --env production"]
