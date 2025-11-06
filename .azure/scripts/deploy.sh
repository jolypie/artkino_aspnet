#!/bin/bash

set -e
set -a
source .env.deploy
set +a

ACR_LOGIN=$(az acr show -n $ACR --query loginServer -o tsv)

az acr login -n $ACR

docker build -t $ACR_LOGIN/artkino-backend:prod -f server/Dockerfile .
docker push $ACR_LOGIN/artkino-backend:prod

docker build -t $ACR_LOGIN/artkino-frontend:prod -f client/Dockerfile .
docker push $ACR_LOGIN/artkino-frontend:prod

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
      CONNECTION_STRING=$CONNECTION_STRING

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
      API_KEY=$API_KEY
