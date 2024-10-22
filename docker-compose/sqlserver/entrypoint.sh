#!/bin/bash

# Start SQL Server
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start
sleep 30s

host="$(hostname --ip-address || echo '127.0.0.1')"
user=SA
password=$MSSQL_SA_PASSWORD

/opt/mssql-tools/bin/sqlcmd -S $host -U $user -P $password -C -i /usr/src/app/init.sql

# Wait indefinitely to keep the container running
wait
