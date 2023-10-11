using InternshipRegistrationAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternshipRegistrationAPI.Data.DataModels;
using InternshipRegistrationAPI.Data.Contracts;

namespace InternshipRegistrationAPI.Data.Repositories
{
    public class ProgramRepository : DataRepository<ProgramData>
    {
        public ProgramRepository(IApplicationDbContext dbContext)
            :this(dbContext, "Programs", "/id")
        {}
        protected ProgramRepository(IApplicationDbContext dbContext,  string containerId, string partitionKeyPath)
            : base(dbContext, containerId, partitionKeyPath)
        {}
        
        public async Task<ProgramData> GetProgramAsync(string id, string partitionKey)
        {
            return await GetAsync(id, partitionKey);
            
        }

        public async Task<IEnumerable<ProgramData>> GetProgramsAsync()
        {
            return await GetAsync();
        }
        public async Task<ProgramData> AddProgramAsync(ProgramData program)
        {
           return await AddAsync(program);
        }
        public async Task<ProgramData> UpdateProgramAsync(ProgramData program)
        {
            return await UpdateAsync(program);
        }

        public async Task<bool> RemoveProgramAsync(string id, string partitionKey)
        {
            return await RemoveAsync(id, partitionKey);
        }
        
    }

}
