using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Data.Contracts
{
    public interface IProgramRepository
    {
        Task<Program> AddDocumentAsync(Program program);
        Task<Program> GetDocumentAsync(string id, string partitionKey);
        Task<IEnumerable<Program>> GetDocumentsAsync();
        Task<Program> UpdateProgramAsync(Program program);
    }
}