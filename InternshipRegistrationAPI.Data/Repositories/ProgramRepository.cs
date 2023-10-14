using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Data.Repositories
{
    public class ProgramRepository : DataRepository<Program>, IProgramRepository
    {
        public ProgramRepository(IApplicationDbContext dbContext)
            : this(dbContext, "Programs", "/Type")
        { }
        protected ProgramRepository(IApplicationDbContext dbContext, string containerId, string partitionKeyPath)
            : base(dbContext, containerId, partitionKeyPath)
        { }

        public async Task<Program> GetProgramAsync(string id, string partitionKey)
        {
            return await GetAsync(id, partitionKey);

        }

        public async Task<IEnumerable<Program>> GetProgramsAsync()
        {
            return await GetAsync();
        }
        public async Task<Program> AddProgramAsync(Program program)
        {
            if (string.IsNullOrEmpty(program.Id)) 
            { 
                program.Id = Guid.NewGuid().ToString();

            }
            return await AddAsync(program);
        }
        public async Task<Program> UpdateProgramAsync(Program program)
        {
            return await UpdateAsync(program);
        }
        

    }

}
