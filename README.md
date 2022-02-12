# zentao_dotnet
zentao api proxy

## deploy
### docker-compose
```
version: '3.1'
services:
  prd_api:
    image: lotosbin/zentao.api
    ports:
      - "5000:80"
```
[docker-compose.yml](docker-compose.yml)

## usage
open http://localhsot:5000/swagger