using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Npgsql;

namespace Client.Services;

public class SalesOrderService<T> : BaseService, ISalesOrdersService<SalesOrder>
{
    readonly ICustomerService<Customer> _customerServices;

    public SalesOrderService(ICustomerService<Customer> customerService)
    {
        _customerServices = customerService;
    }
    public Task<bool> Add(SalesOrder data)
    {
        throw new NotImplementedException();
    }

    public Task Delete(SalesOrder data)
    {
        throw new NotImplementedException();
    }

    public Task<SalesOrder> Find(string search)
    {
        throw new NotImplementedException();
    }

    public Task<SalesOrder> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SalesOrder>> ListAll()
    {
        try
        {
            List<SalesOrder> orders = [];

            using NpgsqlConnection connection = new(ConnectionString);
            using NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT
                                        salesorders.id,
                                        ordernumber,
                                        orderdate,
                                        firstname,
                                        lastname,
                                        email,
                                        SUM(quantity * price) AS ordervalue -- summan per order
                                    FROM salesorders INNER JOIN customers
                                        ON salesorders.customer_id = customers.id INNER JOIN orderitems
                                        ON salesorders.id = orderitems.order_id
                                    GROUP BY salesorders.id,ordernumber,orderdate,firstname,lastname,email;";

            await connection.OpenAsync();

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Customer customer = await _customerServices.Find(reader.GetString(5));
                SalesOrder salesOrder = new SalesOrder()
                {
                    OrderId = reader.GetInt32(0),
                    OrderNumber = reader.GetString(1),
                    OrderDate = reader.GetDateTime(2),
                    Customer = customer,
                    Items = [],
                    OrderSum = (int)reader.GetFloat(6)
                };

                orders.Add(salesOrder);
            }

            return orders;
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

    public Task Update(SalesOrder data)
    {
        throw new NotImplementedException();
    }
}
