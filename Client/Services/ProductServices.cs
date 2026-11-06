using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

public class ProductServices : IProductService
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
        return [];
    }

    public async Task Update(Product product, string _objectId)
    {

    }
}
