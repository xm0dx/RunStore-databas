using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

public class CustomerServices : ICustomerService
{
    public async Task<List<Customer>> List()
    {
        return [];
    }
}
