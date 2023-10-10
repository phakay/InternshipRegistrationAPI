using InternshipRegistrationAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternshipRegistrationAPI.Data.DataModels;

namespace InternshipRegistrationAPI.Data.Repositories
{
    public class ProgramRepository : DataRepository<ProgramData>
    {
        public ProgramRepository()
            :this("Programs", "/id")
        {}
        private ProgramRepository(string containerId, string partitionKeyPath)
            : base(containerId, partitionKeyPath)
        {}
        
        public async Task<ProgramData> GetProgram(string id, string partitionKey)
        {
            return await GetAsync(id, partitionKey);
            
        }

        public async Task<IEnumerable<ProgramData>> GetPrograms()
        {
            return await GetAsync();
        }
        public async Task<ProgramData> AddProgram(ProgramData program)
        {
           return await AddAsync(program);
        }
        public async Task<ProgramData> UpdateProgram(ProgramData program)
        {
            return await UpdateAsync(program);
        }

        public async Task<bool> RemoveProgram(string id, string partitionKey)
        {
            return await RemoveAsync(id, partitionKey);
        }
        
    }

}
