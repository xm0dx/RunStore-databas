using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Npgsql;

namespace Client.Services;

public class ProductServices<T> : BaseService, IProductService<Product>
{
    private readonly ISupplierService<Supplier> _supplierService;
    public ProductServices(ISupplierService<Supplier> supplierService)
    {
        _supplierService = supplierService;
    }

    public async Task<bool> Add(Product data)
    {
        try
        {
            // bygger insert-satsen av det som skrivits i formuläret
            var sqlInsertString = "INSERT INTO Products(itemNumber,productname,imageurl,description,price,supplier_id)";
            sqlInsertString += $"VALUES('{data.ItemNumber}','{data.Name}','https://res.cloudinary.com/softtech-dev/image/upload/v1786556530/shoe-3_qbzcwy.jpg','Test',{data.Price},{data.Supplier.Id});";

            using NpgsqlConnection connection = new(ConnectionString);
            using NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = sqlInsertString;
            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync() > 0;
        }
        catch (InvalidOperationException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task Delete(Product data)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> Find(string search)
    {
        try
        {
            using NpgsqlConnection connection = new(ConnectionString);
            using NpgsqlCommand command = connection.CreateCommand();

            command.CommandText = @$"SELECT 
                                        products.id,
                                        itemnumber,
                                        productname,
                                        COALESCE(description,'') AS description,
                                        COALESCE(imageurl,'') AS imageurl,
                                        price,
                                        suppliers.id AS supplierId,
                                        suppliers.suppliername 
                                    FROM products INNER JOIN suppliers
                                        ON products.supplier_id = suppliers.id 
                                    WHERE itemnumber = '{search}';";

            await connection.OpenAsync();

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            if (!reader.HasRows) throw new Exception("Hittade inget som matchade");
            await reader.ReadAsync();

            Supplier supplier = new()
            {
                Id = reader.GetInt32(6),
                Name = reader.GetString(7)
            };

            Product product = new()
            {
                Id = reader.GetInt32(0),
                ItemNumber = reader.GetString(1),
                Name = reader.GetString(2),
                Description = reader.GetString(3),
                ImageUrl = reader.GetString(4),
                Price = (int)reader.GetFloat(5),
                Supplier = supplier
            };

            return product;
        }
        catch (InvalidOperationException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<Product> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> ListAll()
    {
        try
        {
            List<Product> products = [];

            using NpgsqlConnection connection = new(ConnectionString);
            using NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT
                                        products.id,
                                        itemnumber,
                                        productname,
                                        price,
                                        suppliername
                                    FROM products INNER JOIN suppliers
                                        ON products.supplier_id = suppliers.id";

            await connection.OpenAsync();

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                // hämtar leverantören som eget objekt
                var supplier = await _supplierService.Find(reader.GetString(4));
                products.Add(new Product
                {
                    Id = reader.GetInt32(0),
                    ItemNumber = reader.GetString(1),
                    Name = reader.GetString(2),
                    Price = (int)reader.GetFloat(3),
                    Supplier = supplier
                });
            }

            return products;
        }
        catch (InvalidOperationException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task Update(Product data)
    {
        try
        {
            // Observera att jag bara uppdaterar priset!!!
            var sqlUpdateString = $"UPDATE products SET price = {data.Price} WHERE itemnumber = '{data.ItemNumber}';";

            using NpgsqlConnection connection = new(ConnectionString);
            using NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = sqlUpdateString;
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
        catch (InvalidOperationException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
