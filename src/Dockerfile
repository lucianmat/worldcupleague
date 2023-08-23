FROM mcr.microsoft.com/devcontainers/dotnet:0-7.0 AS base
WORKDIR /drawer

FROM base AS build
WORKDIR /app/src
COPY ./src/ .
RUN dotnet publish "WorldCupLeague.sln" -c Release -o /app/publish

FROM base AS publish
COPY --from=build /app/publish .
# Install SQL Tools: SQLPackage and sqlcmd
COPY ./../.devcontainer/mssql/installSQLtools.sh installSQLtools.sh
COPY ./../.devcontainer/mssql/postCreateCommand.sh postCreateCommand.sh

COPY ./../.devcontainer/mssql/setup.sql setup.sql

RUN bash ./installSQLtools.sh \
     && apt-get clean -y && rm -rf /var/lib/apt/lists/* /tmp/library-scripts 

CMD bash ./postCreateCommand.sh 'P@ssw0rd' '.' '.' \
     && dotnet  Server.dll
