# Ecossistema local de inovação de Campo Grande - MS

Front-end do projeto do Ecossistema implementado utilizando Vue 3, TypeScript e Vite.

## IDE Setup

- [VSCode](https://code.visualstudio.com/) + [Volar](https://marketplace.visualstudio.com/items?itemName=johnsoncodehk.volar)

## Como executar o projeto

### Instalar dependências
```
npm install
```

### Iniciar ambiente de desenvolvimento
```
npm run dev
```

### Gerar arquivos para produção
!!!!
*Antes de executar este comando é importante mudar a url base da API para o respectivo ambiente:*

Caminho: /src/utils/http/index.ts
* Desenvolvimento: http://dev-ecossistemadeinovacaocg.com.br
* Homologação: https://hom-ecossistemadeinovacaocg.com.br
* Produção: https://ecossistemadeinovacaocg.com.br

!!!!
*Além de modificar o ID do tipo usuário sem instituição de acordo com o ambiente:*
Caminho: /src/utils/constantes.ts -> const ID_SEM_INSTITUICAO
* Desenvolvimento: 14
* Homologação: 5
* Produção: 3
```
npm run build
```
Os arquivos para publicação no servidor estão na pasta '/dist'.

#### Informações adicionais

[Repositório de componentes e funções prontas](https://dev.azure.com/startupsesims/Labs/_git/reutilizaveis)

[Documento de padrões a serem seguidos para novas funcionalidades/projetos](https://sesims.sharepoint.com/:w:/s/Startup513/EaMDAk2k_-REjXMhjqN65n0B1mlstVSkJeFwiLK80itMpA?e=T1hN2k)

[Documento de padrões para controle de versão](https://sesims.sharepoint.com/:w:/s/Startup513/ETF6DDxgyJ1CgaPOBmJQJ94BEW8DIYRZ0k9YHUChnX7AwQ?e=rSrCVU)