using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Npgsql;

namespace Client.Services;

public class SupplierServices<T> : BaseService, ISupplierService<Supplier>
{
    // add, delete och update är inte gjorda än
    public Task<bool> Add(Supplier data)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Supplier data)
    {
        throw new NotImplementedException();
    }

    public async Task<Supplier> Find(string search)
    {
        try
        {

            using NpgsqlConnection connection = new(ConnectionString);
            using NpgsqlCommand command = connection.CreateCommand();
            // söker på namnet utan att bry sig om stora och små bokstäver
            command.CommandText = $"SELECT id, suppliername FROM suppliers WHERE LOWER(suppliername)='{search.ToLower()}'";

            await connection.OpenAsync();
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();
            return new Supplier { Id = reader.GetInt32(0), Name = reader.GetString(1) };

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

    public Task<Supplier> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Supplier>> ListAll()
    {
        try
        {
            List<Supplier> suppliers = [];

            using NpgsqlConnection connection = new(ConnectionString);
            using NpgsqlCommand command = connection.CreateCommand();

            command.CommandText = @"SELECT
                                        id,
                                        suppliername
                                    FROM suppliers";

            await connection.OpenAsync();

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                suppliers.Add(new Supplier()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return suppliers;
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

    public Task Update(Supplier data)
    {
        throw new NotImplementedException();
    }
}
