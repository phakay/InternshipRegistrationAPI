using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipRegistrationAPI.Core.Contracts
{
    public interface IDataRepository 
    { }
    public interface IDataRepository<T> : IDataRepository where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAsync();
        Task<T> UpdateAsync(T entity);
        Task<bool> RemoveAsync(string Id);
    }
}
