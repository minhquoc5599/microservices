## Microservices:

## Enviroment
* Install visual studio 2022
* Install .net 8.0 in visual studio installer
* Docker desktop
---
## How to run
Go to folder contain file `docker-compose.yml`
```Powershell
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans
```

## Application Urls - Local Environment (Docker container)
- Product Api: http://localhost:7002/api/product
---
## Application Urls - Dev Environment
- Product Api: http://localhost:5002/api/product

