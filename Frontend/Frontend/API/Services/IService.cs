using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.API.Model;
using System.Net.Http;

namespace Frontend.API.Services
{
   
    public interface IService<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<int> Create(T entity);
        Task Delete(T entity);
        Task<T> Update(T entity);
        Task<T> FindById(int id);
    }
}
