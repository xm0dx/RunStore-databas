# RunStore med databas

Skolprojekt i kursen Databashantering (ITHS). Det är lärarens startprojekt från lektion 3
där jag kopplar RunStore till min PostgreSQL-databas på Neon istället för restdb.

## Det här är gjort

- Paketet Npgsql är installerat (`dotnet add package Npgsql`)
- `Services/BaseService.cs` läser anslutningssträngen från `appsettings.json`
- Ett gemensamt gränssnitt `IServices<T>` med Add, Find, FindById, ListAll, Delete och Update
- Modellerna har `int Id` som i tabellerna och `Product` har en `Supplier`
- `ProductServices` har Add, Find, ListAll och Update (Update ändrar bara priset)
- `SupplierServices` har Find och ListAll, `CustomerServices` och `SalesOrderService` har ListAll och Find
- Tjänsterna är generiska och registreras med `typeof(IProductService<>)` i `AddServices`
- Ny produkt väljer leverantör i en ComboBox
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
- `description` och `imageurl` är NULL i min tabell, då kraschar `reader.GetString`. Därför används `COALESCE(description,'')` i frågan
- `price` är FLOAT i tabellen men `int` i `Product`, så den görs om med `(int)reader.GetFloat(5)`
- Tabellerna måste finnas i databasen först (products och suppliers från lektion 2)
- Min tabell salesorders hade kolumnen `customerid` men koden använder `customer_id`, så jag bytte namn med `dbscript/altertables.sql`
