# zentao_dotnet
zentao api proxy

[![Docker Image Version (tag latest semver)](https://img.shields.io/docker/v/lotosbin/zentao.api/latest?label=docker)](https://hub.docker.com/repository/docker/lotosbin/zentao.api)
[![Nuget](https://img.shields.io/nuget/v/zentao.client)](https://www.nuget.org/packages/zentao.client)

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
