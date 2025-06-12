# UCondo-Challenge

Projeto para desafio técnico, desenvolvido com tecnologias modernas para garantir escalabilidade, organização e facilidade de manutenção.

---

## ✨ Tecnologias Utilizadas

- **.NET 8.0**
- **Clean Architecture**
- **CQRS**
- **PostgreSQL** (via Docker)
- **Redis** (via Docker)

---

## 🚀 Funcionalidades

- Estrutura modular baseada em Clean Architecture
- Separação clara com CQRS
- Migrations automáticas e ambiente consistente via Docker
- Dados iniciais gerados por seeder automático

---

## 🛠️ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) instalado
- [Docker](https://www.docker.com/get-started/) e [Docker Compose](https://docs.docker.com/compose/) instalados
- IDE de sua preferência (Visual Studio, Rider, VSCode, etc)

---

## 🎲 Como Executar

**1. Com Docker Compose (_recomendado_):**

Abra o terminal na raiz do projeto e execute:
```bash
docker-compose up -d
```
---

## ✅ Todos

- [ ] Criar Testes Unitários
- [ ] Criar Componente de Log, para padronizar e melhorar a observabilidade
- [ ] Criar Payload padrão de retorno
- [ ] Criar Resources para internacionalização