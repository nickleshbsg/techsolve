# Deployment Guide

## Architecture
```
GitHub (nickleshbisht-source/Techsolve)
    │
    ├── TechSolve.API  ──►  Render.com (free)  ──►  https://techsolve-api.onrender.com
    │                       SQLite DB (auto-created on first run)
    │
    └── TechSolve.UI  ──►  Vercel (free)       ──►  https://techsolve-ui.vercel.app
        └── clientApp/
```

---

## Step 1 — Push to GitHub  *(already done via the setup script)*

```bash
git clone https://github.com/nickleshbisht-source/Techsolve.git
# OR the repo is already pushed
```

---

## Step 2 — Deploy API on Render

1. Go to **https://render.com** → Sign up / Log in with GitHub
2. Click **"New +"** → **"Web Service"**
3. Connect your GitHub account and select **`nickleshbisht-source/Techsolve`**
4. Fill in:
   | Field | Value |
   |-------|-------|
   | **Name** | `techsolve-api` |
   | **Region** | Singapore |
   | **Root Directory** | `TechSolve.API` |
   | **Runtime** | `.NET` |
   | **Build Command** | `dotnet publish TechSolve.API.csproj -c Release -o ./publish` |
   | **Start Command** | `dotnet ./publish/TechSolve.API.dll` |
   | **Plan** | Free |

5. Add **Environment Variables**:
   | Key | Value |
   |-----|-------|
   | `ASPNETCORE_ENVIRONMENT` | `Production` |
   | `ASPNETCORE_URLS` | `http://0.0.0.0:10000` |
   | `AllowedOrigins__0` | `https://techsolve-ui.vercel.app` |

6. Click **"Create Web Service"**
7. Wait ~5 minutes for first build
8. Your API URL: **`https://techsolve-api.onrender.com`**
9. Swagger UI: **`https://techsolve-api.onrender.com/swagger`**

> **Note:** The free tier sleeps after 15 min inactivity.
> First request after sleep takes ~30 seconds to wake up.

---

## Step 3 — Deploy Angular UI on Vercel

1. Go to **https://vercel.com** → Sign up / Log in with GitHub
2. Click **"Add New Project"**
3. Import **`nickleshbisht-source/Techsolve`**
4. Configure:
   | Field | Value |
   |-------|-------|
   | **Root Directory** | `TechSolve.UI/clientApp` |
   | **Framework** | Other |
   | **Build Command** | `npm run build:prod` |
   | **Output Directory** | `dist/client-app/browser` |
5. Click **"Deploy"**
6. Your UI URL: **`https://techsolve-ui.vercel.app`** (or custom domain)

---

## Step 4 — Update CORS on Render

Once you have your Vercel URL, go to Render → your service → **Environment**:
- Update `AllowedOrigins__0` to your actual Vercel URL

---

## Step 5 — Update API URL in Angular (if Render URL differs)

If your Render URL is different from `techsolve-api.onrender.com`:
1. Edit `TechSolve.UI/clientApp/src/environments/environment.prod.ts`
2. Update `apiUrl` to your actual Render URL + `/api`
3. Push to GitHub — Vercel auto-redeploys

---

## Local Development

```bash
# Terminal 1 — Angular
cd TechSolve.UI/clientApp
npm install
npm start                    # http://localhost:4200

# Terminal 2 — .NET API
cd TechSolve.API
dotnet run                   # https://localhost:5001
                             # Swagger: https://localhost:5001/swagger
```

---

## Auto-redeploy

Both Render and Vercel watch the `main` branch.
Every `git push origin main` automatically rebuilds and redeploys both services.
