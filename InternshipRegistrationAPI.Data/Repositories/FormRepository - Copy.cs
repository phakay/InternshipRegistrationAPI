using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Data.Repositories
{
    public class WorkflowRepository : CosmosDbRepository<Workflow>, IWorkflowRepository
    {
        public WorkflowRepository(IApplicationDbContext dbContext)
            : this(dbContext, "Workflows", "/Type")
        { }
        protected WorkflowRepository(IApplicationDbContext dbContext, string containerId, string partitionKeyPath)
            : base(dbContext, containerId, partitionKeyPath)
        { }
    }
}
