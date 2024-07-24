## Getting Started

First, change the connection string in the appsettings.json
```bash
    "ConnectionStrings": {
      "Postgres": "Servers=localhost;port=5432; Database=Login;User Id=postgres;password=.;"
    }
```
with your own PostgreSQL details

Second, restore the DB Backup file on PGAdmin or other PostgreSQL application
