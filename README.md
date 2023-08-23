# Sample web api service

Aspnet.Core application simulating a drawing competition and storing data in a database.

It uses groups of teams from different countries in a randomized manner - ensuring that no group has 2 teams from the same country. Has a constraint for 4 or 8 groups as required business logic implementation.

## Solution structure
```
./.devcontainer         ->devcontainer structure
./DrawDb                ->database structure project
./src
    ./DrawService       ->actual code implementation
    ./DrawServiceTest   ->unit code testing
    ./Server            ->webapp server
    WorldCupLeague.sln
```

The application uses OpenApi multi-version service implementation (OAS3 https://swagger.io/specification/) to expose swagger definition structure.

## Code overview/running application

Clone this repository to a local folder. 

It is recommended to use Docker to run application or for development purposes.

Unit tests and mocking functions are provided to verify that applicaiton is functioning as expected.

### Using Visual Studio Code & Docker

To run application simple execute 

```
> docker compose up -d
```

Then open https://localhost:5001/ - please wait for images to be built and database initialized.

Use **Dev Containers** ( https://code.visualstudio.com/docs/devcontainers/containers ) extension '_Dev Containers: Open folder in container..._' option to open Visual Studio Code and debug application. 

### Using Microsoft Visual Studio 

Open _WorldCupLeague.sln_ solution and build, ensuring that apporpiate .net-sdk version is installed. 

## Configuration

For changing database hostname or credentials - data need to be updated in the following files :

```
./.devcontainer/mssql/postCreateCommand.sh ->if Docker is used, before building images

./src/DrawService/drawsettings.json -> new connection string used by application

```

If Docker is used _docker-compose.yml_ files should be updated with node name.