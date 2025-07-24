# Manufacturing Execution System (MES) for .NET 9

## For local development
Rabbmit MQ
```
docker run -d --hostname rabbitmq --name rabbitmq -p 5672:5672 -p 15672:15672 masstransit/rabbitmq:latest
```

SQL Server:
```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```