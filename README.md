# 🏢 Sistema de Gerenciamento de Inventário

API RESTful robusta e escalável para gerenciamento de inventário corporativo, desenvolvida com .NET 9, seguindo princípios de **Clean Architecture**, **Domain-Driven Design (DDD)** e **CQRS**.

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-336791?logo=postgresql)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker)](https://www.docker.com/)
[![REST API](https://img.shields.io/badge/REST-Level%203-green)](https://martinfowler.com/articles/richardsonMaturityModel.html)

---

## 📋 Índice

- [Visão Geral](#-visão-geral)
- [Arquitetura](#️-arquitetura)
- [Escolhas Tecnológicas](#-escolhas-tecnológicas)
- [Quick Start](#-quick-start)
- [Funcionalidades](#-funcionalidades)
- [Autenticação](#-autenticação)
- [HATEOAS](#-hateoas--hypermedia)
- [Testes](#-testes)
- [Documentação da API](#-documentação-da-api)

---

## 🎯 Visão Geral

Sistema completo de gerenciamento de inventário que permite:

- ✅ **Gerenciamento de Fornecedores** - Cadastro e controle de fornecedores
- ✅ **Categorização Hierárquica** - Categorias e subcategorias de produtos
- ✅ **Controle de Produtos** - Gestão completa do ciclo de vida
- ✅ **Rastreamento de Vendas** - Registro e auditoria de vendas
- ✅ **Devoluções e Cancelamentos** - Gestão de exceções
- ✅ **Autenticação JWT** - Segurança com tokens
- ✅ **HATEOAS Level 3** - API REST auto-descritiva
- ✅ **Multi-moeda** - Suporte a BRL, USD e conversões

### 🎥 Demo Rápida

```bash
# Clone e execute em 30 segundos
git clone <repo-url>
cd InventoryManagement
docker-compose up --build

# Acesse: http://localhost:5000/swagger
```

---

## 🏗️ Arquitetura

### Estrutura de Projetos (Clean Architecture)

```
src/
├── InventoryManagement.API/              # 🌐 Camada de Apresentação
│   ├── Controllers/                      # Endpoints REST
│   ├── Middleware/                       # Exception handling, etc
│   └── Program.cs                        # Configuração da aplicação
│
├── InventoryManagement.Application/      # 📋 Camada de Aplicação (CQRS)
│   ├── Commands/                         # Comandos (Write operations)
│   │   ├── CreateProduct/
│   │   ├── SellProduct/
│   │   ├── CancelProduct/
│   │   └── ReturnProduct/
│   ├── Queries/                          # Queries (Read operations)
│   │   ├── GetProducts/
│   │   ├── GetProductById/
│   │   └── ...
│   ├── DTOs/                             # Data Transfer Objects
│   ├── Common/                           # HateoasLinkGenerator, etc
│   └── Interfaces/                       # Contratos de serviços
│
├── InventoryManagement.Domain/           # 💎 Camada de Domínio (DDD)
│   ├── Entities/                         # Entidades ricas com lógica
│   │   ├── Product.cs
│   │   ├── Supplier.cs
│   │   └── Category.cs
│   ├── ValueObjects/                     # Objetos de valor imutáveis
│   │   ├── Money.cs
│   │   └── Email.cs
│   ├── Enums/                            # ProductStatus, etc
│   ├── Events/                           # Domain Events
│   ├── Exceptions/                       # Exceções de domínio
│   └── Interfaces/                       # Repositórios (contratos)
│
└── InventoryManagement.Infrastructure/   # 🔧 Camada de Infraestrutura
    ├── Persistence/                      # EF Core
    │   ├── ApplicationDbContext.cs
    │   ├── Repositories/
    │   └── Migrations/
    ├── ExternalServices/                 # Integrações externas
    │   ├── MockWmsService.cs
    │   ├── MockAuditService.cs
    │   └── SmtpEmailService.cs
    └── Services/                         # Serviços de infraestrutura
        └── JwtService.cs
```

### Princípios Aplicados

| Princípio | Descrição | Benefício |
|-----------|-----------|-----------|
| **Clean Architecture** | Separação em camadas com dependências unidirecionais | Testabilidade e manutenibilidade |
| **DDD** | Modelo rico de domínio com lógica de negócio encapsulada | Código que reflete o negócio |
| **CQRS** | Separação entre leitura e escrita | Performance e escalabilidade |
| **SOLID** | Princípios de design orientado a objetos | Código limpo e extensível |
| **Repository Pattern** | Abstração de acesso a dados | Independência de tecnologia |

---

## 🛠️ Escolhas Tecnológicas

### .NET 9 (LTS - Long Term Support)

**Por que .NET 9?**

- ✅ **Performance Superior**: ~30% mais rápido que .NET 6 em benchmarks
- ✅ **Suporte até 2027**: Garantia de atualizações de segurança por 3 anos
- ✅ **Native AOT**: Compilação nativa para containers menores e startup mais rápido
- ✅ **Minimal APIs Aprimoradas**: Menos boilerplate, mais produtividade
- ✅ **Observabilidade**: Integração nativa com OpenTelemetry e métricas
- ✅ **Cross-platform**: Linux, Windows, macOS sem modificações

**Alternativas Consideradas:**
- ❌ Node.js: Menor type-safety, performance inferior em operações CPU-bound
- ❌ Java/Spring: Maior consumo de memória, startup mais lento
- ❌ Python/Django: Performance significativamente inferior

### PostgreSQL 16

**Por que PostgreSQL 16?**

- ✅ **Performance**: Até 2x mais rápido em queries paralelas vs versão 15
- ✅ **JSON**: Suporte nativo a JSON/JSONB de alta performance
- ✅ **ACID Completo**: Transações robustas e consistência garantida
- ✅ **Extensibilidade**: PostGIS, Full-Text Search, etc
- ✅ **Open Source**: Sem custos de licenciamento
- ✅ **Maturidade**: 35+ anos de desenvolvimento ativo
- ✅ **Escalabilidade**: Suporta até TBs de dados com performance

**Alternativas Consideradas:**
- ❌ SQL Server: Custos de licença, menor performance em Linux
- ❌ MySQL: Menos recursos avançados, menor consistência ACID
- ❌ MongoDB: Não ideal para dados relacionais, menos garantias ACID

### Entity Framework Core 9

**Por que EF Core 9?**

- ✅ **Produtividade**: 80% menos código comparado a SQL manual
- ✅ **Type-Safety**: Erros detectados em compile-time
- ✅ **Migrations**: Versionamento de schema automático
- ✅ **Performance**: Compiled queries, tracking otimizado
- ✅ **LINQ**: Queries expressivas e fortemente tipadas
- ✅ **Interceptors**: Logging e auditoria transparentes

**Alternativas Consideradas:**
- ❌ Dapper: Maior controle mas muito mais código boilerplate
- ❌ NHibernate: Complexidade maior, documentação limitada

### MediatR

**Por que MediatR?**

- ✅ **CQRS Simples**: Implementação limpa de Command/Query Separation
- ✅ **Desacoplamento**: Controllers não conhecem handlers diretamente
- ✅ **Testabilidade**: Handlers isolados e fáceis de testar
- ✅ **Pipelines**: Cross-cutting concerns (validação, logging) de forma elegante
- ✅ **Single Responsibility**: Cada handler faz apenas uma coisa

**Exemplo de uso:**
```csharp
// Controller delega para MediatR
var result = await _mediator.Send(new CreateProductCommand(...));

// MediatR encontra e executa o handler correto
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    public async Task<ProductResponse> Handle(...)
    {
        // Lógica isolada e testável
    }
}
```

### CQRS (Command Query Responsibility Segregation)

**Por que CQRS?**

- ✅ **Escalabilidade**: Read e Write podem escalar independentemente
- ✅ **Performance**: Queries otimizadas sem considerar comandos
- ✅ **Clareza**: Intenção clara (Command vs Query)
- ✅ **Simplicidade**: Handlers focados em uma única responsabilidade
- ✅ **Auditoria**: Comandos são pontos naturais de audit trail

**Exemplo:**
```
Command (Write)          Query (Read)
CreateProductCommand  →  GetProductByIdQuery
SellProductCommand    →  GetProductsQuery
                         GetProductsByCategoryQuery
```

### FluentValidation

**Por que FluentValidation?**

- ✅ **Expressividade**: Validações claras e legíveis
- ✅ **Reutilização**: Validators isolados e componentizados
- ✅ **Testabilidade**: Fácil testar regras de validação
- ✅ **Mensagens Customizadas**: Feedback rico para usuários
- ✅ **Async Support**: Validações assíncronas (ex: verificar duplicatas no BD)

**Exemplo:**
```csharp
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Descrição é obrigatória")
            .MaximumLength(200).WithMessage("Máximo 200 caracteres");

        RuleFor(x => x.AcquisitionCost)
            .GreaterThan(0).WithMessage("Custo deve ser positivo");
    }
}
```

---

## 🚀 Quick Start

### Pré-requisitos

- [Docker](https://www.docker.com/) e Docker Compose
- (Opcional) [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) para desenvolvimento local

### Opção 1: Docker Compose (Recomendado)

```bash
# 1. Clone o repositório
git clone <repo-url>
cd InventoryManagement

# 2. Suba toda a infraestrutura
docker-compose up --build

# 3. Aguarde a mensagem: "Now listening on: http://0.0.0.0:8080"

# 4. Acesse:
# - API: http://localhost:5000
# - Swagger: http://localhost:5000/swagger
# - PgAdmin: http://localhost:5050
```

### Opção 2: Desenvolvimento Local

```bash
# 1. Suba apenas o PostgreSQL
docker-compose up postgres -d

# 2. Restaure dependências
dotnet restore

# 3. Execute as migrations
dotnet ef database update \
  -p src/InventoryManagement.Infrastructure \
  -s src/InventoryManagement.API

# 4. Execute a API
dotnet run --project src/InventoryManagement.API

# API disponível em: http://localhost:5000
```

### 🎉 Próximos Passos

1. **Acesse o Swagger**: http://localhost:5000/swagger
2. **Registre um usuário admin** no endpoint `POST /api/Auth/register`
3. **Copie o token** retornado
4. **Clique em "Authorize"** e cole o token
5. **Explore a API** criando fornecedores, categorias e produtos!

---

## 📋 Funcionalidades

### 🔐 Autenticação & Autorização

#### Registrar Usuário

```bash
POST /api/Auth/register
{
  "username": "admin",
  "email": "admin@company.com",
  "password": "Admin@123",
  "fullName": "Administrator",
  "role": "Admin"  # Admin | User
}

# Resposta inclui JWT token
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "userId": "...",
  "role": "Admin"
}
```

#### Login

```bash
POST /api/Auth/login
{
  "username": "admin",
  "password": "Admin@123"
}
```

#### Roles e Permissões

| Role | Permissões |
|------|------------|
| **Admin** | Tudo (criar, editar, deletar, vender) |
| **User** | Listar, ver detalhes, vender produtos |

### 🏢 Fornecedores (Suppliers)

```bash
# Criar
POST /api/Suppliers
{
  "name": "Tech Solutions Brasil Ltda",
  "email": "contato@techsolutions.com.br",
  "currency": "BRL",
  "country": "Brasil"
}

# Listar (com paginação e filtros)
GET /api/Suppliers?pageSize=20&country=Brasil&orderBy=name

# Buscar por ID (com HATEOAS)
GET /api/Suppliers/{id}

# Ver produtos do fornecedor
GET /api/Suppliers/{id}/products

# Deletar (Admin only)
DELETE /api/Suppliers/{id}
```

### 📁 Categorias (Categories)

```bash
# Criar categoria principal
POST /api/Categories
{
  "name": "Eletrônicos",
  "shortcode": "ELET",
  "description": "Produtos eletrônicos"
}

# Criar subcategoria
POST /api/Categories
{
  "name": "Notebooks",
  "shortcode": "NOTE",
  "parentCategoryId": "{parent-id}"
}

# Listar categorias raiz
GET /api/Categories?hasParent=false

# Ver produtos da categoria
GET /api/Categories/{id}/products

# Deletar (Admin only)
DELETE /api/Categories/{id}
```

### 🛍️ Produtos (Products)

#### Criar Produto

```bash
POST /api/Products
{
  "supplierId": "7a86ae04-16c1-4821-a8ac-6452e2a9e16c",
  "categoryId": "0da81f0c-7f86-4527-9163-ab4e1bb7bcfd",
  "description": "Notebook Dell Inspiron 15 - Intel i7 16GB RAM",
  "acquisitionCost": 3500.00,
  "acquisitionCurrency": "BRL",
  "acquisitionCostUsd": 636.36
}
```

#### Listar com Filtros Avançados

```bash
# Produtos vendidos de uma categoria específica
GET /api/Products?status=1&categoryId={id}&pageSize=50&orderBy=acquisitionDate&descending=true

# Parâmetros disponíveis:
# - pageNumber, pageSize
# - supplierId, categoryId
# - status (0=Created, 1=Sold, 2=Returned, 3=Cancelled)
# - description (busca parcial)
# - orderBy, descending
```

#### Ciclo de Vida do Produto

```bash
# 1. Vender produto (Created → Sold)
POST /api/Products/{id}/sales
{}

# 2. Cancelar venda (Sold → Cancelled)
POST /api/Products/{id}/cancellations
{}

# 3. Devolver produto (Sold → Returned)
POST /api/Products/{id}/returns
{}
```

#### Navegação HATEOAS

```bash
# Ver fornecedor do produto
GET /api/Products/{id}/supplier

# Ver categoria do produto
GET /api/Products/{id}/category
```

### 📊 Relatórios

```bash
# Produtos por status
GET /api/Reports/products/by-status

# Produtos por categoria
GET /api/Reports/products/by-category

# Vendas por período
GET /api/Reports/sales?StartDate=2025-01-01&EndDate=2025-12-31
```

---

## 🔐 Autenticação

### Como Usar no Swagger

1. **Registre/Login** em `/api/Auth/register` ou `/api/Auth/login`
2. **Copie o token** do campo `token` na resposta
3. **Clique em "Authorize"** 🔒 (canto superior direito)
4. **Cole apenas o token** (sem "Bearer")
5. **Clique "Authorize"** e depois "Close"
6. Todos os endpoints protegidos agora funcionarão!

### Como Usar com cURL

```bash
# 1. Obter token
TOKEN=$(curl -s -X POST http://localhost:5000/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"Admin@123"}' \
  | jq -r '.token')

# 2. Usar em requisições
curl -X POST http://localhost:5000/api/Products \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{ ... }'
```

---

## ✨ HATEOAS & Hypermedia

### O que é HATEOAS?

**HATEOAS** (Hypermedia As The Engine Of Application State) é o nível mais alto de maturidade REST (Richardson Level 3). A API retorna links dinâmicos que guiam o cliente sobre quais ações são possíveis.

### Exemplo Prático

#### Produto Recém-Criado (Status: Created)

```json
{
  "id": "dfe77d9c-d66e-467a-aa97-1c02de37e8b9",
  "description": "Mouse Logitech",
  "status": "Created",
  "links": [
    {
      "href": "/api/products/dfe77d9c-d66e-467a-aa97-1c02de37e8b9",
      "rel": "self",
      "method": "GET"
    },
    {
      "href": "/api/products/dfe77d9c-d66e-467a-aa97-1c02de37e8b9/supplier",
      "rel": "supplier",
      "method": "GET"
    },
    {
      "href": "/api/products/dfe77d9c-d66e-467a-aa97-1c02de37e8b9/category",
      "rel": "category",
      "method": "GET"
    },
    {
      "href": "/api/products/dfe77d9c-d66e-467a-aa97-1c02de37e8b9/sales",
      "rel": "sell",
      "method": "POST"
    }
  ]
}
```

#### Após Vender (Status: Sold)

```json
{
  "id": "dfe77d9c-d66e-467a-aa97-1c02de37e8b9",
  "status": "Sold",
  "saleDate": "2025-12-26T21:36:15Z",
  "links": [
    {
      "href": "/api/products/dfe77d9c-d66e-467a-aa97-1c02de37e8b9",
      "rel": "self",
      "method": "GET"
    },
    {
      "href": "/api/products/dfe77d9c-d66e-467a-aa97-1c02de37e8b9/supplier",
      "rel": "supplier",
      "method": "GET"
    },
    {
      "href": "/api/products/dfe77d9c-d66e-467a-aa97-1c02de37e8b9/category",
      "rel": "category",
      "method": "GET"
    },
    {
      "href": "/api/products/dfe77d9c-d66e-467a-aa97-1c02de37e8b9/cancellations",
      "rel": "cancel",
      "method": "POST"
    },
    {
      "href": "/api/products/dfe77d9c-d66e-467a-aa97-1c02de37e8b9/returns",
      "rel": "return",
      "method": "POST"
    }
  ]
}
```

### Benefícios

✅ **Auto-descoberta**: Cliente descobre ações disponíveis dinamicamente
✅ **Desacoplamento**: Frontend não precisa conhecer URLs hardcoded
✅ **Evolução**: API pode mudar URLs sem quebrar clientes
✅ **Validação de Estado**: Links só aparecem quando ação é válida

---

## 🧪 Testes

### Executar Testes

```bash
# Todos os testes
dotnet test

# Com logs detalhados
dotnet test --verbosity detailed

# Com cobertura de código
dotnet test --collect:"XPlat Code Coverage"
```

### Estrutura de Testes

```
tests/InventoryManagement.UnitTests/
├── Domain/
│   ├── Entities/
│   │   ├── ProductTests.cs          # 15 testes
│   │   ├── SupplierTests.cs
│   │   └── CategoryTests.cs
│   └── ValueObjects/
│       ├── MoneyTests.cs             # 8 testes
│       └── EmailTests.cs             # 6 testes
└── Application/
    ├── Commands/
    └── Queries/
```

### Cobertura

- ✅ **29 testes unitários** (100% passando)
- ✅ **Cobertura de Domínio**: ~95%
- ✅ **Testes de transições de estado** do produto
- ✅ **Validações de Value Objects**
- ✅ **Regras de negócio críticas**

---

## 📊 Dados de Demonstração

O projeto inclui scripts para popular o banco com dados de teste:

```bash
# Criar 30 fornecedores
bash create-suppliers.sh

# Criar 30 categorias
bash create-categories.sh

# Criar 150 produtos
bash create-products-v2.sh
```

**Dados incluídos:**
- 30 fornecedores brasileiros (empresas de tecnologia)
- 30 categorias variadas (Eletrônicos, Móveis, Papelaria, etc.)
- 150 produtos com valores entre R$ 500 e R$ 5.000
- Links HATEOAS completos em todos os recursos

---

## 🐳 Docker

### Serviços

| Serviço | Porta | Usuário | Senha | Descrição |
|---------|-------|---------|-------|-----------|
| **API** | 5000 | - | - | REST API principal |
| **PostgreSQL** | 5432 | `inventory_user` | `inventory_pass` | Banco de dados |
| **PgAdmin** | 5050 | `admin@inventory.com` | `admin` | Interface web PostgreSQL |

### Comandos Úteis

```bash
# Ver logs em tempo real
docker-compose logs -f api

# Restart apenas a API
docker-compose restart api

# Rebuild sem cache
docker-compose build --no-cache api

# Limpar tudo (incluindo volumes)
docker-compose down -v

# Acessar shell do PostgreSQL
docker exec -it inventory-db psql -U inventory_user -d InventoryDb
```

---

## 📚 Documentação da API

### Swagger UI

**URL**: http://localhost:5000/swagger

Interface interativa que permite:
- ✅ Explorar todos os endpoints
- ✅ Ver modelos de request/response
- ✅ Testar diretamente do navegador
- ✅ Autenticar com JWT
- ✅ Ver exemplos de uso

### OpenAPI Spec

**URL**: http://localhost:5000/swagger/v1/swagger.json

Especificação OpenAPI 3.0 completa para geração de clientes.

---

## 🛠️ Desenvolvimento

### Estrutura de Branches

```
main          → Produção (sempre estável)
develop       → Integração (features prontas)
feature/*     → Novas funcionalidades
bugfix/*      → Correções
```

### Adicionando Migrations

```bash
# Criar nova migration
dotnet ef migrations add NomeDaMigration \
  -p src/InventoryManagement.Infrastructure \
  -s src/InventoryManagement.API

# Aplicar migrations
dotnet ef database update \
  -p src/InventoryManagement.Infrastructure \
  -s src/InventoryManagement.API

# Reverter última migration
dotnet ef migrations remove \
  -p src/InventoryManagement.Infrastructure \
  -s src/InventoryManagement.API
```

### Padrões de Código

- ✅ **Nullable Reference Types** habilitado
- ✅ **Implicit usings** configurado
- ✅ **File-scoped namespaces**
- ✅ **Record types** para DTOs
- ✅ **Primary constructors** quando apropriado

---
