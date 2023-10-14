using InternshipRegistrationAPI.Core.Contracts;

namespace InternshipRegistrationAPI.Data.Contracts
{
    public interface IDistributeableDataRepository<T> where T : class, IDistributableEntity
    {
        Task<T> AddDocumentAsync(T document);
        Task<IEnumerable<T>> GetDocumentsAsync();
        Task<T> GetDocumentAsync(string id, string partitionKey);
        Task<bool> RemoveDocumentAsync(string id, string partitionKey);
        Task<T> UpdateDocumentAsync(T document);
    }
}