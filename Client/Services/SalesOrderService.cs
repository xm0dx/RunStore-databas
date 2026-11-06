using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

public class SalesOrderService : ISalesOrdersService
{
    public async Task Add(SalesOrder order)
    {

    }

    public async Task<List<SalesOrder>> List()
    {
        return [];
    }
}
