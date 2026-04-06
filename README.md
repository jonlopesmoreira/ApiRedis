# ApiRedis

API REST em **.NET 10** demonstrando o uso de **Redis** como cache distribuído, com conexão ao **Redis Cloud**.

## 🛠️ Tecnologias

- [.NET 10](https://dotnet.microsoft.com/)
- [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core)
- [StackExchange.Redis](https://stackexchange.github.io/StackExchange.Redis/)
- [Microsoft.Extensions.Caching.StackExchangeRedis](https://www.nuget.org/packages/Microsoft.Extensions.Caching.StackExchangeRedis)
- [Redis Cloud](https://redis.io/cloud/)
- Swagger / OpenAPI

## 📐 Arquitetura

```
ApiRedis/
├── Controllers/
│   ├── WeatherForecastController.cs  → Cache com TTL de 30s
│   └── HealthController.cs           → Health check do Redis
├── Services/
│   ├── ICacheService.cs              → Interface genérica de cache
│   └── RedisCacheService.cs          → Implementação com IDistributedCache
├── appsettings.json                  → Configurações (sem credenciais)
└── Program.cs                        → Registro do Redis e serviços
```

## 🚀 Como executar localmente

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Uma instância Redis (local ou [Redis Cloud](https://redis.io/cloud/))

### 1. Clone o repositório

```bash
git clone https://github.com/jonlopesmoreira/ApiRedis.git
cd ApiRedis
```

### 2. Configure a connection string via User Secrets

```bash
cd ApiRedis
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:Redis" "seu-host:porta,password=sua-senha,user=default,ssl=False,abortConnect=False"
```

> ⚠️ **Nunca** insira credenciais diretamente no `appsettings.json`.

### 3. Execute a aplicação

```bash
dotnet run
```

Acesse o Swagger em: `https://localhost:7058`

## 📡 Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/WeatherForecast` | Retorna previsão do tempo (cache de 30s) |
| `DELETE` | `/WeatherForecast/cache` | Limpa o cache manualmente |
| `GET` | `/Health/redis` | Verifica a saúde da conexão Redis |

### Exemplo — GET /WeatherForecast

```json
[
  {
    "date": "2026-04-07",
    "temperatureC": 32,
    "temperatureF": 89,
    "summary": "Hot"
  }
]
```

### Exemplo — GET /Health/redis

```json
{
  "status": "healthy",
  "message": "Redis write/read/delete OK",
  "timestamp": "2026-04-06T17:00:00Z"
}
```

## 💡 Como o cache funciona

```
GET /WeatherForecast
        │
        ▼
  Busca no Redis
        │
   ┌────┴────┐
   │ Hit     │ Miss
   ▼         ▼
Retorna   Gera dados
 cache    Salva no Redis (30s TTL)
              │
              ▼
          Retorna dados
```

## 🔐 Segurança de credenciais

| Ambiente | Configuração |
|----------|-------------|
| **Desenvolvimento** | [.NET User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) |
| **Produção** | Variável de ambiente `ConnectionStrings__Redis` |

## 📦 Pacotes NuGet

| Pacote | Versão |
|--------|--------|
| `Microsoft.AspNetCore.OpenApi` | 10.0.5 |
| `Microsoft.Extensions.Caching.StackExchangeRedis` | 10.0.5 |
| `Swashbuckle.AspNetCore.SwaggerUI` | 10.1.7 |

## 📄 Licença

Este projeto está sob a licença MIT.
- [Redis Cloud](https://redis.io/cloud/)
- Swagger / OpenAPI

## 📐 Arquitetura

## 🚀 Como executar localmente

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Uma instância Redis (local ou [Redis Cloud](https://redis.io/cloud/))

### 1. Clone o repositório

```bash
git clone https://github.com/seuusuario/seurepositorio.git
cd seurepositorio
```

### 2. Configure a conexão com o Redis

Abra o arquivo `appsettings.json` e ajuste as configurações de conexão conforme necessário.

> ⚠️ **Nunca** insira credenciais diretamente no `appsettings.json`.

### 3. Execute a aplicação

Acesse o Swagger em: `https://localhost:7058`

## 📡 Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/WeatherForecast` | Retorna previsão do tempo (cache de 30s) |
| `DELETE` | `/WeatherForecast/cache` | Limpa o cache manualmente |
| `GET` | `/Health/redis` | Verifica a saúde da conexão Redis |

### Exemplo — Health Check

## 🔐 Segurança de credenciais

| Ambiente | Configuração |
|----------|-------------|
| **Desenvolvimento** | [.NET User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) |
| **Produção** | Variável de ambiente `ConnectionStrings__Redis` |

## 📦 Pacotes NuGet

| Pacote | Versão |
|--------|--------|
| `Microsoft.AspNetCore.OpenApi` | 10.0.5 |
| `Microsoft.Extensions.Caching.StackExchangeRedis` | 10.0.5 |
| `Swashbuckle.AspNetCore.SwaggerUI` | 10.1.7 |

## 📜 Versionamento

Este projeto utiliza [SemVer](https://semver.org/) para controle de versão. Para versões anteriores, consulte as tags neste repositório.

## 📄 Licença

Este projeto está sob a licença MIT.

## 📝 Notas adicionais

Instruções para deploy, contribuição e informações adicionais podem ser incluídas em seções futuras.
