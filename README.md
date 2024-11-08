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
## Docker Application Urls - Local Environment (Docker container)
- Portainer: http://localhost:9000

## Application Urls - Local Environment (Docker container)
- Product Api: http://localhost:7002/api/product
- Customer Api: http://localhost:7003/api/customer
- Basket Api: http://localhost:7004/api/basket
---
## Application Urls - Dev Environment
- Product Api: http://localhost:5002/api/product
- Customer Api: http://localhost:5003/api/customer
- Basket Api: http://localhost:5004/api/basket

## Docker Commands
- docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build

## Useful Commands
- dotnet ef migrations add "Init" --project {dir} --startup-project {dir} --output-dir {dir}\Migrations
- dotnet ef migrations remove --project {dir} --startup-project {dir}
- dotnet ef migrations database update --project {dir} --startup-project {dir}

