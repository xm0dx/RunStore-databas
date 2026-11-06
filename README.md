# RunStore med databas

Skolprojekt i kursen Databashantering (ITHS). Det är lärarens startprojekt från lektion 3
där jag kopplar RunStore till min PostgreSQL-databas på Neon istället för restdb.

## Det här är gjort

- Paketet Npgsql är installerat (`dotnet add package Npgsql`)
- `Services/BaseService.cs` läser anslutningssträngen från `appsettings.json`
- `Services/ProductServices.cs` hämtar produkterna i `List()` med en INNER JOIN mot suppliers
- `dbscript/testqueries.sql` har frågorna jag testade i databasen först

## Komma igång

1. Kopiera `Client/appsettings.example.json` till `Client/appsettings.json`
2. Byt ut strängen mot din egen från Neon (Connect, välj .NET). Den ska heta `pgsql`
3. Kör programmet från mappen Client:

        cd Client
        dotnet run

`appsettings.json` ligger i `.gitignore` så lösenordet följer inte med upp till GitHub.

## Saker som strulade för mig

- Filen måste heta `appsettings.json` med små bokstäver om man kör Linux, annars hittas den inte
- Programmet letar efter filen i mappen man kör ifrån, så kör från Client
- `description` och `imageurl` är NULL i min tabell, då kraschar `reader.GetString`. Därför kollar jag med `IsDBNull` först
- `price` är FLOAT i tabellen men `int` i `Product`, så den görs om med `(int)reader.GetDouble(5)`
- Tabellerna måste finnas i databasen först (products och suppliers från lektion 2)
