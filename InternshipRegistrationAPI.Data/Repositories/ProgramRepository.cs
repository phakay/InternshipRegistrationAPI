using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Data.Repositories
{
    public class ProgramRepository : CosmosDbRepository<Program>, IProgramRepository
    {
        public ProgramRepository(IApplicationDbContext dbContext)
            : this(dbContext, "Programs", "/Type")
        { }
        protected ProgramRepository(IApplicationDbContext dbContext, string containerId, string partitionKeyPath)
            : base(dbContext, containerId, partitionKeyPath)
        { }
    }

}
