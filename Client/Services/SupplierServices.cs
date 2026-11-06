using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Npgsql;

namespace Client.Services;

// samma upplägg som ProductServices, ärver BaseService för ConnectionString
public class SupplierServices : BaseService, ISupplierService
{
    public async Task<List<Supplier>> List()
    {
        List<Supplier> suppliers = [];
        using NpgsqlConnection connection = new(ConnectionString);
        using NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT id, suppliername FROM suppliers";
        await connection.OpenAsync();

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            Supplier supplier = new()
            {
                // id är ett heltal i tabellen men string i Supplier
                Id = reader.GetInt32(0).ToString(),
                SupplierName = reader.GetString(1)
            };
            suppliers.Add(supplier);
        }

        return suppliers;
    }
}
