# 🚀 Avisos API – CRUD + Validações + Testes de Integração

# 🧪 Testes de Integração da API – Avisos

Este documento resume a camada de testes de integração desenvolvida para a aplicação.  
Os testes utilizam o padrão **Class Fixture**, executando a API em um ambiente real via `WebApplicationFactory`.

---

## ✅ Status Geral dos Testes

- **Total executado:** 32
- **Aprovados:** 32
- **Falhando:** 0

---

## 📦 Endpoints Testados

Todos os endpoints da rota `/avisos` foram validados via **testes de integração reais**:

- **GET /avisos**
- **GET /avisos/{id}**
- **POST /avisos**
- **PUT /avisos/{id}**
- **DELETE /avisos/{id}**
- **PUT /avisos/{id}/reativar**

---

## 🧪 Cenários Cobertos

### ✔ 1. `GET /avisos`

- Retorna lista de avisos com sucesso
- Retorna `204 NoContent` quando não há registros
- Testes incluem paginação

### ✔ 2. `GET /avisos/{id}`

- Retorna aviso existente
- Retorna `404 NotFound` se não existir
- Retorna `400 BadRequest` para ID inválido (0 ou negativo)

### ✔ 3. `POST /avisos`

- Cria um aviso com sucesso
- Valida campos obrigatórios (`Titulo`, `Mensagem`)
- Retorna `200 Ok` com o objeto criado

### ✔ 4. `PUT /avisos/{id}`

- Atualiza aviso existente corretamente
- Valida timestamps (`EditadoEm`)
- Retorna `404 NotFound` quando o aviso não existe
- Retorna `400 BadRequest` para ID ou Request inválido

### ✔ 5. `DELETE /avisos/{id}`

- Realiza soft delete
- Retorna `400 BadRequest` para ID inválido (0 ou negativo)
- Retorna `404 NotFound` para avisos já deletados ou inexistentes
- Retorna `200 Ok` quando o delete é bem-sucedido

### ✔ 6. `PUT /avisos/{id}/reactivar`

- Realiza soft delete
- Retorna `400 BadRequest` para ID inválido (0 ou negativo)
- Retorna `404 NotFound` para avisos já deletados ou inexistentes
- Retorna `200 Ok` quando a reativação é bem-sucedido

---

## 📊 Regras de Negócio Testadas

Os testes garantem o funcionamento das principais regras:

- Soft delete persistente
- Filtro automático de registros deletados
- Controle dos campos:
  - `CriadoEm`
  - `EditadoEm`

---

## ⚙️ Tecnologias Utilizadas

| Finalidade                 | Ferramenta                |
| -------------------------- | ------------------------- |
| Framework de testes        | **xUnit**                 |
| Assertions fluentes        | **FluentAssertions**      |
| Ambiente de testes da API  | **WebApplicationFactory** |
| Banco de dados para testes | **EF Core InMemory**      |
| Validações                 | **FluentValidation**      |
| Serialização               | `System.Text.Json`        |

---

## 🛠 Arquitetura dos Testes

- Padrão **Class Fixture** para reaproveitar o ambiente da API
- `HttpClient` real enviando requisições reais
- banco **isolado para cada execução**, garantindo repetibilidade
- Estrutura **Arrange → Act → Assert** em todos os testes que sejam necessários seguir esse padrão
- Dados criados e controlados diretamente pela própria API durante o teste

---

## 📌 Conclusão

A camada de testes de integração garante:

- 🛡 **Confiabilidade completa dos endpoints**
- 🔄 **Testes fim-a-fim do fluxo de Avisos**
- 📏 **Validação das regras de negócio reais**
- 🧹 **Isolamento, repetibilidade e previsibilidade**
- 🚀 **100% dos testes relacionados aos requisitos passando**

---
