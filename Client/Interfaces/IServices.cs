using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Interfaces;

// alla tjänster har samma metoder, T är modellen (Product, Supplier osv)
public interface IServices<T> where T : class
{
    Task<bool> Add(T data);
    Task<T> FindById(int id);
    Task<T> Find(string search);
    Task<List<T>> ListAll();
    Task Delete(T data);
    Task Update(T data);
}
