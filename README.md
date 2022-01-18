## MedicalResearch

## Installation

### Clone

- Clone this repo to your local machine using `https://github.com/Irongoshan-ux/MedicalResearch.git`

### Setup

> Docker-compose

Use PowerShell:

- `docker-compose  -f "{YOUR_REPOSITORY_FULL_PATH}\docker-compose.yml" -f "{YOUR_REPOSITORY_FULL_PATH}\docker-compose.override.yml" -f "{YOUR_REPOSITORY_FULL_PATH}\obj\Docker\docker-compose.vs.debug.g.yml" -p dockercompose9845593756744887075 --ansi never up -d  medicinemanaging.api mongodb mssql usermanaging.api`

- Get container ID `docker ps --filter "status=running" --filter "label=com.docker.compose.service" --filter "name=^/UserManaging.API$"`
- `docker exec -i -e ASPNETCORE_HTTPS_PORT="9000" {YOUR_CONTAINER_ID} sh -c ""dotnet"  --additionalProbingPath /root/.nuget/packages  "/app/bin/Debug/net6.0/UserManaging.API.dll" | tee /dev/console"`

- Get container ID `docker ps --filter "status=running" --filter "label=com.docker.compose.service" --filter "name=^/MedicineManaging.API$"`
- `docker exec -i -e ASPNETCORE_HTTPS_PORT="63786" {YOUR_CONTAINER_ID} sh -c ""dotnet"  --additionalProbingPath /root/.nuget/packages  "/app/bin/Debug/net6.0/MedicineManaging.API.dll" | tee /dev/console"`

## Getting access to API (when it is running in docker container)

### Tools

- Browser
- Postman

### Documentation

- UserManaging API
located here: `https://localhost:9000/swagger`

- MedicineManaging API
located here: `https://localhost:10000/swagger`

- GraphQL for MedicineManaging: <br>
`https://localhost:10000/api/graphql/clinic` <br>
`https://localhost:10000/api/graphql/medicine` <br>
`https://localhost:10000/api/graphql/patient`
