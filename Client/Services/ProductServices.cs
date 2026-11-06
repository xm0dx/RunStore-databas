using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Npgsql;

namespace Client.Services;

// ärver BaseService för att få tillgång till ConnectionString
public class ProductServices : BaseService, IProductService
{
    public async Task Add(Product product)
    {

    }

    public async Task Delete(string id)
    {

    }

    public Task<Product> Find(string itemNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> List()
    {
        List<Product> products = [];
        using NpgsqlConnection connection = new(ConnectionString);
        using NpgsqlCommand command = connection.CreateCommand();
        // suppliername finns i tabellen suppliers, därför INNER JOIN
        command.CommandText = "SELECT products.id, itemnumber, productname, description, imageurl, price, suppliername FROM products INNER JOIN suppliers ON products.supplier_id = suppliers.id";
        await connection.OpenAsync();

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        // en rad i taget, siffran är kolumnens plats i SELECT
        while (await reader.ReadAsync())
        {
            Product product = new()
            {
                Id = reader.GetInt32(0).ToString(),
                ItemNumber = reader.GetString(1),
                Name = reader.GetString(2),
                // description och imageurl får vara NULL i tabellen
                Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                ImageUrl = reader.IsDBNull(4) ? null : reader.GetString(4),
                // price är FLOAT i tabellen men int i Product
                Price = (int)reader.GetDouble(5),
                SupplierName = reader.GetString(6)
            };
            products.Add(product);
        }

        return products;
    }

    public async Task Update(Product product, string _objectId)
    {

    }
}
