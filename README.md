# Test DevOps CACIB

The aim is to demonstrate a simple HAProxy, GRPC Servers and Keycloak setup.

In a nutshell, we have a simple client which :
- authenticate itself to get a JWT token
- ask for the user name
- send an authenticated request to a GRPC server
- print the reply of the GRPC server (a simple greeting)

## Project structure

**docker-compose.yml** setup HAProxy, GRPC servers, Postgres database and Keycloak services

**srv.Dockerfile** create the image to run .NET GRPC server

**lb.Dockerfile** pulls the HAProxy image and copy the configuration

**src** contains the source code for the .NET GRPC client and server

**haproxy.cfg** contains the configuration use to set up HAProxy

**db-init/init.sql** contains the script use to create the Keycloak user and database

**keycloak/keys** contains the public key used to validate the signature of the JWT token

**keycloak/realm** contains an export of the target Keycloak realm. It is used to set up Keycloak configuration


## Running it

### Run environment

Type in :

```bash
docker-compose up --build
```

or

```bash
docker-compose up --build -d
```

This will launch all services :
- http://localhost:3000 : for GRPC server
- http://localhost:8090/stats : to see the stats page of HAProxy
- http://localhost:8090/auth : to access the Keycloak service

__The user and password for Keycloak is admin__

### Run client

To make sure to run things smoothly make sure you use dotnet 9.

**Also make sure to use http://localhost:8090/stats to verify that all services are up before running**

```bash
cd src/client && dotnet run
```

Then follow instruction
