using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

public interface ISupplierService<T> : IServices<T> where T : class { }
