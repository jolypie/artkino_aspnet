# ⚙️ Fullstack Template: ASP.NET + React + PostgreSQL + Docker

Minimal template for a fullstack project using modern technologies:

- 🎯 ASP.NET 8 + Entity Framework Core
- ⚛️ React + Redux + TypeScript (via Vite)
- 🐘 PostgreSQL + Adminer
- 🐳 Docker / Docker Compose
- ☁️ Ready for Azure Container Apps
- 🔐 Supports `.env` files and dev/prod separation



# Dev URLs

- 🔥 Frontend: [http://localhost:3000](http://localhost:3000)
- 🧪 Swagger: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- 🛠️ Adminer: [http://localhost:8081](http://localhost:8081)

## Development

```bash
docker compose \
  --env-file .env \
  --env-file .env.development \
  up --build
```

## Production

```bash
docker compose \
  --env-file .env \
  --env-file .env.production \
  up --build
```
