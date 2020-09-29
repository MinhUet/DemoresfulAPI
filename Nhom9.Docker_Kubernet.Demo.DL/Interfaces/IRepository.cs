using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9.Docker_Kubernet.Demo.DL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<string> Insert(T entity);
        Task<string> InsertMany(List<T> entity);
        Task Update(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Task<long> Delete(string id);
    }
}
