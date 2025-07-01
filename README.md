## 🧾 Projeto: Sistema de Transações Bancárias

Este projeto simula um sistema básico de gerenciamento de contas e transações bancárias, utilizando:

- .NET 8 (API)
- Angular (Frontend)
- DynamoDB (como banco NoSQL)
- Docker (para desenvolvimento local)

---

### 📁 Estrutura do Projeto

```
/api
    ├── Controllers
    ├── DTOs
    ├── Entities
    ├── Repository
    ├── Services
    └── Program.cs
/frontend
    src/
    ├── app/
    │   ├── assets/
    │   ├── components/
    │   ├── models/
    │   ├── services/
    ├── environments/
```

---

### ⚙️ Tecnologias Utilizadas

- ✅ .NET 8
- ✅ Angular
- ✅ AWS DynamoDB Local (via Docker)
- ✅ Swagger (para documentação da API)
- ✅ Docker Compose (para rodar ambiente local)

---

### 🚀 Como Executar o Projeto

#### Pré-requisitos:

- [Docker](https://www.docker.com/)

#### 1. Clonar o repositório

```bash
git clone https://github.com/PauloZanluqui/Teste-Tecnico-BTG-Pactual.git
cd Teste-Tecnico-BTG-Pactual
```

#### 2. Rodar o Docker Compose

```bash
docker compose up -d
```

#### 3. Acessar o Frontend

[http://localhost:8081](http://localhost:8081)

#### 4. Acessar o Swagger

[http://localhost:8081/api/swagger](http://localhost:8081/api/swagger)

---

### 📌 Funcionalidades

- **Contas**

  - Criar conta
  - Buscar por CPF
  - Listar todas as contas
  - Alterar limite
  - Deletar conta

- **Transações**

  - Criar transação
  - Listar transações por ID
  - Listar todas as transações

---

### 🛠️ Padrões e Boas Práticas

- Clean Architecture (separação em Controllers, Services, Repositories, DTOs)
- DDD básico
- Validações de regra de negócio:

  - CPF único
  - Conta + agência única
  - Limite disponível

- Uso de DTOs para entrada/saída

---

### 🧑‍💻 Autor

- **Paulo Zanluqui**
- Email: [paulozanluqui13@gmail.com](mailto:paulozanluqui13@gmail.com)
- GitHub: [github.com/PauloZanluqui](https://github.com/PauloZanluqui)

---
