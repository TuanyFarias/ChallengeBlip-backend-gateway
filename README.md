# 🌐 Challenge Blip - Weather Gateway API

Esta é uma API Gateway desenvolvida em **.NET 10** como parte do desafio técnico para a Blip. A aplicação atua como um intermediário seguro entre o frontend e a WeatherAPI, fornecendo dados meteorológicos com controle de acesso e políticas de CORS.

## 🚀 Link da API (Deploy)
A API está rodando no Render e pode ser acessada (via Swagger) em:
👉 **[Documentação Swagger - Render](https://challengeblip-backend-gateway.onrender.com/swagger)**

---

## 🛠️ Tecnologias Utilizadas

- **.NET 10 (Preview)**: Framework de última geração para alta performance.
- **ASP.NET Core Web API**: Estrutura para criação de endpoints REST.
- **Docker**: Containerização para garantir paridade entre ambientes de desenvolvimento e produção.
- **Swagger (Swashbuckle)**: Documentação interativa da API.
- **Render**: Plataforma de hospedagem com deploy contínuo via Docker.

---

## 🔒 Segurança e Regras de Negócio

- **API Key Authentication**: Proteção de endpoints via Header de autorização customizado.
- **CORS Policy**: Configuração restrita para permitir requisições apenas de origens autorizadas (Frontend na Vercel).
- **Isolamento de Testes**: Arquitetura de build em múltiplos estágios (Multi-stage Build) no Docker, garantindo que o artefato final de produção seja leve e seguro.

---

## ⚙️ Configuração para Desenvolvimento Local

Para rodar a API localmente:

1. **Clone o repositório**:
   ```bash
   git clone [https://github.com/TuanyFarias/ChallengeBlip.git](https://github.com/TuanyFarias/ChallengeBlip.git)
2. **Configure suas chaves no ambiente virtual**:
3. **Execute**:
   ```bash
   dotnet build
   dotnet run
4. **Execute via docker**:
   ```bash
   docker build -t challenge-blip-api .
   docker run -p 8080:8080 challenge-blip-api
