# CashpointSample

The desktop application on WPF that simulates simple withdrawing machine.

The project uses and requires:
- .NET Core 6
- EntityFramework Core
- PostgreSQL 13.3

In order to connect to the database, change the connection string [here](CashpointWPF/appsettings.json) with your proper *port* and *password*.

###### Creating "Trade" Database

[Here](PostgreDBTrade/) are two initialization files that create a [simple database](PostgreDBTrade/createDB.sql) and create [tables](PostgreDBTrade/createTables.sql) for it. They should be run on PostgreSQL server.
