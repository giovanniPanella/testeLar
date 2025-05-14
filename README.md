# testeLar API (.NET 8 + MongoDB)

API RESTful desenvolvida em .NET 8 com MongoDB, no padrão MVC, para cadastro de pessoas e seus respectivos telefones.

## O que Usei no Projeto

- ASP.NET Core 8
- MongoDB
- Swagger (documentação)
- Insomnia (testes de requisições)
- Padrão MVC

---

## Estrutura da API

A entidade principal é `Pessoa`, com os seguintes campos:

- `nome` (string)
- `cpf` (string)
- `dataDeNascimento` (DateTime)
- `status` (bool)
- `telefones` (lista de objetos do tipo `Telefone`)

Cada `Telefone` possui:

- `tipo` (Celular, Residencial, Comercial)
- `numero` (string)

---

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community)

> A aplicação depende de um banco MongoDB local rodando em `mongodb://localhost:27017`

---

## Como rodar o projeto

1. Clone este repositório:

```bash
git clone https://github.com/seuusuario/testeLar.git
cd testeLar
```
2. Restaure as dependências:
```bash
dotnet restore
```
3. Rode a Aplicação:
```bash
dotnet run
```
4. Acesse a API via Swagger:
http://localhost:5140/swagger


## Endpoints principais

### POST
```bash
http://localhost:5140/api/pessoa

{
  "nome": "Giovanni Gilles Panella",
  "cpf": "123.456.789-00",
  "dataDeNascimento": "1983-09-12T00:00:00",
  "status": true,
  "telefones": [
    {
      "tipo": "Celular",
      "numero": "99999-1234"
    },
    {
      "tipo": "Residencial",
      "numero": "3333-5678"
    }
  ]
}

```

### GET
Lista todas as pessoas ativas.
```bash
http://localhost:5140/api/pessoa

```

### GET /api/pessoa/{id}
Lista Pessoa Específica por ID.
```bash
http://localhost:5140/api/pessoa/{id}

```
### PUT /api/pessoa/{id}
Edita Pessoa por ID.
```bash
http://localhost:5140/api/pessoa/{id}

{
  "nome": "Giovanni Gilles Panella",
  "cpf": "123.456.789-00",
  "dataDeNascimento": "1983-09-12T00:00:00",
  "status": False,
  "telefones": [
    {
      "tipo": "Celular",
      "numero": "99999-1234"
    },
    {
      "tipo": "Residencial",
      "numero": "3333-5678"
    }
  ]
}

```


### Delete /api/pessoa/{id}
Deleta Pessoa por ID.
```bash
http://localhost:5140/api/pessoa/{id}

```

## Organização do Projeto
```bash
testeLar/
├── Controllers/      # Endpoints da API
├── Models/           # Entidades e DTOs
├── Services/         # Regras de negócio
├── Data/             # Configuração do MongoDB
├── appsettings.json  # Configs da aplicação
├── Program.cs        # Startup do app
```
## Logs

Este Teste utiliza Serilog para gerar logs das requisições:

- Logs são salvos em `logs/log.txt`
- Incluem ações como criação, atualização e exclusão de pessoas


## Autor
Desenvolvido por Giovanni Gilles Panella
ggpanella@gmail.com