## MedicalResearch

## Installation

### Clone

- Clone this repo to your local machine using `https://github.com/Irongoshan-ux/MedicalResearch.git`

### Setup

> Docker-compose

Use PowerShell in the directory of local repository:

- `docker-compose -f "docker-compose.yml" up -d --build`

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

### User credentials

#### Admin
- email: admin@gmail.com
- password": qwe

#### User
- email: user@gmail.com
- password": qwe
