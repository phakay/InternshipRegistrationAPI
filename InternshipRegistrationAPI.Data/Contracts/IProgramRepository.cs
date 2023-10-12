using InternshipRegistrationAPI.Data.DataModels;

namespace InternshipRegistrationAPI.Data.Contracts
{
    public interface IProgramRepository
    {
        Task<ProgramData> AddProgramAsync(ProgramData program);
        Task<ProgramData> GetProgramAsync(string id, string partitionKey);
        Task<IEnumerable<ProgramData>> GetProgramsAsync();
        Task<bool> RemoveProgramAsync(string id, string partitionKey);
        Task<ProgramData> UpdateProgramAsync(ProgramData program);
    }
}