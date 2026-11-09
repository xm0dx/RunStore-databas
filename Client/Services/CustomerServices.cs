using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Npgsql;

namespace Client.Services;

public class CustomerServices<T> : BaseService, ICustomerService<Customer>
{
    public Task<bool> Add(Customer data)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Customer data)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> Find(string search)
    {
        try
        {

            using NpgsqlConnection connection = new(ConnectionString);
            using NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = @$"SELECT
                                        id,
                                        firstname,
                                        lastname,
                                        email,
                                        COALESCE(phone,'Saknas') AS phone
                                    FROM customers
                                        WHERE email = '{search}';";

            await connection.OpenAsync();
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();
            return new Customer
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                Phone = reader.GetString(4)
            };

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

    public Task<Customer> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Customer>> ListAll()
    {
        try
        {
            List<Customer> customers = [];

            using NpgsqlConnection connection = new(ConnectionString);
            using NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT
                                        id,
                                        firstname,
                                        lastname,
                                        email,
                                        COALESCE(phone,'Saknas') AS phone
                                    FROM customers;";

            await connection.OpenAsync();

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                customers.Add(new Customer
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4)
                });
            }
            return customers;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task Update(Customer data)
    {
        throw new NotImplementedException();
    }
}
