using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Data.Contracts
{
    public interface IProgramRepository
    {
        Task<Program> AddProgramAsync(Program program);
        Task<Program> GetProgramAsync(string id, string partitionKey);
        Task<IEnumerable<Program>> GetProgramsAsync();
        Task<Program> UpdateProgramAsync(Program program);
    }
}