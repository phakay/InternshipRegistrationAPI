using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipRegistrationAPI.Core.Contracts
{
    public interface IDataRepository 
    { }
    public interface IDataRepository<T> : IDataRepository where T : class
    {
        public Task<T> AddAsync(T entity);
        public Task<T> GetAsync(string id);
        public Task<IEnumerable<T>> GetAsync();
        public Task<T> UpdateAsync(T entity);
        public Task<bool> RemoveAsync(string id);
    }
}
