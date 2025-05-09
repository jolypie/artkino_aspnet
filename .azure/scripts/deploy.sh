#!/bin/bash

set -e
set -a
source .env.deploy
set +a

echo "🔐 Получаем адрес ACR..."
ACR_LOGIN=$(az acr show -n $ACR --query loginServer -o tsv)

echo "🔐 Входим в реестр ACR..."
az acr login -n $ACR

echo "📦 Сборка backend..."
docker build -t $ACR_LOGIN/artkino-backend:prod -f server/Dockerfile .
docker push $ACR_LOGIN/artkino-backend:prod

echo "📦 Сборка frontend..."
docker build -t $ACR_LOGIN/artkino-frontend:prod -f client/Dockerfile .
docker push $ACR_LOGIN/artkino-frontend:prod

echo "🚀 Деплой backend..."
az containerapp create \
  -g $RG -n artkino-backend \
  --environment $ENV \
  --image $ACR_LOGIN/artkino-backend:prod \
  --target-port 8080 --ingress external \
  --registry-server $ACR_LOGIN \
  --registry-username $(az acr credential show -n $ACR --query username -o tsv) \
  --registry-password $(az acr credential show -n $ACR --query passwords[0].value -o tsv) \
  --env-vars \
      ASPNETCORE_ENVIRONMENT=Production \
      JWT_KEY=$JWT_KEY \
      CONNECTION_STRING=$CONNECTION_STRING || echo "✅ Backend уже существует"

echo "🚀 Деплой frontend..."
az containerapp create \
  -g $RG -n artkino-frontend \
  --environment $ENV \
  --image $ACR_LOGIN/artkino-frontend:prod \
  --target-port 5173 --ingress external \
  --registry-server $ACR_LOGIN \
  --registry-username $(az acr credential show -n $ACR --query username -o tsv) \
  --registry-password $(az acr credential show -n $ACR --query passwords[0].value -o tsv) \
  --env-vars \
      API_READ_TOKEN=$API_READ_TOKEN \
      API_KEY=$API_KEY || echo "✅ Frontend уже существует"

echo "🎉 Готово! Проверь портал Azure → Container Apps → artkino-frontend"
